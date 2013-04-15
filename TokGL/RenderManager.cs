using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokGL
{
    public class RenderManager: IDisposable
    {
        public Camera Camera { get; set; }
        private List<RenderObject> _renderObjects = new List<RenderObject>();

        public void Begin()
        {
            foreach (var ro in _renderObjects)
            {
                RenderObject.Free(ro);
            }
            _renderObjects.Clear();
        }

        public void SetViewPort()
        {
            GL.Viewport(0, 0, Camera.Width, Camera.Height);
        }

        public void Add(RenderObject ro)
        {
            _renderObjects.Add(ro);
        }

        //public void AddRange(IEnumerable<RenderObject> collection)
        //{
        //    _renderObjects.AddRange(collection);
        //}

        public void Flush()
        {
            End();
            Begin();
        }

        public void End()
        {
            Material currentMaterial = null;
            foreach (var ro in _renderObjects)
            {
                if (ro.DrawEnd > ro.DrawStart)
                {
                    if (ro.Material != currentMaterial)
                    {
                        if (currentMaterial != null) currentMaterial.Deactivate();
                        currentMaterial = ro.Material;
                        currentMaterial.Activate();
                    }
                    currentMaterial.Shader.SetModel(ro.Transformation);
                    currentMaterial.Shader.SetCamera(Camera.ViewProjectMatrix);
                    currentMaterial.Shader.ApplyParameters();

                    GL.BindVertexArray(ro.VAO);
                    switch (ro.RenderType)
                    {
                        case RenderObjectType.Arrays: GL.DrawArrays(ro.DrawType, ro.DrawStart, ro.DrawEnd - ro.DrawStart + 1); break;
                        case RenderObjectType.Elements: GL.DrawElements(ro.DrawType, ro.DrawEnd - ro.DrawStart + 1, ro.ElementType, ro.DrawStart); break;
                    }
                    GL.BindVertexArray(0);
                }
            }
        }

        public void Dispose()
        {
            foreach (var ro in _renderObjects)
            {
                RenderObject.Free(ro);
            }
            _renderObjects.Clear();
        }
    }
}
