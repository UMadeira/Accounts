using Accounts.Patterns.Commands;

namespace Accounts.Data.Commands
{
    class AddOrganizationUser : ICommand
    {
        public AddOrganizationUser( IOrganization organization, IUser user )
        {
            Organization = organization;
            User = user;
        }

        private IOrganization Organization { get; }
        private IUser User { get; }

        public void Do()
        {
            Organization.Users.Add(User);
        }

        public void Undo()
        {
            Organization.Users.Remove(User);
        }

        public void Redo() => Do();
        public void Cancel() {}
    }
}
