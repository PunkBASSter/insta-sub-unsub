using InstaDomain.Account;

namespace InstaCrawlerApp.Account.Interfaces
{
    public interface IAccountUsageHistorySaver
    {
        void SaveUsageHistory(int lastItemsProcessed, DateTime? antiBotDetectedTime);
    }
}
