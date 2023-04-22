using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using SeleniumUtils;
using System.Reflection;

namespace SeleniumPageObjects
{
    //Scoped factory
    public sealed class WebDriverFactory : IWebDriverFactory
    {
        private readonly IConfiguration _configuration;
        private IWebDriver? _driverInstance;
        private readonly BrowserType _browserType;
        private readonly bool _privateMode = true;

        public WebDriverFactory(IConfiguration conf)
        { 
            _configuration = conf;
            var browserStr = conf.GetRequiredSection("WebDriver:Browser").Value;

            if (!Enum.TryParse(browserStr, true, out _browserType))
                _browserType = BrowserType.Chrome;

            _privateMode = Convert.ToBoolean(conf.GetRequiredSection("WebDriver:PrivateMode").Value);
        }

        public void Dispose()
        {
            _driverInstance?.Dispose();
            GC.SuppressFinalize(this);
        }

        private ChromeOptions GetChromeOptions() 
        {
            var options = new ChromeOptions();
            if (_privateMode)
                options.AddArgument("--incognito");
            options.AddArgument("--start-maximized");
            return options;
        }

        private IWebDriver? GetChromeDriver(string driversDir)
        {
            var chromeDriverPath = Path.Combine(driversDir, "chromedriver.exe");
            if (File.Exists(chromeDriverPath))
            {
                return new ChromeDriver(chromeDriverPath, GetChromeOptions());
            }

            return null;
        }

        private EdgeOptions GetEdgeOptions() 
        {
            var edgeConfig = new EdgeOptions();
            if (_privateMode)
                edgeConfig.AddArgument("--inPrivate");
            edgeConfig.AddArgument("--start-maximized");
            return edgeConfig;
        }

        private IWebDriver? GetEdgeDriver(string driversPath)
        {
            var edgeDriverPath = Path.Combine(driversPath, "msedgedriver.exe");
            if (File.Exists(edgeDriverPath))
            {
                return new EdgeDriver(edgeDriverPath, GetEdgeOptions());
            }

            return null;
        }

        public IWebDriver GetInstance()
        {
            if (_driverInstance != null)
                return _driverInstance;

            var currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;

            switch (_browserType)
            {
                case BrowserType.Chrome: _driverInstance = GetChromeDriver(currentDir); break;
                case BrowserType.Edge: _driverInstance = GetEdgeDriver(currentDir); break;
                default:
                    _driverInstance ??= GetChromeDriver(currentDir) ?? GetEdgeDriver(currentDir);
                    break;
            }

            if (_driverInstance == null)
                throw new NotImplementedException("Unable to find supported WebDriver EXE file.");

            return _driverInstance;
        }
    }
}
