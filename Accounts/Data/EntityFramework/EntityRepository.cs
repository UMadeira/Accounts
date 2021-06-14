using Accounts.Patterns.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Accounts.Data.EntityFramework
{
    class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public EntityRepository( IUnitOfWork unitOfWork, DbContext context )
        {
            UnitOfWork  = unitOfWork;
            DataContext = context;

            var construct = UnitOfWork.Factory.Types.FirstOrDefault(
                type => type.GetInterfaces().Contains(typeof(TEntity)) );
            
            DataSet = DataContext.GetDataSet<TEntity>( construct );
        }

        private IUnitOfWork UnitOfWork  { get; }
        private DbContext   DataContext { get; }

        private IQueryable<TEntity> DataSet { get; }

        public IQueryable<TEntity> Entities => DataSet;

        public void Insert(TEntity entity)
        {
            DataContext.Add( entity );
            UnitOfWork.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            DataContext.Entry(entity).State = EntityState.Modified;
            UnitOfWork.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            DataContext.Remove(entity);
            UnitOfWork.SaveChanges();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return UnitOfWork.GetRepository<T>();
        }
    }
}
