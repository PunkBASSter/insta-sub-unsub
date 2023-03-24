using InstaDomain.Account;

namespace InstaCrawlerApp.Account.Interfaces
{
    public interface IAccountProvider<out TConsumer> : IAccountUsageHistorySaver where TConsumer : class
    {
        InstaAccount Get();
    }
}
