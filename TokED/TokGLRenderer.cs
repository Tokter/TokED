using Autofac;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Squid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TokED.UI;
using TokGL;

namespace TokED
{
    public struct TextureInfo
    {
        public string Name;
        public int X;
        public int Y;
        public int Width;
        public int Height;
    }

    public class TokGLRenderer : ISquidRenderer, IDisposable
    {
        [DllImport("user32.dll")]
        private static extern int GetKeyboardLayout(int dwLayout);
        [DllImport("user32.dll")]
        private static extern int GetKeyboardState(ref byte pbKeyState);
        [DllImport("user32.dll", EntryPoint = "MapVirtualKeyEx")]
        private static extern int MapVirtualKeyExA(int uCode, int uMapType, int dwhkl);
        [DllImport("user32.dll")]
        private static extern int ToAsciiEx(int uVirtKey, int uScanCode, ref byte lpKeyState, ref short lpChar, int uFlags, int dwhkl);
        private int KeyboardLayout;
        private byte[] KeyStates;
        private Dictionary<int, char> _charMap = new Dictionary<int, char>();

        private RenderManager _manager;
        private SpriteBatch _batch;
        private TokGL.Font[] _font = new TokGL.Font[2];
        private int _whiteID;

        public TokGLRenderer()
        {
            KeyboardLayout = GetKeyboardLayout(0);
            KeyStates = new byte[0x100];

            _charMap.Add(82, '0');
            _charMap.Add(79, '1');
            _charMap.Add(80, '2');
            _charMap.Add(81, '3');
            _charMap.Add(75, '4');
            _charMap.Add(76, '5');
            _charMap.Add(77, '6');
            _charMap.Add(71, '7');
            _charMap.Add(72, '8');
            _charMap.Add(73, '9');
            _charMap.Add(83, '.');

            _manager = new RenderManager();
            _manager.Camera = new Camera();
            _manager.Camera.CameraType = CameraType.HUD;

            _batch = new SpriteBatch();
            _font[0] = new TokGL.Font(Plugins.LoadResourceStream("ArialBlack.png"),Plugins.LoadResourceStream("ArialBlack.info"));
            _font[1] = new TokGL.Font(Plugins.LoadResourceStream("ArialWhite.png"), Plugins.LoadResourceStream("ArialWhite.info"));

            //Load all UI textures:
            var textures = Plugins.GetKeys<UITexture>();
            foreach (var texture in textures)
            {
                Plugins.Container.ResolveNamed<UITexture>(texture);
            }
            _whiteID = UITexture.GetID("white");
        }

        public void Resize(int width, int height)
        {
            _manager.Camera.Width = width;
            _manager.Camera.Height = height;
        }

        public void DrawBox(int x, int y, int width, int height, int color)
        {
            var ti = UITexture.GetTexture(_whiteID);
            _batch.AddSprite(ti.Mat, new Vector2(x, y), new Vector2(x + width, y + height), ti.X, ti.Y, ti.Width, ti.Height, Color.FromArgb(color));
        }

        public void DrawText(string text, int x, int y, int font, int color)
        {
            _batch.AddText(_font[font], x, y, text, Color.FromArgb(color), HorizontalAlignment.Left, VerticalAlignment.Top);
        }

        public void DrawTexture(int texture, int x, int y, int width, int height, Squid.Rectangle source, int color)
        {
            var ti = UITexture.GetTexture(texture);
            _batch.AddSprite(ti.Mat, new Vector2(x, y), new Vector2(x + width, y + height), ti.X + source.Left, ti.Y + source.Top, source.Width, source.Height, Color.FromArgb(color));
        }

        public int GetFont(string name)
        {
            switch(name)
            {
                case "White": return 1;
                default: return 0;
            }
        }

        public Squid.Point GetTextSize(string text, int font)
        {
            return new Squid.Point(_font[font].MeasureWidth(text), _font[font].MeasureHeight(text));
        }

        public int GetTexture(string name)
        {
            return UITexture.GetID(name);
        }

        public Squid.Point GetTextureSize(int texture)
        {
            var ti = UITexture.GetTexture(texture);
            return new Squid.Point(ti.Width, ti.Height);
        }

        public void Scissor(int x, int y, int width, int height)
        {
            GL.Scissor(x, _manager.Camera.Height - (y + height), width, height);
            GL.Enable(EnableCap.ScissorTest);
        }

        public void StartBatch()
        {
            _manager.Begin();
            _batch.Begin(_manager);
        }

        public void EndBatch(bool final)
        {
            _batch.End();
            _manager.End();
        }

        public void RedrawAsIs()
        {
            _manager.End();
        }

        public bool TranslateKey(int scancode, ref char character)
        {
            if (_charMap.ContainsKey(scancode))
            {
                character = _charMap[scancode];
                return true;
            }

            short lpChar = 0;
            if (GetKeyboardState(ref KeyStates[0]) == 0)
                return false;

            int result = ToAsciiEx(MapVirtualKeyExA(scancode, 1, KeyboardLayout), scancode, ref KeyStates[0], ref lpChar, 0, KeyboardLayout);
            if (result == 1)
            {
                character = (char)((ushort)lpChar);
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            _batch.Dispose();
            foreach (var font in _font) font.Dispose();
        }
    }
}
