using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumUtils.Helpers;
using System.Collections.ObjectModel;

namespace SeleniumUtils.PageObjects
{
    public class FollowersPage : ProfilePage
    {
        private int _followerItemsLoaded = 0;
        public FollowersPage(IWebDriver driver, string username) : base(driver, username)
        {
        }

        public override string Url => "https://www.instagram.com/{0}/followers";

        protected override By LoadIndicatingElementLocator { get; set; } = By.XPath("//div[@role='dialog']/div/div/following-sibling::div");

        public IList<FollowingItem> FollowingItems { get; private set; } = new List<FollowingItem>();

        private ReadOnlyCollection<IWebElement> FollowerItemElements { get; set; }
            = new ReadOnlyCollection<IWebElement>(new List<IWebElement>());

        public IList<FollowingItem> InfiniteScrollToBottomWithItemsLoading()
        {
            while (WaitForItems())
            {
                //followers dialog scrollable area
                new Scroll(_driver).ToBottom("//div[@role='dialog']//div[@role='dialog']/div/div/div/following-sibling::div");
            }

            FollowingItems = FollowerItemElements.Select(i => new FollowingItem(i, _driver)).ToList();

            return FollowingItems;
        }

        private bool WaitForItems()
        {
            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(8));
                wait.Until(CheckItemsLoaded());
                return true;
            }
            catch { return false; }
        }

        private Func<IWebDriver, bool> CheckItemsLoaded()
        {
            return (driver) =>
            {
                try
                {
                    FollowerItemElements = driver.FindElements(By.XPath("//div[@role='dialog']//div[contains(@style, 'overflow: hidden')]/div/div[@role='button']"));
                    var res = _followerItemsLoaded < FollowerItemElements.Count;
                    _followerItemsLoaded = FollowerItemElements.Count;
                    return res;
                }
                catch { return false; }
            };

        }
    }
}
