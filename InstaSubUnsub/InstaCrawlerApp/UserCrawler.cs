using InstaDomain;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;
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

        public UserCrawler(LoginPage loginPage, FollowingPage followingPage, IRepository<InstaUser> repo, IConfiguration configuration)
        {
            _loginPage = loginPage;
            _followingPage = followingPage;
            _instaUsersRepo = repo;
            _configuration = configuration;
            _serviceUsername = new Lazy<string>(() => _configuration.GetSection("CrawlUser:Username").Value ?? throw new ArgumentException("CrawlUser:Username is not provided"));
            _servicePassword = new Lazy<string>(() => _configuration.GetSection("CrawlUser:Password").Value ?? throw new ArgumentException("CrawlUser:Password is not provided"));
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

            //todo save cookies

            _followingPage.Load("dr.imiller");
            var items = _followingPage.InfiniteScrollToBottomWithItemsLoading();
            var users = items.Select(i => new InstaUser { Name = i.UserName, });
            _isInitialized = true;
        }

        public void Crawl()
        {
            //test DB
            var testUsr = new InstaUser { Name = _serviceUsername.Value, Status = InstaDomain.Enums.UserStatus.Protected };
            //todo normal async
            _instaUsersRepo.InsertOrSkip(testUsr, u => u.Name == testUsr.Name);
            _instaUsersRepo.SaveChanges();
            var readUser = _instaUsersRepo.Query.Take(10).ToList();


            Initialize();


        }

        public List<InstaUser> GetMyFollowed()
        {
            return new List<InstaUser>();
        }
    }
}