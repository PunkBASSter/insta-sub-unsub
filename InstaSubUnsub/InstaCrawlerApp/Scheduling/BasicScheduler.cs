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
            if (JobConfig.Disabled)
            {
                _logger.LogWarning("Job {0} is disabled in the App settings. Skipping...", GetType().GenericTypeArguments.First().Name);
                return;
            }

            var refTime = DateTime.UtcNow.AddSeconds(-1);
            var schedule = GenerateSchedule().Where(js => js.StartTime > refTime);
            LogScheduleItems(schedule);

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

        private void LogScheduleItems(IEnumerable<JobExecutionDetails> items)
        {
            foreach (var item in items) 
            {
                _logger.LogInformation("{0} will start at {1} to process {2}",
                    GetType().GenericTypeArguments.First().Name, item.StartTime,
                    item.LimitPerIteration);
            }
        }

        protected virtual JobExecutionDetails[] GenerateSchedule()
        {
            return new[] { new JobExecutionDetails() { StartTime = DateTime.UtcNow } };
        }
    }
}
