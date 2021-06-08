using Accounts.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Accounts.UI.Commands
{
    class RemoveNode : ICommand
    {
        public RemoveNode(TreeNodeCollection nodes, TreeNode node)
        {
            Nodes = nodes;
            Node  = node;
        }

        private TreeNodeCollection Nodes { get; set; }
        private TreeNode Node { get; set; }

        public void Do()
        {
            Nodes.Remove(Node);
        }

        public void Undo()
        {
            Nodes.Add(Node);
        }

        public void Redo() => Do();
        public void Cancel() { }
    }
}
