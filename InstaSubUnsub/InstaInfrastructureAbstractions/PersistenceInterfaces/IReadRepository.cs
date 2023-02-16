using InstaDomain;

namespace InstaInfrastructureAbstractions.PersistenceInterfaces
{
    public interface IReadRepository<T> where T : BaseEntity
    {
        IQueryable<T> Query { get; }
    }
}
