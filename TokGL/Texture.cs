using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokGL
{
    public class Texture : IDisposable
    {
        private int _textureID;
        private int _width;
        private int _height;

        public TextureMinFilter MinFilter { get; set; }
        public TextureMagFilter MagFilter { get; set; }

        public Texture()
        {
            _textureID = GL.GenTexture();
            MinFilter = TextureMinFilter.Linear;
            MagFilter = TextureMagFilter.Linear;
        }

        public int TextureID
        {
            get { return _textureID; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, _textureID);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)MinFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)MagFilter);
        }

        public void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public static Texture CreateFromStream(Stream stream, bool preMultiplyAlpha, bool createAlpha)
        {
            var texture = new Texture();
            texture.LoadFromStream(stream, preMultiplyAlpha, createAlpha);
            return texture;
        }

        public unsafe void LoadFromStream(Stream stream, bool preMultiplyAlpha, bool createAlpha)
        {
            var bmp = new Bitmap(stream);
            LoadFromBitmap(bmp, preMultiplyAlpha, createAlpha);
            bmp.Dispose();
        }

        public unsafe void LoadFromFile(string filename, bool preMultiplyAlpha, bool createAlpha)
        {
            if (File.Exists(filename))
            {
                var bmp = new Bitmap(filename);
                LoadFromBitmap(bmp, preMultiplyAlpha, createAlpha);
                bmp.Dispose();
            }
        }

        public static Texture FromFile(string filename, bool preMultiplyAlpha, bool createAlpha)
        {
            var result = new Texture();
            result.LoadFromFile(filename, preMultiplyAlpha, createAlpha);
            return result;
        }

        public static Texture FromBitmap(Bitmap image, bool preMultiplyAlpha, bool createAlpha)
        {
            var result = new Texture();
            result.LoadFromBitmap(image, preMultiplyAlpha, createAlpha);
            return result;
        }

        public unsafe void LoadFromBitmap(Bitmap image, bool preMultiplyAlpha, bool createAlpha)
        {
            Bind();
            _width = image.Width;
            _height = image.Height;
            var data = image.LockBits(new Rectangle(0, 0, _width, _height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            if (createAlpha)
            {
                byte* scan = (byte*)data.Scan0.ToPointer();
                for (int y = 0; y < data.Height; y++)
                {
                    for (int x = 0; x < data.Width; x++)
                    {
                        byte* pixel = scan + y * data.Stride + x * 4;

                        if (pixel[3] > 0 && (pixel[0] < 255 || pixel[1] < 255 || pixel[2] < 255))
                        {
                            pixel[3] = (byte)Math.Round(pixel[0] * 0.3f + pixel[1] * 0.59 + pixel[2] * 0.11);
                        }
                    }
                }
            }

            if (preMultiplyAlpha)
            {
                byte* scan = (byte*)data.Scan0.ToPointer();
                for (int y = 0; y < data.Height; y++)
                {
                    for (int x = 0; x < data.Width; x++)
                    {
                        byte* pixel = scan + y * data.Stride + x * 4;
                        pixel[0] = (byte)(pixel[0] * pixel[3] / 255);
                        pixel[1] = (byte)(pixel[1] * pixel[3] / 255);
                        pixel[2] = (byte)(pixel[2] * pixel[3] / 255);
                    }
                }
            }

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, _width, _height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            image.UnlockBits(data);
        }

        public void Dispose()
        {
            GL.DeleteTexture(_textureID);
        }
    }
}
