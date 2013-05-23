using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokED;
using TokGL;

namespace PluginBase.Tools
{
    [Export("Select", typeof(EditorTool)), PartCreationPolicy(CreationPolicy.Shared)]
    public class Select : EditorTool
    {
        public Select()
            : base(ToolEvent.CreateDown(MouseButtons.Right), false)
        {
        }

        public override void Activated()
        {
            Editor.SelectNone();
            Editor.Select(Editor.MouseOverControl);
        }

        public override bool Availbable()
        {
            return Editor != null && Editor.Camera != null && Editor.MouseOverControl != null;
        }
    }
}
