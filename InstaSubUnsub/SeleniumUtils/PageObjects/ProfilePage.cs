using OpenQA.Selenium;

namespace SeleniumUtils.PageObjects
{
    internal class ProfilePage : BasePage
    {
        public ProfilePage(IWebDriver driver) : base(driver)
        {
        }

        

        protected override By LoadIndicatingElementLocator => By.XPath("");
    }
}
