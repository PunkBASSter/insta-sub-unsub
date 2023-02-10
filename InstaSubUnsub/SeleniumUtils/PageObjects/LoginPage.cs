using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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
            SlowSendKeys(UserNameBox, username);
            SlowSendKeys(PasswordBox, password);
            
            //UserNameBox.SendKeys(username);
            //PasswordBox.SendKeys(password);
            PasswordBox.SendKeys(Keys.Enter);
        }

        public void HandleAfrerLoginQuestions()
        {
            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                wait.Until(FindAndClickLoginSaveDataPopup());
            }
            catch { } //we don't care if it's not existing
        }

        private Func<IWebDriver, bool> FindAndClickLoginSaveDataPopup()
        {
            return (driver) =>
            {
                try
                {
                    var element = _driver.FindElement(By.XPath("""//button[text()="Сохранить данные"]"""));
                    if (element.Displayed && element.Enabled)
                        element.Click();
                    return true;
                }
                catch (NoSuchElementException) { return false; }
            };
            
        }

        public override string Url => "https://www.instagram.com/accounts/login/?source=auth_switcher";

        //TODO extract to extension methods
        public void SlowSendKeys(IWebElement target, string str) 
        {
            var delay = new Delay(200,600);
            foreach(var c in str)
            {
                delay.Random(() => target.SendKeys(c.ToString()));
            }
        }
    }
}