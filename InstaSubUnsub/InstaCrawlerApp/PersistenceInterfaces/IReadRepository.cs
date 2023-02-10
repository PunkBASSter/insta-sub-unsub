using InstaDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaCrawlerApp.PersistenceInterfaces
{
    public interface IReadRepository<T> where T : BaseEntity
    {
        IQueryable<T> Query { get; }
    }
}
