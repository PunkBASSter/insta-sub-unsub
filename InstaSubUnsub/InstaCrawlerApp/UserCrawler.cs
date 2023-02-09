using InstaDomain;
using SeleniumUtils.PageObjects;

namespace InstaCrawlerApp
{
    public class UserCrawler
    {
        private readonly LoginPage _loginPage;
        private readonly FollowingPage _followingPage;
        private readonly string _serviceUsername = "hidethetrack123";
        private readonly string _servicePassword = "Imnot4insta!1";
        private bool _isInitialized = false;

        public UserCrawler(LoginPage loginPage, FollowingPage followingPage)
        {
            _loginPage = loginPage;
            _followingPage = followingPage;
        }

        public void Initialize()
        {
            if (_isInitialized) return;

            _loginPage.Load();
            _loginPage.Login(_serviceUsername, _servicePassword);
            _loginPage.HandleAfrerLoginQuestions();

            _isInitialized = true;
            //create web driver instance
            //login to account
            //go to followed page and start discovery
        }

        public void Crawl()
        {
            Initialize();

            //ADD Check if we are on the following page
            _followingPage.Load("dr.imiller");
            
            var items = _followingPage.InfiniteScrollToBottomWithItemsLoading();
        }

        public List<User> GetMyFollowed()
        {
            return new List<User>();
        }
    }
}