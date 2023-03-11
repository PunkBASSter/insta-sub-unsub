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
            var currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;

            var options = new ChromeOptions();
            options.AddArgument("--incognito");

            var chromeDriverPath = Path.Combine(currentDir, "chromedriver.exe");
            if (File.Exists(chromeDriverPath))
                return new ChromeDriver(chromeDriverPath, options);

            var edgeConfig = new EdgeOptions();
            edgeConfig.AddArgument("--inPrivate");

            var edgeDriverPath = Path.Combine(currentDir, "msedgedriver.exe");
            if (File.Exists(edgeDriverPath))
                return new EdgeDriver(edgeDriverPath, edgeConfig);

            throw new NotImplementedException("Unable to find supported WebDriver EXE file.");
        }
    }
}
