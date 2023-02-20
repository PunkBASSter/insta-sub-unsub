using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace SeleniumUtils.PageObjects
{
    public class FollowingItem
    {
        private readonly IWebElement _element;
        private readonly IWebDriver _driver;
        private string? _userName;
        private bool _following = true;

        public FollowingItem(IWebElement element, IWebDriver driver)
        {
            _element = element;
            _driver = driver;
        }

        public void ScrollIntoView()
        {
            //finding the JS element by XPath with a username as a profile link part and scrolling it into view
            _driver.ExecuteJavaScript($$"""
                function getElementByXpath(path) { return document.evaluate(path, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue; }

                let element = getElementByXpath("//div/span/a[contains(@href,'{{UserName}}')]");
                element.scrollIntoView()
                """);
        }

        public string UserName
        {
            get
            {
                _userName ??= _element.FindElement(By.XPath(".//div/span/a[@role=\"link\"]")).GetAttribute("href").Split("/", StringSplitOptions.RemoveEmptyEntries).Last();
                return _userName;
            }
        }

        public bool Unfollow()
        {
            ScrollIntoView();
            if (TryGetBlueSubButton() != null)//button is already blue, so unfollowing is done
            {
                _following = false;
                return true;
            }

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

            wait.Until(driver => TryGetGreySubButton() != null);
            var button = TryGetGreySubButton();
            button.Click();

            wait.Until(driver => TryGetCancelSubButton() != null);
            var cancelSubButton = TryGetCancelSubButton();
            cancelSubButton.Click();

            wait.Until(driver => TryGetBlueSubButton() != null);
            var blueSubButton = TryGetBlueSubButton();
            var result = blueSubButton.Enabled && blueSubButton.Displayed;
            _following = !result;
            return result;
        }

        private IWebElement? TryGetGreySubButton()
        {
            try { return _element.FindElement(By.XPath(".//div/button/div/div[text()=\"Подписки\"]")); }
            catch { return null; };
        }

        private IWebElement? TryGetBlueSubButton()
        {
            try { return _element.FindElement(By.XPath(".//div/button/div/div[text()=\"Подписаться\"]")); }
            catch { return null; };
        }

        private IWebElement? TryGetCancelSubButton()
        {

            try { return _element.FindElement(By.XPath("//div[@role=\"dialog\"]//button[text()=\"Отменить подписку\"]")); }
            catch { return null; };
        }
    }
}
