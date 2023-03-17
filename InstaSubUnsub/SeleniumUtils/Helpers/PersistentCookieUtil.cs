using InstaCommon;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System.Text.Json.Serialization;

namespace SeleniumUtils.Helpers
{
    /// <summary>
    /// Manages cookies save/load for a single browser instance (supports only 1 Instagram user).
    /// </summary>
    public class PersistentCookieUtil
    {
        private readonly IWebDriver _driver;
        private readonly string? _savedCookiesPath;

        public PersistentCookieUtil(IWebDriver driver, IConfiguration conf)
        {
            _driver = driver;
            _savedCookiesPath = conf.GetRequiredSection("SavedCookiesPath").Value;
        }

        public void SaveCookies() //TODO Maybe validate cookies for expiration ?
        {
            ValidatePath();

            var cookies = _driver.Manage().Cookies.AllCookies.ToList();
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            JsonFileIO.Write(_savedCookiesPath, cookies);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
        }

        public bool LoadCookies()
        {
            ValidatePath();

            if (!File.Exists(_savedCookiesPath))
                return false;

            _driver.Manage().Cookies.DeleteAllCookies();
            var loadedCookies = JsonFileIO.Read<List<DeserializeableCookie>>(_savedCookiesPath) ?? new List<DeserializeableCookie>();
            loadedCookies
                .ForEach(_driver.Manage().Cookies.AddCookie);

            return loadedCookies.Count > 0;
        }

        private void ValidatePath() 
        {
            if (_savedCookiesPath is null)
                throw new ArgumentNullException("No value assigned for SavedCookiePath parameter.");
        }
    }

    internal class DeserializeableCookie : Cookie
    {
        [JsonConstructor]
        public DeserializeableCookie(string name, string value,
            string domain, string path, DateTime? expiry, bool secure, bool isHttpOnly, string sameSite) :
            base(name, value, domain, path, expiry, secure, isHttpOnly, sameSite)
        { }
    }
}
