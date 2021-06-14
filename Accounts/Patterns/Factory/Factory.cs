using System;
using System.Collections.Generic;
using System.Linq;

namespace Accounts.Patterns.Factory
{
    class Factory : IFactory
    {
        private IList<Type> Constructs { get; } = new List<Type>();

        public IEnumerable<Type> Types => Constructs;

        public void Regist(Type type)
        {
            if ( Constructs.Contains(type)) return;
            Constructs.Add( type );
        }

        public virtual T Create<T>( params object?[]? args )
        {
            foreach (var type in Constructs )
            {
                if (!type.GetInterfaces().Contains(typeof(T))) continue;
                return Cast<T>(Activator.CreateInstance( type, args ) );
            }
            return Cast<T>(null);
        }

        public static T Cast<T>(object o)
        {
            return (T) o;
        }
    }
}
