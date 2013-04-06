using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokGL
{
    public struct GlyphInfo
    {
        public byte X1, Y1, X2, Y2, YOffset;
    }

    public class Glyph
    {
        private const int MAXGLYPH = 64;
        private Bitmap _bitmap;
        private BitmapData _bitmapData;
        private int _character;
        private Rectangle _outline;
        public GlyphInfo Info;
        public int[] LimitsLeft = new int[MAXGLYPH];
        public int[] LimitsRight = new int[MAXGLYPH];
        public string test;

        public Bitmap Bitmap
        {
            get { return _bitmap; }
        }

        public Glyph(int character, System.Drawing.Font font, TextRenderingHint hint)
        {
            _character = character;

            byte[] b = new byte[1];
            b[0] = (byte)_character;
            var s = Encoding.Default.GetString(b);
            test = s;

            _bitmap = new Bitmap(MAXGLYPH, MAXGLYPH, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Brush brush = Brushes.White;
            var stringFormat = StringFormat.GenericTypographic;
            stringFormat.Alignment = StringAlignment.Near;
            using (var g = Graphics.FromImage(_bitmap))
            {
                //g.PageUnit = GraphicsUnit.Pixel;
                g.TextRenderingHint = hint;
                g.DrawString(s, font, brush, 0, 0, stringFormat);
            }
            Measure();
            if (_outline.Width > 0 && _outline.Height > 0)
            {
                Info.YOffset = (byte)_outline.Top;
                var newBitmap = new Bitmap(_outline.Width, _outline.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using (var g = Graphics.FromImage(newBitmap))
                {
                    g.DrawImage(_bitmap, 0, 0, _outline, GraphicsUnit.Pixel);
                }
                _bitmap.Dispose();
                _bitmap = newBitmap;
                MeasureLimits();
                //_bitmap.Save(@"e:\" + character.ToString() + ".png");
            }
            else
            {
                _bitmap.Dispose();
                _bitmap = null;
            }
        }

        private void LockBits()
        {
            _bitmapData = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.ReadWrite, _bitmap.PixelFormat);
        }

        private void UnlockBits()
        {
            _bitmap.UnlockBits(_bitmapData);
        }

        private unsafe bool IsEmptyPixel(int px, int py)
        {
            byte* scan = (byte*)_bitmapData.Scan0.ToPointer();
            byte* pixel = scan + py * _bitmapData.Stride + px * 4;
            return (pixel[1] == 0 && pixel[2] == 0 && pixel[3] == 0);
        }

        public void Measure()
        {
            LockBits();

            bool empty;

            //Left
            int left = 0;
            for (int x = 0; x < _bitmap.Width; x++)
            {
                empty = true;
                for (int y = 0; y < _bitmap.Height; y++)
                {
                    if (!IsEmptyPixel(x, y))
                    {
                        empty = false;
                        break;
                    }
                }
                if (!empty)
                {
                    left = x;
                    break;
                }
            }

            //Right
            int right = 0;
            for (int x = _bitmap.Width - 1; x >= 0; x--)
            {
                empty = true;
                for (int y = 0; y < _bitmap.Height; y++)
                {
                    if (!IsEmptyPixel(x, y))
                    {
                        empty = false;
                        break;
                    }
                }
                if (!empty)
                {
                    right = x;
                    break;
                }
            }

            //Top
            int top = 0;
            for (int y = 0; y < _bitmap.Height; y++)
            {
                empty = true;
                for (int x = 0; x < _bitmap.Width; x++)
                {
                    if (!IsEmptyPixel(x, y))
                    {
                        empty = false;
                        break;
                    }
                }
                if (!empty)
                {
                    top = y;
                    break;
                }
            }

            //Bottom
            int bottom = 0;
            for (int y = _bitmap.Height - 1; y >= 0; y--)
            {
                empty = true;
                for (int x = 0; x < _bitmap.Width; x++)
                {
                    if (!IsEmptyPixel(x, y))
                    {
                        empty = false;
                        break;
                    }
                }
                if (!empty)
                {
                    bottom = y;
                    break;
                }
            }
            _outline = new Rectangle(left, top, right - left+1, bottom - top+1);

            UnlockBits();
        }

        public void MeasureLimits()
        {
            LockBits();
            for (int i = 0; i < LimitsLeft.Length; i++)
            {
                LimitsLeft[i] = MAXGLYPH;
                LimitsRight[i] = 0;
            }

            for (int y = 0; y < _bitmap.Height; y++)
            {
                for (int x = 0; x < _bitmap.Width; x++)
                {
                    if (!IsEmptyPixel(x, y))
                    {
                        LimitsLeft[y + Info.YOffset] = x;
                        break;
                    }
                }
                for (int x = _bitmap.Width - 1; x >= 0; x--)
                {
                    if (!IsEmptyPixel(x, y))
                    {
                        LimitsRight[y + Info.YOffset] = x;
                        break;
                    }
                }
            }
            UnlockBits();
        }
    }

    public class FontBuilder
    {
        private List<Glyph> _glyphs = new List<Glyph>();
        private byte[] _kerning = new byte[256 * 256];
        private List<int> _noKerning = new List<int>();

        public FontBuilder()
        {
            _noKerning.Add((byte)'"');
            _noKerning.Add((byte)'\'');
            _noKerning.Add((byte)'-');
            _noKerning.Add((byte)'^');
            _noKerning.Add((byte)'+');
            _noKerning.Add((byte)'*');
            _noKerning.Add((byte)'=');
            _noKerning.Add((byte)'.');
            _noKerning.Add((byte)',');
            _noKerning.Add((byte)'_');
        }

        private byte CalculateKerning(int left, int right)
        {
            if (_noKerning.Contains(left) || _noKerning.Contains(right)) return 0;
            var leftg = _glyphs[left-33];
            var rightg = _glyphs[right-33];
            var min = int.MaxValue;
            for (int i = 0; i < leftg.LimitsRight.Length; i++)
            {
                var delta = (leftg.Info.X2-leftg.Info.X1) + rightg.LimitsLeft[i] - leftg.LimitsRight[i];
                if (delta < min) min = delta;
            }
            return (byte)min;
        }

        private void CalculateKernings()
        {
            for (int left = 33; left < 256; left++)
            {
                for (int right = 33; right < 256; right++)
                {
                    _kerning[left * 256 + right] = CalculateKerning(left, right);
                }
            }
        }

        public void Build(System.Drawing.Font font, TextRenderingHint hint, string filename)
        {
            //new Glyph(97, font, hint);

            //Create Glyphs
            _glyphs.Clear();
            for (int i = 33; i < 256; i++)
            {
                _glyphs.Add(new Glyph(i, font, hint));
            }

            //Create Texture
            bool allFit;
            int startWidth = 128;
            int startHeight = 128;
            BitmapPacker root;
            do
            {
                allFit = true;
                root = new BitmapPacker();
                root.Rect = new System.Drawing.Rectangle(0, 0, startWidth, startHeight);
                foreach (var glyph in _glyphs)
                {
                    if (glyph.Bitmap != null)
                    {
                        var bp = root.Insert(glyph.Bitmap);
                        if (bp == null)
                        {
                            if (startWidth > startHeight)
                            {
                                startHeight *= 2;
                            }
                            else
                            {
                                startWidth *= 2;
                            }
                            allFit = false;
                            break;
                        }
                        else
                        {
                            glyph.Info.X1 = (byte)bp.Rect.X;
                            glyph.Info.Y1 = (byte)bp.Rect.Y;
                            glyph.Info.X2 = (byte)bp.Rect.Right;
                            glyph.Info.Y2 = (byte)bp.Rect.Bottom;
                        }
                    }
                }
            } while (!allFit);

            Bitmap result = new Bitmap(root.Rect.Width, root.Rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            root.Render(result);
            result.Save(filename + ".png");

            CalculateKernings();


            using (var fs = new FileStream(filename + ".info", FileMode.Create))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    foreach (var g in _glyphs)
                    {
                        bw.Write(g.Info.X1);
                        bw.Write(g.Info.Y1);
                        bw.Write(g.Info.X2);
                        bw.Write(g.Info.Y2);
                        bw.Write(g.Info.YOffset);
                    }
                    bw.Write(_kerning);
                }
            }

        }
    }
}
