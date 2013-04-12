using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokGL
{
    public enum ShaderParamType
    {
        Int,
        Float,       
        Vec2,
        Vec3,
        Vec4,
        Texture,
        Model,
        Camera,
    }

    public enum ShaderAttributeType
    {
        Vertex,
        Color,
        UV,
    }

    public class ShaderAttribute
    {
        public ShaderAttributeType Type;
        public int Location;
        public string Name;

        public ShaderAttribute(ShaderAttributeType type, string name)
        {
            Type = type;
            Location = -1;
            Name = name;
        }
    }

    public class ShaderParam
    {
        public ShaderParamType Type;
        public int Location;
        public string Name;
        public string LongName;
        public int IntValue;
        public float FloatValue;
        public Matrix4 MatrixValue;
        public Vector2 Vec2Value;
        public Vector3 Vec3Value;
        public Vector4 Vec4Value;

        public ShaderParam(ShaderParamType type, string name, string longName, object value)
        {
            Type = type;
            Location = -1;
            Name = name;
            LongName = longName;
            IntValue = 0;
            FloatValue = 0;
            MatrixValue = Matrix4.Identity;
            Vec2Value = new Vector2();
            Vec3Value = new Vector3();
            Vec4Value = new Vector4();
            Set(value);
        }

        public void Set(object value)
        {
            switch (Type)
            {
                case ShaderParamType.Int: IntValue = (int)value; break;
                case ShaderParamType.Float: FloatValue = (float)value; break;
                case ShaderParamType.Vec2: Vec2Value = (Vector2)value; break;
                case ShaderParamType.Vec3: Vec3Value = (Vector3)value; break;
                case ShaderParamType.Vec4: Vec4Value = (Vector4)value; break;
                case ShaderParamType.Texture: IntValue = (int)value; break;
                case ShaderParamType.Model: MatrixValue = (Matrix4)value; break;
                case ShaderParamType.Camera: MatrixValue = (Matrix4)value; break;
            }
        }
    }

    public class Shader : IDisposable
    {
        private int _vertexShader;
        private int _fragShader;
        private int _shader;

        private Dictionary<ShaderAttributeType, ShaderAttribute> _attributes = new Dictionary<ShaderAttributeType, ShaderAttribute>();
        private Dictionary<string, ShaderParam> _parameters = new Dictionary<string, ShaderParam>();
        private string _camera;
        private string _model;

        public Shader(string vertexProgram, string fragmentProgram, IEnumerable<ShaderAttribute> attributes, IEnumerable<ShaderParam> parameters)
        {
            int result;

            //Create Vertex Shader
            _vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(_vertexShader, vertexProgram);
            GL.CompileShader(_vertexShader);
            GL.GetShader(_vertexShader, ShaderParameter.CompileStatus, out result);
            if (result == 0)
            {
                System.Diagnostics.Debug.WriteLine("Failed to compile vertex shader!");
                System.Diagnostics.Debug.WriteLine(GL.GetShaderInfoLog(_vertexShader));
                GL.DeleteShader(_vertexShader);
            }

            //Create Fragment Shader
            _fragShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(_fragShader, fragmentProgram);
            GL.CompileShader(_fragShader);
            GL.GetShader(_fragShader, ShaderParameter.CompileStatus, out result);
            if (result == 0)
            {
                System.Diagnostics.Debug.WriteLine("Failed to compile fragment shader!");
                System.Diagnostics.Debug.WriteLine(GL.GetShaderInfoLog(_fragShader));
                GL.DeleteShader(_fragShader);
            }

            //Link to program
            _shader = GL.CreateProgram();
            GL.AttachShader(_shader, _vertexShader);
            GL.AttachShader(_shader, _fragShader);
            GL.LinkProgram(_shader);
            GL.GetProgram(_shader, ProgramParameter.LinkStatus, out result);
            if (result == 0)
            {
                System.Diagnostics.Debug.WriteLine("Failed to link shader program!");
                System.Diagnostics.Debug.WriteLine(GL.GetProgramInfoLog(_shader));
                GL.DeleteProgram(_shader);
            }

            //Get Attribute and Parameter locations and apply values
            Activate();
            foreach (var a in attributes) _attributes.Add(a.Type, a);
            foreach (var key in _attributes.Keys)
            {
                var a = _attributes[key];
                a.Location = GL.GetAttribLocation(_shader, a.Name);
            }
            foreach (var p in parameters) _parameters.Add(p.Name, p);
            foreach (var key in _parameters.Keys)
            {
                var p = _parameters[key];
                p.Location = GL.GetUniformLocation(_shader, key);
                switch (p.Type)
                {
                    case ShaderParamType.Camera: _camera = key; break;
                    case ShaderParamType.Model: _model = key; break;
                }
            }
            ApplyParameters();
            Deactivate();
        }

        public void Activate()
        {
            GL.UseProgram(_shader);
        }

        public void Deactivate()
        {
            GL.UseProgram(0);
        }

        public void SetParameter(string name, object value)
        {
            _parameters[name].Set(value);
            ApplyParameter(_parameters[name]);
        }

        public void SetCamera(Matrix4 camera)
        {
            SetParameter(_camera, camera);
        }

        public void SetModel(Matrix4 model)
        {
            SetParameter(_model, model);
        }

        private void ApplyParameter(ShaderParam param)
        {
            switch (param.Type)
            {
                case ShaderParamType.Int: GL.Uniform1(param.Location, param.IntValue); break;
                case ShaderParamType.Float: GL.Uniform1(param.Location, param.FloatValue); break;
                case ShaderParamType.Texture: GL.Uniform1(param.Location, param.IntValue); break;
                case ShaderParamType.Vec2: GL.Uniform2(param.Location, ref param.Vec2Value); break;
                case ShaderParamType.Vec3: GL.Uniform3(param.Location, ref param.Vec3Value); break;
                case ShaderParamType.Vec4: GL.Uniform4(param.Location, ref param.Vec4Value); break;
                case ShaderParamType.Model: GL.UniformMatrix4(param.Location, 1, false, ref param.MatrixValue.Row0.X); break;
                case ShaderParamType.Camera: GL.UniformMatrix4(param.Location, 1, false, ref param.MatrixValue.Row0.X); break;
            }
        }

        public void ApplyParameters()
        {
            foreach (var param in _parameters.Values)
            {
                ApplyParameter(param);
            }
        }

        public void Dispose()
        {
            if (_shader > 0) GL.DeleteProgram(_shader); _shader = 0;
            if (_vertexShader > 0) GL.DeleteShader(_vertexShader); _vertexShader = 0;
            if (_fragShader > 0) GL.DeleteShader(_fragShader); _fragShader = 0;
        }

        public int GetAttributeLocation(ShaderAttributeType type)
        {
            return _attributes[type].Location;
        }
    }
}
