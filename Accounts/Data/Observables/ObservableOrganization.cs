
using Accounts.Patterns.Observer;
using System.Collections.Generic;

namespace Accounts.Data.Observables
{
    class ObservableOrganization : ObservableItem, IOrganization
    {
        public ObservableOrganization(IOrganization organization) : base(organization)
        {
            Organization = organization;

            var users = new ObservableCollection<IUser>( organization.Users );
            users.Notify += ( sender, args ) => InvokeNotify( args );
            Users = users;
        }

        private IOrganization Organization { get; }
        
        public string Name 
        { 
            get => Organization.Name; 
            set { Organization.Name = value; InvokeNotify(); } 
        }

        public ICollection<IUser> Users { get; }
    }
}
