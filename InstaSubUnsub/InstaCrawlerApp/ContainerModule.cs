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
            services.AddScoped(f => new WebDriverFactory().GetInstance());
            services.AddScoped<LoginPage>();
            services.AddScoped<FollowingPage>();
            services.AddScoped<FollowersPage>();
            services.AddScoped<ProfilePage>();
            services.AddScoped<UserCrawler>()
                .AddTransient(sp => new Account(sp.GetRequiredService<IConfiguration>(), "CrawlUser"));
            services.AddScoped<UserFullDetailsProvider>()
                .AddTransient(sp => new Account(sp.GetRequiredService<IConfiguration>(), "CrawlUser"));
            services.AddScoped<UserQuickDetailsProvider>();
            services.AddScoped<Unfollower>()
                .AddTransient(sp => new Account(sp.GetRequiredService<IConfiguration>(), "UnfollowUser"));
            services.AddScoped(svcProvider => new CookieUtil(svcProvider.GetRequiredService<IWebDriver>(), config.GetRequiredSection("SavedCookiesPath")?.Value));
        }
    }
}
