using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Patterns.Observer
{
    class ObservableCollection<T> : ICollection<T>, IObservable
    {
        public ObservableCollection( ICollection<T> collection )
        {
            Collection = collection;
        }

        public event EventHandler Notify;

        private ICollection<T> Collection { get; }

        public int Count => Collection.Count;
        public bool IsReadOnly => Collection.IsReadOnly;
        public bool Contains(T item) => Collection.Contains(item);
        public IEnumerator<T> GetEnumerator() => Collection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Collection.GetEnumerator();

        public void Add(T item)
        {
            Collection.Add(item);
            Notify?.Invoke( this, EventArgs.Empty );
        }

        public bool Remove(T item)
        {
            var result = Collection.Remove(item);
            Notify?.Invoke(this, EventArgs.Empty);
            return result;
        }

        public void Clear()
        {
            Collection.Clear();
            Notify?.Invoke(this, EventArgs.Empty);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Collection.CopyTo( array, arrayIndex );
            Notify?.Invoke(this, EventArgs.Empty);
        }
    }
}
