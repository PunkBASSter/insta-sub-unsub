using InstaCrawlerApp.PersistenceInterfaces;
using InstaPersistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InstaPersistence
{
    public class ContainerModule
    {
        public void Register(IServiceCollection services)
        {
            services.AddDbContext<InstaDbContext>(options =>
                //TODO extract configuration to JSON config
                //options.UseNpgsql(Configuration.GetConnectionString("BloggingContext"))
                options.UseNpgsql("Host=localhost;Database=insta_subs;Username=insta_service;Password=insta_service"),
                ServiceLifetime.Singleton);
            services.AddTransient(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
