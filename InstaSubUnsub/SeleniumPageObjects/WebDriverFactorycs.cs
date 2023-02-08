using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumPageObjects
{
    public class WebDriverFactory
    {
        public IWebDriver GetInstance()
        {
            return new ChromeDriver("chromedriver.exe");
        }
    }
}
