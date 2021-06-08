using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Patterns.Observer
{
    internal interface IObservable
    {
        event EventHandler Notify;
    }
}
