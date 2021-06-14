namespace Accounts.Patterns.Commands
{
    interface ICommandManager
    {
        bool HasUndo();
        bool HasRedo();

        void Execute(ICommand command);
        void Undo();
        void Redo();
    }
}
