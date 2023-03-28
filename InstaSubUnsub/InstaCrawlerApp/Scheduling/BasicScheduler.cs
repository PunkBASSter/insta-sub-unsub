using InstaCommon.Config.Jobs;
using InstaCrawlerApp.Jobs;
using Microsoft.Extensions.Logging;

namespace InstaCrawlerApp.Scheduling
{
    public class BasicScheduler<T> where T: JobBase
    {
        protected readonly T JobInstance;
        protected readonly JobConfigBase JobConfig;
        protected readonly ILogger<BasicScheduler<T>> _logger;

        public BasicScheduler(T jobInstance, ILogger<BasicScheduler<T>> logger)
        {
            JobInstance = jobInstance;
            JobConfig = JobInstance.GetConfig();
            _logger = logger;
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
                    _logger.LogInformation("{0} will start after {1} seconds",
                         JobInstance.GetType().Name, timeDiff.TotalSeconds);
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
