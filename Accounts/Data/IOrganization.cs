using System.Collections.Generic;

namespace Accounts.Data
{
    interface IOrganization : IItem
    {
        string Name { get; set; }
        ICollection<User> Users { get; set; }
    }
}