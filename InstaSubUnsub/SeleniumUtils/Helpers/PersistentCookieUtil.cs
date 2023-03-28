using InstaCommon.Contracts;
using InstaInfrastructureAbstractions;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using SeleniumPageObjects;

namespace SeleniumUtils.Helpers
{
    /// <summary>
    /// Manages cookies save/load for a single browser instance (supports only 1 Instagram user).
    /// </summary>
    public class PersistentCookieUtil
    {
        private readonly Lazy<IWebDriver> _lazyDriver;
        private IWebDriver _driver => _lazyDriver.Value;
        private readonly string? _cookieKey;
        private readonly IKeyValueObjectStorage<InstaCookies> _cookieStorage;

        public PersistentCookieUtil(IWebDriverFactory driverFactory, IConfiguration conf, IKeyValueObjectStorage<InstaCookies> cookieStorage)
        {
            _lazyDriver = new Lazy<IWebDriver>(() => driverFactory.GetInstance());
            _cookieKey = conf.GetRequiredSection("SavedCookiesPath").Value;
            _cookieStorage = cookieStorage;
        }

        public void SaveCookies(string username)
        {
            ValidateKey(username);

            var cookies = new InstaCookies(_driver.Manage().Cookies.AllCookies.Select(c => new DeserializeableCookie(c)));
#pragma warning disable CS8604 //validation will fail if null
            _cookieStorage.Write(_cookieKey + username, cookies);
#pragma warning restore CS8604
        }

        public bool LoadCookies(string username)
        {
            ValidateKey(username);

            _driver.Manage().Cookies.DeleteAllCookies();
            var loadedCookies = _cookieStorage.Read(_cookieKey + username) ?? new List<DeserializeableCookie>();
            loadedCookies
                .ForEach(_driver.Manage().Cookies.AddCookie);

            return loadedCookies.Count > 0;
        }

        private void ValidateKey(string username)
        {
            if (string.IsNullOrWhiteSpace(_cookieKey + username))
                throw new ArgumentNullException("No value assigned for SavedCookiePath parameter.");
        }
    }
}
