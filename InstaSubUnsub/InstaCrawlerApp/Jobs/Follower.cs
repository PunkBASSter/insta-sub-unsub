using InstaCommon.Config.Jobs;
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
        private readonly int _subLimitPerIteration;

        public Follower(IUserFollower userFollower, IRepository repo, ILogger<Follower> logger, FollowerJobConfig config) : base(repo, logger, config)
        {
            _userFollower = userFollower;
            _repo = repo;
            _subLimitPerIteration = Config.LimitPerIteration + new Random(DateTime.Now.Millisecond).Next(-Config.LimitDispersion, Config.LimitPerIteration);
        }

        protected override int ExecuteInternal()
        {
            var usersToFollow = _repo.Query<InstaUser>().Where(u => u.Rank >= 3 && u.HasRussianText == true
                && u.LastPostDate >= DateTime.UtcNow.AddDays(-7).Date
                && u.FollowingDate == null && u.UnfollowingDate == null)
                .OrderByDescending(u => u.LastPostDate)
                .Take(_subLimitPerIteration)
                .ToArray();

            var followed = 0;
            foreach (var user in usersToFollow)
            {
                if (_userFollower.Follow(user))
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
