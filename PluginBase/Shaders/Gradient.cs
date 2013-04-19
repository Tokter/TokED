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
    [Export("Gradient", typeof(ShaderDefinition)), PartCreationPolicy(CreationPolicy.Shared)]
    public class Gradient : ShaderDefinition
    {
        public Gradient()
        {
            VertexProgram =
@"#version 150

uniform mat4 camera;
uniform mat4 model;
in vec3 in_vertex;
//in vec4 in_color;
in vec2 in_uv;
out vec2 frag_TexCoord;
//out vec4 frag_Color;
    
void main()
{
    gl_Position = camera * model * vec4(in_vertex, 1);
    frag_TexCoord = in_uv;
    //frag_Color = in_color;
}";

            FragmentProgram =
@"#version 150

uniform vec4 ColorA;
uniform vec4 ColorB;
uniform float Angle;
//in vec4 frag_Color;
in vec2 frag_TexCoord;
out vec4 final_color;

void main()
{
    vec2 rot = vec2(cos(Angle), sin(Angle));
    vec2 pos = frag_TexCoord - vec2(0.5,0.5);
    vec2 result = (rot*pos) + vec2(0.5,0.5);
    final_color = mix(ColorA,ColorB,(result.x+result.y)/2.0);
}";
            AddAttribute(new ShaderAttribute(ShaderAttributeType.Vertex, "in_vertex"));
            //AddAttribute(new ShaderAttribute(ShaderAttributeType.Color, "in_color"));
            AddAttribute(new ShaderAttribute(ShaderAttributeType.UV, "in_uv"));
            AddParameter(new ShaderParam(ShaderParamType.Camera, "camera", "Camera Matrix", Matrix4.Identity));
            AddParameter(new ShaderParam(ShaderParamType.Model, "model", "Model Matrix", Matrix4.Identity));
            AddParameter(new ShaderParam(ShaderParamType.Color, "ColorA", "Color A", Color.Blue));
            AddParameter(new ShaderParam(ShaderParamType.Color, "ColorB", "Color B", Color.Red));
            AddParameter(new ShaderParam(ShaderParamType.Float, "Angle", "Angle", 0.0f));
        }
    }
}
