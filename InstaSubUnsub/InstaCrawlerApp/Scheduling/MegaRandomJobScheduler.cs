using InstaCrawlerApp.Jobs;
using Microsoft.Extensions.Logging;

namespace InstaCrawlerApp.Scheduling
{
    /// <summary>
    /// Decorates the ordinary job to be executed with randomized intervals 
    /// within allowed timeframe (day) acheiving planned processing results.
    /// To make it work just schedule the decorated job at the new day start.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MegaRandomJobScheduler<T> : BasicScheduler<T> where T : JobBase
    {
        public MegaRandomJobScheduler(T jobInstance, Logger<MegaRandomJobScheduler<T>> logger)
            : base(jobInstance, logger)
        {
        }

        protected override JobExecutionDetails[] GenerateSchedule()
        {
            var rnd = new Random(DateTime.Now.Millisecond);

            var startingHour = TimeSpan.FromHours(JobConfig.WorkStartingHour ?? 10);
            var duration = TimeSpan.FromHours(JobConfig.WorkDurationHours ?? 15);

            var periodStart = (DateTime.UtcNow.Date + startingHour).AddSeconds(rnd.Next(JobConfig.MinDelay, JobConfig.MaxDelay));
            var periodEnd = (periodStart + duration).AddSeconds(-rnd.Next(JobConfig.MinDelay, JobConfig.MaxDelay));

            var dailyLimit = JobConfig.LimitPerDay + rnd.Next(-JobConfig.LimitPerDayDispersion, JobConfig.LimitPerDayDispersion);
            var iterationsNumber = rnd.Next(JobConfig.MinIterationsPerDay, JobConfig.MaxIterationsPerDay);
            var iterationLimitBase = Math.Max(dailyLimit / iterationsNumber, JobConfig.LimitPerIteration);

            var iterationsPlanned = new JobExecutionDetails[iterationsNumber];

            var iterationSum = 0;
            var workingTimeSpan = periodEnd - periodStart;
            var intervalBetweenIterations = workingTimeSpan.TotalSeconds / (iterationsNumber - 1);
            int intervalDispersion = Math.Min(Convert.ToInt32(intervalBetweenIterations / 2), JobConfig.MaxIntervalDispersion);
            for (var i = 1; i < iterationsNumber; i++)
            {
                var iteration = new JobExecutionDetails
                {
                    StartTime = periodStart
                    + TimeSpan.FromSeconds((i - 1) * intervalBetweenIterations
                    + rnd.Next(-intervalDispersion, intervalDispersion)),
                    LimitPerIteration = iterationLimitBase + rnd.Next(-JobConfig.LimitPerIterationDispersion,
                        JobConfig.LimitPerIterationDispersion),
                };
                iterationsPlanned[i - 1] = iteration;
                iterationSum += iteration.LimitPerIteration;
            }

            //last iteration calculated separately
            iterationsPlanned[iterationsNumber - 1] = new JobExecutionDetails
            {
                StartTime = periodEnd + TimeSpan.FromSeconds(rnd.Next(-intervalDispersion, intervalDispersion)),
                LimitPerIteration = dailyLimit - iterationSum,
            };

            return iterationsPlanned.OrderBy(i => i.StartTime).ToArray();
        }
    }
}
