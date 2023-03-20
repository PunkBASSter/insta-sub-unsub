using InstaCrawlerApp.Jobs;

namespace InstaCrawlerServiceWindows
{
    [Obsolete("Since Quartz.Net was introduced it replaced this class with its own ServiceHost.")]
    public sealed class ScopedWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ScopedWorker> _logger;

        public ScopedWorker(
            IServiceProvider serviceProvider,
            ILogger<ScopedWorker> logger) =>
            (_serviceProvider, _logger) = (serviceProvider, logger);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(ScopedWorker)} is running.");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await DoWorkAsync(stoppingToken);
                await Task.Delay(1000000, stoppingToken);
            }
        }

        private async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(ScopedWorker)} is working.");

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var userCrawler = scope.ServiceProvider.GetRequiredService<UserCrawler>();
                //await Task.Run(userCrawler.Crawl, stoppingToken);
            }

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var detailsProvider = scope.ServiceProvider.GetRequiredService<UserFullDetailsProvider>();
                //await Task.Run(detailsProvider.ProvideDetails, stoppingToken);
            }

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var detailsProvider = scope.ServiceProvider.GetRequiredService<Follower>();
                //await Task.Run(detailsProvider.Follow, stoppingToken);
            }

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var detailsProvider = scope.ServiceProvider.GetRequiredService<Unfollower>();
                //await Task.Run(detailsProvider.Unfollow, stoppingToken);
            }

            //using (IServiceScope scope = _serviceProvider.CreateScope())
            //{
            //    var quickDetailsProvider = scope.ServiceProvider.GetRequiredService<UserQuickDetailsProvider>();
            //    await Task.Run(quickDetailsProvider.ProvideDetails);
            //}
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(ScopedWorker)} is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
