using InstaCrawlerApp.Account.Interfaces;
using InstaDomain.Account;
using InstaInfrastructureAbstractions.PersistenceInterfaces;

namespace InstaCrawlerApp.Account
{
    public abstract class AccountProviderBase : IAccountUsageHistorySaver
    {
        protected readonly IRepository Repository;

        protected InstaAccount? LastUsedAccount;

        public AccountProviderBase(IRepository repo) 
        {
            Repository = repo;
        }

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
