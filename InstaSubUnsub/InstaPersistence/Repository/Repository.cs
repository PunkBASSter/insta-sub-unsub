using InstaInfrastructureAbstractions.PersistenceInterfaces;
using InstaDomain;

namespace InstaPersistence.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly InstaDbContext _dbContext;
        private readonly IReadRepository<T> _readRepository;

        public Repository(InstaDbContext dbContext)
        {
            _dbContext = dbContext;
            _readRepository = new ReadRepository<T>(dbContext);
        }

        public IQueryable<T> Query => _readRepository.Query;

        public async ValueTask<long> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            var res = await _dbContext.AddAsync(entity, cancellationToken);
            _dbContext.SaveChanges();
            return res.Entity.Id;
        }

        public async Task CreateAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await _dbContext.AddRangeAsync(entities, cancellationToken);
            _dbContext.SaveChanges();
        }
    }
}
