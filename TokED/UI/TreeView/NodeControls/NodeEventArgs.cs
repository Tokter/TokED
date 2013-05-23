using System;
using System.Collections.Generic;
using System.Text;

namespace TokED.UI.Tree.NodeControls
{
	public class NodeEventArgs : EventArgs
	{
		private TreeNodeAdv _node;
		public TreeNodeAdv Node
		{
			get { return _node; }
		}

		public NodeEventArgs(TreeNodeAdv node)
		{
			_node = node;
		}
	}
}
