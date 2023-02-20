using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InstaInfrastructureAbstractions
{
    public interface IContainerModule
    {
        void Register(IServiceCollection services, IConfiguration config);
    }
}
