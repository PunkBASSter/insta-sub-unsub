using InstaCrawlerApp.Account;
using InstaCrawlerApp.Account.Interfaces;
using InstaDomain.Account;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;

namespace InstaCommon
{
    //Object is probably not thread-safe
    public class ServiceAccountPoolProvider<TConsumer> : AccountFromConfigProvider<TConsumer>,
        IAccountProvider<TConsumer>, IDisposable where TConsumer : class
    {
        private static readonly HashSet<string> BusyAccounts = new HashSet<string>();
        private static readonly object Lock = new();
        private InstaAccount? _accountInUse;

        public ServiceAccountPoolProvider(IRepository repo, IConfigurationSection section) : base(repo, section)
        {
        }

        protected override InstaAccount GetAccount()
        {
            var acc = GetFreeLastUsedAccount();
            if (acc == null)
            {
                return base.GetAccount(); //Take an account from the config as a fallback
            }

            return acc;
        }

        private InstaAccount? GetFreeLastUsedAccount()
        {
            if (_accountInUse != null)
                return _accountInUse;

            lock(Lock) 
            {
                _accountInUse = Repository.Query<AccountUsageHistory>()
                    .Where(auh => !BusyAccounts.Contains(auh.Username))
                    .OrderBy(auh => auh.LastUsedTime)
                    .FirstOrDefault(auh =>
                        auh.AntiBotDetectedTime == null || auh.AntiBotDetectedTime > DateTime.UtcNow.AddHours(-24));
                
                if (_accountInUse != null)
                {
                    BusyAccounts.Add(_accountInUse.Username);
                }
            }

            return _accountInUse;
        }

        public void Dispose()
        {
            if (_accountInUse != null)
            lock (Lock)
            {
                BusyAccounts.Remove(_accountInUse.Username);
                _accountInUse = null;
            }
        }

        ~ServiceAccountPoolProvider() 
        {
            Dispose();
        }

    }
}
