using Accounts.Patterns.Commands;

namespace Accounts.Data.Commands
{
    class CreateOrganization : ICommand
    {
        public CreateOrganization( IOrganization organization)
        {
            Organization = organization;
        }

        private IOrganization Organization { get; set; }

        public void Do()
        {
            Organization.Zombie = false;
        }

        public void Undo()
        {
            Organization.Zombie = true;
        }

        public void Redo() => Do();
        public void Cancel() { }
    }
}
