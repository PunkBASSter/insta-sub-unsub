using InstaCrawlerApp.Jobs;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;

namespace InstaCrawlerApp.Scheduling
{
    /// <summary>
    /// Decorates the ordinary job to be executed with randomized intervals 
    /// within allowed timeframe (day) acheiving planned processing results.
    /// To make it work just schedule the decorated job at the new day start.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MegaRandomJobScheduler<T> : JobSchedulerBase<T> where T : JobBase
    {
        public MegaRandomJobScheduler(T jobInstance) : base(jobInstance) { }

        protected override JobScheduleItem[] GenerateSchedule()
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var startingHour = TimeSpan.FromHours(10);
            var duration = TimeSpan.FromHours(15);
            var midnight = DateTime.UtcNow.Date;
            var periodStart = (midnight + startingHour).AddSeconds(rnd.Next(1800));
            var periodEnd = (periodStart + duration).AddSeconds(-rnd.Next(1800));

            var dailyLimit = 45 + rnd.Next(-3, 4);
            var iterationsNumber = rnd.Next(3, 11);
            var iterationLimitBase = dailyLimit / iterationsNumber;

            var iterationsPlanned = new JobScheduleItem[iterationsNumber];

            var iterationSum = 0;
            var workingTimeSpan = periodEnd - periodStart;
            var intervalBetweenIterations = workingTimeSpan.TotalSeconds / (iterationsNumber - 1);
            int intervalDispersion = Math.Min(Convert.ToInt32(intervalBetweenIterations / 2), 2400);
            for (var i = 1; i < iterationsNumber; i++)
            {
                var iteration = new JobScheduleItem
                {
                    StartTime = periodStart
                    + TimeSpan.FromSeconds((i - 1) * intervalBetweenIterations
                    + rnd.Next(-intervalDispersion, intervalDispersion)),
                    LimitPerIteration = iterationLimitBase + rnd.Next(-1, 1),
                };
                iterationsPlanned[i - 1] = iteration;
                iterationSum += iteration.LimitPerIteration;
            }

            //last iteration calculated separately
            iterationsPlanned[iterationsNumber - 1] = new JobScheduleItem
            {
                StartTime = periodEnd + TimeSpan.FromSeconds(rnd.Next(-intervalDispersion, intervalDispersion)),
                LimitPerIteration = dailyLimit - iterationSum,
            };

            return iterationsPlanned;
        }
    }
}
