using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokGL;

namespace TokED.Editors
{
    public enum HandleType
    {
        Box, Cross
    }

    public class Handle : EditorControl
    {
        private int _length = 5;
        private HandleType _handleType = HandleType.Box;

        public Handle()
        {
            PositionType = PositionType.World;
            Alignment = ControlAlignment.Center;
            Snap = SnapType.One;
            Width = _length * 2;
            Height = _length * 2;
        }

        public HandleType HandleType
        {
            get { return _handleType; }
            set { _handleType = value; }
        }

        protected override void OnDrawGui(LineBatch lineBatch)
        {
            Color c = Selected ? Color.Orange : Color.White;

            switch (_handleType)
            {
                case HandleType.Box:
                    lineBatch.AddBox(ScreenPos.X - _length / 2.0f, ScreenPos.Y - _length / 2.0f, _length, _length, c);
                    break;

                case Editors.HandleType.Cross:
                    lineBatch.Add(new Vector2(ScreenPos.X, ScreenPos.Y - _length), new Vector2(ScreenPos.X, ScreenPos.Y + _length), c);
                    lineBatch.Add(new Vector2(ScreenPos.X - _length, ScreenPos.Y), new Vector2(ScreenPos.X + _length, ScreenPos.Y), c);
                    break;
            }
        }
    }
}
