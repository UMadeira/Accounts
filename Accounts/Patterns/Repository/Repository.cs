using System.Collections.Generic;
using System.Linq;

namespace Accounts.Patterns.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork    UnitOfWork { get; }
        private IList<TEntity> Elements   { get; } = new List<TEntity>();

        public IQueryable<TEntity> Entities => Elements.AsQueryable<TEntity>();

        public void Insert( TEntity entity )
        {
            if ( Entities.Contains( entity ) ) return;
            Elements.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            if ( ! Entities.Contains( entity ) ) return;
            Elements.Remove( entity );
        }

        public void Update(TEntity entity)
        {
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return UnitOfWork.GetRepository<T>();
        }
    }
}
