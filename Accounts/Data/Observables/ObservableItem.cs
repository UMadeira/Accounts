using Accounts.Patterns.Observer;

namespace Accounts.Data.Observables
{
    internal class ObservableItem<T> : Observable, IItem, IObservable where T : class, IItem
    {
        public ObservableItem( T item )
        {
            Item = item;
        }

        public T Item { get; }

        public bool Zombie 
        { 
            get => Item.Zombie;
            set { Item.Zombie = value; InvokeNotify(); } 
        }

        public override bool Equals( object obj )
        {
            if ( obj is ObservableItem<T> observable ) return Equals( observable );
            if ( obj is IItem item ) return Equals( item );
            return Item.Equals( obj );
        }

        protected bool Equals( IItem other )
        {
            return Equals( Item, other );
        }

        protected bool Equals( ObservableItem<T> other )
        {
            return Equals( Item, other.Item );
        }

        public override int GetHashCode()
        {
            return ( Item?.GetHashCode() ?? 0 );
        }
    }
}
