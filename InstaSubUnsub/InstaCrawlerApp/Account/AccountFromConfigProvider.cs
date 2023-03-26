using Domain.Account;
using InstaCrawlerApp.Account.Interfaces;
using InstaDomain.Account;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;

namespace InstaCrawlerApp.Account
{
    public class AccountFromConfigProvider<TConsumer> : AccountProviderBase, IAccountProvider<TConsumer> where TConsumer : class
    {
        private readonly IConfigurationSection _section;

        public AccountFromConfigProvider(IConfigurationSection section)
        {
            _section = section;
        }

        protected override InstaAccount GetAccount()
        {
            return new ConfigurableInstaAccount(_section);
        }
    }
}
