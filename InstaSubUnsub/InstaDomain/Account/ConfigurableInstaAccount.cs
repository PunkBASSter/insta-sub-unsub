using InstaDomain.Account;
using Microsoft.Extensions.Configuration;

namespace Domain.Account
{
    public class ConfigurableInstaAccount : InstaAccount
    {

        public ConfigurableInstaAccount(IConfiguration config, string sectionName) : this(config.GetRequiredSection(sectionName))
        {
        }

        public ConfigurableInstaAccount(IConfigurationSection configSection) 
            : base(configSection?.GetRequiredSection("Username")?.Value, configSection?.GetRequiredSection("Password")?.Value)
        {
        }
    }
}
