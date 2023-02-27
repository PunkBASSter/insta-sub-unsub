using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumPageObjects
{
    public class WebDriverFactory
    {
        public IWebDriver GetInstance()
        {
            var options = new ChromeOptions();
            options.AddArgument("--incognito");
            //DesiredCapabilities capabilities = DesiredCapabilities.chrome();
            //capabilities.setCapability(ChromeOptions.CAPABILITY, options);

            return new ChromeDriver("chromedriver.exe", options);
        }
    }
}
