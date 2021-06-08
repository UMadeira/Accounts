using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Classes
{
    class Organization : Item, IOrganization
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
