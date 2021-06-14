using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Accounts.Patterns.Mappers
{
    class CollectionMapper<TClass,TInterface> : ICollection<TInterface> where TClass : TInterface
    {
        public CollectionMapper( ICollection<TClass> collection )
        {
            Collection = collection;
        }

        private ICollection<TClass> Collection { get; }

        public int Count       => Collection.Count;
        public bool IsReadOnly => Collection.IsReadOnly;

        public bool Contains(TInterface item) => Collection.Contains((TClass) item);

        public IEnumerator<TInterface> GetEnumerator() => Collection.Cast<TInterface>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Collection.GetEnumerator();

        public void Add(TInterface item)
        {
            Collection.Add( (TClass) item);
        }

        public bool Remove(TInterface item)
        {
            return Collection.Remove( (TClass) item );
        }

        public void Clear()
        {
            Collection.Clear();
        }

        public void CopyTo( TInterface[] array, int arrayIndex)
        {
            Collection.CopyTo( array.Cast<TClass>().ToArray(), arrayIndex );
        }
    }
}
