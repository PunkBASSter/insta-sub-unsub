using InstaDomain.Account;

namespace InstaInfrastructureAbstractions
{
    public interface IAccountUsageHistorySaver
    {
        void SaveUsageHistory(int lastItemsProcessed);
    }
}
