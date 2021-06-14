using System.Diagnostics;

namespace Accounts.Patterns.Commands
{
    class TraceCommandDecorator : CommandDecorator
    {
        public TraceCommandDecorator(ICommand command) : base(command)
        {
        }

        public override void Do()
        {
            Trace.WriteLine($"{ Command.ToString() }.Do()");
            base.Do();
        }
        public override void Undo()
        {
            Trace.WriteLine($"{ Command.ToString() }.Undo()");
            base.Undo();
        }
        public override void Redo()
        {
            Trace.WriteLine($"{ Command.ToString() }.Redo()");
            base.Redo();
        }
        public override void Cancel()
        {
            Trace.WriteLine($"{ Command.ToString() }.Cancel()");
            base.Cancel();
        }
    }
}
