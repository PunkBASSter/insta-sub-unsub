using InstaCommon.Exceptions;
using InstaCrawlerApp.Account;
using InstaCrawlerApp.Account.Interfaces;
using InstaDomain.Account;
using Microsoft.Extensions.Configuration;

namespace InstaCommon
{
    //Object is probably not thread-safe
    public class ServiceAccountPoolProvider<TConsumer> : AccountFromConfigProvider<TConsumer>,
        IAccountProvider<TConsumer> where TConsumer : class
    {

        private readonly AccountPool _accountPool;
        
        public ServiceAccountPoolProvider(AccountPool pool, IConfigurationSection section) : base(section)
        {
            _accountPool = pool;
        }

        protected override InstaAccount GetAccount()
        {
            var acc = _accountPool.GetLeastRecentAccount();
            if (acc == null)
            {
                throw new InstaAntiBotException("Unable to provide a free account which is not blocked or throttled.");
                //return base.GetAccount(); //Take an account from the config as a fallback
            }

            return acc;
        }

        public override void SaveUsageHistory(int lastEntitiesProcessed, DateTime? antiBotDetectedTime)
        {
            if (LastUsedAccount == null)
                return;

            _accountPool.ReleaseAccountAndSaveUsageHistory(LastUsedAccount, lastEntitiesProcessed, antiBotDetectedTime);
        }
    }
}
