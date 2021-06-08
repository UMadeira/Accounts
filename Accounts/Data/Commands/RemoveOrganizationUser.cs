using Accounts.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Commands
{
    class RemoveOrganizationUser : ICommand
    {
        public RemoveOrganizationUser( IOrganization organization, IUser user )
        {
            Organization = organization;
            User = user;
        }

        private IOrganization Organization { get; }
        private IUser User { get; }

        public void Do()
        {
            Organization.Users.Remove(User);
        }

        public void Undo()
        {
            Organization.Users.Add(User);
        }

        public void Redo() => Do();
        public void Cancel() {}
    }
}
