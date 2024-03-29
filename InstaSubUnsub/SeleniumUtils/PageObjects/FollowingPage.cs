﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumUtils.Helpers;
using System.Collections.ObjectModel;

namespace SeleniumUtils.PageObjects
{
    public class FollowingPage : ProfilePage
    {
        private int _followingItemsLoaded = 0;
        public FollowingPage(IWebDriver driver, string username) : base(driver, username)
        {
        }

        public override string Url => "https://www.instagram.com/{0}/following";

        protected override By LoadIndicatingElementLocator { get; set; } = By.XPath("//div[@role=\"dialog\"]//div[@role=\"tablist\"]/following-sibling::div");

        public IList<FollowingItem> FollowingItems { get; private set; } = new List<FollowingItem>();

        private ReadOnlyCollection<IWebElement> FollowingItemElements { get; set; }
            = new ReadOnlyCollection<IWebElement>(new List<IWebElement>());

        public IList<FollowingItem> InfiniteScrollToBottomWithItemsLoading(int limit = 0)
        {
            while (WaitForItems())
            {
                if (limit > 0 && FollowingItemElements.Count >= limit)
                    break;
                //Followings scrollable area XPath
                new Scroll(_driver).ToBottom("//div[@role='dialog']//div[@role='tablist']/following-sibling::div");
            }

            FollowingItems = FollowingItemElements.Select(i => new FollowingItem(i, _driver)).ToList();

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
            catch { return false; } //for some reason the condition is not satisfied
        }

        private Func<IWebDriver, bool> CheckItemsLoaded()
        {
            return (driver) =>
            {
                try
                {
                    FollowingItemElements = driver
                        .FindElements(By.XPath("//div[@role='dialog']//div[@style='display: flex; flex-direction: column; padding-bottom: 0px; padding-top: 0px; position: relative;']/div/div/div/div"));
                    var res = _followingItemsLoaded < FollowingItemElements.Count;
                    _followingItemsLoaded = FollowingItemElements.Count;
                    return res;
                }
                catch { return false; }
            };

        }
    }
}
