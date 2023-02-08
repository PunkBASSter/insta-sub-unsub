using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace SeleniumUtils.PageObjects
{
    public class FollowingPage : BasePage
    {
        public FollowingPage(IWebDriver driver) : base(driver)
        {
        }

        public override string Url => "https://www.instagram.com/{0}/following";

        protected override By LoadIndicatingElementLocator => By.XPath("//h1/div[text()=\"Ваши подписки\"]");

        public ReadOnlyCollection<IWebElement> FollowingItems => _driver.FindElements(By.XPath("//div[@aria-labelledby"));
    }
}
