using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumPageObjects;
using SeleniumUtils.PageObjects;

namespace InstaCrawlerApp
{
    public class ContainerModule
    {
        public void Register(IServiceCollection services)
        {
            services.AddSingleton<IWebDriver>(f => new WebDriverFactory().GetInstance());
            services.AddTransient<LoginPage>();
            services.AddTransient<UserCrawler>();
        }
    }
}
