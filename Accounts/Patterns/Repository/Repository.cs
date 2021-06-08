using System.Collections.Generic;
using System.Linq;

namespace Accounts.Patterns.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private IList<T> Entities { get; } = new List<T>();

        public IEnumerable<T> All => Entities;
        public IEnumerable<S> GetOfType<S>() where S : T => Entities.OfType<S>();

        public void Add( T entity )
        {
            if ( Entities.Contains( entity ) ) return;
            Entities.Add(entity);
        }

        public void Remove(T entity)
        {
            if ( ! Entities.Contains( entity ) ) return;
            Entities.Remove( entity );
        }

        public void Update(T entity)
        {
        }
    }
}
