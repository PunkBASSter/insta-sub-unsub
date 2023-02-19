using InstaDomain;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;
using SeleniumUtils.PageObjects;
using UserStatus = InstaDomain.Enums.UserStatus;

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
        private readonly int _unfollowLimitPerIteration;

        public Unfollower(LoginPage loginPage, FollowingPage followingPage, IRepository<InstaUser> repo, IConfiguration configuration)
        {
            _loginPage = loginPage;
            _followingPage = followingPage;
            _instaUsersRepo = repo;
            _configuration = configuration;
            _serviceUsername = new Lazy<string>(() => _configuration.GetSection("UnfollowUser:Username").Value ?? throw new ArgumentException("UnfollowUser:Username is not provided"));
            _servicePassword = new Lazy<string>(() => _configuration.GetSection("UnfollowUser:Password").Value ?? throw new ArgumentException("UnfollowUser:Password is not provided"));
            _unfollowLimitPerIteration = Convert.ToInt32(_configuration.GetSection("Unfollow:LimitPerIteration").Value);
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

        public IList<InstaUser> GetFollowingFromUi()
        {
            _followingPage.Load(_serviceUsername.Value);
            var items = _followingPage.InfiniteScrollToBottomWithItemsLoading(); //TODO replace with InfiniteScrollToBottom

            //TODO filter grey buttons

            var users = items.Select(i => new InstaUser { Name = i.UserName, Status = UserStatus.Followed });
            return users.ToList();
        }

        public void Unfollow()
        {
            Initialize();

            var users = GetFollowingFromUi();
            foreach (var user in users)
            {
                _instaUsersRepo.InsertOrSkip(user, u => u.Name == user.Name);
            }
            _instaUsersRepo.SaveChanges();

            var subsToCancel = _instaUsersRepo.Query.Where(usr => usr.Status == UserStatus.Followed)
                .OrderBy(usr => usr.FollowingDate)
                .Take(_unfollowLimitPerIteration)
                .ToList();

            foreach(var sub in subsToCancel)
            {
                var uiSubItem = _followingPage.FollowingItems.FirstOrDefault(item => item.UserName == sub.Name);
                if (uiSubItem is null)
                {
                    //TODO log as warning and consider deleting from DB or marking as unfollowed + add date
                    continue;
                }

                var success = uiSubItem.Unfollow();
                if (success)
                {
                    //_instaUsersRepo.Update + date
                }
                //else -- LOG Warning
            }
            
        }
    }
}
