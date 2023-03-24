using InstaCrawlerApp.Account.Interfaces;
using InstaDomain.Account;
using InstaInfrastructureAbstractions.PersistenceInterfaces;

namespace InstaCrawlerApp.Account
{
    public abstract class AccountProviderBase : IAccountUsageHistorySaver
    {
        protected readonly IRepository Repository;

        private InstaAccount? _lastUsedAccount;

        public AccountProviderBase(IRepository repo) 
        {
            Repository = repo;
        }

        public virtual InstaAccount Get()
        {
            _lastUsedAccount ??= GetAccount();
            return _lastUsedAccount;
        }

        protected abstract InstaAccount GetAccount();

        /// <summary>
        /// Saves history of the account returned by Get() method.
        /// </summary>
        /// <param name="lastEntitiesProcessed"></param>
        public virtual void SaveUsageHistory(int lastEntitiesProcessed)
        {
            _lastUsedAccount ??= Get();
            var history = new AccountUsageHistory 
            {
                Username = _lastUsedAccount.Username, 
                Password = _lastUsedAccount.Password,
                LastEntitiesProcessed = lastEntitiesProcessed,
                LastUsedTime = DateTime.UtcNow,
                LastUsedInService = GetType().GetGenericArguments().First().Name,
            };

            Repository.InsertOrUpdate(history, h => h.Username == history.Username);
            Repository.SaveChanges();
        }
    }
}
