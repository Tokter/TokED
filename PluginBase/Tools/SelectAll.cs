using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED;
using TokGL;

namespace PluginBase.Tools
{
    [Export("Select All", typeof(EditorTool)), PartCreationPolicy(CreationPolicy.Shared)]
    public class SelectAll : EditorTool
    {
        public SelectAll()
            : base(ToolEvent.CreateDown(Key.A), false)
        {
        }

        public override void Activated()
        {
            if (Editor.SelectionCount == 0)
            {
                Editor.SelectAll();
            }
            else
            {
                Editor.SelectNone();
            }
        }

        public override bool Availbable()
        {
            return Editor != null && Editor.Camera != null;
        }
    }
}
