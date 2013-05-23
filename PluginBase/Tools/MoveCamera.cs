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
    [Export("Move Camera", typeof(EditorTool)), PartCreationPolicy(CreationPolicy.Shared)]
    public class MoveCamera : EditorTool
    {
        private Ray _startMouseRay;
        private Ray _endMouseRay;
        private Plane _cameraPlane;
        private Camera _camera;

        public MoveCamera()
            : base(ToolEvent.CreateDown(MouseButtons.Middle), true)
        {            
        }

        public override void Mouse_Down(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                //freeze camera
                _camera = Editor.Camera.Clone();
                _startMouseRay = _camera.GetWorldRay(new Vector2(e.X, e.Y));
                _cameraPlane = new Plane(_camera.LookAt, _camera.LookAt + _camera.Right, _camera.LookAt + _camera.UpEffective);
            }
        }

        public override void Mouse_Up(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle) Done = true;
        }

        public override void Mouse_Move(MouseEventArgs e)
        {
            _endMouseRay = _camera.GetWorldRay(new Vector2(e.X, e.Y));

            float? distanceStart, distance;
            _startMouseRay.Intersects(ref _cameraPlane, out distanceStart);
            _endMouseRay.Intersects(ref _cameraPlane, out distance);

            if (distanceStart != null && distance != null)
            {
                var deltaMouse = (_endMouseRay.Position + Vector3.Multiply(_endMouseRay.Direction, distance.Value))
                    - (_startMouseRay.Position + Vector3.Multiply(_startMouseRay.Direction, distanceStart.Value));

                Editor.Camera.LookAt = _camera.LookAt - deltaMouse;
                Editor.Camera.Position = _camera.Position - deltaMouse;
            }
        }

        public override bool Availbable()
        {
            return Editor != null && Editor.Camera != null;
        }
    }
}
