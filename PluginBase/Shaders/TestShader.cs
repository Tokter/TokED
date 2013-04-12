using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED;

namespace PluginBase.Shaders
{
    [Export("Test", typeof(Shader)), PartCreationPolicy(CreationPolicy.Shared)]
    public class TestShader : Shader
    {
        public TestShader()
        {
            Texture0Enabled = false;
            Texture1Enabled = false;
            Texture2Enabled = false;
            Texture3Enabled = false;

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

uniform float freq1;
uniform float freq2;
uniform float freq3;
in vec4 frag_Color;
in vec2 frag_TexCoord;
out vec4 final_color;

void main()
{
    final_color = vec4(sin(frag_TexCoord.t * freq1),sin(frag_TexCoord.s * freq2),sin(frag_TexCoord.s * frag_TexCoord.t * freq3),1);
}";

            AddParameter(new ShaderParameter { Type = ShaderParamaterType.Float, Name = "freq1", Desc = "Frequency 1", Value = 3.0f });
            AddParameter(new ShaderParameter { Type = ShaderParamaterType.Float, Name = "freq2", Desc = "Frequency 2", Value = 6.0f });
            AddParameter(new ShaderParameter { Type = ShaderParamaterType.Float, Name = "freq3", Desc = "Frequency 3", Value = 9.0f });

        }
    }
}
