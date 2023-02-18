using InstaDomain;


namespace InstaInfrastructureAbstractions.PersistenceInterfaces
{
    public interface IRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        long InsertOrSkip(T entity, Func<T, bool> searchPredicate);
        void SaveChanges();
    }
}
