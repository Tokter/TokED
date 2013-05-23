using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokED.Editors;

namespace TokED
{
    public class Tools
    {
        private static List<EditorTool> _tools = new List<EditorTool>();
        private Stack<EditorTool> _activeTools = new Stack<EditorTool>();
        private Keys _modifier = Keys.None;
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

        public void MouseMove(MouseEventArgs e)
        {
            Push(ToolEvent.CreateMouseMove());
            if (_activeTools.Count > 0) _activeTools.Peek().Mouse_Move(e);
            CheckIfCurrentToolIsDone();
        }

        public void MouseWheel(MouseEventArgs e)
        {
            Push(ToolEvent.CreateMouseWheel());
            if (_activeTools.Count > 0) _activeTools.Peek().Mouse_Wheel(e);
            CheckIfCurrentToolIsDone();
        }

        public void MouseUp(MouseEventArgs e)
        {
            Push(ToolEvent.CreateUp(e.Button, _modifier));
            if (_activeTools.Count > 0) _activeTools.Peek().Mouse_Up(e);
            CheckIfCurrentToolIsDone();
        }

        public void MouseDown(MouseEventArgs e)
        {
            Push(ToolEvent.CreateDown(e.Button, _modifier));
            if (_activeTools.Count > 0) _activeTools.Peek().Mouse_Down(e);
            CheckIfCurrentToolIsDone();
        }

        public void KeyDown(KeyEventArgs e)
        {
            _modifier = e.Modifiers;
            Push(ToolEvent.CreateDown(e.KeyCode, _modifier));
            if (_activeTools.Count > 0) _activeTools.Peek().Key_Down(e);
            CheckIfCurrentToolIsDone();
        }

        public void KeyUp(KeyEventArgs e)
        {
            _modifier = e.Modifiers;
            Push(ToolEvent.CreateUp(e.KeyCode, _modifier));
            if (_activeTools.Count > 0) _activeTools.Peek().Key_Up(e);
            CheckIfCurrentToolIsDone();
        }

    }
}
