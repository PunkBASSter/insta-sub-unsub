using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace InstaCrawlerApp
{
    public abstract class JobBase
    {
        protected readonly IRepository Repository;
        protected readonly ILogger<JobBase> Logger;
        
        protected abstract int LimitPerIteration { get; set; }
        protected JobBase(IRepository repo, ILogger<JobBase> logger)
        {
            Repository = repo;
            Logger= logger;
        }

        public async Task Execute(CancellationToken stoppingToken)
        {
            Logger.LogInformation("Job {0} started, limit per iteration: {1}.", GetType().Name, LimitPerIteration);
            var auditRecord = new JobAuditRecord
            { 
                JobName = GetType().Name,
                ExecutionStart = DateTime.UtcNow,
                LimitPerIteration= LimitPerIteration,
            };

            try
            {
                auditRecord = await ExecuteInternal(auditRecord, stoppingToken);
            }
            catch(Exception ex)
            {
                auditRecord.ErrorInfo = JsonSerializer.Serialize(ex);
            }

            auditRecord.ExecutionEnd = DateTime.UtcNow;
            Repository.Insert(auditRecord);
            Repository.SaveChanges();
            Logger.LogInformation("Job {0} finished, limit per iteration: {1}, actually processed {2}.",
                GetType().Name, LimitPerIteration, auditRecord.ProcessedNumber);
        }

        /// <summary>
        /// Receives and returns the modified JobAuditRecord.
        /// </summary>
        /// <param name="auditRecord"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected abstract Task<JobAuditRecord> ExecuteInternal(JobAuditRecord auditRecord, CancellationToken stoppingToken);
    }
}
