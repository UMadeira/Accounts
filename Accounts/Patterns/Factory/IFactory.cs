using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Patterns.Factory
{
    public interface IFactory
    {
        T Create<T>();
    }
}
