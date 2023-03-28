using OpenQA.Selenium;

namespace SeleniumPageObjects
{
    public interface IWebDriverFactory : IDisposable
    {
        IWebDriver GetInstance();
    }
}