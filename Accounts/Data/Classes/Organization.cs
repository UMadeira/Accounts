using Accounts.Patterns.Mappers;
using System.Collections.Generic;
using System.Linq;

namespace Accounts.Data.Classes
{
    internal class Organization : Item, IOrganization
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
        
        ICollection<IUser> IOrganization.Users => new CollectionMapper<User,IUser>( Users );
    }
}
