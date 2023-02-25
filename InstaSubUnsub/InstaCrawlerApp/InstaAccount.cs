using Microsoft.Extensions.Configuration;

namespace InstaCrawlerApp
{
    public class Account
    {
        private readonly string _username;
        private readonly string _password;

        public Account(IConfiguration config, string sectionName) : this(config.GetRequiredSection(sectionName))
        {
        }

        public Account(IConfigurationSection configSection) 
            : this(configSection?.GetRequiredSection("Username")?.Value, configSection?.GetRequiredSection("Password")?.Value)
        {
        }

        public Account(string? username, string? password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Missing username or password for Account object creation.");

            _username = username;
            _password = password;
        }

        public string Username => _username;
        public string Password => _password;
    }
}
