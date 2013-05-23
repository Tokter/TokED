using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TokED;

namespace PluginBase.Components
{
    [Export("Transformation", typeof(Component)), HasIcon("Transformation.png"), PartCreationPolicy(CreationPolicy.NonShared)]
    public class Transformation : Component
    {
        private Matrix4 _transform = Matrix4.Identity;
        private Vector3 _translation;
        private Vector3 _rotation;
        private Vector3 _scale = new Vector3(1, 1, 1);
        private bool _isDirty = true;

        public Vector3 Translation
        {
            get { return _translation; }
            set { _translation = value; _isDirty = true; }
        }

        public float PosX
        {
            get { return _translation.X; }
            set { _translation.X = value; _isDirty = true; }
        }

        public float PosY
        {
            get { return _translation.Y; }
            set { _translation.Y = value; _isDirty = true; }
        }

        public float PosZ
        {
            get { return _translation.Z; }
            set { _translation.Z = value; _isDirty = true; }
        }

        public Vector3 Rotation
        {
            get { return _rotation; }
            set { _rotation = value; _isDirty = true; }
        }

        public float RotX
        {
            get { return 180.0f * _rotation.X / (float)Math.PI; }
            set { _rotation.X = (float)Math.PI * value / 180.0f; _isDirty = true; }
        }

        public float RotY
        {
            get { return 180.0f * _rotation.Y / (float)Math.PI; ; }
            set { _rotation.Y = (float)Math.PI * value / 180.0f; _isDirty = true; }
        }

        public float RotZ
        {
            get { return 180.0f * _rotation.Z / (float)Math.PI; ; }
            set { _rotation.Z = (float)Math.PI * value / 180.0f; _isDirty = true; }
        }

        public Vector3 Scale
        {
            get { return _scale; }
            set { _scale = value; _isDirty = true; }
        }

        public float ScaleX
        {
            get { return _scale.X; }
            set { _scale.X = value; _isDirty = true; }
        }

        public float ScaleY
        {
            get { return _scale.Y; }
            set { _scale.Y = value; _isDirty = true; }
        }

        public float ScaleZ
        {
            get { return _scale.Z; }
            set { _scale.Z = value; _isDirty = true; }
        }

        public Matrix4 Transform
        {
            get
            {
                if (_isDirty)
                {
                    _isDirty = false;
                    _transform = Matrix4.Scale(Scale)
                        * Matrix4.CreateRotationX(Rotation.X)
                        * Matrix4.CreateRotationY(Rotation.Y)
                        * Matrix4.CreateRotationZ(Rotation.Z)
                        * Matrix4.CreateTranslation(Translation);
                }
                return _transform;
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("PosX", _translation.X.ToString());
            writer.WriteAttributeString("PosY", _translation.Y.ToString());
            writer.WriteAttributeString("PosZ", _translation.Z.ToString());
            writer.WriteAttributeString("RotX", _rotation.X.ToString());
            writer.WriteAttributeString("RotY", _rotation.Y.ToString());
            writer.WriteAttributeString("RotZ", _rotation.Z.ToString());
            writer.WriteAttributeString("ScaleX", _scale.X.ToString());
            writer.WriteAttributeString("ScaleY", _scale.Y.ToString());
            writer.WriteAttributeString("ScaleZ", _scale.Z.ToString());
        }

        public override void ReadXml(XmlReader reader)
        {
            if (reader.HasAttributes)
            {
                _translation = new Vector3(
                    Convert.ToSingle(reader.GetAttribute("PosX")),
                    Convert.ToSingle(reader.GetAttribute("PosY")),
                    Convert.ToSingle(reader.GetAttribute("PosZ")));
                _rotation = new Vector3(
                    Convert.ToSingle(reader.GetAttribute("RotX")),
                    Convert.ToSingle(reader.GetAttribute("RotY")),
                    Convert.ToSingle(reader.GetAttribute("RotZ")));
                _scale = new Vector3(
                    Convert.ToSingle(reader.GetAttribute("ScaleX")),
                    Convert.ToSingle(reader.GetAttribute("ScaleY")),
                    Convert.ToSingle(reader.GetAttribute("ScaleZ")));
                _isDirty = true;
            }
        }
    }
}
