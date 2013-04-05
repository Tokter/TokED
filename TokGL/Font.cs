using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TokGL
{
    struct CharInfo
    {
        public float U1, V1, U2, V2; //Texture Coordinates
        //public int X1, Y1, X2, Y2; //Bitmap Coordinates
        public int Width, Height; //Char Size
        public int YOffset;
    }

    public enum HorizontalAlignment
    {
        Left,Center,Right
    }

    public enum VerticalAlignment
    {
        Top, Center, Bottom
    }

    public class Font : IDisposable
    {
        private CharInfo[] _charInfo = new CharInfo[256];
        private byte[] _kerning = new byte[256 * 256];
        private Material _material;

        public Font(string fontName, string fontInfoName)
	    {
            CreateMaterial(fontName, fontInfoName);
	    }

        public Material Material
        {
            get { return _material; }
        }

        private void CreateMaterial(string fontName, string fontInfoName)
        {
            var texture = new Texture();
            texture.LoadFromResource(fontName, true, false);

            var names = System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceNames();
            var name = names.First((n) => n.EndsWith(fontInfoName));

            using (var sr = System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream(name))
            {
                using (var br = new BinaryReader(sr))
                {
                    for (int i = 33; i < 256; i++)
                    {
                        var x1 = br.ReadByte();
                        var y1 = br.ReadByte();
                        var x2 = br.ReadByte();
                        var y2 = br.ReadByte();

                        _charInfo[i].U1 = x1 / (float)texture.Width;
                        _charInfo[i].V1 = y1 / (float)texture.Height;
                        _charInfo[i].U2 = x2 / (float)texture.Width;
                        _charInfo[i].V2 = y2 / (float)texture.Height;
                        _charInfo[i].Width = x2 - x1;
                        _charInfo[i].Height = y2 - y1;
                        _charInfo[i].YOffset = br.ReadByte();
                    }
                    for (int i = 0; i < 256*256; i++)
                    {
                        _kerning[i] = br.ReadByte();
                    }
                }
            }
            _charInfo[32].Width = _charInfo[101].Width-2;
            _charInfo[32].Height = _charInfo[101].Height;


            _material = Material.CreateTextureColor(texture);
            _material.AlphaBlend = true;
            _material.DepthTest = false;
        }

        public int MeasureWidth(string s)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(s);
            int width = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                width += _charInfo[bytes[i]].Width;
                if (i > 0)
                {
                    width -= (_kerning[bytes[i - 1] * 256 + bytes[i]] - 1);
                }
            }

            return width;
        }

        public int MeasureHeight(string s)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(s);
            int height = 0;
            foreach (byte b in bytes)
            {
                height = Math.Max(height, _charInfo[b].Height + _charInfo[b].YOffset);
            }
            return height;
        }

        private int ClosestPowerOfTwo(int value)
        {
            int pOfTwo = 4;
            while (pOfTwo < value)
            {
                pOfTwo *= 2;
            }
            return pOfTwo;
        }

        public void CreateSprite(SpriteBatch batch, int px, int py, string text, Color color, HorizontalAlignment horizontal, VerticalAlignment vertical)
        {
            int width = MeasureWidth(text);
            int height = MeasureHeight(text);
            int x = 0, y = 0;
            switch (horizontal)
            {
                case HorizontalAlignment.Left: x = px; break;
                case HorizontalAlignment.Center: x = px - width / 2; break;
                case HorizontalAlignment.Right: x = px - width; break;
            }
            switch (vertical)
            {
                case VerticalAlignment.Top: y = py; break;
                case VerticalAlignment.Center: y = py - height / 2 - 1; break;
                case VerticalAlignment.Bottom: y = py - height; break;
            }

            byte[] bytes = System.Text.Encoding.Default.GetBytes(text);
            for (int i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i];
                if (b != 32)
                {
                    if (i > 0)
                    {
                        x -= _kerning[bytes[i - 1] * 256 + bytes[i]]-1;
                    }

                    batch.AddSprite(_material, new Vector2(x, y + _charInfo[b].YOffset), new Vector2(x + _charInfo[b].Width, y + _charInfo[b].YOffset + _charInfo[b].Height), _charInfo[b].U1, _charInfo[b].V1, _charInfo[b].U2, _charInfo[b].V2, color);
                }
                x += _charInfo[b].Width;
            }
        }

        public void Dispose()
        {
            _material.Dispose();
        }
    }
}
