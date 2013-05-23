using OpenTK.Graphics.OpenGL;
using PluginBase.GameObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TokED;
using TokGL;

namespace PluginBase.Components
{
    [Export("Sprite", typeof(Component)), HasIcon("Sprite.png"),  PartCreationPolicy(CreationPolicy.NonShared)]
    public class Sprite : Component, IDrawable
    {
        private string _spriteDefName;
        private SpriteDefinition _spriteDef;
        private TokGL.Material _material = null;

        public string SpriteDefName
        {
            get { return _spriteDefName; }
            set { _spriteDefName = value; }
        }

        protected override void OnLoad()
        {
            _spriteDef = this.Owner.Root.FindChild<SpriteDefinition>(_spriteDefName);
            if (_spriteDef != null)
            {
                var materialObj = _spriteDef.FindParent<PluginBase.GameObjects.Material>();
                materialObj.Load();
                _material = materialObj.Mat;
            }
            else
            {
                _material = null;
            }
        }

        public void Draw(TokGL.LineBatch lineBatch, TokGL.SpriteBatch spriteBatch)
        {
            if (_spriteDef!=null && _material!=null)
            {
                spriteBatch.AddSprite(_material, _spriteDef.P1, _spriteDef.P2, _spriteDef.P3, _spriteDef.P4, _spriteDef.Origin, Color.White);
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Definition", _spriteDefName);
        }

        public override void ReadXml(XmlReader reader)
        {
            if (reader.HasAttributes)
            {
                _spriteDefName = reader.GetAttribute("Definition");
            }
        }
    }
}
