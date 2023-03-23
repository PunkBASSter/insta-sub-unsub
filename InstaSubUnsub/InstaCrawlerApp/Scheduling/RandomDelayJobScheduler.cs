﻿using InstaCrawlerApp.Jobs;

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
        public RandomDelayJobScheduler(T jobInstance): base(jobInstance) { }

        protected override JobExecutionDetails[] GenerateSchedule()
        {
            var rnd = new Random(DateTime.Now.Millisecond);

            var scheduleItem = new JobExecutionDetails
            {
                StartTime = DateTime.UtcNow + TimeSpan.FromSeconds(rnd.Next(_jobConfig.MinDelay, _jobConfig.MaxDelay)),
            };

            return new[] { scheduleItem };
        }
    }
}
