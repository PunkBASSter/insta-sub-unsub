using InstaInfrastructureAbstractions.PersistenceInterfaces;
using InstaInfrastructureAbstractions;
using InstaPersistence.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace InstaPersistence
{
    public class ContainerModule : IContainerModule
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<InstaDbContext>();
            services.AddTransient(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
