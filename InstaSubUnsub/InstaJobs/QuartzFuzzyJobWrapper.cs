using InstaCrawlerApp;
using InstaCrawlerApp.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace InstaJobs
{
    [DisallowConcurrentExecution]
    internal class QuartzFuzzyJobWrapper<T> : QuartzJobWrapper<T> where T : JobBase
    {
        public QuartzFuzzyJobWrapper(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override async Task Execute(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var job = scope.ServiceProvider.GetRequiredService<T>();
            var fuzzyScheduledJob = new FuzzyJobScheduler<T>(job);
            await fuzzyScheduledJob.Execute(stoppingToken);
        }
    }
}
