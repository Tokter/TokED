using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TokGL
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SpriteVertex
    {
        public Vector2 Position;
        public Vector2 TextureCoords;
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public static int SizeInBytes
        {
            get { return Vector2.SizeInBytes + Vector2.SizeInBytes + 4; }
        }

        public static int PositionStart
        {
            get { return 0; }
        }

        public static int TextureStart
        {
            get { return Vector2.SizeInBytes; }
        }

        public static int ColorStart
        {
            get { return Vector2.SizeInBytes + Vector2.SizeInBytes; }
        }


    }

    public struct Sprite
    {
        public Vector4 Position;
        public Vector4 TextureCoordinates;
        public Color Color;
    }

    public class SpriteBatch : IDisposable
    {
        private Dictionary<Material, List<Sprite>> _sprites = new Dictionary<Material, List<Sprite>>(50);
        private SpriteVertex[] _buffer = new SpriteVertex[4000];
        private int[] _bufferI = new int[4000];
        private int _bufferSize;
        private int _bufferISize;
        private RenderManager _manager;

        private void Resize()
        {
            var newBuffer = new SpriteVertex[_buffer.Length * 2]; _buffer.CopyTo(newBuffer, 0);
            _buffer = newBuffer;
        }

        private void ResizeI()
        {
            var newBufferI = new int[_bufferI.Length * 2]; _bufferI.CopyTo(newBufferI, 0);
            _bufferI = newBufferI;
        }

        public void Flush(Matrix4 transformation)
        {
            End(transformation);
            Begin(_manager);
        }

        public void Begin(RenderManager manager)
        {
            _manager = manager;
            foreach (var key in _sprites.Keys)
            {
                _sprites[key].Clear();
            }
        }

        public void AddSprite(Material mat, float x, float y, int px1, int py1, int width, int height, Color color)
        {
            if (!_sprites.ContainsKey(mat)) _sprites.Add(mat, new List<Sprite>(1000));
            _sprites[mat].Add(new Sprite
            {
                Position = new Vector4(x, y, x + width, y + height),
                TextureCoordinates = new Vector4((float)px1 / (float)mat.Texture0.Width, (float)py1 / (float)mat.Texture0.Height, (float)(px1 + width) / (float)mat.Texture0.Width, (float)(py1 + height) / (float)mat.Texture0.Height),
                Color = color
            });
        }

        public void AddSprite(Material mat, Vector2 a, Vector2 b, int px1, int py1, int width, int height, Color color)
        {
            if (!_sprites.ContainsKey(mat)) _sprites.Add(mat, new List<Sprite>(1000));
            _sprites[mat].Add(new Sprite
            {
                Position = new Vector4(a.X, a.Y, b.X, b.Y),
                TextureCoordinates = new Vector4((float)px1 / (float)mat.Texture0.Width, (float)py1 / (float)mat.Texture0.Height, (float)(px1 + width) / (float)mat.Texture0.Width, (float)(py1 + height) / (float)mat.Texture0.Height),
                Color = color
            });
        }

        public void AddSprite(Material mat, Vector2 a, Vector2 b, float u1, float v1, float u2, float v2, Color color)
        {
            if (!_sprites.ContainsKey(mat)) _sprites.Add(mat, new List<Sprite>(1000));
            _sprites[mat].Add(new Sprite
            {
                Position = new Vector4(a.X, a.Y, b.X, b.Y),
                TextureCoordinates = new Vector4(u1, v1, u2, v2),
                Color = color
            });
        }

        public void AddText(Font font, int px, int py, string text, Color color, HorizontalAlignment horizontal, VerticalAlignment vertical)
        {
            if (!_sprites.ContainsKey(font.Material)) _sprites.Add(font.Material, new List<Sprite>(1000));
            font.CreateSprite(this, px, py, text, color, horizontal, vertical);
        }

        public void End()
        {
            End(Matrix4.Identity);
        }

        public void End(Matrix4 transformation)
        {
            foreach (var mat in _sprites.Keys)
            {
                if (_sprites[mat].Count > 0)
                {
                    _bufferSize = 0;
                    _bufferISize = 0;

                    //Fill Buffer
                    foreach (var sprite in _sprites[mat])
                    {
                        _bufferSize += 4; if (_bufferSize > _buffer.Length) Resize();
                        _buffer[_bufferSize - 4].Position = new Vector2(sprite.Position.X, sprite.Position.Y);
                        _buffer[_bufferSize - 4].TextureCoords = new Vector2(sprite.TextureCoordinates.X, sprite.TextureCoordinates.Y);
                        _buffer[_bufferSize - 4].R = sprite.Color.R;
                        _buffer[_bufferSize - 4].G = sprite.Color.G;
                        _buffer[_bufferSize - 4].B = sprite.Color.B;
                        _buffer[_bufferSize - 4].A = sprite.Color.A;

                        _buffer[_bufferSize - 3].Position = new Vector2(sprite.Position.X, sprite.Position.W);
                        _buffer[_bufferSize - 3].TextureCoords = new Vector2(sprite.TextureCoordinates.X, sprite.TextureCoordinates.W);
                        _buffer[_bufferSize - 3].R = sprite.Color.R;
                        _buffer[_bufferSize - 3].G = sprite.Color.G;
                        _buffer[_bufferSize - 3].B = sprite.Color.B;
                        _buffer[_bufferSize - 3].A = sprite.Color.A;

                        _buffer[_bufferSize - 2].Position = new Vector2(sprite.Position.Z, sprite.Position.Y);
                        _buffer[_bufferSize - 2].TextureCoords = new Vector2(sprite.TextureCoordinates.Z, sprite.TextureCoordinates.Y);
                        _buffer[_bufferSize - 2].R = sprite.Color.R;
                        _buffer[_bufferSize - 2].G = sprite.Color.G;
                        _buffer[_bufferSize - 2].B = sprite.Color.B;
                        _buffer[_bufferSize - 2].A = sprite.Color.A;

                        _buffer[_bufferSize - 1].Position = new Vector2(sprite.Position.Z, sprite.Position.W);
                        _buffer[_bufferSize - 1].TextureCoords = new Vector2(sprite.TextureCoordinates.Z, sprite.TextureCoordinates.W);
                        _buffer[_bufferSize - 1].R = sprite.Color.R;
                        _buffer[_bufferSize - 1].G = sprite.Color.G;
                        _buffer[_bufferSize - 1].B = sprite.Color.B;
                        _buffer[_bufferSize - 1].A = sprite.Color.A;

                        _bufferISize += 6; if (_bufferISize > _bufferI.Length) ResizeI();
                        _bufferI[_bufferISize - 6] = _bufferSize - 4;
                        _bufferI[_bufferISize - 5] = _bufferSize - 3;
                        _bufferI[_bufferISize - 4] = _bufferSize - 2;
                        _bufferI[_bufferISize - 3] = _bufferSize - 2;
                        _bufferI[_bufferISize - 2] = _bufferSize - 3;
                        _bufferI[_bufferISize - 1] = _bufferSize - 1;
                    }


                    //Get Render Object
                    var ro = RenderObject.Get();
                    ro.RenderType = RenderObjectType.Elements;
                    ro.DrawType = BeginMode.Triangles;
                    ro.ElementType = DrawElementsType.UnsignedInt;
                    ro.Material = mat;
                    ro.Transformation = transformation;

                    //The vertex array object has to be bound first, so that the following BindBuffer & VertexAtrribPointer calls
                    //are associated with it.
                    GL.BindVertexArray(ro.VAO);
                    GL.EnableVertexAttribArray(ro.Material.Shader.VertexLocation);
                    GL.EnableVertexAttribArray(ro.Material.Shader.ColorLocation);
                    GL.EnableVertexAttribArray(ro.Material.Shader.UVLocation);

                    GL.BindBuffer(BufferTarget.ArrayBuffer, ro.VBO);
                    GL.BufferData<SpriteVertex>(BufferTarget.ArrayBuffer, new IntPtr(_bufferSize * SpriteVertex.SizeInBytes), _buffer, BufferUsageHint.StreamDraw);

                    GL.BindBuffer(BufferTarget.ElementArrayBuffer, ro.IBO);
                    GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(_bufferISize * sizeof(uint)), _bufferI, BufferUsageHint.StreamDraw);

                    GL.VertexAttribPointer(ro.Material.Shader.VertexLocation, 3, VertexAttribPointerType.Float, false, SpriteVertex.SizeInBytes, SpriteVertex.PositionStart);
                    GL.VertexAttribPointer(ro.Material.Shader.UVLocation, 2, VertexAttribPointerType.Float, true, SpriteVertex.SizeInBytes, SpriteVertex.TextureStart);
                    GL.VertexAttribPointer(ro.Material.Shader.ColorLocation, 4, VertexAttribPointerType.UnsignedByte, true, SpriteVertex.SizeInBytes, SpriteVertex.ColorStart);

                    GL.BindVertexArray(0);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                    GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

                    ro.DrawStart = 0;
                    ro.DrawEnd = _bufferISize;

                    _manager.Add(ro);
                }
            }
        }


        public void Dispose()
        {
        }
    }
}
