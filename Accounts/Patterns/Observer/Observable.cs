using System;

namespace Accounts.Patterns.Observer
{
    public class Observable : IObservable
    {
        public event EventHandler Notify;

        protected void InvokeNotify() => Notify?.Invoke( this, EventArgs.Empty );
        protected void InvokeNotify( EventArgs args ) => Notify?.Invoke( this, args );
        protected void InvokeNotity( object sender, EventArgs args ) => Notify?.Invoke( sender, args );
    }
}
