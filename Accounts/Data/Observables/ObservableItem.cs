using Accounts.Patterns.Observer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Observables
{
    internal class ObservableItem : Observable, IItem, IObservable
    {
        public ObservableItem(IItem item)
        {
            Item = item;
        }

        private IItem Item { get; }

        public bool Zoombie 
        { 
            get => Item.Zoombie;
            set { Item.Zoombie = value; InvokeNotify(); } 
        }
    }
}
