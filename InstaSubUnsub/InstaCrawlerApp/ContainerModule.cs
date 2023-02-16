using InstaInfrastructureAbstractions;
using Microsoft.Extensions.DependencyInjection;
using SeleniumPageObjects;
using SeleniumUtils.PageObjects;

namespace InstaCrawlerApp
{
    public class ContainerModule : IContainerModule
    {
        public void Register(IServiceCollection services)
        {
            services.AddScoped(f => new WebDriverFactory().GetInstance());
            services.AddScoped<LoginPage>();
            services.AddScoped<FollowingPage>();
            services.AddScoped<IUserCrawler, UserCrawler>();
        }
    }
}
