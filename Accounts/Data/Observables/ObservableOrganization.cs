
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Observables
{
    class ObservableOrganization : ObservableItem, IOrganization
    {
        public ObservableOrganization(IOrganization organization) : base(organization)
        {
            Organization = organization;
        }

        private IOrganization Organization { get; }

        public string Name 
        { 
            get => Organization.Name; 
            set { Organization.Name = value; InvokeNotify(); } 
        }
        public ICollection<IUser> Users { get => Organization.Users; }
    }
}
