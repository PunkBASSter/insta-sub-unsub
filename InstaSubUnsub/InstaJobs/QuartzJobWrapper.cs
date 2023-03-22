using InstaCrawlerApp.Jobs;
using InstaCrawlerApp.Scheduling;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace InstaJobs
{
    [DisallowConcurrentExecution]
    public class QuartzJobWrapper<T> : IJob where T: JobBase
    {
        protected readonly IServiceProvider _serviceProvider;

        public QuartzJobWrapper(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
        }

        protected virtual async Task Execute(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            
            var job = scope.ServiceProvider.GetRequiredService<RandomDelayJobScheduler<T>>();
            await job.Execute(stoppingToken);
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await Execute(context.CancellationToken);
        }
    }
}
