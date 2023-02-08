using InstaCrawlerApp;

namespace InstaCrawlerServiceWindows
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly UserCrawler _userCrawler;

        public Worker(ILogger<Worker> logger, UserCrawler userCrawler)
        {
            _logger = logger;
            _userCrawler = userCrawler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _userCrawler.Crawl();
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}