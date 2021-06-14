using Accounts.Patterns.Factory;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace Accounts.Data.Observables
{
    class ObservableFactory : Factory, IFactory
    {
        public ObservableFactory()
        {
            Regist(typeof(ObservableUser));
            Regist(typeof(ObservableOrganization));
        }

        private IDictionary<object, object> Observables { get; }  = new Dictionary<object, object>();

        public T Create<T>( T subject ) //where T : IItem
        {
            if ( Observables.ContainsKey( subject ) )
                return Cast<T>(Observables[subject]);

            var observable = base.Create<T>( subject );
            Observables.Add( subject, observable );

            return observable;
        }

        public override T Create<T>( params object?[]? args )
        {
            System.Diagnostics.Trace.Assert( args.Length == 1 );
            return Cast<T>( Create( (T) args[0] ) );
        }
    }
}
