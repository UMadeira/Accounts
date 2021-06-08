using Accounts.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Accounts.UI.Commands
{
    class AddNode : ICommand
    {
        public AddNode(TreeNodeCollection nodes, TreeNode node)
        {
            Nodes = nodes;
            Node  = node;
        }

        private TreeNodeCollection Nodes { get; set; }
        private TreeNode Node { get; set; }

        public void Do()
        {
            Nodes.Add( Node );
        }

        public void Undo()
        {
            Nodes.Remove(Node);
        }


        public void Redo() => Do();
        public void Cancel() { }
    }
}
