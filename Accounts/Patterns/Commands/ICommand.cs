namespace Accounts.Patterns.Commands
{
    interface ICommand
    {
        void Do();
        void Undo();
        void Redo();
        void Cancel();
    }
}
