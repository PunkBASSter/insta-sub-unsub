using InstaInfrastructureAbstractions;
using InstaInfrastructureAbstractions.DataProviderInterfaces;
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
            services.AddTransient<IFollowingsProvider, InstaUiFollowingsProvider>(); //maybe useless
            services.AddTransient<IUserDetailsProvider, InstaUiUserDetailsProvider>();
            services.AddScoped(f => new WebDriverFactory().GetInstance());
        }
    }
}
