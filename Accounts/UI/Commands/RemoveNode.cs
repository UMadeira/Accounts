using Accounts.Patterns.Commands;
using System.Windows.Forms;

namespace Accounts.UI.Commands
{
    class RemoveNode : ICommand
    {
        public RemoveNode(TreeNodeCollection nodes, TreeNode node)
        {
            Nodes = nodes;
            Node  = node;
            Index = nodes.IndexOf( node );
        }

        private TreeNodeCollection Nodes { get; set; }
        private TreeNode Node { get; set; }
        private int Index { get; set; }

        public void Do()
        {
            Nodes.Remove(Node);
        }

        public void Undo()
        {
            Nodes.Insert( Index, Node );
        }

        public void Redo() => Do();
        public void Cancel() { }
    }
}
