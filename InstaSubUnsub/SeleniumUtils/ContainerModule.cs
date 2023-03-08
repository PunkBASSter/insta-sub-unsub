using InstaInfrastructureAbstractions;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeleniumPageObjects;
using SeleniumUtils.UiActions;

namespace SeleniumUtils
{
    public class ContainerModule : IContainerModule
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IFollowersProvider, InstaUiFollowersProvider>();
            services.AddTransient<IUserFollower, InstaUiUserFollower>();
            //services.AddTransient<IFollowingsProvider, InstaUiFollowingsProvider>(); //currently useless
            services.AddTransient<IUserDetailsProvider, InstaUiUserDetailsProvider>();
            services.AddScoped(f => new WebDriverFactory().GetInstance());
        }
    }
}
