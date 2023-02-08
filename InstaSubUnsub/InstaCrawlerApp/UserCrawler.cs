using InstaDomain;
using SeleniumUtils.PageObjects;

namespace InstaCrawlerApp
{
    public class UserCrawler
    {
        private readonly LoginPage _loginPage;
        private readonly string _serviceUsername = "hidethetrack123";
        private readonly string _servicePassword = "Imnot4insta!1";
        private readonly bool _isInitialized = false;

        public UserCrawler(LoginPage loginPage, FollowingPage followingPage)
        {
            _loginPage = loginPage;
        }

        public void Initialize()
        {
            if (_isInitialized) return;

            _loginPage.Load();
            _loginPage.Login(_serviceUsername, _servicePassword);


            //create web driver instance
            //login to account
            //go to followed page and start discovery
        }

        public void Crawl()
        {
            Initialize();


        }

        public List<User> GetMyFollowed()
        {
            return new List<User>();
        }
    }
}