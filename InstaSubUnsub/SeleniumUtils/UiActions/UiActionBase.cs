using InstaDomain;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumUtils.PageObjects;

namespace SeleniumUtils.UiActions
{
    public class UiActionBase
    {
        protected readonly IWebDriver _webDriver;
        protected string? _loggedInUser = null;
        protected readonly ILogger<UiActionBase> _logger;

        public UiActionBase(IWebDriver driver, ILogger<UiActionBase> logger)
        {
            _webDriver = driver;
            _logger = logger;
        }

        protected virtual bool Login(InstaAccount account)
        {
            try
            {
                var loginPage = new LoginPage(_webDriver);
                loginPage.Load();
                loginPage.Login(account.Username, account.Password);
                _loggedInUser = account.Username;
                return true;
            }
            catch
            {
                _loggedInUser = null;
                return false;
            }
        }
    }
}
