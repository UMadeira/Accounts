namespace Accounts.Patterns.Commands
{
    class CommandDecorator : ICommand
    {
        public CommandDecorator(ICommand command)
        {
            Command = command;
        }

        protected ICommand Command { get; set; } 

        public virtual void Do()
        {
            Command.Do();
        }

        public virtual void Undo()
        {
            Command.Undo();
        }

        public virtual void Redo()
        {
            Command.Redo();
        }

        public virtual void Cancel()
        {
            Command.Cancel();
        }

        public override string ToString()
        {
            return Command.ToString();
        }
    }
}
