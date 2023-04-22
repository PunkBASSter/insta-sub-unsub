using InstaDomain;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;
using InstaDomain.Enums;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Logging;
using System.Data;
using InstaCommon.Config.Jobs;
using InstaDomain.Account;
using InstaCrawlerApp.Account.Interfaces;
using InstaCommon.Exceptions;

namespace InstaCrawlerApp.Jobs
{
    /// <summary>
    /// Acts on behalf of main account owner
    /// </summary>
    public class Unfollower : JobBase
    {
        private readonly IUserUnfollower _userUnfollower;
        private readonly IFollowingsProvider _followingsProvider;
        private readonly IRepository _repo;
        
        public Unfollower(IUserUnfollower unfollower, IFollowingsProvider followingsProvider,
            IRepository repo, ILogger<Unfollower> logger, UnfollowerJobConfig conf, IAccountProvider<Unfollower> accProvider)
            : base(repo, logger, conf, accProvider)
        {
            _userUnfollower = unfollower;
            _repo = repo;
            _followingsProvider = followingsProvider;
        }

        private InstaUser? _mainAccountUser;
        private InstaUser MainAccountUser
        {
            get
            {
                _mainAccountUser ??= _repo.Query<InstaUser>().First(u => u.Name == Account.Username);
                return _mainAccountUser;
            }
        }

        protected override void ExecuteInternal()
        {
            _userUnfollower.Login(Account);

            UnfollowBasedOnDb();

            if (ItemsProcessedPerIteration >= LimitPerIteration)
                return;

            var numberToUnfollowInUi = LimitPerIteration - ItemsProcessedPerIteration;
            UnfollowBasedOnUi(numberToUnfollowInUi);

            return;
        }

        private void UnfollowBasedOnDb()
        {
            var dbUsersToUnfollow = _repo.Query<InstaUser>()
                .Where(usr => usr.Status == UserStatus.Followed
                    && usr.FollowingDate > default(DateTime)
                    && usr.FollowingDate <= DateTime.UtcNow.AddDays(-14) && usr.UnfollowingDate == null)
                .OrderBy(usr => usr.FollowingDate)
                .Take(LimitPerIteration)
                .ToArray();

            UnfollowFromUsers(dbUsersToUnfollow);
        }

        private void UnfollowBasedOnUi(int number)
        {
            var protectedUserNames = _repo.Query<InstaUser>().Where(u => u.Status == UserStatus.Protected).Select(u => u.Name).ToArray();

            _followingsProvider.LoggedInUsername = Account.Username;
            _followingsProvider.Limit = number + protectedUserNames.Length;
            var uiUsersToUnfollow = _followingsProvider.GetByUser(MainAccountUser, Account);

            uiUsersToUnfollow = uiUsersToUnfollow.ExceptBy(protectedUserNames, u => u.Name).Take(number).ToArray();

            UnfollowFromUsers(uiUsersToUnfollow);
        }

        private void UnfollowFromUsers(IEnumerable<InstaUser> users)
        {
            foreach (var user in users)
            {
                try
                {
                    if (_userUnfollower.Unfollow(user, Account))
                    {
                        var updUser = user;
                        if (user.Id > 0) // todo record user in DB ??
                            UnfollowUserInDb(user);
                        ItemsProcessedPerIteration++;
                    }
                }
                catch (InstaAntiBotException)
                {
                    throw;
                }
                catch (UserPageUnavailable)
                {
                    var upd = user;
                    upd.Status = UserStatus.Unavailable;
                    _repo.Update(upd);
                    _repo.SaveChanges();
                }
            }
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
