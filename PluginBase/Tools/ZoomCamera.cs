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
    [Export("Zoom Camera", typeof(EditorTool)), PartCreationPolicy(CreationPolicy.Shared)]
    public class ZoomCamera : EditorTool
    {
        public ZoomCamera()
            : base(ToolEvent.CreateMouseWheel(), true)
        {
        }

        public override void Mouse_Wheel(MouseEventArgs e)
        {
            switch (Editor.Camera.CameraType)
            {
                case CameraType.Orthogonal:
                    Editor.Camera.Fov /= (float)Math.Pow(1.3d, (e.Delta / 120.0d));
                    break;

                case CameraType.Perspective:
                    var direction = Editor.Camera.Position - Editor.Camera.LookAt;
                    var distance = direction.Length;
                    direction.Normalize();
                    Editor.Camera.Position = Editor.Camera.LookAt + Vector3.Multiply(direction, distance / (float)Math.Pow(1.3d, (e.Delta / 120.0d)));

                    break;
            }
            Done = true;
        }

        public override bool Availbable()
        {
            return Editor != null && Editor.Camera != null;
        }
    }
}
