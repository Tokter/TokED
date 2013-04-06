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
    [Export("Select Toggle", typeof(EditorTool)), PartCreationPolicy(CreationPolicy.Shared)]
    public class SelectToggle : EditorTool
    {
        public SelectToggle()
            : base(ToolEvent.CreateDown(MouseButton.Right, Modifier.Shift), false)
        {
        }

        public override void Activated()
        {
            if (Editor.MouseOverControl.Selected)
            {
                Editor.UnSelect(Editor.MouseOverControl);
            }
            else
            {
                Editor.Select(Editor.MouseOverControl);
            }
        }

        public override bool Availbable()
        {
            return Editor != null && Editor.Camera != null && Editor.MouseOverControl != null;
        }
    }
}
