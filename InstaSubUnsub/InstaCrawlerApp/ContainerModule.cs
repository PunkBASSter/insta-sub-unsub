using InstaCrawlerApp.Jobs;
using InstaCrawlerApp.Scheduling;
using InstaInfrastructureAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InstaCrawlerApp
{
    public class ContainerModule : IContainerModule
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            //services.AddScoped<UserQuickDetailsProvider>(); potentially useless

            services.AddScoped<Follower>();
            services.AddScoped<Unfollower>();

            services.AddScoped<UserCrawler>();
            services.AddScoped<UserFullDetailsProvider>();

            services.AddScoped(typeof(MegaRandomJobScheduler<>));
            services.AddScoped(typeof(RandomDelayJobScheduler<>));
        }
    }
}
