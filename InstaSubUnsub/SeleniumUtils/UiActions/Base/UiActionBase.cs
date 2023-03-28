using InstaDomain.Account;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumPageObjects;
using SeleniumUtils.PageObjects;

namespace SeleniumUtils.UiActions.Base
{
    public abstract class UiActionBase : ILoggedInUserState
    {
        private readonly Lazy<IWebDriver> _lazyDriver;
        protected IWebDriver WebDriver => _lazyDriver.Value;

        public string? LoggedInUsername { get; set; } = null;
        protected readonly ILogger<UiActionBase> _logger;
        protected readonly IConfiguration _configuration;

        public UiActionBase(IWebDriverFactory driverFactory, ILogger<UiActionBase> logger, IConfiguration configuration)
        {
            _lazyDriver = new Lazy<IWebDriver>(() => driverFactory.GetInstance());
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Performs login via UI
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public virtual bool Login(InstaAccount account)
        {
            if (account.Username == LoggedInUsername)
                return true;

            if (!string.IsNullOrWhiteSpace(LoggedInUsername))
                //TODO implement re-logging as the required user
                return false;

            var loginPage = new LoginPage(WebDriver);
            loginPage.Load();
            loginPage.Login(account.Username, account.Password);
            LoggedInUsername = account.Username;

            return true;
        }
    }
}
