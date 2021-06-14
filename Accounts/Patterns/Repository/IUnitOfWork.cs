using System;
using Accounts.Patterns.Factory;

namespace Accounts.Patterns.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IFactory Factory { get; }

        void Begin();
        void SaveChanges();
        void Commit();
        void Rollback();

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
