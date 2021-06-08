using Accounts.Patterns.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Observables
{
    class ObservableFactory : Factory, IFactory
    {
        public ObservableFactory()
        {
            Regist(typeof(ObservableUser));
            Regist(typeof(ObservableOrganization));
        }

        private IFactory BaseFactory { get; } = new DataFactory(); 

        public override T Create<T>()
        {
            var item = BaseFactory.Create<T>();
            return base.Create<T>(item);
        }
    }
}
