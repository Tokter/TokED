using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED;
using TokGL;

namespace PluginBase.Shaders
{
    [Export("Checker", typeof(ShaderDefinition)), PartCreationPolicy(CreationPolicy.Shared)]
    public class Checker : ShaderDefinition
    {
        public Checker()
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

uniform vec4 color0;
uniform vec4 color1;
uniform int frequency;
in vec4 frag_Color;
in vec2 frag_TexCoord;
out vec4 final_color;

void main()
{
    vec2 tcmod = mod(frag_TexCoord * float(frequency), 1.0);

    if (tcmod.s < 0.5)
    {
        if (tcmod.t < 0.5) final_color = color1 * frag_Color; else final_color = color0 * frag_Color;
    }
    else
    {
        if (tcmod.t < 0.5) final_color = color0 * frag_Color; else final_color = color1 * frag_Color;
    }
}";

            AddAttribute(new ShaderAttribute(ShaderAttributeType.Vertex, "in_vertex"));
            AddAttribute(new ShaderAttribute(ShaderAttributeType.Color, "in_color"));
            AddAttribute(new ShaderAttribute(ShaderAttributeType.UV, "in_uv"));
            AddParameter(new ShaderParam(ShaderParamType.Camera, "camera", "Camera Matrix", Matrix4.Identity));
            AddParameter(new ShaderParam(ShaderParamType.Model, "model", "Model Matrix", Matrix4.Identity));
            AddParameter(new ShaderParam(ShaderParamType.Color, "color0", "Color A", Color.White));
            AddParameter(new ShaderParam(ShaderParamType.Color, "color1", "Color B", Color.Black));
            AddParameter(new ShaderParam(ShaderParamType.Int, "frequency", "Frequency", 1));
        }
    }
}
