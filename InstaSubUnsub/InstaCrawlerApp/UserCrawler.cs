using InstaDomain;
using InstaDomain.Enums;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;
using SeleniumUtils;
using SeleniumUtils.PageObjects;

namespace InstaCrawlerApp
{
    public class UserCrawler : IUserCrawler
    {
        private readonly LoginPage _loginPage;
        private readonly FollowingPage _followingPage;
        private readonly Lazy<string> _serviceUsername;
        private readonly Lazy<string> _servicePassword;
        private bool _isInitialized = false;
        private readonly IRepository<InstaUser> _instaUsersRepo;
        private readonly IConfiguration _configuration;
        private readonly int _crawlLimitPerIteration = 2873;

        public UserCrawler(LoginPage loginPage, FollowingPage followingPage, IRepository<InstaUser> repo, IConfiguration configuration)
        {
            _loginPage = loginPage;
            _followingPage = followingPage;
            _instaUsersRepo = repo;
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
            var seedUser = _instaUsersRepo.Query.LastOrDefault();
            var followingItems = VisitUserAndGetFollowing(seedUser?.Name ?? _serviceUsername.Value);

            var users = followingItems.Select(i => new InstaUser { Name = i.UserName, Status = UserStatus.New });

            foreach (var user in users)
            {
                var id = _instaUsersRepo.InsertOrSkip(user, userToSkip => userToSkip.Name == user.Name);
                //todo use Id and current user Id to save connection to a table (potentially to build a social graph)
            }
            _instaUsersRepo.SaveChanges();

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