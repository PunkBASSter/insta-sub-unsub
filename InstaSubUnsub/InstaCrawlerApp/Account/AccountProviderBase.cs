using InstaCrawlerApp.Account.Interfaces;
using InstaDomain.Account;

namespace InstaCrawlerApp.Account
{
    public abstract class AccountProviderBase : IAccountUsageHistorySaver
    {
        protected InstaAccount? LastUsedAccount;

        public virtual InstaAccount Get()
        {
            LastUsedAccount ??= GetAccount();
            return LastUsedAccount;
        }

        protected abstract InstaAccount GetAccount();

        /// <summary>
        /// Saves history of the account returned by Get() method.
        /// </summary>
        /// <param name="lastEntitiesProcessed"></param>
        public virtual void SaveUsageHistory(int lastEntitiesProcessed, DateTime? antiBotDetectedTime)
        {
        }
    }
}
