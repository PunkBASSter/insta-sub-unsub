using InstaCrawlerApp;

namespace InstaCrawlerServiceWindows
{
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
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    _userCrawler.Crawl();
            //    await Task.Delay(10000, stoppingToken);
            //}

            _logger.LogInformation(
                $"{nameof(ScopedWorker)} is running.");

            await DoWorkAsync(stoppingToken);
        }

        private async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(ScopedWorker)} is working.");

            //using (IServiceScope scope = _serviceProvider.CreateScope())
            //{
            //    //var userCrawler = scope.ServiceProvider.GetRequiredService<IUserCrawler>();
            //    //await Task.Run(userCrawler.Crawl);

            //    var unfollower = scope.ServiceProvider.GetRequiredService<Unfollower>();
            //    await Task.Run(unfollower.Unfollow);
            //}

            //using (IServiceScope scope = _serviceProvider.CreateScope())
            //{
            //    var crawler = scope.ServiceProvider.GetRequiredService<UserCrawler>();
            //    await Task.Run(crawler.Crawl);
            //}

            //using (IServiceScope scope = _serviceProvider.CreateScope())
            //{
            //    var detailsProvider = scope.ServiceProvider.GetRequiredService<UserFullDetailsProvider>();
            //    await Task.Run(detailsProvider.ProvideUserDetails);
            //}

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var quickDetailsProvider = scope.ServiceProvider.GetRequiredService<UserQuickDetailsProvider>();
                await Task.Run(quickDetailsProvider.AnonymousProvideDetails);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(ScopedWorker)} is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
