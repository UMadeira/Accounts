using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Accounts.Commands
{
    class MacroCommand : ICommand
    {
        private List<ICommand> Commands { get; } = new List<ICommand>();

        public bool IsEmpty => Commands.Count() == 0;

        public void Add( ICommand command )
        {
            Commands.Add( command );
        }

        public void Do()
        {
            foreach ( var command in Commands )
                command.Do();
        }

        public void Undo()
        {
            Commands.Reverse();
            foreach (var command in Commands )
                command.Undo();
            Commands.Reverse();
        }

        public void Redo() => Do();
        public void Cancel() { }

        public override string ToString()
        {
            var names = Commands.Select( cmd => cmd.ToString() );
            return $"macro( { string.Join( ", ", names ) } )";
        }
    }
}
