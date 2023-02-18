using InstaInfrastructureAbstractions.PersistenceInterfaces;
using InstaDomain;
using Microsoft.EntityFrameworkCore;

namespace InstaPersistence.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly InstaDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        private readonly IReadRepository<T> _readRepository;

        public Repository(InstaDbContext dbContext)
        {
            _dbContext = dbContext;
            _readRepository = new ReadRepository<T>(dbContext);
            _dbSet = _dbContext.Set<T>();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public IQueryable<T> Query => _readRepository.Query;

        public virtual long Insert(T entity)
        {
            return _dbSet.Add(entity).Entity.Id;
        }

        public virtual void Delete(long id)
        {
            var entityToDelete = _dbSet.Find(id);
            if (entityToDelete != null)
                Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual long InsertOrSkip(T entity, Func<T, bool> searchPredicate)
        {
            var storedEntity = _dbSet.Find(searchPredicate);

            if (storedEntity is null)
            {
                return Insert(entity);
            }

            return storedEntity.Id;
        }
    }
}
