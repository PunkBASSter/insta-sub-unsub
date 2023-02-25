using InstaInfrastructureAbstractions.PersistenceInterfaces;
using InstaDomain;
using Microsoft.EntityFrameworkCore;

namespace InstaPersistence.Repository
{
    public class ReadRepository : IReadRepository
    {
        private readonly InstaDbContext _dbContext;
        public ReadRepository(InstaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Exposed IQueryable without EF change tracking.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> Query<T>() where T : BaseEntity
        {
            return _dbContext.Set<T>().AsQueryable().AsNoTracking();
        }
    }
}
