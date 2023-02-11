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
            services.AddDbContext<InstaDbContext>();
            services.AddTransient(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }

        public void RegisterForServiceWorker(IServiceCollection services)
        {
            services.AddDbContext<InstaDbContext>(ServiceLifetime.Singleton);
            services.AddTransient(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
