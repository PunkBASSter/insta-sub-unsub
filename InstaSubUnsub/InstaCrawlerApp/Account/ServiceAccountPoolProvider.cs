using InstaCrawlerApp.Account;
using InstaDomain.Account;
using InstaInfrastructureAbstractions;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;

namespace InstaCommon
{
    public class ServiceAccountPoolProvider<TConsumer> : AccountFromConfigProvider<TConsumer>,
        IAccountProvider<TConsumer> where TConsumer : class
    {
        public ServiceAccountPoolProvider(IRepository repo, IConfigurationSection section) : base(repo, section)
        {
        }

        protected override InstaAccount GetAccount()
        {
            var acc = Repository.Query<AccountUsageHistory>().OrderBy(auh => auh.LastUsedTime).FirstOrDefault(auh =>
                auh.LastEntitiesProcessed == null || auh.LastEntitiesProcessed > 0);

            if (acc == null)
            {
                return base.GetAccount(); //Take an account from the config as a fallback
            }

            return acc;
        }
    }
}
