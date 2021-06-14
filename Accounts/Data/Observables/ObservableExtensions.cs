using Accounts.Patterns.Observer;
using System;

namespace Accounts.Data.Observables
{
    static class ObservableExtensions
    {
        public static void Subscribe<T>( this IItem self, EventHandler handler )
        {
            var observable = self as Patterns.Observer.IObservable;
            if (observable == null) return;

            observable.Notify += handler;
        }

        public static T GetSubject<T>( this IItem self ) where T : class, IItem
        {
            var observable = self as ObservableItem<T>;
            return observable?.Item;
        }
    }
}
