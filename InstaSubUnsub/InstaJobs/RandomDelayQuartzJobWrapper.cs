using InstaCrawlerApp.Jobs;
using InstaCrawlerApp.Scheduling;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace InstaJobs
{
    [DisallowConcurrentExecution]
    public class RandomDelayQuartzJobWrapper<T> : QuartzJobWrapper<T>, IJob where T: JobBase
    {
        public RandomDelayQuartzJobWrapper(IServiceProvider serviceProvider) : base(serviceProvider) { }

        protected override async Task Execute(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            
            var job = scope.ServiceProvider.GetRequiredService<RandomDelayJobScheduler<T>>();
            await job.Execute(stoppingToken);
        }
    }
}
