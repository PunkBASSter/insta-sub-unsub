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

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var userCrawler = scope.ServiceProvider.GetRequiredService<IUserCrawler>();

                await Task.Run(userCrawler.Crawl);
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
