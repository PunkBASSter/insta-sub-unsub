using InstaCommon.Config.Limits;
using InstaCrawlerApp.Scheduling;
using InstaInfrastructureAbstractions.PersistenceInterfaces;


namespace InstaCrawlerApp.ProcessingLimits
{
    public class SharedHourLimitTracker
    {
        private readonly SharedFollowUnfollowLimitConfig _conf;
        private readonly IRepository _repo;

        public SharedHourLimitTracker(SharedFollowUnfollowLimitConfig config, IRepository repo)
        {
            _conf = config;
            _repo = repo;
        }

        public int GetFreeLimit()
        {
            var maxHourLimit = _conf.LimitPerHour;
            var jobNames = _conf.JobTypeNames;

            var lastHourStart = DateTime.UtcNow.AddHours(-1);

            var sumOfProcessedBefore = _repo.Query<JobExecutionDetails>()
                .OrderByDescending(x => x.Id)
                .Take(20)
                .Where(x => jobNames.Contains(x.JobName) && x.StartTime >= lastHourStart)
                .Sum(x => x.ProcessedNumber);

            return maxHourLimit - sumOfProcessedBefore;

        }
    }
}
