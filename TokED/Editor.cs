using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokGL;

namespace TokED
{
    [Export("Editor", typeof(Editor)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class Editor : IDisposable
    {
        private Tools _tools;
        private RenderManager _manager;
        private SpriteBatch _spriteBatch;
        private LineBatch _lineBatch;
        private Camera _editorCamera;
        private Camera _guiCamera;
        private TokGL.Font _guiFont;
        private GameObject _selectedGameObject;
        private Color BRIGHT = Color.FromArgb(50, 255, 255, 255);
        private Color DARK = Color.FromArgb(10, 255, 255, 255);
        private Vector2 _mousePos;

        public Editor()
        {
            _tools = new Tools(this);

            _manager = new RenderManager();
            _editorCamera = new Camera();
            _editorCamera.CameraType = CameraType.Orthogonal;
            _editorCamera.Position = new Vector3(0, 0, 200);
            _editorCamera.LookAt = new Vector3(0, 0, 0);
            _editorCamera.ZNear = 0;
            _editorCamera.ZFar = 10000;
            _editorCamera.Up = new Vector3(0, 1, 0);
            _editorCamera.Fov = 1.0f;

            _guiCamera = new Camera();
            _guiCamera.CameraType = CameraType.HUD;
            _guiCamera.Position = new Vector3(0, 0, 200);
            _guiCamera.LookAt = new Vector3(0, 0, 0);
            _guiCamera.ZNear = 0;
            _guiCamera.ZFar = 10000;
            _guiCamera.Up = new Vector3(0, 1, 0);

            _guiFont = new TokGL.Font("ArialWhite.png", "ArialWhite.info");

            _spriteBatch = new SpriteBatch();
            _lineBatch = new LineBatch();
        }

        public Camera Camera
        {
            get { return _editorCamera; }
        }

        public TokGL.Font GuiFont
        {
            get { return _guiFont; }
        }

        public Vector2 MousePos
        {
            get { return _mousePos; }
        }

        public GameObject SelectedGameObject
        {
            get { return _selectedGameObject; }
            set
            {
                if (_selectedGameObject != null) _selectedGameObject.PropertyChanged -= SelectedGameObject_PropertyChanged;
                _selectedGameObject = value;
                _selectedGameObject.PropertyChanged += SelectedGameObject_PropertyChanged;
                Load();
            }
        }

        private void SelectedGameObject_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UnLoad();
            Load();
            SelectedGameObjectChanged(e.PropertyName);
        }

        public virtual void Load()
        {
        }

        public virtual void UnLoad()
        {
        }

        public virtual void SelectedGameObjectChanged(string propertyName)
        {
        }

        public bool HasActiveTools
        {
            get { return _tools.HasAvtiveTools; }
        }

        public void ActivateTool(string name)
        {
            _tools.Activate(name);
        }

        #region Rendering

        public void Update(FrameEventArgs e)
        {
        }

        public void Draw()
        {
            _manager.Camera = _editorCamera;
            _manager.Begin();
            _spriteBatch.Begin(_manager);
            _lineBatch.Begin(_manager);
            DrawGrid();
            DrawContent(_lineBatch, _spriteBatch);
            _lineBatch.End();
            _spriteBatch.End();
            _manager.End();

            _manager.Camera = _guiCamera;
            _manager.Begin();
            _spriteBatch.Begin(_manager);
            _lineBatch.Begin(_manager);
            DrawGui(_lineBatch, _spriteBatch);
            _lineBatch.End();
            _spriteBatch.End();
            _manager.End();
        }

        public virtual void DrawContent(LineBatch lineBatch, SpriteBatch spriteBatch)
        {
        }

        public virtual void DrawGui(LineBatch lineBatch, SpriteBatch spriteBatch)
        {
        }

        public void Resize(int width, int height)
        {
            _editorCamera.Width = width;
            _editorCamera.Height = height;
            _guiCamera.Width = width;
            _guiCamera.Height = height;
        }

        #endregion

        public void Dispose()
        {
            UnLoad();
            if (_selectedGameObject != null) _selectedGameObject.PropertyChanged -= SelectedGameObject_PropertyChanged;
            _spriteBatch.Dispose();
            _lineBatch.Dispose();
        }

        #region Input Handling

        public void MouseMove(MouseMoveEventArgs e)
        {
            _tools.MouseMove(e);
            _mousePos = new Vector2(e.X, e.Y);
        }

        public void MouseWheel(MouseWheelEventArgs e)
        {
            _tools.MouseWheel(e);
        }

        public void MouseButton(MouseButtonEventArgs e)
        {
            _tools.MouseButton(e);
        }

        public void KeyDown(KeyboardKeyEventArgs e)
        {
            _tools.KeyDown(e);
        }

        public void KeyUp(KeyboardKeyEventArgs e)
        {
            _tools.KeyUp(e);
        }

        #endregion

        #region Grid

        private void DrawGrid()
        {
            switch (this.Camera.CameraType)
            {
                case CameraType.Orthogonal:
                    var a = Camera.GetWorldCoordinates(new Vector3(0, 0, 0));
                    var b = Camera.GetWorldCoordinates(new Vector3(Camera.Width, Camera.Height, 0));
                    var unitPixel = (a - Camera.GetWorldCoordinates(new Vector3(35, 35, 0))).Length;
                    float unitSize;
                    int i = -10;
                    do { unitSize = (float)Math.Pow(10.0d, i++); } while (unitPixel > unitSize);

                    if (Math.Abs(Camera.Forward.X) > 0.98f)
                    {
                        GridLines(a.Y, b.Y, unitSize, (v) => { return new Vector3(0.0f, v, a.Z); }, (v) => { return new Vector3(0.0f, v, b.Z); });
                        GridLines(a.Z, b.Z, unitSize, (v) => { return new Vector3(0.0f, a.Y, v); }, (v) => { return new Vector3(0.0f, b.Y, v); });
                    }
                    else if (Math.Abs(Camera.Forward.Y) > 0.98f)
                    {
                        GridLines(a.X, b.X, unitSize, (v) => { return new Vector3(v, 0.0f, a.Z); }, (v) => { return new Vector3(v, 0.0f, b.Z); });
                        GridLines(a.Z, b.Z, unitSize, (v) => { return new Vector3(a.X, 0.0f, v); }, (v) => { return new Vector3(b.X, 0.0f, v); });
                    }
                    else if (Math.Abs(Camera.Forward.Z) > 0.98f)
                    {
                        GridLines(a.Y, b.Y, unitSize, (v) => { return new Vector3(a.X, v, 0.0f); }, (v) => { return new Vector3(b.X, v, 0.0f); });
                        GridLines(a.X, b.X, unitSize, (v) => { return new Vector3(v, a.Y, 0.0f); }, (v) => { return new Vector3(v, b.Y, 0.0f); });
                    }
                    break;

                case CameraType.Perspective:
                    for (int x = -100; x <= 100; x++)
                    {
                        _lineBatch.Add(new Vector3(x * 10.0f, -100 * 10.0f, 0), new Vector3(x * 10.0f, 100 * 10.0f, 0), (x % 10 == 0) ? BRIGHT : DARK);
                    }

                    for (int y = -100; y <= 100; y++)
                    {
                        _lineBatch.Add(new Vector3(-100 * 10.0f, y * 10.0f, 0), new Vector3(100 * 10.0f, y * 10.0f, 0), (y % 10 == 0) ? BRIGHT : DARK);
                    }
                    break;
            }
        }

        private void GridLines(float from, float to, float unitSize, Func<float, Vector3> a, Func<float, Vector3> b)
        {
            if (from > to)
            {
                var temp = from;
                from = to;
                to = temp;
            }
            float v = (float)Math.Round(from / unitSize, MidpointRounding.AwayFromZero) * unitSize - unitSize;
            unitSize /= 10.0f;
            int i = 0;
            while (v < to)
            {
                _lineBatch.Add(a(v), b(v), (i++ % 10 == 0) ? BRIGHT : DARK);
                v += unitSize;
            }
        }

        #endregion
    }
}
