using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace SeleniumPageObjects
{
    public class WebDriverFactory
    {
        public IWebDriver GetInstance()
        {
            var options = new ChromeOptions();
            options.AddArgument("--incognito");

            if (File.Exists("chromedriver.exe"))
                return new ChromeDriver("chromedriver.exe", options);

            var edgeConfig = new EdgeOptions();
            edgeConfig.AddAdditionalEdgeOption("InPrivate", true);

            if (File.Exists("msedgedriver.exe"))
                return new EdgeDriver("msedgedriver.exe", edgeConfig);

            throw new NotImplementedException("Unable to find supported WebDriver EXE file.");
        }
    }
}
