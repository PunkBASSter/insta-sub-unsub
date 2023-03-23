using InstaCommon.Config.Jobs;
using InstaCrawlerApp.Jobs;

namespace InstaCrawlerApp.Scheduling
{
    public abstract class JobSchedulerBase<T> where T: JobBase
    {
        protected readonly T JobInstance;
        protected readonly JobConfigBase _jobConfig;

        public JobSchedulerBase(T jobInstance)
        {
            JobInstance = jobInstance;
            _jobConfig = JobInstance.GetConfig();
        }

        public virtual async Task Execute(CancellationToken cancellationToken)
        {
            var schedule = GenerateSchedule().Where(js => js.StartTime > DateTime.UtcNow);

            foreach (var iteration in schedule)
            {
                var timeDiff = iteration.StartTime - DateTime.UtcNow;
                if (timeDiff.TotalSeconds >= -1)
                {
                    await Task.Delay(timeDiff, cancellationToken);
                    await JobInstance.Execute(iteration, cancellationToken);
                }
            }
        }

        protected abstract JobExecutionDetails[] GenerateSchedule();
    }
}
