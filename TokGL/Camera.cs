using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokGL
{
    public enum CameraType
    {
        Perspective,
        Orthogonal,
        HUD,
    }

    public class Camera
    {
        private CameraType _cameraType = CameraType.Perspective;
        private float _distance = 5.0f;
        private Vector3 _lookAt = new Vector3(0, 0, 0);
        private Vector3 _position = new Vector3(0, 0, 5.0f);
        private bool _isDirty = true;
        private Vector3 _up = new Vector3(0, 1, 0);
        private int _width = 640;
        private int _height = 480;
        private float _fov = (float)Math.PI / 4.0f;
        private float _aspectRatio = 1.2f;
        private float _zNear = 1.0f;
        private float _zFar = 100.0f;
        private Matrix4 _viewProjectMatrix;
        private Matrix4 _invViewProjectMatrix;

        public CameraType CameraType
        {
            get { return _cameraType; }
            set
            {
                if (value != _cameraType)
                {
                    if (_cameraType == CameraType.Orthogonal && value == CameraType.Perspective)
                    {
                        var halfWidth = _width * _fov / (2.0f * _aspectRatio);
                        var a = (_lookAt - Right * halfWidth) - _position; a.Normalize();
                        var b = (_lookAt + Right * halfWidth) - _position; b.Normalize();
                        _fov = (float)Math.Acos(Vector3.Dot(a, b));
                        _zNear = Math.Max(_zNear, 1.0f);
                        _zFar = Math.Max(_zFar, _zNear + 1.0f);
                    }
                    else if (_cameraType == CameraType.Perspective && value == CameraType.Orthogonal)
                    {
                        var distance = (_lookAt - _position).Length;
                        var w = 2.0f * distance * (float)Math.Tan(_fov / 2.0d);
                        _fov = w * _aspectRatio / _width;
                    }
                    _cameraType = value;
                    _isDirty = true;
                }
            }
        }

        public Vector3 LookAt
        {
            get { return _lookAt; }
            set
            {
                _lookAt = value;
                _isDirty = true;
            }
        }

        public Vector3 Position
        {
            get { return _position; }
            set
            {

                _position = value;
                _isDirty = true;
            }
        }

        public Vector3 Up
        {
            get { return _up; }
            set
            {
                _up = value;
                _isDirty = true;
            }
        }

        public Vector3 Forward
        {
            get
            {
                var result = _lookAt - _position; result.Normalize();
                return result;
            }
        }

        public Vector3 Right
        {
            get
            {
                var result = Vector3.Cross(Forward, _up); result.Normalize();
                return result;
            }
        }

        public Vector3 UpEffective
        {
            get
            {
                var result = Vector3.Cross(Right, Forward); result.Normalize();
                return result;
            }
        }

        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                _aspectRatio = (float)_width / (float)_height;
                _isDirty = true;
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                _aspectRatio = (float)_width / (float)_height;
                _isDirty = true;
            }
        }

        public float Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                var direction = _position - _lookAt;
                direction.Normalize();
                _position = _lookAt + Vector3.Multiply(direction, _distance);
                _isDirty = true;
            }
        }

        public float Fov
        {
            get
            {
                switch (_cameraType)
                {
                    case TokGL.CameraType.Orthogonal: return _fov;
                    default: return 180.0f * _fov / (float)Math.PI;
                }
            }
            set
            {
                switch (_cameraType)
                {
                    case TokGL.CameraType.Orthogonal:
                        _fov = value;
                        break;
                    default: 
                        _fov = (float)Math.PI * value / 180.0f;
                        if (_fov <= 0.01f) _fov = 0.01f;
                        if (_fov >= 3.0f) _fov = 3.0f;
                        break;
                }
                _isDirty = true;
            }
        }

        public float ZNear
        {
            get { return _zNear; }
            set
            {
                _zNear = value;
                _isDirty = true;
            }
        }

        public float ZFar
        {
            get { return _zFar; }
            set
            {
                _zFar = value;
                _isDirty = true;
            }
        }

        private void CalculateProjection()
        {
            Matrix4 view = Matrix4.LookAt(_position, _lookAt, _up);
            Matrix4 projection = Matrix4.Identity;
            switch (_cameraType)
            {
                case CameraType.Perspective:
                    projection = Matrix4.CreatePerspectiveFieldOfView(_fov, _aspectRatio, _zNear, _zFar);
                    break;

                case CameraType.Orthogonal:
                    projection = Matrix4.CreateOrthographic(_width * _fov, _height * _fov, _zNear, _zFar);
                    break;

                case CameraType.HUD:
                    projection = Matrix4.CreateOrthographicOffCenter(0.0f, _width, _height, 0.0f, _zNear, _zFar);
                    break;
            }
            _viewProjectMatrix = view * projection;
            _invViewProjectMatrix = _viewProjectMatrix;
            _invViewProjectMatrix.Invert();
            _isDirty = false;
        }

        public Vector3 GetWorldCoordinates(Vector3 screenPos)
        {
            var temp = new Vector4(
                2.0f * screenPos.X / _width - 1.0f,
                -2.0f * screenPos.Y / _height + 1.0f,
                screenPos.Z, 1.0f);
            var result = Vector4.Transform(temp, InvViewProjectMatrix);
            result = result / result.W;
            return new Vector3(result.X, result.Y, result.Z);
        }

        public Ray GetWorldRay(Vector2 screenPos)
        {
            var pos = GetWorldCoordinates(new Vector3(screenPos.X, screenPos.Y, 0.0f));

            if (_cameraType == CameraType.Perspective)
            {
                var dir = pos - _position; dir.Normalize();
                return new Ray(_position, dir);
            }
            else
            {
                var pos2 = GetWorldCoordinates(new Vector3(screenPos.X, screenPos.Y, -1.0f));
                var dir = pos2 - pos; dir.Normalize();
                return new Ray(pos, dir);
            }
        }

        public Vector2 GetScreenCoordinates(Vector3 worldPos)
        {
            var temp = new Vector4(worldPos, 1.0f);
            temp = Vector4.Transform(temp, ViewProjectMatrix);
            temp.X = temp.X / temp.W * 0.5f + 0.5f;
            temp.Y = temp.Y / temp.W * 0.5f + 0.5f;
            var result = new Vector2((float)Math.Round(_width * temp.X), (float)Math.Round(_height * (1.0f - temp.Y)));
            return result;
        }

        public Matrix4 ViewProjectMatrix
        {
            get
            {
                if (_isDirty) CalculateProjection();
                return _viewProjectMatrix;
            }
        }

        public Matrix4 InvViewProjectMatrix
        {
            get
            {
                if (_isDirty) CalculateProjection();
                return _invViewProjectMatrix;
            }
        }

        public Camera Clone()
        {
            var result = new Camera();
            result.CameraType = this.CameraType;
            result.LookAt = this.LookAt;
            result.Position = this.Position;
            result.Up = this.Up;
            result.Width = this.Width;
            result.Height = this.Height;
            result.Fov = this.Fov;
            result.ZNear = this.ZNear;
            result.ZFar = this.ZFar;
            return result;
        }
    }
}
