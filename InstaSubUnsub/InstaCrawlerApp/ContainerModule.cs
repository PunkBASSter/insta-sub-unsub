using InstaCommon;
using InstaCrawlerApp.Account;
using InstaCrawlerApp.Jobs;
using InstaCrawlerApp.Scheduling;
using InstaInfrastructureAbstractions;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
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

            services.AddScoped<IAccountProvider<Follower>, AccountFromConfigProvider<Follower>>(sp =>
                new AccountFromConfigProvider<Follower>(
                    sp.GetRequiredService<IRepository>(),
                    sp.GetRequiredService<IConfiguration>().GetRequiredSection("FollowUser")
                    )
                );

            services.AddScoped<IAccountProvider<Unfollower>, AccountFromConfigProvider<Unfollower>>(sp =>
                new AccountFromConfigProvider<Unfollower>(
                    sp.GetRequiredService<IRepository>(),
                    sp.GetRequiredService<IConfiguration>().GetRequiredSection("FollowUser")
                    )
                );

            services.AddScoped<IAccountProvider<UserCrawler>, ServiceAccountPoolProvider<UserCrawler>>(sp =>
                new ServiceAccountPoolProvider<UserCrawler>(
                    sp.GetRequiredService<IRepository>(),
                    sp.GetRequiredService<IConfiguration>().GetRequiredSection("CrawlUser")
                    )
                );

            services.AddScoped<IAccountProvider<UserFullDetailsProvider>, ServiceAccountPoolProvider<UserFullDetailsProvider>>(sp =>
                new ServiceAccountPoolProvider<UserFullDetailsProvider>(
                    sp.GetRequiredService<IRepository>(),
                    sp.GetRequiredService<IConfiguration>().GetRequiredSection("CrawlUser")
                    )
                );
        }
    }
}
