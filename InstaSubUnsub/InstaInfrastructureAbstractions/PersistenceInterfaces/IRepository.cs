using InstaDomain;


namespace InstaInfrastructureAbstractions.PersistenceInterfaces
{
    public interface IRepository : IReadRepository
    {
        IQueryable<T> TrackedQuery<T>() where T : BaseEntity;
        long InsertOrUpdate<T>(T entity, Func<T, bool> conditionToMatch) where T : BaseEntity;
        long InsertOrSkip<T>(T entity, Func<T, bool> conditionToSkip) where T : BaseEntity;
        long Insert<T>(T entity) where T : BaseEntity;
        void Update<T>(T entity) where T : BaseEntity;
        void SaveChanges();
    }
}
