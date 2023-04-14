using InstaDomain.Account;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumPageObjects;
using SeleniumUtils.PageObjects;
using System.Reflection;

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

        protected virtual UiActionResult InvokeWithScreenshotOnError(Func<UiActionResult> func)
        {
            try
            {
                return func();
            }
            catch (WebDriverException ex) // assumption: it's a base for other selenium exceptions
            {
                var ss = ((ITakesScreenshot)_lazyDriver.Value).GetScreenshot();
                var pathFromConf = _configuration.GetRequiredSection("WebDriver:ScreenshotsPath").Value;
                var dirName = string.IsNullOrWhiteSpace(pathFromConf) 
                    ? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                    : pathFromConf;

                var fileName = Path.Combine(dirName ?? string.Empty, $"ScreenShot_{DateTime.UtcNow:yy-MM-dd_hh-mm}.png");
                ss.SaveAsFile(fileName, ScreenshotImageFormat.Png);
                _logger.LogError(ex, ex.Message, ex.Data.Values);
                _logger.LogError("Screenshot saved. Available here: {0}.", fileName);

                throw;
            }
        }
    }
}
