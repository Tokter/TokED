﻿using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokGL
{
    public class Material : IDisposable
    {
        public Shader Shader { get; set; }
        public Texture Texture0 { get; set; }
        public Texture Texture1 { get; set; }
        public Texture Texture2 { get; set; }
        public Texture Texture3 { get; set; }
        public bool DepthTest { get; set; }
        public bool AlphaBlend { get; set; }
        public bool SmoothLines { get; set; }

        public void Activate()
        {
            Shader.Activate();
            if (Texture0 != null)
            {
                GL.ActiveTexture(TextureUnit.Texture0);
                Texture0.Bind();
            }
            if (Texture1 != null)
            {
                GL.ActiveTexture(TextureUnit.Texture1);
                Texture1.Bind();
            }
            if (Texture2 != null)
            {
                GL.ActiveTexture(TextureUnit.Texture2);
                Texture2.Bind();
            }
            if (Texture3 != null)
            {
                GL.ActiveTexture(TextureUnit.Texture3);
                Texture3.Bind();
            }
            if (DepthTest)
            {
                GL.Enable(EnableCap.DepthTest);
                GL.DepthFunc(DepthFunction.Less);
            }
            else
            {
                GL.Disable(EnableCap.DepthTest);
            }
            if (AlphaBlend)
            {
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            }
            else
            {
                GL.Disable(EnableCap.Blend);
            }
            if (SmoothLines)
            {
                GL.Enable(EnableCap.LineSmooth);
            }
            else
            {
                GL.Disable(EnableCap.LineSmooth);
            }
        }

        public void Deactivate()
        {
            if (Texture0 != null)
            {
                GL.ActiveTexture(TextureUnit.Texture0);
                Texture0.Unbind();
            }
            if (Texture1 != null)
            {
                GL.ActiveTexture(TextureUnit.Texture1);
                Texture1.Unbind();
            }
            if (Texture2 != null)
            {
                GL.ActiveTexture(TextureUnit.Texture2);
                Texture2.Unbind();
            }
            if (Texture3 != null)
            {
                GL.ActiveTexture(TextureUnit.Texture3);
                Texture3.Unbind();
            }
            Shader.Deactivate();
        }

        public void Dispose()
        {
            if (Shader != null) { Shader.Dispose(); Shader = null; }
            if (Texture0 != null) { Texture0.Dispose(); Texture0 = null; }
            if (Texture1 != null) { Texture1.Dispose(); Texture1 = null; }
            if (Texture2 != null) { Texture2.Dispose(); Texture2 = null; }
            if (Texture3 != null) { Texture3.Dispose(); Texture3 = null; }
        }

        public static Material CreateColor()
        {
            var mat = new Material();
            mat.Shader = new Shader(
@"#version 150

uniform mat4 camera;
uniform mat4 model;
in vec3 in_vertex;
in vec4 in_color;
out vec4 frag_color;
    
void main()
{
    gl_Position = camera * model * vec4(in_vertex, 1);
    frag_color = in_color;
}",
@"#version 150

in vec4 frag_color;
out vec4 final_color;

void main()
{
    final_color = frag_color;
}");
            mat.Texture0 = null;
            mat.Texture1 = null;
            mat.Texture2 = null;
            mat.Texture3 = null;
            mat.DepthTest = true;
            mat.AlphaBlend = true;
            mat.SmoothLines = true;
            return mat;
        }

        public static Material CreateTextureColor(Texture texture)
        {
            var mat = new Material();
            mat.Shader = new Shader(
@"#version 150

uniform mat4 camera;
uniform mat4 model;
in vec3 in_vertex;
in vec4 in_color;
in vec2 in_uv;
out vec2 frag_TexCoord;
out vec4 frag_Color;
    
void main()
{
    gl_Position = camera * model * vec4(in_vertex, 1);
    frag_TexCoord = in_uv;
    frag_Color = in_color;
}",
@"#version 150

uniform sampler2D tex;
in vec4 frag_Color;
in vec2 frag_TexCoord;
out vec4 final_color;

void main()
{
    final_color = texture(tex, frag_TexCoord) * frag_Color;
}");
            mat.Texture0 = texture;
            mat.Texture1 = null;
            mat.Texture2 = null;
            mat.Texture3 = null;
            mat.DepthTest = true;
            mat.AlphaBlend = true;
            mat.SmoothLines = true;
            return mat;
        }
    }
}