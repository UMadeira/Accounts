using Accounts.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Commands
{
    class CreateOrganization : ICommand
    {
        public CreateOrganization( IOrganization organization)
        {
            Organization = organization;
        }

        private IOrganization Organization { get; set; }

        public void Do()
        {
            Organization.Zoombie = false;
        }

        public void Undo()
        {
            Organization.Zoombie = true;
        }

        public void Redo() => Do();
        public void Cancel() { }
    }
}
