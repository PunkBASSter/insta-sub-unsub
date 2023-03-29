using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.Reflection;

namespace SeleniumPageObjects
{
    //Scoped factory
    public sealed class WebDriverFactory : IWebDriverFactory
    {
        private IWebDriver? _driverInstance;

        public void Dispose()
        {
            _driverInstance?.Dispose();
            GC.SuppressFinalize(this);
        }

        public IWebDriver GetInstance()
        {
            if (_driverInstance != null)
                return _driverInstance;

            var currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;

            var options = new ChromeOptions();
            options.AddArgument("--incognito");
            options.AddArgument("--start-maximized");

            var chromeDriverPath = Path.Combine(currentDir, "chromedriver.exe");
            if (File.Exists(chromeDriverPath))
            {
                _driverInstance = new ChromeDriver(chromeDriverPath, options);
                return _driverInstance;
            }

            var edgeConfig = new EdgeOptions();
            edgeConfig.AddArgument("--inPrivate");
            edgeConfig.AddArgument("--start-maximized");

            var edgeDriverPath = Path.Combine(currentDir, "msedgedriver.exe");
            if (File.Exists(edgeDriverPath))
            {
                _driverInstance = new EdgeDriver(edgeDriverPath, edgeConfig);
                return _driverInstance;
            }

            throw new NotImplementedException("Unable to find supported WebDriver EXE file.");
        }
    }
}
