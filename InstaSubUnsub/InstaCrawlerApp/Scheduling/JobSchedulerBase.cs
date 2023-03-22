using InstaCrawlerApp.Jobs;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;

namespace InstaCrawlerApp.Scheduling
{
    public abstract class JobSchedulerBase<T> where T: JobBase
    {
        protected readonly T JobInstance;

        public JobSchedulerBase(T jobInstance)
        {
            JobInstance = jobInstance;
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

        protected abstract JobScheduleItem[] GenerateSchedule();
    }
}
