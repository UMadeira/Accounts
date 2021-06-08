using Accounts.Patterns.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data
{
    class DataFactory : Factory
    {
        public DataFactory()
        {
            Regist( typeof(User) );
            Regist( typeof(Organization) );
        }
    }
}
