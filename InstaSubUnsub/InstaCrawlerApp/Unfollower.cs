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
        private readonly int _unfollowLimitPerIteration;
        private readonly InstaAccount _account;
        private readonly ILogger<Unfollower> _logger;
        
        public Unfollower(IUserUnfollower unfollower, IFollowingsProvider followingsProvider, IRepository repo, ILogger<Unfollower> logger, IConfiguration conf)
        {
            _userUnfollower = unfollower;
            _repo = repo;
            _followingsProvider = followingsProvider;
            _account = new InstaAccount(conf.GetRequiredSection("FollowUser:Username").Value, conf.GetRequiredSection("FollowUser:Password").Value);
            _logger = logger;
            _unfollowLimitPerIteration = Convert.ToInt32(conf.GetRequiredSection("Unfollow:LimitPerIteration").Value)
                + new Random(DateTime.Now.Microsecond).Next(-4,4);
        }

        private InstaUser? _mainAccountUser;
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
            _logger.LogInformation("Started unfollowing. Limit: {0}", _unfollowLimitPerIteration);

            _userUnfollower.Login(_account);

            var unfollowedCount = UnfollowBasedOnDb();

            if (unfollowedCount >= _unfollowLimitPerIteration)
                return;

            var numberToUnfollowInUi = _unfollowLimitPerIteration - unfollowedCount;
            unfollowedCount += UnfollowBasedOnUi(numberToUnfollowInUi);

            _logger.LogInformation("Completed unfollowing. Processed: {0}", unfollowedCount);
        }

        private int UnfollowBasedOnDb()
        {
            var dbUsersToUnfollow = _repo.Query<InstaUser>()
                .Where(usr => usr.Status == UserStatus.Followed 
                    && usr.FollowingDate > default(DateTime) 
                    && usr.FollowingDate <= DateTime.UtcNow.AddDays(-14) && usr.UnfollowingDate == null)
                .OrderBy(usr => usr.FollowingDate)
                .Take(_unfollowLimitPerIteration)
                .ToArray();

            return UnfollowFromUsers(dbUsersToUnfollow);
        }

        private int UnfollowBasedOnUi(int number)
        {
            var protectedUserNames = _repo.Query<InstaUser>().Where(u => u.Status == UserStatus.Protected).Select(u => u.Name).ToArray();

            _followingsProvider.LoggedInUsername = _account.Username;
            _followingsProvider.Limit = number + protectedUserNames.Length;
            var uiUsersToUnfollow = _followingsProvider.GetByUser(MainAccountUser, _account);

            uiUsersToUnfollow = uiUsersToUnfollow.ExceptBy(protectedUserNames, u => u.Name).Take(number).ToArray();

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
