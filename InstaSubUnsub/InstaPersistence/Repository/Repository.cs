using InstaInfrastructureAbstractions.PersistenceInterfaces;
using InstaDomain;
using Microsoft.EntityFrameworkCore;

namespace InstaPersistence.Repository
{
    public class Repository : IRepository
    {
        private readonly InstaDbContext _dbContext;
        private readonly IReadRepository _readRepository;

        public Repository(InstaDbContext dbContext)
        {
            _dbContext = dbContext;
            _readRepository = new ReadRepository(dbContext);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public IQueryable<T> Query<T>() where T : BaseEntity
        {
            return _readRepository.Query<T>();
        }

        public virtual long Insert<T>(T entity) where T : BaseEntity
        {
            return _dbContext.Set<T>().Add(entity).Entity.Id;
        }

        public virtual void Delete<T>(long id) where T : BaseEntity
        {
            var entityToDelete = _dbContext.Set<T>().Find(id);
            if (entityToDelete != null)
                Delete(entityToDelete);
        }

        public virtual void Delete<T>(T entityToDelete) where T : BaseEntity
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbContext.Set<T>().Attach(entityToDelete);
            }
            _dbContext.Set<T>().Remove(entityToDelete);
        }

        public virtual void Update<T>(T entityToUpdate) where T : BaseEntity
        {
            _dbContext.Set<T>().Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual long InsertOrSkip<T>(T entity, Func<T, bool> conditionToSkip) where T : BaseEntity
        {
            var storedEntity = _dbContext.Set<T>().AsQueryable().FirstOrDefault(conditionToSkip);

            if (storedEntity is null)
            {
                return Insert(entity);
            }

            return storedEntity.Id;
        }

        public virtual long InsertOrUpdate<T>(T entity, Func<T, bool> conditionToMatch) where T : BaseEntity
        {
            var storedEntity = _dbContext.Set<T>().AsQueryable().FirstOrDefault(conditionToMatch);

            if (storedEntity is null)
            {
                return Insert(entity);
            }

            Update(entity);

            return storedEntity.Id;
        }
    }
}
