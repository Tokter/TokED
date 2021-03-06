﻿using OpenTK;
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
    [Export("Test", typeof(ShaderDefinition)), PartCreationPolicy(CreationPolicy.Shared)]
    public class TestShader : ShaderDefinition
    {
        public TestShader()
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

uniform vec4 testColor;
uniform float iGlobalTime;
uniform float pulseSpeed;
in vec4 frag_Color;
in vec2 frag_TexCoord;
out vec4 final_color;

void main()
{
    vec2 pos = frag_TexCoord - vec2(0.5,0.5);
    float len = length(pos);
    final_color = testColor * ((sin(pulseSpeed * iGlobalTime)+1.0)/2.0 * (1.0-len));
}";
            AddAttribute(new ShaderAttribute(ShaderAttributeType.Vertex, "in_vertex"));
            //AddAttribute(new ShaderAttribute(ShaderAttributeType.Color, "in_color"));
            AddAttribute(new ShaderAttribute(ShaderAttributeType.UV, "in_uv"));
            AddParameter(new ShaderParam(ShaderParamType.Camera, "camera", "Camera Matrix", Matrix4.Identity));
            AddParameter(new ShaderParam(ShaderParamType.Model, "model", "Model Matrix", Matrix4.Identity));
            AddParameter(new ShaderParam(ShaderParamType.Color, "testColor", "Test Color", Color.White));
            AddParameter(new ShaderParam(ShaderParamType.Time, "iGlobalTime", "Global Time", 0.0f));
            AddParameter(new ShaderParam(ShaderParamType.Float, "pulseSpeed", "Pulse Speed", 4.0f));
        }
    }
}
