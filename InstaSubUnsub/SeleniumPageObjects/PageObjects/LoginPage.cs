using OpenQA.Selenium;

namespace SeleniumUtils.PageObjects
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        protected override By LoadIndicatingElementLocator => By.XPath("//button[@type=\"submit\"]");

        public IWebElement UserNameBox => _driver.FindElement(By.XPath("//input[@name=\"username\"]"));
        public IWebElement PasswordBox => _driver.FindElement(By.XPath("//input[@name=\"password\"]"));

        public void Login(string username, string password)
        {
            UserNameBox.SendKeys(username);
            PasswordBox.SendKeys(password);
            PasswordBox.SendKeys(Keys.Enter);
        }

        public override string Url => "https://www.instagram.com/accounts/login/?source=auth_switcher";
    }
}