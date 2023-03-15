using InstaDomain;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;
using InstaDomain.Enums;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Logging;
using System.Data;

namespace InstaCrawlerApp
{
    /// <summary>
    /// Acts on behalf of main account owner
    /// </summary>
    public class Unfollower
    {
        private readonly IUserUnfollower _userUnfollower;
        private readonly IFollowingsProvider _followingsProvider;
        private readonly IRepository _repo;
        private readonly IConfiguration _configuration;
        private readonly int _unfollowLimitPerIteration;
        private readonly InstaAccount _account;
        
        public Unfollower(IUserUnfollower unfollower, IFollowingsProvider followingsProvider, IRepository repo, ILogger<Follower> logger, IConfiguration conf)
        {
            _userUnfollower = unfollower;
            _repo = repo;
            _configuration = conf;
            _followingsProvider = followingsProvider;
            _account = new InstaAccount(conf.GetRequiredSection("FollowUser:Username").Value, conf.GetRequiredSection("FollowUser:Password").Value);
        }

        private InstaUser _mainAccountUser;
        private InstaUser MainAccountUser 
        {
            get
            {
                _mainAccountUser ??= _repo.Query<InstaUser>().First(u => u.Name == _account.Username);
                return _mainAccountUser; 
            } 
        }

        public void Unfollow()
        {
            _userUnfollower.Login(_account);

            var unfollowedCount = UnfollowBasedOnDb();

            if (unfollowedCount >= _unfollowLimitPerIteration)
                return;

            var numberToUnfollowInUi = _unfollowLimitPerIteration - unfollowedCount;
            unfollowedCount += UnfollowBasedOnUi(numberToUnfollowInUi);

            //todo logging to output numbers
        }

        private int UnfollowBasedOnDb()
        {
            var dbUsersToUnfollow = _repo.Query<InstaUser>()
                .Where(usr => usr.Status == UserStatus.Followed && usr.FollowingDate > default(DateTime) && usr.UnfollowingDate == null)
                .OrderBy(usr => usr.FollowingDate)
                .Take(_unfollowLimitPerIteration)
                .ToArray();

            return UnfollowFromUsers(dbUsersToUnfollow);
        }

        private int UnfollowBasedOnUi(int number)
        {
            _followingsProvider.LoggedInUsername = _account.Username;
            var uiUsersToUnfollow = _followingsProvider.GetByUser(MainAccountUser, _account);

            var protectedUserNames = _repo.Query<InstaUser>().Where(u => u.Status == UserStatus.Protected).Select(u => u.Name).ToArray();
            _ = uiUsersToUnfollow.ExceptBy(protectedUserNames, u => u.Name).ToArray();

            return UnfollowFromUsers(uiUsersToUnfollow);
        }

        private int UnfollowFromUsers(IEnumerable<InstaUser> users)
        {
            var total = 0;
            foreach (var user in users)
            {
                if (_userUnfollower.Unfollow(user))
                {
                    var updUser = user;
                    UnfollowUserInDb(updUser);
                    total++;
                }
            }
            return total;
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
