using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokED.UI.Tree;

namespace TokED.UI
{
    public class GameObjectTreeModel : ITreeModel
    {
        public GameObject Root { get; set; }
        public ImageList ImageList { get; set; }

        public GameObject FindNode(TreePath path)
        {
            if (path.IsEmpty()) return Root;
            else return path.LastNode as GameObject;
        }

        public TreePath GetPath(GameObject node)
        {
            if (node == Root)
                return TreePath.Empty;
            else
            {
                Stack<object> stack = new Stack<object>();
                while (node != Root)
                {
                    stack.Push(node);
                    node = node.Parent;
                }
                return new TreePath(stack.ToArray());
            }
        }

        public System.Collections.IEnumerable GetChildren(TreePath treePath)
        {
            GameObject node = FindNode(treePath);
            return node.Children;
        }

        public bool IsLeaf(TreePath treePath)
        {
            GameObject node = FindNode(treePath);
            if (node != null)
                return node.NumChildrens == 0;
            else
                throw new ArgumentException("treePath");
        }

        public event EventHandler<TreeModelEventArgs> NodesChanged;
        internal void OnNodesChanged(TreeModelEventArgs args)
        {
            if (NodesChanged != null) NodesChanged(this, args);
        }
        
        public event EventHandler<TreeModelEventArgs> NodesInserted;
        internal void OnNodeInserted(GameObject parent, int index, GameObject node)
        {
            if (NodesInserted != null)
            {
                TreeModelEventArgs args = new TreeModelEventArgs(GetPath(parent), new int[] { index }, new object[] { node });
                NodesInserted(this, args);
            }

        }

        public event EventHandler<TreeModelEventArgs> NodesRemoved;
        internal void OnNodeRemoved(GameObject parent, int index, GameObject node)
        {
            if (NodesRemoved != null)
            {
                TreeModelEventArgs args = new TreeModelEventArgs(GetPath(parent), new int[] { index }, new object[] { node });
                NodesRemoved(this, args);
            }
        }

        public event EventHandler<TreePathEventArgs> StructureChanged;
        public void OnStructureChanged(TreePathEventArgs args)
        {
            if (StructureChanged != null) StructureChanged(this, args);
        }
    }
}
