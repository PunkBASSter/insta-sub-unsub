using InstaCommon.Exceptions;
using InstaDomain;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumUtils.PageObjects;

namespace SeleniumUtils.UiActions
{
    public class UiActionBase : ILoggedInUserState
    {
        protected readonly IWebDriver _webDriver;
        public string? LoggedInUsername { get; set; } = null;
        protected readonly ILogger<UiActionBase> _logger;

        public UiActionBase(IWebDriver driver, ILogger<UiActionBase> logger)
        {
            _webDriver = driver;
            _logger = logger;
        }

        protected virtual bool Login(InstaAccount account)
        {
            if (account.Username == LoggedInUsername)
                return true;

            if (!string.IsNullOrWhiteSpace(LoggedInUsername))
                //TODO implement re-logging as the required user
                return false;

            try
            {
                var loginPage = new LoginPage(_webDriver);
                loginPage.Load();
                loginPage.Login(account.Username, account.Password);
                LoggedInUsername = account.Username;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.Data.Values);
                LoggedInUsername = null;
                throw new LoginFailedException($"Login failed for user {account.Username}.");
            }
        }
    }
}
