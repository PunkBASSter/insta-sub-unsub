using InstaCrawlerApp.PersistenceInterfaces;
using InstaDomain;
using SeleniumUtils.PageObjects;

namespace InstaCrawlerApp
{
    public class UserCrawler : IUserCrawler
    {
        private readonly LoginPage _loginPage;
        private readonly FollowingPage _followingPage;
        private readonly string _serviceUsername = "hidethetrack123";
        private readonly string _servicePassword = "Imnot4insta!1";
        private bool _isInitialized = false;
        private readonly IRepository<InstaUser> _instaUsersRepo;

        public UserCrawler(LoginPage loginPage, FollowingPage followingPage, IRepository<InstaUser> repo)
        {
            _loginPage = loginPage;
            _followingPage = followingPage;
            _instaUsersRepo = repo;
        }

        /// <summary>
        /// The script signs in and goes to the main user's following page (/{username}/following).
        /// Scrolls the following page to bottom saving the users to the DB.
        /// </summary>
        public void Initialize()
        {
            if (_isInitialized) return;

            _loginPage.Load();
            _loginPage.Login(_serviceUsername, _servicePassword);
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
            var testUsr = new InstaUser { Name = _serviceUsername, Status = InstaDomain.Enums.UserStatus.Protected };
            //todo normal async
            var id = _instaUsersRepo.CreateAsync(testUsr, CancellationToken.None).Result;
            var readUser = _instaUsersRepo.Query.Take(10).ToList();


            Initialize();


        }

        public List<InstaUser> GetMyFollowed()
        {
            return new List<InstaUser>();
        }
    }
}