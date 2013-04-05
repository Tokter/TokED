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
    [Export("Reset Zoom", typeof(EditorTool)), PartCreationPolicy(CreationPolicy.Shared)]
    public class ResetZoom : EditorTool
    {
        public ResetZoom()
            : base(ToolEvent.CreateDown(Key.Keypad0), false)
        {
        }

        public override void Activated()
        {
            switch (Editor.Camera.CameraType)
            {
                case CameraType.Orthogonal:
                    Editor.Camera.Fov = 1.0f;
                    break;

                case CameraType.Perspective:
                    break;
            }
        }

        public override bool Availbable()
        {
            return Editor != null && Editor.Camera != null;
        }
    }
}
