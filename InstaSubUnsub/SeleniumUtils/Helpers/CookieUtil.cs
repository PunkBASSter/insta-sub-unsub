using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumUtils.Helpers
{
    public class CookieUtil
    {
        //todo complete saving according to https://stackoverflow.com/questions/6115721/how-to-save-restore-serializable-object-to-from-file

        private readonly IWebDriver _driver;
        private readonly string _savedCookiesPath;

        public CookieUtil(IWebDriver driver, string? savedCookiesPath = null)
        {
            _driver = driver;
            _savedCookiesPath = savedCookiesPath ?? $"SavedCookies_{GetHashCode()}"; //??? if hash is a valid approach
        }

        public void SaveCookies()
        {
            var cookies = _driver.Manage().Cookies.AllCookies;
        }

        public void LoadCookies()
        {
            _driver.Manage().Cookies.DeleteAllCookies();

        }
    }
}
