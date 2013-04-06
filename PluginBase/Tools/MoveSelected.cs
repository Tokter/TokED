using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED;
using TokED.Editors;
using TokGL;

namespace PluginBase.Tools
{
    [Export("Move Selected", typeof(EditorTool)), PartCreationPolicy(CreationPolicy.Shared)]
    public class MoveSelected : EditorTool
    {
        private bool _startSet = false;
        private Ray _startMouseRay;
        private Ray _endMouseRay;
        private Plane _cameraPlane;
        private Dictionary<EditorControl, Vector3> _startPositions = new Dictionary<EditorControl, Vector3>();

        public MoveSelected()
            : base(ToolEvent.CreateDown(Key.G), true)
        {
        }

        public override bool Availbable()
        {
            return Editor != null && Editor.Camera != null && Editor.SelectionCount > 0;
        }

        public override void Activated()
        {
            _startSet = false;
        }

        public override void Mouse_Move(MouseMoveEventArgs e)
        {
            _endMouseRay = Editor.Camera.GetWorldRay(new Vector2(e.X, e.Y));
            if (!_startSet)
            {
                _startPositions.Clear();
                foreach (var control in Editor.Selection)
                {
                    _startPositions.Add(control, control.Position);
                }
                _startMouseRay = _endMouseRay;
                _cameraPlane = new Plane(Editor.Camera.LookAt, Editor.Camera.LookAt + Editor.Camera.Right, Editor.Camera.LookAt + Editor.Camera.UpEffective);
                _startSet = true;
            }

            float? distanceStart, distance;
            _startMouseRay.Intersects(ref _cameraPlane, out distanceStart);
            _endMouseRay.Intersects(ref _cameraPlane, out distance);

            if (distanceStart != null && distance != null)
            {
                var deltaMouse = (_endMouseRay.Position + Vector3.Multiply(_endMouseRay.Direction, distance.Value))
                    - (_startMouseRay.Position + Vector3.Multiply(_startMouseRay.Direction, distanceStart.Value));

                foreach (var control in Editor.Selection)
                {
                    control.Position = _startPositions[control] + deltaMouse;
                }
            }
        }

        public override void Mouse_Button(MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Right)
            {
                foreach (var control in Editor.Selection)
                {
                    if (_startPositions.ContainsKey(control))
                    {
                        control.Position = _startPositions[control];
                    }
                }
            }
            Done = true;
        }
    }
}
