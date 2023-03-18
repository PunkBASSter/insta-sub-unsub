using InstaCrawlerApp;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace InstaJobs
{
    [DisallowConcurrentExecution]
    public sealed class QuartzJobWrapper<T> : IJob where T: JobBase
    {
        private readonly IServiceProvider _serviceProvider;

        public QuartzJobWrapper(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
        }

        private async Task Execute(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            
            var job = scope.ServiceProvider.GetRequiredService<T>();
            await job.Execute(stoppingToken);
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await Execute(context.CancellationToken);
        }
    }
}
