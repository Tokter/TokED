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
        private Dictionary<int, TextureInfo> _textures = new Dictionary<int, TextureInfo>();
        private SpriteBatch _batch;
        private Material _uiMat;
        private TokGL.Font[] _font = new TokGL.Font[2];

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

            _uiMat = Material.CreateTextureColor(Texture.CreateFromResource("UI.png", true, false));
            _uiMat.Texture0.MagFilter = TextureMinFilter.Nearest;
            _uiMat.Texture0.MinFilter = TextureMinFilter.Nearest;
            _uiMat.AlphaBlend = true;
            _uiMat.DepthTest = false;

            _batch = new SpriteBatch();
            _font[0] = new TokGL.Font("ArialBlack.png", "ArialBlack.info");
            _font[1] = new TokGL.Font("ArialWhite.png", "ArialWhite.info");

            _textures.Add(1, new TextureInfo { Name = "button_default", X = 0, Y = 0, Width = 15, Height = 15 });
            _textures.Add(2, new TextureInfo { Name = "button_hot", X = 15, Y = 0, Width = 15, Height = 15 });
            _textures.Add(3, new TextureInfo { Name = "button_down", X = 30, Y = 0, Width = 15, Height = 15 });
            _textures.Add(4, new TextureInfo { Name = "tooltip", X = 16, Y = 29, Width = 16, Height = 16 });
            _textures.Add(5, new TextureInfo { Name = "vscroll_track", X = 0, Y = 73, Width = 13, Height = 16 });
            _textures.Add(6, new TextureInfo { Name = "vscroll_button", X = 13, Y = 73, Width = 13, Height = 13 });
            _textures.Add(7, new TextureInfo { Name = "vscroll_button_hot", X = 26, Y = 73, Width = 13, Height = 13 });
            _textures.Add(8, new TextureInfo { Name = "vscroll_button_down", X = 39, Y = 73, Width = 13, Height = 13 });
            _textures.Add(9, new TextureInfo { Name = "frame", X = 45, Y = 0, Width = 15, Height = 15 });
            _textures.Add(10, new TextureInfo { Name = "listbox_frame", X = 32, Y = 29, Width = 16, Height = 16 });
            _textures.Add(11, new TextureInfo { Name = "listbox_item", X = 27, Y = 45, Width = 9, Height = 9 });
            _textures.Add(12, new TextureInfo { Name = "listbox_item_hot", X = 36, Y = 45, Width = 9, Height = 9 });
            _textures.Add(13, new TextureInfo { Name = "listbox_item_selected", X = 18, Y = 45, Width = 9, Height = 9 });
            _textures.Add(14, new TextureInfo { Name = "listbox_item_selected_hot", X = 45, Y = 45, Width = 9, Height = 9 });
            _textures.Add(15, new TextureInfo { Name = "treeview_button_plus", X = 9, Y = 45, Width = 9, Height = 9 });
            _textures.Add(16, new TextureInfo { Name = "treeview_button_minus", X = 0, Y = 45, Width = 9, Height = 9 });
            _textures.Add(17, new TextureInfo { Name = "eye_active", X = 52, Y = 73, Width = 13, Height = 9 });
            _textures.Add(18, new TextureInfo { Name = "eye_inactive", X = 65, Y = 73, Width = 13, Height = 9 });
            _textures.Add(19, new TextureInfo { Name = "combo_button", X = 40, Y = 89, Width = 10, Height = 20 });
            _textures.Add(20, new TextureInfo { Name = "combo_button_hot", X = 50, Y = 89, Width = 10, Height = 20 });
            _textures.Add(21, new TextureInfo { Name = "combo_button_down", X = 60, Y = 89, Width = 10, Height = 20 });
            _textures.Add(22, new TextureInfo { Name = "combo", X = 0, Y = 89, Width = 20, Height = 20 });
            _textures.Add(23, new TextureInfo { Name = "combo_hot", X = 0, Y = 109, Width = 20, Height = 20 });
            _textures.Add(24, new TextureInfo { Name = "combo_lisbox_frame", X = 20, Y = 89, Width = 20, Height = 20 });
            _textures.Add(25, new TextureInfo { Name = "flat_combo_button", X = 70, Y = 89, Width = 10, Height = 20 });
            _textures.Add(26, new TextureInfo { Name = "flat_combo_button_hot", X = 80, Y = 89, Width = 10, Height = 20 });
            _textures.Add(27, new TextureInfo { Name = "flat_combo_button_down", X = 90, Y = 89, Width = 10, Height = 20 });
            _textures.Add(28, new TextureInfo { Name = "combo_plus", X = 100, Y = 89, Width = 20, Height = 20 });
            _textures.Add(29, new TextureInfo { Name = "combo_minus", X = 120, Y = 89, Width = 20, Height = 20 });
            _textures.Add(30, new TextureInfo { Name = "combo_plus_hot", X = 20, Y = 109, Width = 20, Height = 20 });
            _textures.Add(31, new TextureInfo { Name = "combo_minus_hot", X = 40, Y = 109, Width = 20, Height = 20 });
            _textures.Add(32, new TextureInfo { Name = "combo_plus_down", X = 60, Y = 109, Width = 20, Height = 20 });
            _textures.Add(33, new TextureInfo { Name = "combo_minus_down", X = 80, Y = 109, Width = 20, Height = 20 });

            _textures.Add(34, new TextureInfo { Name = "vsplitter", X = 52, Y = 85, Width = 4, Height = 4 });
            _textures.Add(35, new TextureInfo { Name = "vsplitter_hot", X = 56, Y = 85, Width = 4, Height = 4 });
            _textures.Add(36, new TextureInfo { Name = "vsplitter_down", X = 60, Y = 85, Width = 4, Height = 4 });

            _textures.Add(37, new TextureInfo { Name = "hsplitter", X = 64, Y = 85, Width = 4, Height = 4 });
            _textures.Add(38, new TextureInfo { Name = "hsplitter_hot", X = 68, Y = 85, Width = 4, Height = 4 });
            _textures.Add(39, new TextureInfo { Name = "hsplitter_down", X = 72, Y = 85, Width = 4, Height = 4 });

            _textures.Add(40, new TextureInfo { Name = "input", X = 100, Y = 109, Width = 20, Height = 20 });
            _textures.Add(41, new TextureInfo { Name = "input_hot", X = 120, Y = 109, Width = 20, Height = 20 });

            _textures.Add(42, new TextureInfo { Name = "save", X = 140, Y = 89, Width = 20, Height = 20 });
            _textures.Add(43, new TextureInfo { Name = "save_hot", X = 160, Y = 89, Width = 20, Height = 20 });
            _textures.Add(44, new TextureInfo { Name = "save_down", X = 180, Y = 89, Width = 20, Height = 20 });

            _textures.Add(45, new TextureInfo { Name = "load", X = 140, Y = 109, Width = 20, Height = 20 });
            _textures.Add(46, new TextureInfo { Name = "load_hot", X = 160, Y = 109, Width = 20, Height = 20 });
            _textures.Add(47, new TextureInfo { Name = "load_down", X = 180, Y = 109, Width = 20, Height = 20 });

            _textures.Add(48, new TextureInfo { Name = "checkbox", X = 0, Y = 15, Width = 14, Height = 14 });
            _textures.Add(49, new TextureInfo { Name = "checkbox_checked", X = 14, Y = 15, Width = 14, Height = 14 });
            _textures.Add(50, new TextureInfo { Name = "checkbox_hot", X = 98, Y = 15, Width = 14, Height = 14 });
            _textures.Add(51, new TextureInfo { Name = "checkbox_checked_hot", X = 112, Y = 15, Width = 14, Height = 14 });


            //Icons
            _textures.Add(999, new TextureInfo { Name = "white", X = 54, Y = 45, Width = 9, Height = 9 });
            _textures.Add(1000, new TextureInfo { Name = "Project", X = 42, Y = 15, Width = 14, Height = 14 });
            _textures.Add(1001, new TextureInfo { Name = "Folder", X = 28, Y = 15, Width = 14, Height = 14 });
            _textures.Add(1002, new TextureInfo { Name = "Scene", X = 56, Y = 15, Width = 14, Height = 14 });
            _textures.Add(1003, new TextureInfo { Name = "Texture", X = 70, Y = 15, Width = 14, Height = 14 });
            _textures.Add(1004, new TextureInfo { Name = "Sprite", X = 84, Y = 15, Width = 14, Height = 14 });
            _textures.Add(1005, new TextureInfo { Name = "ArrowHead-Right", X = 126, Y = 15, Width = 14, Height = 14 });
            _textures.Add(1006, new TextureInfo { Name = "ArrowHead-Down", X = 140, Y = 15, Width = 14, Height = 14 });
            _textures.Add(1007, new TextureInfo { Name = "Transformation", X = 154, Y = 15, Width = 14, Height = 14 });
            _textures.Add(1008, new TextureInfo { Name = "SpriteDefinition", X = 168, Y = 15, Width = 14, Height = 14 });
            _textures.Add(1009, new TextureInfo { Name = "Empty", X = 182, Y = 15, Width = 14, Height = 14 });
        }

        public void Resize(int width, int height)
        {
            _manager.Camera.Width = width;
            _manager.Camera.Height = height;
        }

        public void DrawBox(int x, int y, int width, int height, int color)
        {
            var ti = _textures[999];
            _batch.AddSprite(_uiMat, new Vector2(x, y), new Vector2(x + width, y + height), ti.X, ti.Y, ti.Width, ti.Height, Color.FromArgb(color));
        }

        public void DrawText(string text, int x, int y, int font, int color)
        {
            _batch.AddText(_font[font], x, y, text, Color.FromArgb(color), HorizontalAlignment.Left, VerticalAlignment.Top);
        }

        public void DrawTexture(int texture, int x, int y, int width, int height, Squid.Rectangle source, int color)
        {
            var ti = _textures[texture];
            _batch.AddSprite(_uiMat, new Vector2(x, y), new Vector2(x + width, y + height), ti.X + source.Left, ti.Y + source.Top, source.Width, source.Height, Color.FromArgb(color));
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
            foreach (var key in _textures.Keys)
            {
                if (_textures[key].Name == name) return key;
            }
            return -1;
        }

        public Squid.Point GetTextureSize(int texture)
        {
            var ti = _textures[texture];
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
            _uiMat.Dispose();
            foreach (var font in _font) font.Dispose();
        }
    }
}
