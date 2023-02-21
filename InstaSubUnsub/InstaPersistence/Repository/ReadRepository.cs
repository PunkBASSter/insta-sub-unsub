using InstaInfrastructureAbstractions.PersistenceInterfaces;
using InstaDomain;

namespace InstaPersistence.Repository
{
    public class ReadRepository : IReadRepository
    {
        private readonly InstaDbContext _dbContext;
        public ReadRepository(InstaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Query<T>() where T : BaseEntity
        {
            return _dbContext.Set<T>().AsQueryable();
        }
    }
}
