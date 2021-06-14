namespace Accounts.Data.Observables
{
    class ObservableUser : ObservableItem<IUser>, IUser
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
