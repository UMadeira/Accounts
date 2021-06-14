﻿using Accounts.Patterns.Commands;

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
            User.Zombie = false;
        }

        public void Undo()
        {
            User.Zombie = true;
        }

        public void Redo() => Do();
        public void Cancel() { }
    }
}
