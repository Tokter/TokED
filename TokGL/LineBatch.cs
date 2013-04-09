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
    public struct LineVertex
    {
        public Vector3 Position;
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public static int SizeInBytes
        {
            get { return Vector3.SizeInBytes + 4; }
        }

        public static int PositionStart
        {
            get { return 0; }
        }

        public static int ColorStart
        {
            get { return Vector3.SizeInBytes; }
        }

    }

    public class LineBatch : IDisposable
    {
        private RenderManager _manager;
        private LineVertex[] _buffer = new LineVertex[100];
        private int _bufferSize;
        private Material _material;

        public LineBatch()
        {
            _material = Material.CreateColor();
            _material.AlphaBlend = true;
            _material.DepthTest = false;
            _material.SmoothLines = false;
        }

        public void Flush(Matrix4 transformation)
        {
            End(transformation);
            Begin(_manager);
        }

        public void Begin(RenderManager manager)
        {
            _manager = manager;
            _bufferSize = 0;
        }

        public void AddBox(float x1, float y1, float width, float height, Color color)
        {
            Add(new Vector2(x1, y1), new Vector2(x1 + width, y1), color);
            Add(new Vector2(x1 + width, y1 + height));
            Add(new Vector2(x1, y1 + height));
            Add(new Vector2(x1, y1));
        }

        public void Add(Vector3 a, Vector3 b, Color color)
        {
            _bufferSize += 2; if (_bufferSize > _buffer.Length) Resize();
            _buffer[_bufferSize - 2].Position = a;
            _buffer[_bufferSize - 2].R = color.R;
            _buffer[_bufferSize - 2].G = color.G;
            _buffer[_bufferSize - 2].B = color.B;
            _buffer[_bufferSize - 2].A = color.A;
            _buffer[_bufferSize - 1].Position = b;
            _buffer[_bufferSize - 1].R = color.R;
            _buffer[_bufferSize - 1].G = color.G;
            _buffer[_bufferSize - 1].B = color.B;
            _buffer[_bufferSize - 1].A = color.A;
        }

        public void Add(Vector2 a, Vector2 b, Color color)
        {
            _bufferSize += 2; if (_bufferSize > _buffer.Length) Resize();
            _buffer[_bufferSize - 2].Position = new Vector3(a.X, a.Y, 0);
            _buffer[_bufferSize - 2].R = color.R;
            _buffer[_bufferSize - 2].G = color.G;
            _buffer[_bufferSize - 2].B = color.B;
            _buffer[_bufferSize - 2].A = color.A;
            _buffer[_bufferSize - 1].Position = new Vector3(b.X, b.Y, 0);
            _buffer[_bufferSize - 1].R = color.R;
            _buffer[_bufferSize - 1].G = color.G;
            _buffer[_bufferSize - 1].B = color.B;
            _buffer[_bufferSize - 1].A = color.A;
        }

        public void Add(Vector2 a, Vector2 b, Color colorA, Color colorB)
        {
            _bufferSize += 2; if (_bufferSize > _buffer.Length) Resize();
            _buffer[_bufferSize - 2].Position = new Vector3(a.X, a.Y, 0);
            _buffer[_bufferSize - 2].R = colorA.R;
            _buffer[_bufferSize - 2].G = colorA.G;
            _buffer[_bufferSize - 2].B = colorA.B;
            _buffer[_bufferSize - 2].A = colorA.A;
            _buffer[_bufferSize - 1].Position = new Vector3(b.X, b.Y, 0);
            _buffer[_bufferSize - 1].R = colorB.R;
            _buffer[_bufferSize - 1].G = colorB.G;
            _buffer[_bufferSize - 1].B = colorB.B;
            _buffer[_bufferSize - 1].A = colorB.A;
        }

        public void Add(Vector2 a, Color color)
        {
            _bufferSize += 2; if (_bufferSize > _buffer.Length) Resize();
            _buffer[_bufferSize - 2].Position = _buffer[_bufferSize - 3].Position;
            _buffer[_bufferSize - 2].R = _buffer[_bufferSize - 3].R;
            _buffer[_bufferSize - 2].G = _buffer[_bufferSize - 3].G;
            _buffer[_bufferSize - 2].B = _buffer[_bufferSize - 3].B;
            _buffer[_bufferSize - 2].A = _buffer[_bufferSize - 3].A;

            _buffer[_bufferSize - 1].Position = new Vector3(a.X, a.Y, 0);
            _buffer[_bufferSize - 1].R = color.R;
            _buffer[_bufferSize - 1].G = color.G;
            _buffer[_bufferSize - 1].B = color.B;
            _buffer[_bufferSize - 1].A = color.A;
        }

        public void Add(Vector2 a)
        {
            _bufferSize += 2; if (_bufferSize > _buffer.Length) Resize();
            _buffer[_bufferSize - 2].Position = _buffer[_bufferSize - 3].Position;
            _buffer[_bufferSize - 2].R = _buffer[_bufferSize - 3].R;
            _buffer[_bufferSize - 2].G = _buffer[_bufferSize - 3].G;
            _buffer[_bufferSize - 2].B = _buffer[_bufferSize - 3].B;
            _buffer[_bufferSize - 2].A = _buffer[_bufferSize - 3].A;

            _buffer[_bufferSize - 1].Position = new Vector3(a.X, a.Y, 0);
            _buffer[_bufferSize - 1].R = _buffer[_bufferSize - 3].R;
            _buffer[_bufferSize - 1].G = _buffer[_bufferSize - 3].G;
            _buffer[_bufferSize - 1].B = _buffer[_bufferSize - 3].B;
            _buffer[_bufferSize - 1].A = _buffer[_bufferSize - 3].A;
        }

        public void Add(Vector3 a)
        {
            _bufferSize += 2; if (_bufferSize > _buffer.Length) Resize();
            _buffer[_bufferSize - 2].Position = _buffer[_bufferSize - 3].Position;
            _buffer[_bufferSize - 2].R = _buffer[_bufferSize - 3].R;
            _buffer[_bufferSize - 2].G = _buffer[_bufferSize - 3].G;
            _buffer[_bufferSize - 2].B = _buffer[_bufferSize - 3].B;
            _buffer[_bufferSize - 2].A = _buffer[_bufferSize - 3].A;

            _buffer[_bufferSize - 1].Position = a;
            _buffer[_bufferSize - 1].R = _buffer[_bufferSize - 3].R;
            _buffer[_bufferSize - 1].G = _buffer[_bufferSize - 3].G;
            _buffer[_bufferSize - 1].B = _buffer[_bufferSize - 3].B;
            _buffer[_bufferSize - 1].A = _buffer[_bufferSize - 3].A;
        }

        public void End()
        {
            End(Matrix4.Identity);
        }

        public void End(Matrix4 transformation)
        {
            if (_bufferSize > 0)
            {
                //Get Render Object
                var ro = RenderObject.Get();
                ro.RenderType = RenderObjectType.Arrays;
                ro.DrawType = BeginMode.Lines;
                ro.Material = _material;
                ro.Transformation = transformation;

                //The vertex array object has to be bound first, so that the following BindBuffer & VertexAtrribPointer calls
                //are associated with it.
                GL.BindVertexArray(ro.VAO);
                GL.EnableVertexAttribArray(ro.Material.Shader.VertexLocation);
                GL.EnableVertexAttribArray(ro.Material.Shader.ColorLocation);

                GL.BindBuffer(BufferTarget.ArrayBuffer, ro.VBO);
                GL.BufferData<LineVertex>(BufferTarget.ArrayBuffer, new IntPtr(_bufferSize * LineVertex.SizeInBytes), _buffer, BufferUsageHint.StreamDraw);

                GL.VertexAttribPointer(ro.Material.Shader.VertexLocation, 3, VertexAttribPointerType.Float, false, LineVertex.SizeInBytes, LineVertex.PositionStart);
                GL.VertexAttribPointer(ro.Material.Shader.ColorLocation, 4, VertexAttribPointerType.UnsignedByte, true, LineVertex.SizeInBytes, LineVertex.ColorStart);

                GL.BindVertexArray(0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                ro.DrawStart = 0;
                ro.DrawEnd = _bufferSize;

                _manager.Add(ro);
            }
        }

        private void Resize()
        {
            var newBuffer = new LineVertex[_buffer.Length * 2]; _buffer.CopyTo(newBuffer, 0);
            _buffer = newBuffer;
        }

        public void Dispose()
        {
            _material.Dispose();
        }
    }
}
