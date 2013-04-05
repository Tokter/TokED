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
    [Export("Toggle Camera Type", typeof(EditorTool)), PartCreationPolicy(CreationPolicy.Shared)]
    public class ToggleCameraType : EditorTool
    {
        public ToggleCameraType()
            : base(ToolEvent.CreateDown(Key.Keypad5), false)
        {
        }

        public override void Activated()
        {
            if (Editor.Camera.CameraType == CameraType.Orthogonal)
            {
                Editor.Camera.CameraType = CameraType.Perspective;
            }
            else
            {
                Editor.Camera.CameraType = CameraType.Orthogonal;
            }
        }

        public override bool Availbable()
        {
            return Editor != null && Editor.Camera != null;
        }
    }
}
