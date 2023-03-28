using InstaCommon.Contracts;
using InstaInfrastructureAbstractions;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using SeleniumPageObjects;
using SeleniumUtils.Helpers;
using SeleniumUtils.UiActions;

namespace SeleniumUtils
{
    public class ContainerModule : IContainerModule
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IFollowersProvider, InstaUiFollowersProvider>();
            services.AddTransient<IUserFollower, InstaUiUserFollower>();
            services.AddTransient<IUserUnfollower, InstaUiUserUnfollower>();
            services.AddTransient<IFollowingsProvider, InstaUiFollowingsProvider>();
            services.AddTransient<IUserDetailsProvider, InstaUiUserDetailsProvider>();
            services.AddScoped<IWebDriverFactory>(f => new WebDriverFactory());
            
            services.AddScoped<PersistentCookieUtil>();
        }
    }
}
