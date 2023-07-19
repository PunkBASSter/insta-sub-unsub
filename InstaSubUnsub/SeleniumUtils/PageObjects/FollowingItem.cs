using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumUtils.Helpers;

namespace SeleniumUtils.PageObjects
{
    public class FollowingItem
    {
        private readonly IWebElement _element;
        private readonly IWebDriver _driver;
        private string? _userName;
        private string? _description;

        public FollowingItem(IWebElement element, IWebDriver driver)
        {
            _element = element;
            _driver = driver;
        }

        public string UserName
        {
            get
            {
                _userName ??= _element.FindElement(By.XPath(".//a[@role='link']")).GetAttribute("href").Split("/", StringSplitOptions.RemoveEmptyEntries).Last();
                return _userName;
            }
        }

        public string Description
        {
            get
            {
                _description ??= TryGetDescription();
                return _description;
            }
        }

        private string TryGetDescription()
        {
            try
            {
                return _element.FindElement(By.XPath(".//div/span/span[text()]")).Text;
            }
            catch(NoSuchElementException) { return string.Empty; }
        }

        public bool CheckHasStory()
        {
            try
            {
                var photoDiv = _element.FindElement(By.XPath(".//div[@role='button']"));
                var ariaDisabled = photoDiv.GetAttribute("aria-disabled").ToLowerInvariant() == "false";
                var styleCursor = photoDiv.GetAttribute("style").ToLowerInvariant().Contains("cursor: pointer;");
                return ariaDisabled && styleCursor;
            }
            catch(NoSuchElementException) { return false; }
            
        }

        public bool Unfollow()
        {
            new Scroll(_driver).IntoView($".//div//a[contains(@href,'{UserName}')]");

            if (TryGetBlueSubButton() != null)//button is already blue, so unfollowing is done
            {
                return true;
            }

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

            wait.Until(driver => TryGetGreySubButton() != null);
            var button = TryGetGreySubButton();
            if (button == null)
                return false;
            button.Click();

            wait.Until(driver => TryGetCancelSubButton() != null);
            var cancelSubButton = TryGetCancelSubButton();
            if (cancelSubButton == null) 
                return false;
            cancelSubButton.Click();

            wait.Until(driver => TryGetBlueSubButton() != null);
            var blueSubButton = TryGetBlueSubButton();
            if (blueSubButton == null)
                return false;
            var result = blueSubButton.Enabled && blueSubButton.Displayed;
            return result;
        }

        private IWebElement? TryGetGreySubButton()
        {
            try { return _element.FindElement(By.XPath(".//div/button/div/div[text()='Подписки']")); }
            catch { return null; };
        }

        private IWebElement? TryGetBlueSubButton()
        {
            try { return _element.FindElement(By.XPath(".//div/button/div/div[text()='Подписаться']")); }
            catch { return null; };
        }

        private IWebElement? TryGetCancelSubButton()
        {

            try { return _element.FindElement(By.XPath("//div[@role=\"dialog\"]//button[text()='Отменить подписку']")); }
            catch { return null; };
        }
    }
}
