using InstaCommon.Config.Jobs;
using InstaCrawlerApp.Scheduling;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace InstaCrawlerApp.Jobs
{
    public abstract class JobBase
    {
        protected readonly IRepository Repository;
        protected readonly ILogger<JobBase> Logger;
        protected readonly JobConfigBase Config;

        /// <summary>
        /// Optional information about the current scheduled job execution.
        /// If passed, overwrites the config values, particularly LimitPerIteration if non-default.
        /// </summary>
        protected JobScheduleItem ScheduleItem { get; private set; } = new JobScheduleItem();

        private int? _limitPerIteration;
        protected virtual int LimitPerIteration => _limitPerIteration.GetValueOrDefault();
        
        protected JobBase(IRepository repo, ILogger<JobBase> logger, JobConfigBase config)
        {
            Repository = repo;
            Logger = logger;
            Config = config;
            _limitPerIteration ??= Config.LimitPerIteration + new Random(DateTime.Now.Millisecond).Next(-Config.LimitDispersion, Config.LimitDispersion);
        }

        public async Task<JobScheduleItem> Execute(JobScheduleItem scheduleItem, CancellationToken cancellation)
        {
            ScheduleItem = scheduleItem;
            if (ScheduleItem.LimitPerIteration != default)
                _limitPerIteration = ScheduleItem.LimitPerIteration;

            Logger.LogInformation("Job {0} started, limit per iteration: {1}.", GetType().Name, LimitPerIteration);

            ScheduleItem.JobName = GetType().Name;
            ScheduleItem.StartTime = DateTime.UtcNow;

            int processed = 0;
            try
            {
                processed = await Task.Run(ExecuteInternal, cancellation);
            }
            catch (Exception ex)
            {
                ScheduleItem.ErrorInfo = JsonSerializer.Serialize(ex);
            }

            ScheduleItem.ProcessedNumber = processed;
            ScheduleItem.CompletionTime = DateTime.UtcNow;

            Repository.Insert(ScheduleItem);
            Repository.SaveChanges();

            Logger.LogInformation("Job {0} finished, limit per iteration: {1}, actually processed {2}.",
                GetType().Name, LimitPerIteration, ScheduleItem.ProcessedNumber);

            return ScheduleItem;
        }

        public JobConfigBase GetConfig() => Config;

        protected abstract int ExecuteInternal();
    }
}
