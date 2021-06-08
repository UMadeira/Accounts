using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Patterns.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> All { get; }

        IEnumerable<S> GetOfType<S>() where S : T;

        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
