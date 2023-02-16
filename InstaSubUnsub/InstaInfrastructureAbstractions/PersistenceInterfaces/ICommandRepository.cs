using InstaDomain;


namespace InstaInfrastructureAbstractions.PersistenceInterfaces
{
    public interface IRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        ValueTask<long> CreateAsync(T entity, CancellationToken cancellationToken);
    }
}
