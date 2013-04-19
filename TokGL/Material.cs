using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokGL
{
    public class Material : IDisposable
    {
        private Dictionary<TextureUnit, Texture> _textures = new Dictionary<TextureUnit, Texture>();

        public Shader Shader { get; set; }
        public bool DepthTest { get; set; }
        public bool AlphaBlend { get; set; }
        public bool SmoothLines { get; set; }

        public void Activate()
        {
            Shader.Activate();
            foreach (var texUnit in _textures.Keys)
            {
                GL.ActiveTexture(texUnit);
                _textures[texUnit].Bind();
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
            foreach (var texUnit in _textures.Keys)
            {
                GL.ActiveTexture(texUnit);
                _textures[texUnit].Unbind();
            }
            Shader.Deactivate();
        }

        public void Dispose()
        {
            if (Shader != null) { Shader.Dispose(); Shader = null; }
            foreach (var texture in _textures.Values)
            {
                texture.Dispose();
            }
            _textures.Clear();
        }

        public void AddTexture(TextureUnit texUnit, Texture texture)
        {
            _textures.Add(texUnit, texture);
        }

        public Texture this[TextureUnit texUnit]
        {
            get
            {
                if (!_textures.ContainsKey(texUnit)) return null;
                return _textures[texUnit]; 
            }
        }

        public int TextureCount
        {
            get { return _textures.Values.Count(); }
        }

        public static Material CreateColor()
        {
            var mat = new Material();

            var attributes = new List<ShaderAttribute>();
            attributes.Add(new ShaderAttribute(ShaderAttributeType.Vertex, "in_vertex"));
            attributes.Add(new ShaderAttribute(ShaderAttributeType.Color, "in_color"));

            var parameters = new List<ShaderParam>();
            parameters.Add(new ShaderParam(ShaderParamType.Camera, "camera", "Camera Matrix", Matrix4.Identity));
            parameters.Add(new ShaderParam(ShaderParamType.Model, "model", "Model Matrix", Matrix4.Identity));

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
}", attributes, parameters);
            mat.DepthTest = true;
            mat.AlphaBlend = true;
            mat.SmoothLines = true;
            return mat;
        }

        public static Material CreateTextureColor(Texture texture)
        {
            var mat = new Material();

            var attributes = new List<ShaderAttribute>();
            attributes.Add(new ShaderAttribute(ShaderAttributeType.Vertex, "in_vertex"));
            attributes.Add(new ShaderAttribute(ShaderAttributeType.Color, "in_color"));
            attributes.Add(new ShaderAttribute(ShaderAttributeType.UV, "in_uv"));

            var parameters = new List<ShaderParam>();
            parameters.Add(new ShaderParam(ShaderParamType.Camera, "camera", "Camera Matrix", Matrix4.Identity));
            parameters.Add(new ShaderParam(ShaderParamType.Model, "model", "Model Matrix", Matrix4.Identity));
            parameters.Add(new ShaderParam(ShaderParamType.Texture, "tex", "Texture", TextureUnit.Texture0));

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
}", attributes, parameters);
            mat.AddTexture(TextureUnit.Texture0, texture);
            mat.DepthTest = true;
            mat.AlphaBlend = true;
            mat.SmoothLines = true;
            return mat;
        }
    }
}
