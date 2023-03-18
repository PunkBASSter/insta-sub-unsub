using InstaDomain;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Logging;

namespace InstaCrawlerApp
{
    public class Follower : JobBase
    {
        private readonly IUserFollower _userFollower;
        private readonly IRepository _repo;
        private readonly int _subLimitPerIteration;

        public Follower(IUserFollower userFollower, IRepository repo, ILogger<Follower> logger) : base(repo, logger)
        {
            _userFollower = userFollower;
            _repo = repo;
            _subLimitPerIteration = 13 + new Random(DateTime.Now.Millisecond).Next(-2,2);
        }

        protected override int LimitPerIteration { get; set; }

        protected override async Task<JobAuditRecord> ExecuteInternal(JobAuditRecord auditRecord, CancellationToken stoppingToken)
        {
            return await Task.Run(() =>
            {
                var followed = Follow();
                auditRecord.ProcessedNumber = followed;
                auditRecord.LimitPerIteration = LimitPerIteration;
                auditRecord.AccountName = _userFollower.LoggedInUsername ?? string.Empty;

                return auditRecord;
            }, stoppingToken);
        }

        public int Follow()
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
