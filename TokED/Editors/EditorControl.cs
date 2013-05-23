using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokGL;

namespace TokED.Editors
{
    public enum PositionType
    {
        World, Screen
    }

    public enum ControlAlignment
    {
        TopLeft, Center, BottomRight
    }

    public enum SnapType
    {
        None, One, Half
    }

    public class EditorControl : INotifyPropertyChanged, IDisposable
    {
        private PositionType _positionType = PositionType.Screen;
        private ControlAlignment _alignment = ControlAlignment.Center;
        private SnapType _snapType = SnapType.None;
        private Vector3 _position = new Vector3();
        private Vector2 _screenPos = new Vector2();
        private int _width = 1;
        private int _height = 1;
        private bool _visible = true;
        private bool _selected = false;
        private bool _exactHitTest = false;
        private List<EditorControl> _children = new List<EditorControl>();

        public PositionType PositionType
        {
            get { return _positionType; }
            set { _positionType = value; NotifyChange(); }
        }

        public ControlAlignment Alignment
        {
            get { return _alignment; }
            set { _alignment = value; NotifyChange(); }
        }

        public SnapType Snap
        {
            get { return _snapType; }
            set { _snapType = value; NotifyChange(); }
        }

        public Vector3 Position
        {
            get { return _position; }
            set
            {
                switch (_snapType)
                {
                    case SnapType.None:
                        _position = value;
                        break;

                    case SnapType.One:
                        _position = new Vector3((float)Math.Round(value.X), (float)Math.Round(value.Y), (float)Math.Round(value.Z));
                        break;

                    case SnapType.Half:
                        _position = new Vector3((float)Math.Round(value.X * 2.0f) / 2.0f, (float)Math.Round(value.Y * 2.0f) / 2.0f, (float)Math.Round(value.Z * 2.0f) / 2.0f);
                        break;
                }
                NotifyChange();
            }
        }

        public Vector2 ScreenPos
        {
            get { return _screenPos; }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; NotifyChange(); }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; NotifyChange(); }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; NotifyChange(); }
        }

        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; NotifyChange(); }
        }

        public bool ExactHitTest
        {
            get { return _exactHitTest; }
            set { _exactHitTest = value; NotifyChange(); }
        }

        public bool Intersects(Vector2 pos)
        {
            var a = new Vector2();
            var b = new Vector2();
            switch (_alignment)
            {
                case ControlAlignment.TopLeft:
                    a = new Vector2(_screenPos.X, _screenPos.Y);
                    b = new Vector2(_screenPos.X + _width, _screenPos.Y + _height);
                    break;

                case ControlAlignment.BottomRight:
                    a = new Vector2(_screenPos.X - _width, _screenPos.Y - _height);
                    b = new Vector2(_screenPos.X, _screenPos.Y);
                    break;

                case ControlAlignment.Center:
                    float hw = _width / 2.0f;
                    float hh = _height / 2.0f;
                    a = new Vector2(_screenPos.X - hw, _screenPos.Y - hh);
                    b = new Vector2(_screenPos.X + hw, _screenPos.Y + hh);
                    break;
            }

            return (pos.X >= a.X) && (pos.X <= b.X) && (pos.Y >= a.Y) && (pos.Y <= b.Y);
        }

        public void AddChild(EditorControl control)
        {
            _children.Add(control);
        }

        public int ChildCount
        {
            get { return _children.Count; }
        }

        public void Clear()
        {
            foreach (var child in _children)
            {
                child.Clear();
                child.Dispose();
            }
            _children.Clear();
        }

        public void Remove(EditorControl control)
        {
            if (_children.Contains(control))
            {
                _children.Remove(control);
            }
        }

        public float Distance(Vector2 pos)
        {
            return (pos - _screenPos).Length;
        }

        /// <summary>
        /// Executes a recursive function, and keeps going as long as true is returned
        /// </summary>
        /// <param name="f">Function to execute</param>
        /// <returns>Whether or not to continue any deeper</returns>
        public bool DoRecurive(Func<EditorControl, bool> f)
        {
            if (f(this))
            {
                foreach (var child in _children)
                {
                    if (child.DoRecurive(f) == false) return false;
                }
                return true;
            }
            else return false;
        }

        public void Update(Editor editor, double elapsedTime)
        {
            switch (_positionType)
            {
                case PositionType.World:
                    _screenPos = editor.Camera.GetScreenCoordinates(_position);
                    break;

                default:
                    _screenPos = new Vector2(_position.X, _position.Y);
                    break;
            }
            foreach (var child in _children)
            {
                child.Update(editor, elapsedTime);
            }
        }

        public void DrawGui(LineBatch lineBatch)
        {
            if (_visible)
            {
                foreach (var child in _children)
                {
                    child.DrawGui(lineBatch);
                }
                OnDrawGui(lineBatch);
            }
        }

        public void DrawWorld(LineBatch lineBatch)
        {
            if (_visible)
            {
                foreach (var child in _children)
                {
                    child.DrawWorld(lineBatch);
                }
                OnDrawWorld(lineBatch);
            }
        }

        protected virtual void OnDrawGui(LineBatch lineBatch)
        {
        }

        protected virtual void OnDrawWorld(LineBatch lineBatch)
        {
        }

        public virtual void MouseEnter()
        {
        }

        public virtual void MouseMove(MouseEventArgs e)
        {
        }

        public virtual void MouseLeave()
        {
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyChange([CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(propertyName);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual void OnPropertyChanged(string name)
        {
        }

        #endregion

        #region IDisposable
        public void Dispose()
        {
            foreach (var child in _children)
            {
                child.Dispose();
            }
            DoDispose();
        }

        public virtual void DoDispose()
        {
        }
        #endregion

    }
}
