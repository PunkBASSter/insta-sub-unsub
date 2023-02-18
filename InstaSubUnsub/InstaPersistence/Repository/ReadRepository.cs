using InstaInfrastructureAbstractions.PersistenceInterfaces;
using InstaDomain;

namespace InstaPersistence.Repository
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly InstaDbContext _dbContext;
        public ReadRepository(InstaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Query => _dbContext.Set<T>().AsQueryable();
    }
}
