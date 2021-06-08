using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Accounts.Data.Classes
{
    internal class Organization : Item, IOrganization
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
        
        ICollection<IUser> IOrganization.Users { get => new List<IUser>(Users.Cast<IUser>()); }
    }
}
