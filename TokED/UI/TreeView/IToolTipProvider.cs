using System;
using System.Collections.Generic;
using System.Text;
using TokED.UI.Tree.NodeControls;

namespace TokED.UI.Tree
{
	public interface IToolTipProvider
	{
		string GetToolTip(TreeNodeAdv node, NodeControl nodeControl);
	}
}
