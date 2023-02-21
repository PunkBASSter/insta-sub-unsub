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
        private readonly Lazy<string> _serviceUsername;
        private readonly Lazy<string> _servicePassword;
        private bool _isInitialized = false;
        private readonly IRepository _repo;
        private readonly IConfiguration _configuration;
        private readonly int _crawlLimitPerIteration = 2873;

        public UserCrawler(LoginPage loginPage, FollowingPage followingPage, IRepository repo, IConfiguration configuration)
        {
            _loginPage = loginPage;
            _followingPage = followingPage;
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
                var id = _repo.InsertOrSkip(user, userToSkip => userToSkip.Name == user.Name);
                _repo.InsertOrUpdate(
                    new UserRelation { FolloweeId = seedUser.Id, FollowerId = id, LastUpdate = DateTime.UtcNow },
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
    }
}