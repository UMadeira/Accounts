using Accounts.Patterns.Commands;

namespace Accounts.Data.Commands
{
    class RemoveOrganizationUser : ICommand
    {
        public RemoveOrganizationUser( IOrganization organization, IUser user )
        {
            Organization = organization;
            User = user;
        }

        private IOrganization Organization { get; }
        private IUser User { get; }

        public void Do()
        {
            Organization.Users.Remove(User);
        }

        public void Undo()
        {
            Organization.Users.Add(User);
        }

        public void Redo() => Do();
        public void Cancel() {}
    }
}
