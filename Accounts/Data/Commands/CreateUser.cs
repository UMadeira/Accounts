using Accounts.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Commands
{
    class CreateUser : ICommand
    {
        public CreateUser( IUser user)
        {
            User = user;
        }

        private IUser User { get; set; }

        public void Do()
        {
            User.Zoombie = false;
        }

        public void Undo()
        {
            User.Zoombie = true;
        }

        public void Redo() => Do();
        public void Cancel() { }
    }
}
