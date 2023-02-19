using InstaDomain;


namespace InstaInfrastructureAbstractions.PersistenceInterfaces
{
    public interface IRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        long InsertOrSkip(T entity, Func<T, bool> conditionToSkip);
        void Update(T entity);
        void SaveChanges();
    }
}
