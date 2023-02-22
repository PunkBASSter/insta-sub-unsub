using InstaDomain;
using InstaDomain.Enums;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;
using SeleniumUtils;
using SeleniumUtils.PageObjects;

namespace InstaCrawlerApp
{
    public class UserCrawler
    {
        private readonly LoginPage _loginPage;
        private readonly FollowingPage _followingPage;
        private readonly ProfilePage _profilePage;
        private readonly Lazy<string> _serviceUsername;
        private readonly Lazy<string> _servicePassword;
        private bool _isInitialized = false;
        private readonly IRepository _repo;
        private readonly IConfiguration _configuration;
        private readonly int _crawlLimitPerIteration = 2873;

        public UserCrawler(LoginPage loginPage, FollowingPage followingPage, ProfilePage profilePage, IRepository repo, IConfiguration configuration)
        {
            _loginPage = loginPage;
            _followingPage = followingPage;
            _profilePage = profilePage;
            _repo = repo;
            _configuration = configuration;
            _serviceUsername = new Lazy<string>(() => _configuration.GetSection("CrawlUser:Username").Value ?? throw new ArgumentException("CrawlUser:Username is not provided"));
            _servicePassword = new Lazy<string>(() => _configuration.GetSection("CrawlUser:Password").Value ?? throw new ArgumentException("CrawlUser:Password is not provided"));
            _crawlLimitPerIteration += new Random(DateTime.Now.Microsecond).Next(-982, 847); //randomizing the iteration limit
        }

        /// <summary>
        /// The script signs in and goes to the main user's following page (/{username}/following).
        /// Scrolls the following page to bottom saving the users to the DB.
        /// </summary>
        public void Initialize()
        {
            if (_isInitialized) return;

            _loginPage.Load();
            _loginPage.Login(_serviceUsername.Value, _servicePassword.Value);
            _loginPage.HandleAfrerLoginQuestions();
            _isInitialized = true;
            //todo save cookies (now or before quitting iteration?)
        }

        public void Crawl()
        {
            Initialize();

            var crawledUsersCount = 0;

            while (crawledUsersCount <= _crawlLimitPerIteration)
            {
                crawledUsersCount += CrawlFromLastUser();
                new Delay().Random();
            }
        }

        private int CrawlFromLastUser()
        {
            var userQuery = _repo.Query<InstaUser>();
            var seedUser = userQuery.OrderBy(u => u.Id).LastOrDefault(u => u.HasRussianText == true)
                ?? userQuery.First(u => u.Name == _serviceUsername.Value);
            var followingItems = VisitUserAndGetFollowing(seedUser.Name);

            var users = followingItems.Select(i => new InstaUser 
            {
                Name = i.UserName,
                Status = UserStatus.New,
                HasRussianText = i.Description.HasRussianText()
            });

            //long transaction? :/
            foreach (var user in users)
            {
                //var id = _repo.InsertOrSkip(user, userToSkip => userToSkip.Name == user.Name);
                var userDetails = VisitUserProfile(user.Name);
                var id = _repo.InsertOrUpdate(userDetails, usr => usr.Name == userDetails.Name);

                _repo.InsertOrUpdate(
                    new UserRelation { FollowerId = seedUser.Id, FolloweeId = id, LastUpdate = DateTime.UtcNow },
                    rec => rec.FollowerId == seedUser.Id && rec.FolloweeId == id);
            }
            _repo.SaveChanges();

            return users.Count();
        }

        private IList<FollowingItem> VisitUserAndGetFollowing(string userName)
        {
            //basically a following page action
            _followingPage.Load(userName);
            var items = _followingPage.InfiniteScrollToBottomWithItemsLoading();
            return items;
        }

        private InstaUser VisitUserProfile(string userName)
        {
            _profilePage.Load(userName);
            var postInfos = _profilePage.GetLastPosts();
            var followersNum = Convert.ToInt32(_profilePage.FollowersNumElement.Text);
            var followingsNum = Convert.ToInt32(_profilePage.FollowingsNumElement.Text);
            var lastPostDate = postInfos.Select(p => p.PublishDate).Max();
            var hasRussianText = postInfos.Any(p => p.Description.HasRussianText()); //TODO Deal with empty descriptions
            var rank = Convert.ToInt32(CalculateRank(followersNum, followingsNum, lastPostDate, hasRussianText ));
            //var followingsLoaded = VisitUserAndGetFollowing(userName);
            var res = new InstaUser
            {
                Name = userName,
                FollowersNum = followersNum,
                FollowingsNum = followingsNum,
                LastPostDate = lastPostDate,
                HasRussianText = hasRussianText,
                Rank = rank,
                Status = UserStatus.Visited
            };

            return res;
        }

        private double CalculateRank(double followers, double followings, DateTime lastPostDate, bool hasRussianText)
        {
            followers = Math.Min(followers, 1);
            
            var followingsRatio = followings / followers;
            followingsRatio = Math.Min(followings, 0.1);

            //todo make more readable and maintainable
            var lastPostMultiplier = 1;
            if (DateTime.UtcNow - lastPostDate <= TimeSpan.FromDays(30))
                lastPostMultiplier = 2;
            if (DateTime.UtcNow - lastPostDate <= TimeSpan.FromDays(14))
                lastPostMultiplier = 4;
            if (DateTime.UtcNow - lastPostDate <= TimeSpan.FromDays(7))
                lastPostMultiplier = 5;
            

            var rusMultimplier = 1 + Convert.ToByte(hasRussianText)*3;

            return (followingsRatio * lastPostMultiplier * rusMultimplier);
        }
    }
}