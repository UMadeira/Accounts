using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Commands
{
    class CommandManager : ICommandManager
    {
        #region Singleton
        private CommandManager() { }
        public static CommandManager Instance { get; } = new CommandManager();
        #endregion

        private int Position { get; set; } = -1;
        private List<ICommand> Commands { get; }  = new List<ICommand>();

        public event EventHandler Notify;

        public bool HasUndo()
        {
            return Position > -1;
        }

        public bool HasRedo()
        {
            return ( Position < Commands.Count - 1 );
        }

        public void Execute( ICommand command )
        {
            if (HasRedo())
            {
                Commands.RemoveRange( Position + 1, Commands.Count - Position - 1 );
            }

            command = new TraceCommandDecorator(command);
            Commands.Add( command  );
            command.Do();
            Position++;

            Notify?.Invoke( this, EventArgs.Empty );
        }

        public void Undo()
        {
            if ( ! HasUndo() ) return;

            Commands[Position].Undo();
            Position--;

            Notify?.Invoke(this, EventArgs.Empty);
        }

        public void Redo()
        {
            if ( ! HasRedo() ) return;

            Position++;
            Commands[ Position ].Redo();

            Notify?.Invoke(this, EventArgs.Empty);
        }
    }
}
