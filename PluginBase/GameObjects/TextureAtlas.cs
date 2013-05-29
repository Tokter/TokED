using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TokED;
using TokGL;

namespace PluginBase.GameObjects
{

    public class TextureAtlasItem
    {
        public string Name { get; set; }
        public string Filename { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    [Export("TextureAtlas", typeof(GameObject)), HasIcon("TextureAtlas.png"), PartCreationPolicy(CreationPolicy.NonShared), DoesNotAllowChildren()]
    public class TextureAtlas : GameObject, ITexture
    {
        private BindingList<TextureAtlasItem> _items = new BindingList<TextureAtlasItem>();

        public TextureAtlas()
        {
            Name = "TextureAtlas";
        }

        private int _width = 256;
        private int _height = 256;

        public BindingList<TextureAtlasItem> Items
        {
            get { return _items; }
        }

        protected override void OnLoad()
        {
            _items.ListChanged += Items_ListChanged;
        }

        private void Items_ListChanged(object sender, ListChangedEventArgs e)
        {
            NotifyChange("Items");
        }

        protected override void OnUnLoad()
        {
            _items.ListChanged -= Items_ListChanged;
        }

        public override void ReadXml(XmlReader reader)
        {
            _items.Clear();

            base.ReadXml(reader);
            while (reader.Name != "Textures") reader.Read();
            while (reader.Read() && (reader.NodeType != XmlNodeType.EndElement))
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    var tex = new TextureAtlasItem();
                    tex.Name = reader.GetAttribute("Name");
                    tex.Filename = reader.GetAttribute("Filename");
                    tex.Width = Convert.ToInt32(reader.GetAttribute("Width"));
                    tex.Height = Convert.ToInt32(reader.GetAttribute("Height"));
                    tex.X = Convert.ToInt32(reader.GetAttribute("X"));
                    tex.Y = Convert.ToInt32(reader.GetAttribute("Y"));
                    _items.Add(tex);
                }
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteStartElement("Textures");
            foreach (var tex in _items)
            {
                writer.WriteStartElement("Texture");
                writer.WriteAttributeString("Name", tex.Name);
                writer.WriteAttributeString("Filename", tex.Filename);
                writer.WriteAttributeString("Width", tex.Width.ToString());
                writer.WriteAttributeString("Height", tex.Height.ToString());
                writer.WriteAttributeString("X", tex.X.ToString());
                writer.WriteAttributeString("Y", tex.Y.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public Bitmap GetBitmap()
        {
            var bitmaps = new List<Bitmap>();
            foreach (var tex in _items)
            {
                if (File.Exists(tex.Filename))
                {
                    var bmp = new Bitmap(tex.Filename);
                    bmp.Tag = tex;
                    bitmaps.Add(bmp);
                    if (string.IsNullOrWhiteSpace(tex.Name)) tex.Name = Path.GetFileNameWithoutExtension(tex.Filename);
                }
            }
            bitmaps = bitmaps.OrderByDescending(b => b.Width * b.Height).ToList();

            Bitmap result = null;
            BitmapPacker packTest = null;
            if (bitmaps.Count > 0)
            {
                _width = 256;
                _height = 256;
                do
                {
                    var packer = new BitmapPacker { Rect = new Rectangle(0, 0, _width, _height) };
                    foreach (var bmp in bitmaps)
                    {
                        packTest = packer.Insert(bmp);
                        if (packTest == null) break;
                        (bmp.Tag as TextureAtlasItem).Width = packTest.Rect.Width;
                        (bmp.Tag as TextureAtlasItem).Height = packTest.Rect.Height;
                        (bmp.Tag as TextureAtlasItem).X = packTest.Rect.X;
                        (bmp.Tag as TextureAtlasItem).Y = packTest.Rect.Y;
                    }
                    if (packTest == null)
                    {
                        if (_width > _height) _height *= 2; else _width *= 2;
                    }
                    else
                    {
                        result = new Bitmap(_width, _height, PixelFormat.Format32bppArgb);
                        packer.Render(result);
                    }
                } while (packTest == null);
            }
            bitmaps.Clear();
            return result;
        }
    }
}
