using InstaDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaCrawlerApp.PersistenceInterfaces
{
    public interface IRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        ValueTask<long> CreateAsync(T entity, CancellationToken cancellationToken);
    }
}
