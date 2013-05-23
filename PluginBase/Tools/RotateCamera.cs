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
    [Export("Rotate Camera", typeof(EditorTool)), PartCreationPolicy(CreationPolicy.Shared)]
    public class RotateCamera : EditorTool
    {
        private Camera _camera;
        private Vector2 _mouseStartPos;

        public RotateCamera()
            : base(ToolEvent.CreateDown(MouseButtons.Middle, Keys.Shift), true)
        {
        }

        public override bool Availbable()
        {
            return Editor != null && Editor.Camera != null;
        }

        public override void Mouse_Down(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                _camera = Editor.Camera.Clone();
                _mouseStartPos = new Vector2(e.X, e.Y);
            }
        }

        public override void Mouse_Up(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle) Done = true;
        }

        public override void Mouse_Move(MouseEventArgs e)
        {
            var delta = _mouseStartPos - new Vector2(e.X, e.Y);

            var rotation = Matrix4.CreateFromAxisAngle(_camera.Right, delta.Y / 150.0f) * Matrix4.CreateRotationZ(delta.X / 150.0f);
            var rotationUp = Matrix4.CreateFromAxisAngle(_camera.Right, delta.Y / 150.0f);

            var dir = _camera.Position - _camera.LookAt;
            var distance = dir.Length;
            dir.Normalize();

            dir = Vector3.Transform(dir, rotation);
            var up = Vector3.Transform(_camera.UpEffective, rotationUp);
            if (up.Z < 0) up = new Vector3(0, 0, -1); else up = new Vector3(0, 0, 1);


            Editor.Camera.Position = _camera.LookAt + Vector3.Multiply(dir, distance);
            Editor.Camera.Up = up;

        }
    }
}
