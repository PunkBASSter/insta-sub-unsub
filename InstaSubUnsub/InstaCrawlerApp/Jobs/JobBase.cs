using InstaCommon.Config.Jobs;
using InstaCommon.Exceptions;
using InstaCommon.JsonConverters;
using InstaCrawlerApp.Account.Interfaces;
using InstaCrawlerApp.Scheduling;
using InstaDomain.Account;
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

            executionDetails.LimitPerIteration = _limitPerIteration.GetValueOrDefault();
            executionDetails.JobName = GetType().Name;
            executionDetails.Username = Account.Username;
            executionDetails.StartTime = DateTime.UtcNow;

            DateTime? antiBotDetectedTime = null;
            int processed = 0;
            try
            {
                processed = await Task.Run(ExecuteInternal, cancellation);
            }
            catch (InstaAntiBotException)
            {
                antiBotDetectedTime= DateTime.UtcNow;
                executionDetails.ErrorInfo = nameof(InstaAntiBotException);
            }
            catch (Exception ex)
            {
                executionDetails.ErrorInfo = HandleException(ex);
            }

            executionDetails.ProcessedNumber = processed;
            executionDetails.CompletionTime = DateTime.UtcNow;

            Repository.Insert(executionDetails);
            Repository.SaveChanges();

            Logger.LogInformation("Job {0} finished, limit per iteration: {1}, actually processed {2}.",
                GetType().Name, LimitPerIteration, executionDetails.ProcessedNumber);

            _accountProvider.SaveUsageHistory(processed, antiBotDetectedTime);

            return executionDetails;
        }

        public JobConfigBase GetConfig() => Config;

        protected abstract int ExecuteInternal();

        private string HandleException(Exception ex)
        {
            Logger.LogError(ex, ex.Message, ex.StackTrace);
            var error = "ERROR";
            try
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new ExceptionConverter());
                error = JsonSerializer.Serialize(ex, options);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message, e.StackTrace);
            }

            return error;
        }
    }
}
