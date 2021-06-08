using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Commands
{
    interface ICommand
    {
        void Do();
        void Undo();
        void Redo();
        void Cancel();
    }
}
