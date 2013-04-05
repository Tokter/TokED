using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokGL
{
    public class Shader : IDisposable
    {
        private int _vertexShader;
        private int _fragShader;
        private int _shader;

        private int _vertexLocation;
        private int _colorLocation;
        private int _uvLocation;

        private int _textureLocation;
        private int _texture;

        private int _modelLocation;
        private Matrix4 _model;

        private int _cameraLocation;
        private Matrix4 _camera;

        public Shader(string vertexProgram, string fragmentProgram)
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

            Activate();
            FindLocations();
            Deactivate();
        }

        public virtual void FindLocations()
        {
            //Attributes
            _vertexLocation = GetAttribLocation("in_vertex");
            _colorLocation = GetAttribLocation("in_color");
            _uvLocation = GetAttribLocation("in_uv");

            //Uniforms
            _cameraLocation = GetUniformLocation("camera");
            _modelLocation = GetUniformLocation("model");
            _textureLocation = GetAttribLocation("tex");
        }

        public void Activate()
        {
            GL.UseProgram(_shader);
        }

        public void Deactivate()
        {
            GL.UseProgram(0);
        }

        public int GetAttribLocation(string name)
        {
            return GL.GetAttribLocation(_shader, name);
        }

        public int GetUniformLocation(string name)
        {
            return GL.GetUniformLocation(_shader, name);
        }

        public int VertexLocation
        {
            get { return _vertexLocation; }
        }

        public int ColorLocation
        {
            get { return _colorLocation; }
        }

        public int UVLocation
        {
            get { return _uvLocation; }
        }

        public int TextureLocation
        {
            get { return _textureLocation; }
        }

        public Matrix4 Model
        {
            get { return _model; }
            set
            {
                _model = value;
                GL.UniformMatrix4(_modelLocation, false, ref _model);
            }
        }

        public Matrix4 Camera
        {
            get { return _camera; }
            set
            {
                _camera = value;
                GL.UniformMatrix4(_cameraLocation, false, ref _camera);
            }
        }

        public int Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
                GL.Uniform1(_textureLocation, _texture);
            }
        }

        public void Dispose()
        {
            if (_shader > 0) GL.DeleteProgram(_shader); _shader = 0;
            if (_vertexShader > 0) GL.DeleteShader(_vertexShader); _vertexShader = 0;
            if (_fragShader > 0) GL.DeleteShader(_fragShader); _fragShader = 0;
        }

    }
}
