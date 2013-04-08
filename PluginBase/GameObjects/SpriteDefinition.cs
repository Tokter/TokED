using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TokED;

namespace PluginBase.GameObjects
{
    [Export("SpriteDefinition", typeof(GameObject)), PartCreationPolicy(CreationPolicy.NonShared), RequiresParent("Texture"), DoesNotAllowChildren()]
    public class SpriteDefinition : GameObject
    {
        private Vector2 _p1 = new Vector2(0, 0);
        private Vector2 _p2 = new Vector2(1, 0);
        private Vector2 _p3 = new Vector2(1, 1);
        private Vector2 _p4 = new Vector2(0, 1);
        private Vector2 _origin = new Vector2(0.5f, 0.5f);

        public SpriteDefinition()
        {
            Name = "SpriteDefinition";
        }

        public override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);
            switch (name)
            {
                case "Parent":
                    var texture = Parent as GameObjects.Texture;
                    _p1 = new Vector2(0, 0);
                    _p2 = new Vector2(texture.Width, 0);
                    _p3 = new Vector2(texture.Width, texture.Height);
                    _p4 = new Vector2(0, texture.Height);
                    _origin = new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);
                    break;
            }
        }

        public Vector2 P1
        {
            get { return _p1; }
            set
            {
                _p1 = value;
                NotifyChange();
                NotifyChange("P1X");
                NotifyChange("P1Y");
            }
        }

        public float P1X
        {
            get { return _p1.X; }
            set { _p1.X = value; NotifyChange(); }
        }

        public float P1Y
        {
            get { return _p1.Y; }
            set { _p1.Y = value; NotifyChange(); }
        }

        public Vector2 P2
        {
            get { return _p2; }
            set
            {
                _p2 = value;
                NotifyChange();
                NotifyChange("P2X");
                NotifyChange("P2Y");
            }
        }

        public float P2X
        {
            get { return _p2.X; }
            set { _p2.X = value; NotifyChange(); }
        }

        public float P2Y
        {
            get { return _p2.Y; }
            set { _p2.Y = value; NotifyChange(); }
        }

        public Vector2 P3
        {
            get { return _p3; }
            set
            {
                _p3 = value;
                NotifyChange();
                NotifyChange("P3X");
                NotifyChange("P3Y");
            }
        }

        public float P3X
        {
            get { return _p3.X; }
            set { _p3.X = value; NotifyChange(); }
        }

        public float P3Y
        {
            get { return _p3.Y; }
            set { _p3.Y = value; NotifyChange(); }
        }

        public Vector2 P4
        {
            get { return _p4; }
            set
            {
                _p4 = value;
                NotifyChange();
                NotifyChange("P4X");
                NotifyChange("P4Y");
            }
        }

        public float P4X
        {
            get { return _p4.X; }
            set { _p4.X = value; NotifyChange(); }
        }

        public float P4Y
        {
            get { return _p4.Y; }
            set { _p4.Y = value; NotifyChange(); }
        }

        public Vector2 Origin
        {
            get { return _origin; }
            set
            {
                _origin = value;
                NotifyChange();
                NotifyChange("OriginX");
                NotifyChange("OriginY");
            }
        }

        public float OriginX
        {
            get { return _origin.X; }
            set { _origin.X = value; NotifyChange(); }
        }

        public float OriginY
        {
            get { return _origin.Y; }
            set { _origin.Y = value; NotifyChange(); }
        }

        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            P1 = new Vector2(Convert.ToSingle(reader.GetAttribute("P1X")), Convert.ToSingle(reader.GetAttribute("P1Y")));
            P2 = new Vector2(Convert.ToSingle(reader.GetAttribute("P2X")), Convert.ToSingle(reader.GetAttribute("P2Y")));
            P3 = new Vector2(Convert.ToSingle(reader.GetAttribute("P3X")), Convert.ToSingle(reader.GetAttribute("P3Y")));
            P4 = new Vector2(Convert.ToSingle(reader.GetAttribute("P4X")), Convert.ToSingle(reader.GetAttribute("P4Y")));
            Origin = new Vector2(Convert.ToSingle(reader.GetAttribute("OriginX")), Convert.ToSingle(reader.GetAttribute("OriginY")));
        }

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteAttributeString("P1X", P1.X.ToString());
            writer.WriteAttributeString("P1Y", P1.Y.ToString());
            writer.WriteAttributeString("P2X", P2.X.ToString());
            writer.WriteAttributeString("P2Y", P2.Y.ToString());
            writer.WriteAttributeString("P3X", P3.X.ToString());
            writer.WriteAttributeString("P3Y", P3.Y.ToString());
            writer.WriteAttributeString("P4X", P4.X.ToString());
            writer.WriteAttributeString("P4Y", P4.Y.ToString());
            writer.WriteAttributeString("OriginX", Origin.X.ToString());
            writer.WriteAttributeString("OriginY", Origin.Y.ToString());
        }
    }
}
