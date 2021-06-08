using Accounts.Patterns.Observer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Observables
{
    static class ObservableExtensions
    {
        public static void Subscribe( this IItem self, EventHandler handler )
        {
            var observable = self as IObservable;
            if (observable == null) return;

            observable.Notify += handler;
        }
    }
}
