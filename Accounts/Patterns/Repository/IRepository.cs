using System.Collections.Generic;
using System.Linq;

namespace Accounts.Patterns.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Entities { get; }

        void Insert(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);

        IRepository<T> GetRepository<T>() where T : class;
    }
}
