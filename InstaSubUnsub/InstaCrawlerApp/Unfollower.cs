using InstaDomain;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;
using SeleniumUtils.PageObjects;


namespace InstaCrawlerApp
{
    public class Unfollower
    {
        private readonly LoginPage _loginPage;
        private readonly FollowingPage _followingPage;
        private readonly Lazy<string> _serviceUsername;
        private readonly Lazy<string> _servicePassword;
        private bool _isInitialized = false;
        private readonly IRepository<InstaUser> _instaUsersRepo;
        private readonly IConfiguration _configuration;

        public Unfollower(LoginPage loginPage, FollowingPage followingPage, IRepository<InstaUser> repo, IConfiguration configuration)
        {
            _loginPage = loginPage;
            _followingPage = followingPage;
            _instaUsersRepo = repo;
            _configuration = configuration;
            _serviceUsername = new Lazy<string>(() => _configuration.GetSection("UnfollowUser:Username").Value ?? throw new ArgumentException("UnfollowUser:Username is not provided"));
            _servicePassword = new Lazy<string>(() => _configuration.GetSection("UnfollowUser:Password").Value ?? throw new ArgumentException("UnfollowUser:Password is not provided"));
        }

        public void Initialize()
        {
            if (_isInitialized) return;

            _loginPage.Load();
            _loginPage.Login(_serviceUsername.Value, _servicePassword.Value);
            _loginPage.HandleAfrerLoginQuestions();

            //todo save cookies

            _isInitialized = true;
        }

        public IEnumerable<InstaUser> GetFollowingFromUi()
        {
            _followingPage.Load(_serviceUsername.Value);
            var items = _followingPage.InfiniteScrollToBottomWithItemsLoading();

            //TODO filter grey buttons

            var users = items.Select(i => new InstaUser { Name = i.UserName, });
            return users;
        }

        public void Unfollow()
        {
            Initialize();

            var users = GetFollowingFromUi();

            //todo saving users with add/skip
        }
    }
}
