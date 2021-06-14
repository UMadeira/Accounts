using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Accounts.Data.EntityFramework
{
    public static class EntityExtensions
    {
        public static IQueryable<T> GetDataSet<T>( this DbContext self, Type type ) where T : class
        {
            var method = self.GetType().GetMethod( "Set", new Type[] {} );
            return Cast<IQueryable<T>>( method?.MakeGenericMethod( type ).Invoke( self, null ) ); 
        }

        public static T Cast<T>( object o )
        {
            return (T) o;
        }
    }
}
