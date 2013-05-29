using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TokED;

namespace PluginBase.GameObjects
{
    public interface ITexture
    {
        Bitmap GetBitmap();
    }

    [Export("Texture", typeof(GameObject)), HasIcon("Texture.png"), PartCreationPolicy(CreationPolicy.NonShared), DoesNotAllowChildren()]
    public class Texture : GameObject, ITexture
    {
        private string _filename = "";

        public Texture()
        {
            Name = "Texture";
        }

        public string Filename
        {
            get { return _filename; }
            set { _filename = value; NotifyChange(); }
        }

        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            _filename = reader.GetAttribute("Filename");
        }

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteAttributeString("Filename", _filename);
        }

        public Bitmap GetBitmap()
        {
            if (File.Exists(_filename))
                return new Bitmap(_filename);
            else
                return null;
        }
    }
}
