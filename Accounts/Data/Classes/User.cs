using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Classes
{
    class User : Item, IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
