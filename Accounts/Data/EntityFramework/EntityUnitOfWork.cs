using System;
using Accounts.Patterns.Factory;
using Accounts.Patterns.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Accounts.Data.EntityFramework
{
    class EntityUnitOfWork : IUnitOfWork
    {
        public EntityUnitOfWork( DbContext context, IFactory factory )
        {
            Context = context;
            Factory = factory;
        }

        public DbContext Context { get; }
        public IFactory  Factory { get; }


        private IDbContextTransaction Transaction { get; set; }

        public void Begin()
        {
            Transaction = Context.Database.BeginTransaction();
        }
        
        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void Commit()
        {
            Transaction?.Commit();
            Transaction?.Dispose();
            Transaction = null;
        }

        public void Rollback()
        {
            Transaction?.Rollback();
            Transaction?.Dispose();
            Transaction = null;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new EntityRepository<TEntity>( this, Context );
        }

        public void Dispose()
        {
            Context?.Dispose();
            Transaction?.Dispose();
        }
    }
}
