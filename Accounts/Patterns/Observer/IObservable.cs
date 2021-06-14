using System;

namespace Accounts.Patterns.Observer
{
    internal interface IObservable
    {
        event EventHandler Notify;
    }
}
