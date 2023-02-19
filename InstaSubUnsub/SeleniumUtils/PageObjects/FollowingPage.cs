using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace SeleniumUtils.PageObjects
{
    public class FollowingPage : BasePage
    {
        private int _followingItemsLoaded = 0;
        public FollowingPage(IWebDriver driver) : base(driver)
        {
        }

        public override string Url => "https://www.instagram.com/{0}/following";

        protected override By LoadIndicatingElementLocator => By.XPath("//h1/div[text()=\"Ваши подписки\"]");

        public IList<FollowingItem> FollowingItems { get; private set; } = new List<FollowingItem>();

        private ReadOnlyCollection<IWebElement> FollowingItemElements { get; set; }
            = new ReadOnlyCollection<IWebElement>(new List<IWebElement>());

        public IWebElement FollowingDialogScrollableArea => _driver.FindElement(By.XPath("//div[@role=\"dialog\"]//div[@role=\"tablist\"]/following-sibling::div"));

        public void ScrollToBottom()
        {
            _driver.ExecuteJavaScript(""""
                function getElementByXpath(path) {
                  return document.evaluate(path, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                }

                let scrollableArea = getElementByXpath("//div[@role='dialog']//div[@role='tablist']/following-sibling::div");
                scrollableArea.scroll(0, scrollableArea.scrollHeight);
                """");
        }

        public IList<FollowingItem> InfiniteScrollToBottomWithItemsLoading()
        {
            while (WaitForItems())
            {
                ScrollToBottom();
            }

            FollowingItems = FollowingItemElements.Select(i => new FollowingItem(i, _driver)).ToList();

            return FollowingItems;
        }

        private bool WaitForItems()
        {
            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
                wait.Until(CheckItemsLoaded());
                return true;
            }
            catch { return false; } //for some reason the condition is not satisfied
        }

        private Func<IWebDriver, bool> CheckItemsLoaded()
        {
            return (driver) =>
            {
                try
                {
                    FollowingItemElements = driver.FindElements(By.XPath("//div[@aria-labelledby]"));
                    var res = _followingItemsLoaded < FollowingItemElements.Count;
                    _followingItemsLoaded = FollowingItemElements.Count;
                    return res;
                }
                catch { return false; }
            };

        }
    }

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
