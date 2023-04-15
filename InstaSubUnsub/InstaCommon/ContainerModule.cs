using InstaCommon.Config.Jobs;
using InstaCommon.Config.Limits;
using InstaInfrastructureAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InstaCommon
{
    public class ContainerModule : IContainerModule
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            //After-scope refreshable configuration
            services.AddScoped<UserCrawlerJobConfig>();
            services.AddScoped<UserFullDetailsProviderJobConfig>();
            services.AddScoped<FollowerJobConfig>();
            services.AddScoped<UnfollowerJobConfig>();
            services.AddScoped<SharedFollowUnfollowLimitConfig>();
        }
    }
}
