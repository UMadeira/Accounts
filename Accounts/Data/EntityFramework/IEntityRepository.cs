using Accounts.Patterns.Repository;
using System.Linq;

namespace Accounts.Data.EntityFramework
{
    public interface IEntityRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Queryable();
    }
}
