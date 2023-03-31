using InstaCommon.Contracts;
using InstaCommon.Exceptions;
using InstaCrawlerApp.Account;
using InstaCrawlerApp.Account.Interfaces;
using InstaDomain.Account;
using InstaInfrastructureAbstractions;
using Microsoft.Extensions.Configuration;

namespace InstaCommon
{
    //Object is probably not thread-safe
    public class ServiceAccountPoolProvider<TConsumer> : AccountFromConfigProvider<TConsumer>,
        IAccountProvider<TConsumer> where TConsumer : class
    {
        private readonly AccountPool _accountPool;
        private readonly IPersistentCookieUtil _cookiesStorage;

        public ServiceAccountPoolProvider(AccountPool pool, IConfigurationSection section, IPersistentCookieUtil cookiesStorage)
            : base(section)
        {
            _accountPool = pool;
            _cookiesStorage = cookiesStorage;
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

            if (antiBotDetectedTime > default(DateTime))
                _cookiesStorage.DeleteCookies(LastUsedAccount.Username);

            var jobName = GetType().GetGenericArguments().First().Name;
            _accountPool.ReleaseAccountAndSaveUsageHistory(LastUsedAccount, jobName, lastEntitiesProcessed, antiBotDetectedTime);
        }
    }
}
