using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED;
using TokGL;

namespace PluginBase.Shaders
{
    [Export("DiffuseWithDetail", typeof(ShaderDefinition)), PartCreationPolicy(CreationPolicy.Shared)]
    public class DiffuseWithDetail : ShaderDefinition
    {
        public DiffuseWithDetail()
        {
            VertexProgram =
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
}";

            FragmentProgram =
@"#version 150

uniform sampler2D tex;
uniform sampler2D detail;
in vec4 frag_Color;
in vec2 frag_TexCoord;
out vec4 final_color;

void main()
{
    final_color = texture(tex, frag_TexCoord) * texture(detail, frag_TexCoord * 4) * frag_Color;
}";

            AddAttribute(new ShaderAttribute(ShaderAttributeType.Vertex, "in_vertex"));
            AddAttribute(new ShaderAttribute(ShaderAttributeType.Color, "in_color"));
            AddAttribute(new ShaderAttribute(ShaderAttributeType.UV, "in_uv"));
            AddParameter(new ShaderParam(ShaderParamType.Camera, "camera", "Camera Matrix", Matrix4.Identity));
            AddParameter(new ShaderParam(ShaderParamType.Model, "model", "Model Matrix", Matrix4.Identity));
            AddParameter(new ShaderParam(ShaderParamType.Texture, "tex", "Base Texture", TextureUnit.Texture0));
            AddParameter(new ShaderParam(ShaderParamType.Texture, "detail", "Detail Texture", TextureUnit.Texture1));
        }
    }
}
