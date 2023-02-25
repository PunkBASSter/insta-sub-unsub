using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace SeleniumUtils
{
    /// <summary>
    /// More lightweight and usable wrapper for Selenium WebDriverWait
    /// </summary>
    public class Wait
    {
        private readonly IWebDriver _driver;
        private readonly IWebElement? _element;

        public Wait(IWebDriver driver, IWebElement element) : this(driver)
        {
            _element= element;
        }

        public Wait(IWebDriver driver)
        {
            _driver = driver;
            _element = null;
        }

        public IWebElement? TryWaitForElement(By locator, int timeoutSec = 5)
        {
            try
            {
                return WaitForElement(locator, timeoutSec);

            }
            catch (WebDriverTimeoutException) { return null; }
        }

        public IWebElement WaitForElement(By locator, int timeoutSec = 8)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSec));
            IWebElement? result = default;
            wait.Until(d => TryFindElement(locator, out result));
#pragma warning disable CS8603
            return result; //Can't be null because of WebDriverTimeoutException thrown otherwise.
#pragma warning restore CS8603
        }

        public bool TryFindElement(By locator, out IWebElement? result)
        {
            try
            {
                if (_element is null)
                {
                    result = _driver.FindElement(locator);
                    return true;
                }
                
                result = _element.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException) 
            {
                result = null;
                return false; 
            }
            catch (StaleElementReferenceException)
            {
                result = null;
                return false;
            }
        }
    }
}
