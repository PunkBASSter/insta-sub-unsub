using InstaDomain;
using InstaInfrastructureAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InstaCrawlerApp
{
    public class ContainerModule : IContainerModule
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<UserCrawler>()
                .AddTransient<InstaAccount>(sp => new ConfigurableInstaAccount(sp.GetRequiredService<IConfiguration>(), "CrawlUser"));
            services.AddScoped<UserFullDetailsProvider>()
                .AddTransient<InstaAccount>(sp => new ConfigurableInstaAccount(sp.GetRequiredService<IConfiguration>(), "CrawlUser"));
            services.AddScoped<UserQuickDetailsProvider>();
            services.AddScoped<Unfollower>()
                .AddTransient<InstaAccount>(sp => new ConfigurableInstaAccount(sp.GetRequiredService<IConfiguration>(), "FollowUser"));
        }
    }
}
