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

        public ReadOnlyCollection<IWebElement> FollowingItems { get; private set; }
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

        public List<FollowingItem> GetCurrentFollowingItems()
        {
            WaitForItems();
            return FollowingItems.Select(i => new FollowingItem(i)).ToList();
        }

        public List<FollowingItem> InfiniteScrollToBottomWithItemsLoading()
        {
            while (WaitForItems())
            {
                ScrollToBottom();
            }

            return FollowingItems.Select(i => new FollowingItem(i)).ToList();
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
                    FollowingItems = driver.FindElements(By.XPath("//div[@aria-labelledby]"));
                    var res = _followingItemsLoaded < FollowingItems.Count;
                    _followingItemsLoaded = FollowingItems.Count;
                    return res;
                }
                catch { return false; }
            };
            
        }
    }

    public class FollowingItem
    {
        private readonly IWebElement _element;
        public FollowingItem(IWebElement element)
        {
            _element = element;
        }

        public string UserName => _element.FindElement(By.XPath("//div/span/a[@role=\"link\"]")).GetAttribute("href").Split("/", StringSplitOptions.RemoveEmptyEntries).Last();
    
        //Todo sub/unsub buttons
    }
}
