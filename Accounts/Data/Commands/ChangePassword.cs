using Accounts.Patterns.Commands;

namespace Accounts.Data.Commands
{
    class ChangePassword : ICommand
    {
        public ChangePassword( IUser user, string password )
        {
            User = user;
            Password = password;
        }

        private IUser User { get; set; }

        private string Password { get; set; }

        public void Do()
        {
            var password = User.Password;
            User.Password = Password;
            Password = password;
        }

        public void Redo() => Do();
        public void Undo() => Do();
        public void Cancel() { }
    }
}
