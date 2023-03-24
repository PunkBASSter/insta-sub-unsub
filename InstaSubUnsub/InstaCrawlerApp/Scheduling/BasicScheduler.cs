using InstaCommon.Config.Jobs;
using InstaCrawlerApp.Jobs;

namespace InstaCrawlerApp.Scheduling
{
    public class BasicScheduler<T> where T: JobBase
    {
        protected readonly T JobInstance;
        protected readonly JobConfigBase JobConfig;

        public BasicScheduler(T jobInstance)
        {
            JobInstance = jobInstance;
            JobConfig = JobInstance.GetConfig();
        }

        public virtual async Task Execute(CancellationToken cancellationToken)
        {
            var refTime = DateTime.UtcNow.AddSeconds(-1);
            var schedule = GenerateSchedule().Where(js => js.StartTime > refTime);

            foreach (var iteration in schedule)
            {
                var timeDiff = iteration.StartTime - refTime;
                if (timeDiff.TotalSeconds >= 0)
                {
                    await Task.Delay(timeDiff, cancellationToken);
                    await JobInstance.Execute(iteration, cancellationToken);
                }
            }
        }

        protected virtual JobExecutionDetails[] GenerateSchedule()
        {
            return new[] { new JobExecutionDetails() { StartTime = DateTime.UtcNow } };
        }
    }
}
