using InstaCrawlerApp.Jobs;
using InstaCrawlerApp.Scheduling;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace InstaJobs
{
    [DisallowConcurrentExecution]
    public class MegaRandomQuartzJobWrapper<T> : QuartzJobWrapper<T> where T : JobBase
    {
        public MegaRandomQuartzJobWrapper(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override async Task Execute(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var fuzzyScheduledJob = scope.ServiceProvider.GetRequiredService<MegaRandomJobScheduler<T>>();
            await fuzzyScheduledJob.Execute(stoppingToken);
        }
    }
}
