using InstaDomain;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;
using SeleniumUtils;
using SeleniumUtils.PageObjects;
using UserStatus = InstaDomain.Enums.UserStatus;

namespace InstaCrawlerApp
{
    /// <summary>
    /// Acts on behalf of main account owner
    /// </summary>
    public class Unfollower
    {
        private readonly LoginPage _loginPage;
        private readonly FollowingPage _followingPage;
        private readonly Lazy<string> _serviceUsername;
        private readonly Lazy<string> _servicePassword;
        private bool _isInitialized = false;
        private readonly IRepository _repo;
        private readonly IConfiguration _configuration;
        private readonly int _unfollowLimitPerIteration;
        private readonly CookieUtil _cookieUtil;

        public Unfollower(LoginPage loginPage, FollowingPage followingPage, IRepository repo, IConfiguration configuration, CookieUtil cookieUtil)
        {
            _loginPage = loginPage;
            _followingPage = followingPage;
            _repo = repo;
            _configuration = configuration;
            _serviceUsername = new Lazy<string>(() => _configuration.GetSection("UnfollowUser:Username").Value ?? throw new ArgumentException("UnfollowUser:Username is not provided"));
            _servicePassword = new Lazy<string>(() => _configuration.GetSection("UnfollowUser:Password").Value ?? throw new ArgumentException("UnfollowUser:Password is not provided"));
            _unfollowLimitPerIteration = Convert.ToInt32(_configuration.GetSection("Unfollow:LimitPerIteration").Value);
            _cookieUtil = cookieUtil;
        }

        public void Initialize()
        {
            if (_isInitialized) return;

            _loginPage.Load();
            _loginPage.Login(_serviceUsername.Value, _servicePassword.Value);

            //todo save cookies

            _isInitialized = true;
        }

        public IList<InstaUser> GetFollowingFromUi()
        {
            _followingPage.Load(_serviceUsername.Value);
            var items = _followingPage.InfiniteScrollToBottomWithItemsLoading();

            var users = items.Select(i => new InstaUser { Name = i.UserName, Status = UserStatus.Followed });
            return users.ToList();
        }

        public void Unfollow()
        {
            Initialize();

            var users = GetFollowingFromUi();
            SaveUsersToDb(users);

            var usersToUnfollow = _repo.Query<InstaUser>()                
                .Where(usr => usr.Status == UserStatus.Followed)
                .OrderBy(usr => usr.FollowingDate)
                .Take(_unfollowLimitPerIteration)
                .ToList();

            foreach(var user in usersToUnfollow)
            {
                var uiFollowingItem = _followingPage.FollowingItems.FirstOrDefault(item => item.UserName == user.Name);
                if (uiFollowingItem is null)
                {
                    //TODO logging of unsynchronized data between DB and UI
                    UnfollowUserInDb(user);
                    continue;
                }

                var success = uiFollowingItem.Unfollow();
                if (success)
                {
                    UnfollowUserInDb(user);
                }
                //else { } -- //TODO logging of unsynchronized data between DB and UI
            }

        }

        private void SaveUsersToDb(IEnumerable<InstaUser> users)
        {
            foreach (var user in users)
            {
                _repo.InsertOrSkip(user, u => u.Name == user.Name);
            }
            _repo.SaveChanges();
        }

        private void UnfollowUserInDb(InstaUser user)
        {
            user.Status = UserStatus.Unfollowed;
            user.UnfollowingDate = DateTime.UtcNow;
            _repo.Update(user);
            _repo.SaveChanges();
        }
    }
}
