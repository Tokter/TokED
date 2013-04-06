using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TokED;

namespace PluginBase.GameObjects
{

    [Export("Texture", typeof(GameObject)), PartCreationPolicy(CreationPolicy.NonShared), AllowsChild("SpriteDefinition")]
    public class Texture : GameObject
    {
        private string _fileName = "";
        private Bitmap _bitmap;
        private bool _preMultiplyAlpha = true;

        public Texture()
        {
            Name = "Texture";
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

        public int Width
        {
            get { return _bitmap != null ? _bitmap.Width : 0; }
        }

        public int Height
        {
            get { return _bitmap != null ? _bitmap.Height : 0; }
        }

        public Bitmap Bitmap
        {
            get { return _bitmap; }
        }

        public override void OnPropertyChanged(string name)
        {
            switch (name)
            {
                case "FileName":
                    _bitmap = new Bitmap(_fileName);
                    break;
            }
        }

        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            FileName = reader.GetAttribute("FileName");
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
