using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED.Editors;

namespace TokED
{
    public class Tools
    {
        private static List<EditorTool> _tools = new List<EditorTool>();
        private Stack<EditorTool> _activeTools = new Stack<EditorTool>();
        private Modifier _modifier = Modifier.None;
        private Editor _editor;

        public Tools(Editor editor)
        {
            _editor = editor;
        }

        public static void AddTool(EditorTool tool)
        {
            _tools.Add(tool);
        }

        public void Activate(string name)
        {
            foreach (var tool in _tools)
            {
                if (tool.ExportName == name)
                {
                    Activate(tool);
                    return;
                }
            }
            throw new Exception(string.Format("Tool {0} does not exist! Could not activate it...", name));
        }

        private void Push(ToolEvent se)
        {
            CheckIfCurrentToolIsDone();

            foreach (var tool in _tools)
            {
                if (tool.Trigger == se)
                {
                    Activate(tool);
                    break;
                }
            }
        }

        private void Activate(EditorTool tool)
        {
            tool.Editor = _editor;
            if (tool.Availbable())
            {
                tool.Activated();
                if (tool.StaysActivated)
                {
                    tool.Done = false;
                    _activeTools.Push(tool);
                }
            }
        }

        public bool HasAvtiveTools
        {
            get { return _activeTools.Count > 0; }
        }

        private void CheckIfCurrentToolIsDone()
        {
            if (_activeTools.Count > 0)
            {
                if (_activeTools.Peek().Done)
                {
                    _activeTools.Pop();
                }
            }
        }

        public void MouseMove(MouseMoveEventArgs e)
        {
            Push(ToolEvent.CreateMouseMove());
            if (_activeTools.Count > 0) _activeTools.Peek().Mouse_Move(e);
            CheckIfCurrentToolIsDone();
        }

        public void MouseWheel(MouseWheelEventArgs e)
        {
            Push(ToolEvent.CreateMouseWheel());
            if (_activeTools.Count > 0) _activeTools.Peek().Mouse_Wheel(e);
            CheckIfCurrentToolIsDone();
        }

        public void MouseButton(MouseButtonEventArgs e)
        {
            if (e.IsPressed)
            {
                Push(ToolEvent.CreateDown(e.Button, _modifier));
            }
            else
            {
                Push(ToolEvent.CreateUp(e.Button, _modifier));
            }
            if (_activeTools.Count > 0) _activeTools.Peek().Mouse_Button(e);
            CheckIfCurrentToolIsDone();
        }

        public void KeyDown(KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.ShiftLeft:
                case Key.ShiftRight:
                    _modifier |= Modifier.Shift;
                    break;

                case Key.ControlLeft:
                case Key.ControlRight:
                    _modifier |= Modifier.Control;
                    break;

                case Key.AltLeft:
                case Key.AltRight:
                    _modifier |= Modifier.Alt;
                    break;
            }
            Push(ToolEvent.CreateDown(e.Key, _modifier));
            if (_activeTools.Count > 0) _activeTools.Peek().Key_Down(e);
            CheckIfCurrentToolIsDone();
        }

        public void KeyUp(KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.ShiftLeft:
                case Key.ShiftRight:
                    _modifier ^= Modifier.Shift;
                    break;

                case Key.ControlLeft:
                case Key.ControlRight:
                    _modifier ^= Modifier.Control;
                    break;

                case Key.AltLeft:
                case Key.AltRight:
                    _modifier ^= Modifier.Alt;
                    break;
            }
            Push(ToolEvent.CreateUp(e.Key, _modifier));
            if (_activeTools.Count > 0) _activeTools.Peek().Key_Up(e);
            CheckIfCurrentToolIsDone();
        }

    }
}
