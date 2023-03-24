using Domain.Account;
using InstaDomain.Account;
using InstaInfrastructureAbstractions;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;

namespace InstaCrawlerApp.Account
{
    public class AccountFromConfigProvider<TConsumer> : AccountProviderBase, IAccountProvider<TConsumer> where TConsumer : class
    {
        private readonly IConfigurationSection _section;

        public AccountFromConfigProvider(IRepository repo, IConfigurationSection section) : base(repo)
        {
            _section = section;
        }

        protected override InstaAccount GetAccount()
        {
            return new ConfigurableInstaAccount(_section);
        }
    }
}
