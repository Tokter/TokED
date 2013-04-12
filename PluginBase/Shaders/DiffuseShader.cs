﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED;

namespace PluginBase.Shaders
{
    [Export("Diffuse", typeof(Shader)), PartCreationPolicy(CreationPolicy.Shared)]
    public class DiffuseShader : Shader
    {
        public DiffuseShader()
        {
            Texture0Enabled = true;
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

uniform sampler2D tex;
in vec4 frag_Color;
in vec2 frag_TexCoord;
out vec4 final_color;

void main()
{
    final_color = texture(tex, frag_TexCoord) * frag_Color;
}";
        }
    }
}