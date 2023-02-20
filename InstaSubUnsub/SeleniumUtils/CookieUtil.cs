using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumUtils
{
    public class CookieUtil
    {
        public static IEnumerable<Cookie> CookieStorageDummy = Enumerable.Empty<Cookie>();

        private readonly IWebDriver _driver;
        private readonly string _savedCookiesPath;
        
        public CookieUtil(IWebDriver driver, string? savedCookiesPath = null)
        {
            _driver = driver;
            _savedCookiesPath = savedCookiesPath ?? "SavedCookies";
        }

        public void SaveCookies()
        {
            CookieStorageDummy = _driver.Manage().Cookies.AllCookies;
        }

        public void LoadCookies()
        {
            _driver.Manage().Cookies.DeleteAllCookies();

        }
    }
}
