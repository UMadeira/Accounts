using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Observables
{
    class ObservableUser : ObservableItem, IUser
    {
        public ObservableUser( IUser user ) : base( user )
        {
            User = user;
        }

        private IUser User { get; }

        public string Username 
        { 
            get => User.Username;
            set { User.Username = value; InvokeNotify(); } 
        }

        public string Password
        { 
            get => User.Password;
            set { User.Password = value; InvokeNotify(); }
        }
    }
}
