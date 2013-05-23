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
    [Export("Project", typeof(GameObject)), HasIcon("Project.png"), PartCreationPolicy(CreationPolicy.Shared), DoesNotAllowChild("Project")]
    public class Project : GameObject
    {
        private int _width = 1280;
        private int _height = 768;

        public Project()
        {
            Name = "Project";
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; NotifyChange(); }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; NotifyChange(); }
        }

        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            _width = Convert.ToInt32(reader.GetAttribute("Width"));
            _height = Convert.ToInt32(reader.GetAttribute("Height"));
        }

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteAttributeString("Width", _width.ToString());
            writer.WriteAttributeString("Height", _height.ToString());
        }
    }
}
