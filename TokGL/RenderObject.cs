using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokGL
{
    public enum RenderObjectType
    {
        Arrays, Elements
    }

    public class RenderObject : IDisposable
    {
        private static Stack<RenderObject> _unusedRenderObjects = new Stack<RenderObject>();
        private static List<RenderObject> _createdRenderObjects = new List<RenderObject>();

        public static RenderObject Get()
        {
            RenderObject result;
            if (_unusedRenderObjects.Count <= 0)
            {
                result = new RenderObject();
                _createdRenderObjects.Add(result);
            }
            else
            {
                result = _unusedRenderObjects.Pop();
            }
            return result;
        }

        public static void Free(RenderObject obj)
        {
            _unusedRenderObjects.Push(obj);
        }

        public static void FreeAll()
        {
            foreach(var ro in _createdRenderObjects) ro.Dispose();
            _unusedRenderObjects.Clear();
            _createdRenderObjects.Clear();
        }

        private RenderObject()
        {
            int vbo;
            int vao;
            int ibo;
            GL.GenBuffers(1, out vbo);
            GL.GenVertexArrays(1, out vao);
            GL.GenBuffers(1, out ibo);
            VBO = vbo;
            VAO = vao;
            IBO = ibo;
            DrawStart = 0;
            DrawEnd = 0;
            Transformation = Matrix4.Identity;
            DrawType = BeginMode.TriangleStrip;
            ElementType = DrawElementsType.UnsignedByte;
            RenderType = RenderObjectType.Arrays;
        }

        public Matrix4 Transformation { get; set; }
        public Material Material  { get; set; }

        public int VBO;
        public int IBO;
        public int VAO;
        public int DrawStart { get; set; }
        public int DrawEnd { get; set; }

        public BeginMode DrawType { get; set; }
        public DrawElementsType ElementType { get; set; }
        public RenderObjectType RenderType { get; set; }

        public void Dispose()
        {
            if (VBO > 0) { GL.DeleteBuffers(1, ref VBO); VBO = 0; }
            if (IBO > 0) { GL.DeleteBuffers(1, ref IBO); IBO = 0; }
            if (VAO > 0) { GL.DeleteVertexArrays(1, ref VAO); VAO = 0; }
        }
    }
}
