using InstaInfrastructureAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using SeleniumPageObjects;
using SeleniumUtils;
using SeleniumUtils.PageObjects;

namespace InstaCrawlerApp
{
    public class ContainerModule : IContainerModule
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IWebDriver>(f => new WebDriverFactory().GetInstance());
            services.AddScoped<LoginPage>();
            services.AddScoped<FollowingPage>();
            services.AddScoped<IUserCrawler, UserCrawler>();
            services.AddScoped<Unfollower>();
            services.AddScoped(svcProvider => new CookieUtil(svcProvider.GetRequiredService<IWebDriver>(), config.GetRequiredSection("SavedCookiesPath")?.Value));
        }
    }
}
