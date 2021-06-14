using Accounts.Patterns.Commands;

namespace Accounts.Data.Commands
{
    class ChangeUsername : ICommand
    {
        public ChangeUsername( IUser user, string username)
        {
            User = user;
            Username = username;
        }

        private IUser User { get; set; }

        private string Username { get; set; }

        public void Do()
        {
            var username = User.Username;
            User.Username = Username;
            Username = username;
        }

        public void Redo() => Do();
        public void Undo() => Do();
        public void Cancel() { }
    }
}
