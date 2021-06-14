using Accounts.Patterns.Commands;

namespace Accounts.Data.Commands
{
    class DeleteItem : ICommand
    {
        public DeleteItem( IItem item )
        {
            Item = item;
        }

        private IItem Item { get; set; }

        public void Do()
        {
            Item.Zombie = false;
        }

        public void Undo()
        {
            Item.Zombie = true;
        }

        public void Redo() => Do();
        public void Cancel() { }
    }
}
