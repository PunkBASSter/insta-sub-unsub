using InstaCommon.Config.Jobs;
using InstaCrawlerApp.Jobs;

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
        private readonly JobConfigBase _jobConfig;

        public MegaRandomJobScheduler(T jobInstance) : base(jobInstance)
        {
            _jobConfig = jobInstance.GetConfig();
        }

        protected override JobExecutionDetails[] GenerateSchedule()
        {
            var rnd = new Random(DateTime.Now.Millisecond);

            var startingHour = TimeSpan.FromHours(_jobConfig.WorkStartingHour ?? 10);
            var duration = TimeSpan.FromHours(_jobConfig.WorkDurationHours ?? 15);

            var periodStart = (DateTime.UtcNow.Date + startingHour).AddSeconds(rnd.Next(_jobConfig.MinDelay, _jobConfig.MaxDelay));
            var periodEnd = (periodStart + duration).AddSeconds(-rnd.Next(_jobConfig.MinDelay, _jobConfig.MaxDelay));

            var dailyLimit = _jobConfig.LimitPerDay + rnd.Next(-_jobConfig.LimitPerDayDispersion, _jobConfig.LimitPerDayDispersion);
            var iterationsNumber = rnd.Next(_jobConfig.MinIterationsPerDay, _jobConfig.MaxIterationsPerDay);
            var iterationLimitBase = Math.Max(dailyLimit / iterationsNumber, _jobConfig.LimitPerIteration);

            var iterationsPlanned = new JobExecutionDetails[iterationsNumber];

            var iterationSum = 0;
            var workingTimeSpan = periodEnd - periodStart;
            var intervalBetweenIterations = workingTimeSpan.TotalSeconds / (iterationsNumber - 1);
            int intervalDispersion = Math.Min(Convert.ToInt32(intervalBetweenIterations / 2), _jobConfig.MaxIntervalDispersion);
            for (var i = 1; i < iterationsNumber; i++)
            {
                var iteration = new JobExecutionDetails
                {
                    StartTime = periodStart
                    + TimeSpan.FromSeconds((i - 1) * intervalBetweenIterations
                    + rnd.Next(-intervalDispersion, intervalDispersion)),
                    LimitPerIteration = iterationLimitBase + rnd.Next(-_jobConfig.LimitPerIterationDispersion,
                        _jobConfig.LimitPerIterationDispersion),
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
