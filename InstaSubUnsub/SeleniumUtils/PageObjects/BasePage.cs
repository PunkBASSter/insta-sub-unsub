using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumUtils.Exceptions;

namespace SeleniumUtils.PageObjects
{
    public abstract class BasePage
    {
        protected IWebDriver _driver;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public virtual bool Load(params string[] urlParams)
        {
            var pageUrl = string.Format(Url, urlParams);
            var attempts = 3;

            do
            {
                attempts--;
                try
                {
                    _driver.Navigate().GoToUrl(pageUrl); //sometimes does not work for the first time
                    HandlePageLoading();
                }
                catch (WebDriverException) { }

                if (HandleLoadErrors())
                    return true;
                    
            }
            while (!ElementIsVisible(LoadIndicatingElementLocator)(_driver) && attempts > 0);

            return ElementIsVisible(LoadIndicatingElementLocator)(_driver);
        }

        protected virtual void HandlePageLoading()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 6));
            wait.Until(ElementIsVisible(LoadIndicatingElementLocator));
        }

        protected virtual bool HandleLoadErrors() { return true; }

        /// <summary>
        /// An element on the page by which we can judge if the page is loaded or not.
        /// </summary>
        protected abstract By LoadIndicatingElementLocator { get; set; }

        protected virtual By LoadErrorElementLocator
            => By.XPath("//body//main//*[contains(text(),'К сожалению, эта страница недоступна.')]");

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
