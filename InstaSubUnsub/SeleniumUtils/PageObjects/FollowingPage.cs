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

        protected override By LoadIndicatingElementLocator { get; set; } = By.XPath("//div[@role=\"dialog\"]//div[@role=\"tablist\"]/following-sibling::div");

        public IList<FollowingItem> FollowingItems { get; private set; } = new List<FollowingItem>();

        private ReadOnlyCollection<IWebElement> FollowingItemElements { get; set; }
            = new ReadOnlyCollection<IWebElement>(new List<IWebElement>());

        public IWebElement FollowingDialogScrollableArea => _driver.FindElement(By.XPath("//div[@role=\"dialog\"]//div[@role=\"tablist\"]/following-sibling::div"));

        public IList<FollowingItem> InfiniteScrollToBottomWithItemsLoading()
        {
            while (WaitForItems())
            {
                new Scroll(_driver).ToBottom("//div[@role='dialog']//div[@role='tablist']/following-sibling::div");
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
}
