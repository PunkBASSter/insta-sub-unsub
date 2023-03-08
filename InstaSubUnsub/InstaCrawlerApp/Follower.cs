using InstaDomain;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Logging;

namespace InstaCrawlerApp
{
    public class Follower
    {
        private readonly IUserFollower _userFollower;
        private readonly IRepository _repo;
        private readonly int _subLimitPerIteration;
        private readonly ILogger<Follower> _logger;

        public Follower(IUserFollower userFollower, IRepository repo, ILogger<Follower> logger)
        {
            _userFollower = userFollower;
            _repo = repo;
            _logger = logger;
            _subLimitPerIteration = 13 + new Random(DateTime.Now.Millisecond).Next(-2,2);
        }

        public void Follow()
        {
            var usersToFollow = _repo.Query<InstaUser>().Where(u => u.Rank >= 3 && u.HasRussianText == true
                && u.LastPostDate >= DateTime.UtcNow.AddDays(-7).Date
                && u.FollowingDate == null && u.UnfollowingDate == null)
                .OrderByDescending(u => u.LastPostDate)
                .Take(_subLimitPerIteration)
                .ToArray();

            //usersToFollow = _repo.Query<InstaUser>().Where(u => u.Name == "meltali_handmade").ToArray();

            foreach (var user in usersToFollow)
            {
                if (_userFollower.Follow(user))
                {
                    var updated = user;
                    updated.FollowingDate = DateTime.UtcNow;
                    updated.Status = InstaDomain.Enums.UserStatus.Followed; //todo condider deprecating status
                    _repo.Update(updated);
                    _repo.SaveChanges();
                }
            }

        }
    }
}
