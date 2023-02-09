using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumUtils.PageObjects
{
    public abstract class BasePage
    {
        protected IWebDriver _driver;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public virtual void Load(params string[] urlParams)
        {
            _driver.Navigate().GoToUrl(string.Format(Url, urlParams));
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            wait.Until(ElementIsVisible(LoadIndicatingElementLocator));
        }

        /// <summary>
        /// An element on the page by which we can judge if the page is loaded or not.
        /// </summary>
        protected abstract By LoadIndicatingElementLocator { get; }

        public virtual string Url { get; } = "https://www.instagram.com";

        public static Func<IWebDriver, bool> ElementIsVisible(By elementLocator)
        {
            return (driver) =>
            {
                try
                {
                    var element = driver.FindElement(elementLocator);
                    return element.Displayed;
                }
                catch (Exception)
                {
                    // If element is null, stale or if it cannot be located
                    return false;
                }
            };
        }
    }
}
