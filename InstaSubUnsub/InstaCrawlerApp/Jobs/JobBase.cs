using InstaCommon.Config.Jobs;
using InstaCrawlerApp.Scheduling;
using InstaDomain.Account;
using InstaInfrastructureAbstractions;
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

        private readonly IAccountProvider<JobBase> _accountProvider;

        protected InstaAccount Account => _accountProvider.Get();

        private int? _limitPerIteration;
        protected virtual int LimitPerIteration => _limitPerIteration.GetValueOrDefault();
        
        protected JobBase(IRepository repo, ILogger<JobBase> logger, JobConfigBase config, IAccountProvider<JobBase> accountProvider)
        {
            Repository = repo;
            Logger = logger;
            Config = config;
            _limitPerIteration ??= Config.LimitPerIteration + new Random(DateTime.Now.Millisecond)
                .Next(-Config.LimitPerIterationDispersion, Config.LimitPerIterationDispersion);
            _accountProvider = accountProvider;
        }

        public async Task<JobExecutionDetails> Execute(JobExecutionDetails scheduleItem, CancellationToken cancellation)
        {
            var executionDetails = scheduleItem;
            if (executionDetails.LimitPerIteration != default)
                _limitPerIteration = executionDetails.LimitPerIteration;

            Logger.LogInformation("Job {0} started, limit per iteration: {1}.", GetType().Name, LimitPerIteration);

            executionDetails.JobName = GetType().Name;
            executionDetails.StartTime = DateTime.UtcNow;

            int processed = 0;
            try
            {
                processed = await Task.Run(ExecuteInternal, cancellation);
            }
            catch (Exception ex)
            {
                executionDetails.ErrorInfo = JsonSerializer.Serialize(ex);
            }

            executionDetails.ProcessedNumber = processed;
            executionDetails.CompletionTime = DateTime.UtcNow;

            Repository.Insert(executionDetails);
            Repository.SaveChanges();

            Logger.LogInformation("Job {0} finished, limit per iteration: {1}, actually processed {2}.",
                GetType().Name, LimitPerIteration, executionDetails.ProcessedNumber);

            _accountProvider.SaveUsageHistory(processed);

            return executionDetails;
        }

        public JobConfigBase GetConfig() => Config;

        protected abstract int ExecuteInternal();
    }
}
