using InstaInfrastructureAbstractions.PersistenceInterfaces;
using InstaDomain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InstaPersistence.Repository
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly InstaDbContext _dbContext;
        public ReadRepository(InstaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Query 
        { 
            get
            {
                var dbSetProperty = typeof(InstaDbContext)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                    .First(p => p.PropertyType.GetGenericArguments().First() == typeof(T));

                return (dbSetProperty.GetValue(_dbContext) as DbSet<T>)?.AsQueryable()
                    ?? Enumerable.Empty<T>().AsQueryable(); ;
            }
        }
    }
}
