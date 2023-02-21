using InstaDomain;

namespace InstaInfrastructureAbstractions.PersistenceInterfaces
{
    public interface IReadRepository 
    {
        IQueryable<T> Query<T>() where T : BaseEntity;
    }
}
