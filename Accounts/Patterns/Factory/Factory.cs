using System;
using System.Collections.Generic;
using System.Linq;

namespace Accounts.Patterns.Factory
{
    class Factory : IFactory
    {
        private IList<Type> Types { get; } = new List<Type>();

        public void Regist(Type type)
        {
            if (Types.Contains(type)) return;
            Types.Add( type );
        }

        public virtual T Create<T>()
        {
            foreach ( var type in Types )
            {
                if ( ! type.GetInterfaces().Contains( typeof(T) ) ) continue;
                return Cast<T>(Activator.CreateInstance(type));
            }
            return Cast<T>(null);
        }

        public virtual T Create<T>( params object?[]? args )
        {
            foreach (var type in Types)
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
