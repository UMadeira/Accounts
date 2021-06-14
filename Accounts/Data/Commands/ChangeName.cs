using Accounts.Patterns.Commands;

namespace Accounts.Data.Commands
{
    class ChangeName : ICommand
    {
        public ChangeName( IOrganization organization, string name )
        {
            Organization = organization;
            Name = name;
        }

        private IOrganization Organization { get; set; }

        private string Name { get; set; }

        public void Do()
        {
            var name = Organization.Name;
            Organization.Name = Name;
            Name = name;
        }

        public void Redo() => Do();
        public void Undo() => Do();
        public void Cancel() { }
    }
}
