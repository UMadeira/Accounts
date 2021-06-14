using Accounts.Data.Classes;
using Accounts.Patterns.Factory;

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
