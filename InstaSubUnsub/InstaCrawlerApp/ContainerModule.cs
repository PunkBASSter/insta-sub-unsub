using Microsoft.Extensions.DependencyInjection;
using SeleniumPageObjects;
using SeleniumUtils.PageObjects;

namespace InstaCrawlerApp
{
    public class ContainerModule
    {
        public void Register(IServiceCollection services)
        {
            services.AddScoped(f => new WebDriverFactory().GetInstance());
            services.AddScoped<LoginPage>();
            services.AddScoped<FollowingPage>();
            services.AddScoped<IUserCrawler, UserCrawler>();
        }

        public void RegisterForServiceWorker(IServiceCollection services)
        {
            services.AddSingleton(f => new WebDriverFactory().GetInstance());
            services.AddSingleton<LoginPage>();
            services.AddSingleton<FollowingPage>();
            services.AddSingleton<IUserCrawler, UserCrawler>();
        }
    }
}
