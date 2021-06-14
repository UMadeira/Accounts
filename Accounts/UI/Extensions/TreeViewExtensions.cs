using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Accounts.UI.Extensions
{
    public static class TreeViewExtensions
    {
        public static IEnumerable<TreeNode> GetAllNodes( this TreeView self )
        {
            var result = new List<TreeNode>();
            foreach ( TreeNode child in self.Nodes )
            {
                result.AddRange( child.GetAllNodes() );
            }
            return result;
        }

        public static List<TreeNode> GetAllNodes( this TreeNode self )
        {
            var result = new List<TreeNode> { self };
            foreach ( TreeNode child in self.Nodes )
            {
                result.AddRange( child.GetAllNodes() );
            }
            return result;
        }
    }
}
