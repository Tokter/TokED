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

    [Export("Texture", typeof(GameObject)), PartCreationPolicy(CreationPolicy.NonShared), AllowsChild("Sprite")]
    public class Texture : GameObject
    {
        private string _fileName = "";
        private bool _preMultiplyAlpha = true;

        public Texture()
        {
            Name = "Texture";
            TextureName = "Texture";
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; NotifyChange(); }
        }

        public bool PreMultiplyAlpha
        {
            get { return _preMultiplyAlpha; }
            set { _preMultiplyAlpha = value; NotifyChange(); }
        }

        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            _fileName = reader.GetAttribute("FileName");
            if (reader.GetAttribute("PreMultipliedAlpha") != null) _preMultiplyAlpha = reader.GetAttribute("PreMultipliedAlpha").ToUpper() == "TRUE";
        }

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteAttributeString("FileName", _fileName);
            writer.WriteAttributeString("PreMultipliedAlpha", _preMultiplyAlpha ? "TRUE" : "FALSE");
        }
    }
}
