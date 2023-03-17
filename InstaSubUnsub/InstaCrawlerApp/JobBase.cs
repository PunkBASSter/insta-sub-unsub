namespace InstaCrawlerApp
{
    public abstract class JobBase
    {
        public abstract Task Execute(CancellationToken stoppingToken);
    }
}
