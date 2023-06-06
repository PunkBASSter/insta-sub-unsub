using Domain.Account;
using InstaCommon;
using InstaCrawlerApp.Account;
using InstaCrawlerApp.Account.Interfaces;
using InstaCrawlerApp.Jobs;
using InstaCrawlerApp.Scheduling;
using InstaDomain.Account;
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
            services.AddScoped<InstaDbSyncher>();

            services.AddScoped<UserCrawler>();
            services.AddScoped<UserFullDetailsProvider>();

            services.AddScoped(typeof(BasicScheduler<>));
            services.AddScoped(typeof(MegaRandomJobScheduler<>));
            services.AddScoped(typeof(RandomDelayJobScheduler<>));

            //This is required to generate migrations as AccountUsageHistory entity does not have a default ctor.
            services.AddTransient<InstaAccount>(sp =>
                new ConfigurableInstaAccount(sp.GetRequiredService<IConfiguration>().GetRequiredSection("CrawlUser")));

            services.AddScoped<IAccountProvider<Follower>, AccountFromConfigProvider<Follower>>(sp =>
                new AccountFromConfigProvider<Follower>(
                    sp.GetRequiredService<IConfiguration>().GetRequiredSection("FollowUser")
                    )
                );

            services.AddScoped<IAccountProvider<Unfollower>, AccountFromConfigProvider<Unfollower>>(sp =>
                new AccountFromConfigProvider<Unfollower>(
                    sp.GetRequiredService<IConfiguration>().GetRequiredSection("FollowUser")
                    )
                );

            services.AddScoped<IAccountProvider<InstaDbSyncher>, AccountFromConfigProvider<InstaDbSyncher>>(sp =>
                new AccountFromConfigProvider<InstaDbSyncher>(
                    sp.GetRequiredService<IConfiguration>().GetRequiredSection("FollowUser")
                    )
                );

            services.AddScoped<IAccountProvider<UserCrawler>, ServiceAccountPoolProvider<UserCrawler>>(sp =>
                new ServiceAccountPoolProvider<UserCrawler>(
                    sp.GetRequiredService<AccountPool>(),
                    sp.GetRequiredService<IConfiguration>().GetRequiredSection("CrawlUser"),
                    sp.GetRequiredService<IPersistentCookieUtil>()
                    )
                );

            services.AddScoped<IAccountProvider<UserFullDetailsProvider>, ServiceAccountPoolProvider<UserFullDetailsProvider>>(sp =>
                new ServiceAccountPoolProvider<UserFullDetailsProvider>(
                    sp.GetRequiredService<AccountPool>(),
                    sp.GetRequiredService<IConfiguration>().GetRequiredSection("CrawlUser"),
                    sp.GetRequiredService<IPersistentCookieUtil>()
                    )
                );

            services.AddSingleton<AccountPool>();

            services.AddTransient<InitialAccountImporterFromConfig>();
        }
    }
}
