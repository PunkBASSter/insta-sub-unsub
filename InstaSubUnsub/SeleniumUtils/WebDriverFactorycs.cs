using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.Reflection;

namespace SeleniumPageObjects
{
    public class WebDriverFactory
    {
        public IWebDriver GetInstance()
        {
            var currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var options = new ChromeOptions();
            options.AddArgument("--incognito");

            if (File.Exists(Path.Combine(currentDir, "chromedriver.exe")))
                return new ChromeDriver("chromedriver.exe", options);

            var edgeConfig = new EdgeOptions();
            edgeConfig.AddArgument("--inPrivate");

            if (File.Exists(Path.Combine(currentDir, "msedgedriver.exe")))
                return new EdgeDriver("msedgedriver.exe", edgeConfig);

            throw new NotImplementedException("Unable to find supported WebDriver EXE file.");
        }
    }
}
