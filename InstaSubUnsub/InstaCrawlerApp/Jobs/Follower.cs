using InstaCommon.Config.Jobs;
using InstaCrawlerApp.Account.Interfaces;
using InstaDomain;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Logging;

namespace InstaCrawlerApp.Jobs
{
    public class Follower : JobBase
    {
        private readonly IUserFollower _userFollower;
        private readonly IRepository _repo;

        public Follower(IUserFollower userFollower, IRepository repo, ILogger<Follower> logger,
            FollowerJobConfig config, IAccountProvider<Follower> accountProvider)
            : base(repo, logger, config, accountProvider)
        {
            _userFollower = userFollower;
            _repo = repo;
        }

        protected override int ExecuteInternal()
        {
            var usersToFollow = _repo.Query<InstaUser>().Where(u => u.Rank >= 3 && u.HasRussianText == true
                && u.LastPostDate >= DateTime.UtcNow.AddDays(-7).Date
                && u.FollowingDate == null && u.UnfollowingDate == null)
                .OrderByDescending(u => u.LastPostDate)
                .Take(LimitPerIteration)
                .ToArray();

            var followed = 0;
            foreach (var user in usersToFollow)
            {
                if (_userFollower.Follow(user, Account))
                {
                    var updated = user;
                    updated.FollowingDate = DateTime.UtcNow;
                    updated.Status = InstaDomain.Enums.UserStatus.Followed; //todo condider deprecating status
                    _repo.Update(updated);
                    _repo.SaveChanges();
                    followed++;
                }
            }

            return followed;
        }
    }
}
