using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
        Color,
        Time,
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

        public ShaderAttribute Clone()
        {
            return new ShaderAttribute(Type, Name);
        }
    }

    public class ShaderParam
    {
        public ShaderParamType Type;
        public int Location;
        public string Name;
        public string LongName { get; set; }
        public int IntValue { get; set; }
        public float FloatValue;
        public Vector2 Vec2Value;
        public Vector3 Vec3Value;
        public Vector4 Vec4Value;
        public Matrix4 MatrixValue;
        public TextureUnit TexUnit;

        private string _filename;
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

        public float X
        {
            get
            {
                switch (Type)
                {
                    case ShaderParamType.Vec2: return Vec2Value.X;
                    case ShaderParamType.Vec3: return Vec3Value.X;
                    case ShaderParamType.Vec4: return Vec4Value.X;
                }
                return 0.0f;
            }
            set
            {
                switch (Type)
                {
                    case ShaderParamType.Vec2: Vec2Value.X = value; break;
                    case ShaderParamType.Vec3: Vec3Value.X = value; break;
                    case ShaderParamType.Vec4: Vec4Value.X = value; break;
                }
            }
        }

        public float Y
        {
            get
            {
                switch (Type)
                {
                    case ShaderParamType.Vec2: return Vec2Value.Y;
                    case ShaderParamType.Vec3: return Vec3Value.Y;
                    case ShaderParamType.Vec4: return Vec4Value.Y;
                }
                return 0.0f;
            }
            set
            {
                switch (Type)
                {
                    case ShaderParamType.Vec2: Vec2Value.Y = value; break;
                    case ShaderParamType.Vec3: Vec3Value.Y = value; break;
                    case ShaderParamType.Vec4: Vec4Value.Y = value; break;
                }
            }
        }

        public float Z
        {
            get
            {
                switch (Type)
                {
                    case ShaderParamType.Vec3: return Vec3Value.Z;
                    case ShaderParamType.Vec4: return Vec4Value.Z;
                }
                return 0.0f;
            }
            set
            {
                switch (Type)
                {
                    case ShaderParamType.Vec3: Vec3Value.Z = value; break;
                    case ShaderParamType.Vec4: Vec4Value.Z = value; break;
                }
            }
        }

        public float W
        {
            get { return Vec4Value.W; }
            set { Vec4Value.W = value; }
        }

        public Color Color
        {
            get { return Color.FromArgb(FloatToByte(Vec4Value.W), FloatToByte(Vec4Value.X), FloatToByte(Vec4Value.Y), FloatToByte(Vec4Value.Z)); }
            set
            {
                Vec4Value.X = value.R / 255.0f;
                Vec4Value.Y = value.G / 255.0f;
                Vec4Value.Z = value.B / 255.0f;
                Vec4Value.W = value.A / 255.0f;
            }
        }

        private byte FloatToByte(float f)
        {
            return (byte)(Math.Min(Math.Max(f, 0.0f), 1.0f) * 255);
        }

        public ShaderParam()
        {
        }

        public ShaderParam(ShaderParamType type, string name, string longName, string filename, object value)
        {
            Type = type;
            Location = -1;
            Name = name;
            LongName = longName;
            Filename = filename;
            Set(value);
        }

        public ShaderParam(ShaderParamType type, string name, string longName, object value)
        {
            Type = type;
            Location = -1;
            Name = name;
            LongName = longName;
            Filename = null;
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
                case ShaderParamType.Texture: TexUnit = (TextureUnit)value; break;
                case ShaderParamType.Model: MatrixValue = (Matrix4)value; break;
                case ShaderParamType.Camera: MatrixValue = (Matrix4)value; break;
                case ShaderParamType.Color: Color = (Color)value; break;
                case ShaderParamType.Time: FloatValue = (float)value; break;
            }
        }

        public void CopyFrom(ShaderParam param)
        {
            Type = param.Type;
            Name = param.Name;
            LongName = param.LongName;
            IntValue = param.IntValue;
            FloatValue = param.FloatValue;
            Vec2Value = param.Vec2Value;
            Vec3Value = param.Vec3Value;
            Vec4Value = param.Vec4Value;
            MatrixValue = param.MatrixValue;
            TexUnit = param.TexUnit;
            Filename = param.Filename;
        }

        public ShaderParam Clone()
        {
            var result = new ShaderParam();
            result.CopyFrom(this);
            return result;
        }

        public bool IsNotTransient
        {
            get { return (Type != ShaderParamType.Model && Type != ShaderParamType.Camera && Type != ShaderParamType.Time); }
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
        private string _time;

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
                System.Diagnostics.Debug.WriteLine(GL.GetString(StringName.ShadingLanguageVersion));
                System.Diagnostics.Debug.WriteLine(GL.GetShaderInfoLog(_vertexShader));
            }

            //Create Fragment Shader
            _fragShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(_fragShader, fragmentProgram);
            GL.CompileShader(_fragShader);
            GL.GetShader(_fragShader, ShaderParameter.CompileStatus, out result);
            if (result == 0)
            {
                System.Diagnostics.Debug.WriteLine(GL.GetString(StringName.ShadingLanguageVersion));
                System.Diagnostics.Debug.WriteLine(GL.GetShaderInfoLog(_fragShader));
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

            //We don't need the shaders anymore...
            GL.DetachShader(_shader, _vertexShader);
            GL.DetachShader(_shader, _fragShader);
            GL.DeleteShader(_vertexShader); _vertexShader = 0;
            GL.DeleteShader(_fragShader); _fragShader = 0;

            //Get Attribute and Parameter locations and apply values
            Activate();
            foreach (var a in attributes) _attributes.Add(a.Type, a);
            foreach (var key in _attributes.Keys)
            {
                var a = _attributes[key];
                a.Location = GL.GetAttribLocation(_shader, a.Name);
                if (a.Location < 0) throw new Exception("Attribute Location Not Found!");
            }
            foreach (var p in parameters) _parameters.Add(p.Name, p);
            foreach (var key in _parameters.Keys)
            {
                var p = _parameters[key];
                p.Location = GL.GetUniformLocation(_shader, key);
                if (p.Location < 0) throw new Exception("Attribute Location Not Found!");
                switch (p.Type)
                {
                    case ShaderParamType.Camera: _camera = key; break;
                    case ShaderParamType.Model: _model = key; break;
                    case ShaderParamType.Time: _time = key; break;
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

        public void CopyParameter(ShaderParam param)
        {
            _parameters[param.Name].CopyFrom(param);
            ApplyParameter(_parameters[param.Name]);
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

        public void SetTime(float elapsedTime)
        {
            if (!String.IsNullOrEmpty(_time)) SetParameter(_time, elapsedTime);
        }

        private void ApplyParameter(ShaderParam param)
        {
            switch (param.Type)
            {
                case ShaderParamType.Int: GL.Uniform1(param.Location, param.IntValue); break;
                case ShaderParamType.Float: GL.Uniform1(param.Location, param.FloatValue); break;
                case ShaderParamType.Texture: GL.Uniform1(param.Location, (int)(param.TexUnit) - (int)(TextureUnit.Texture0)); break;
                case ShaderParamType.Vec2: GL.Uniform2(param.Location, ref param.Vec2Value); break;
                case ShaderParamType.Vec3: GL.Uniform3(param.Location, ref param.Vec3Value); break;
                case ShaderParamType.Vec4:
                case ShaderParamType.Color: GL.Uniform4(param.Location, ref param.Vec4Value); break;
                case ShaderParamType.Model: GL.UniformMatrix4(param.Location, 1, false, ref param.MatrixValue.Row0.X); break;
                case ShaderParamType.Camera: GL.UniformMatrix4(param.Location, 1, false, ref param.MatrixValue.Row0.X); break;
                case ShaderParamType.Time: GL.Uniform1(param.Location, param.FloatValue); break;
            }
        }

        public void ApplyParameters()
        {
            foreach (var param in _parameters.Values)
            {
                if (param.IsNotTransient) ApplyParameter(param);
            }
        }

        public IEnumerable<ShaderParam> Parameters
        {
            get { return _parameters.Values; }
        }

        public void Dispose()
        {
            if (_shader > 0) GL.DeleteProgram(_shader); _shader = 0;
        }

        public int GetAttributeLocation(ShaderAttributeType type)
        {
            return _attributes[type].Location;
        }

        public bool HasAttribute(ShaderAttributeType type)
        {
            return _attributes.ContainsKey(type);
        }
    }
}
