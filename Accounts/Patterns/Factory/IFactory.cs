using System;
using System.Collections.Generic;

namespace Accounts.Patterns.Factory
{
    public interface IFactory
    {
        IEnumerable<Type> Types { get; }

        T Create<T>( params object?[]? args );
    }
}
