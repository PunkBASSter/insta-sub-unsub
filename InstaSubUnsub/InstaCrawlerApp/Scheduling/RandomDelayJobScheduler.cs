using InstaCommon.Config.Schedulers;
using InstaCrawlerApp.Jobs;

namespace InstaCrawlerApp.Scheduling
{
    /// <summary>
    /// Decorates the ordinary job to be executed with randomized intervals 
    /// within allowed timeframe (day) acheiving planned processing results.
    /// To make it work just schedule the decorated job at the new day start.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RandomDelayJobScheduler<T> : JobSchedulerBase<T> where T : JobBase
    {
        private readonly RandomDelayJobSchedulerConfig _config;

        public RandomDelayJobScheduler(T jobInstance, RandomDelayJobSchedulerConfig config): base(jobInstance)
        {
            _config = config;
        }

        protected override JobScheduleItem[] GenerateSchedule()
        {
            var rnd = new Random(DateTime.Now.Millisecond);

            var scheduleItem = new JobScheduleItem
            {
                StartTime = DateTime.UtcNow + TimeSpan.FromSeconds(rnd.Next(_config.MinDelay, _config.MaxDelay)),
            };

            return new[] { scheduleItem };
        }
    }
}
