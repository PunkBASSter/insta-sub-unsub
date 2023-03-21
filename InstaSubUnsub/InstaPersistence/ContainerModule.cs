using InstaInfrastructureAbstractions.PersistenceInterfaces;
using InstaInfrastructureAbstractions;
using InstaPersistence.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using InstaCommon.Contracts;
using InstaCommon;

namespace InstaPersistence
{
    public class ContainerModule : IContainerModule
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            var pgConnectionString = config.GetRequiredSection("PgConnectionString").Value;
            services.AddDbContext<InstaDbContext>(options =>
            {
                options.UseNpgsql(pgConnectionString, bldr =>
                {
                    bldr.MigrationsAssembly(typeof(InstaDbContext).Assembly.GetName().Name);
                });
            });
            services.AddTransient(typeof(IReadRepository), typeof(ReadRepository));
            services.AddTransient(typeof(IRepository), typeof(Repository.Repository));
            services.AddScoped<IKeyValueObjectStorage<InstaCookies>, JsonDbStorage<InstaCookies>>();
        }
    }
}
