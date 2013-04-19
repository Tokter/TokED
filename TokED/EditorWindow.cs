using Autofac;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Squid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TokED.UI;
using TokGL;

namespace TokED
{
    public class KeyboardBuffer : Dictionary<int, bool> { }

    public class EditorWindow : GameWindow
    {
        private int _wheel;
        private int _lastWheel;
        private int _mouseX;
        private int _mouseY;
        private bool[] _buttons = new bool[4];
        private KeyboardMapper _mapper = new KeyboardMapper();
        private KeyboardBuffer _keyBuffer = new KeyboardBuffer();
        private List<KeyData> _keyList = new List<KeyData>();

        private float _fps;
        private int _frames;
        private double _frameTime = 0;
        private TokGLRenderer _renderer;

        private EditorDesktop _desktop;

        public EditorWindow()
            : base(800, 600, new GraphicsMode(new ColorFormat(8, 8, 8, 8), 16), "TokED", GameWindowFlags.Default, DisplayDevice.Default, 4, 0, GraphicsContextFlags.ForwardCompatible)
        {
            VSync = VSyncMode.Off;
            Plugins.LoadPlugins();

            //FontBuilder builder = new FontBuilder();
            //builder.Build(new System.Drawing.Font("Arial", 10.0f), System.Drawing.Text.TextRenderingHint.SystemDefault, @"e:\ArialWhite");
            //builder.Build(new System.Drawing.Font("Arial", 10.0f), System.Drawing.Text.TextRenderingHint.AntiAlias, @"e:\ArialBlack");

            //Load Tools
            var tools = Plugins.GetKeys<EditorTool>();
            foreach (var tool in tools)
            {
                Tools.AddTool(Plugins.Container.ResolveKeyed<EditorTool>(tool));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            _renderer = new TokGLRenderer();
            GuiHost.Renderer = _renderer;

            _desktop = new UI.EditorDesktop { Name = "desk" };
            _desktop.ShowCursor = true;
            _desktop.AutoSize = AutoSize.HorizontalVertical;

            this.Mouse.Move += Mouse_Move;
            this.Mouse.WheelChanged += Mouse_WheelChanged;
            this.Mouse.ButtonDown += Mouse_Button;
            this.Mouse.ButtonUp += Mouse_Button;
            this.Keyboard.KeyDown += EditorWindow_KeyDown;
            this.Keyboard.KeyUp += EditorWindow_KeyUp;
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            RenderObject.FreeAll();
        }

        void EditorWindow_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            int code = _mapper.Map(e.Key);
            if (!_keyBuffer.ContainsKey(code)) _keyBuffer.Add(code, false);
            if (_desktop.FocusedControl == null) _desktop.Editor.KeyUp(e);
        }

        void EditorWindow_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            int code = _mapper.Map(e.Key);
            if (!_keyBuffer.ContainsKey(code)) _keyBuffer.Add(code, true);
            if (_desktop.FocusedControl == null) _desktop.Editor.KeyDown(e);
        }

        void Mouse_Button(object sender, MouseButtonEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButton.Left: _buttons[0] = e.IsPressed; break;
                case MouseButton.Right: _buttons[1] = e.IsPressed; break;
                case MouseButton.Middle: _buttons[2] = e.IsPressed; break;
            }
            if (e.X > _desktop.Width || _desktop.Editor.HasActiveTools) _desktop.Editor.MouseButton(e);
        }

        void Mouse_WheelChanged(object sender, MouseWheelEventArgs e)
        {
            _wheel = e.Delta > _lastWheel ? -1 : (e.Delta < _lastWheel ? 1 : 0);
            _lastWheel = e.Delta;
            if (e.X > _desktop.Width || _desktop.Editor.HasActiveTools) _desktop.Editor.MouseWheel(e);
        }

        void Mouse_Move(object sender, MouseMoveEventArgs e)
        {
            _mouseX = e.X;
            _mouseY = e.Y;
            if (e.X > _desktop.Width || _desktop.Editor.HasActiveTools) _desktop.Editor.MouseMove(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            _keyList.Clear();
            foreach (int key in _keyBuffer.Keys)
                _keyList.Add(new KeyData { Pressed = _keyBuffer[key], Released = !_keyBuffer[key], Scancode = key });
            _keyBuffer.Clear();

            if (!_desktop.Editor.HasActiveTools)
            {
                GuiHost.SetMouse(_mouseX, _mouseY, _wheel);
                GuiHost.SetButtons(_buttons);
            }
            GuiHost.SetKeyboard(_keyList.ToArray());
            GuiHost.TimeElapsed = (float)(e.Time * 1000);
            _desktop.Update();

            _desktop.Editor.Update(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            //FPS Calculation
            _frames++;
            _frameTime += e.Time;
            if (_frames >= 10)
            {
                _fps = _frames / (float)_frameTime;
                _frames = 0;
                _frameTime = 0.0d;
                this.Title = string.Format("FPS: {0:n1}", _fps);
            }

            GL.Disable(EnableCap.ScissorTest);
            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            _renderer.Resize(ClientRectangle.Width, ClientRectangle.Height);

            GL.ClearColor(0.20f, 0.20f, 0.20f, 0.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _desktop.Editor.Resize(ClientRectangle.Width, ClientRectangle.Height);
            _desktop.Editor.Draw(e);

            _desktop.Size = new Point(ClientRectangle.Width, ClientRectangle.Height);
            _desktop.Draw();

            GL.Flush();
            SwapBuffers();
        }
    }
}
