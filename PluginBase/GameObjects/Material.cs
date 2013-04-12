using Autofac;
using OpenTK.Graphics.OpenGL;
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
using TokGL;

namespace PluginBase.GameObjects
{

    [Export("Material", typeof(GameObject)), PartCreationPolicy(CreationPolicy.NonShared), AllowsChild("SpriteDefinition")]
    public class Material : GameObject
    {
        private string _shader = "Diffuse";
        private Color _color = Color.White;
        private string _fileName0 = "";
        private string _fileName1 = "";
        private string _fileName2 = "";
        private string _fileName3 = "";
        private bool _depthTest = true;
        private bool _preMultiplyAlpha = true;
        private bool _alphaBlend = true;
        private bool _smoothLines = true;
        private TextureMinFilter _minFilter = TextureMinFilter.Linear;
        private TextureMagFilter _magFilter = TextureMagFilter.Linear;

        private TokGL.Material _material = null;

        public Material()
        {
            Name = "Material";
        }

        public string Shader
        {
            get { return _shader; }
            set { _shader = value; NotifyChange(); }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; NotifyChange(); }
        }

        public string Texture0FileName
        {
            get { return _fileName0; }
            set { _fileName0 = value; NotifyChange(); }
        }

        public string Texture1FileName
        {
            get { return _fileName1; }
            set { _fileName1 = value; NotifyChange(); }
        }

        public string Texture2FileName
        {
            get { return _fileName2; }
            set { _fileName2 = value; NotifyChange(); }
        }

        public string Texture3FileName
        {
            get { return _fileName3; }
            set { _fileName3 = value; NotifyChange(); }
        }

        public bool DepthTest
        {
            get { return _depthTest; }
            set { _depthTest = value; NotifyChange(); }
        }

        public bool PreMultiplyAlpha
        {
            get { return _preMultiplyAlpha; }
            set { _preMultiplyAlpha = value; NotifyChange(); }
        }

        public bool AlphaBlend
        {
            get { return _alphaBlend; }
            set { _alphaBlend = value; NotifyChange(); }
        }

        public bool SmoothLines
        {
            get { return _smoothLines; }
            set { _smoothLines = value; NotifyChange(); }
        }

        public TextureMinFilter MinFilter
        {
            get { return _minFilter; }
            set { _minFilter = value; NotifyChange(); }
        }

        public TextureMagFilter MagFilter
        {
            get { return _magFilter; }
            set { _magFilter = value; NotifyChange(); }
        }

        public TokGL.Material Mat
        {
            get { return _material; }
        }

        protected override void OnLoad()
        {
            _material = new TokGL.Material();
            var shader = Plugins.Container.ResolveNamed<TokED.Shader>(_shader);
            if (shader == null) throw new Exception(string.Format("Did not find shader {0}!", _shader));
            _material.Shader = new TokGL.Shader(shader.VertexProgram, shader.FragmentProgram);
            _material.Activate();
            _material.Color = _color;

            //Apply Parameters
            foreach (var p in shader.Parameters)
            {
                switch(p.Type)
                {
                    case ShaderParamaterType.Float: _material.Shader.SetUniform(p.Name, (float)p.Value); break;
                } 
            }

            if (File.Exists(_fileName0))
            {
                _material.Texture0 = Texture.FromFile(_fileName0, _preMultiplyAlpha, false);
                _material.Texture0.MinFilter = _minFilter;
                _material.Texture0.MagFilter = _magFilter;
            }
            if (File.Exists(_fileName1))
            {
                _material.Texture1 = Texture.FromFile(_fileName1, _preMultiplyAlpha, false);
                _material.Texture1.MinFilter = _minFilter;
                _material.Texture1.MagFilter = _magFilter;
            }
            if (File.Exists(_fileName2))
            {
                _material.Texture2 = Texture.FromFile(_fileName2, _preMultiplyAlpha, false);
                _material.Texture2.MinFilter = _minFilter;
                _material.Texture2.MagFilter = _magFilter;
            }
            if (File.Exists(_fileName3))
            {
                _material.Texture3 = Texture.FromFile(_fileName3, _preMultiplyAlpha, false);
                _material.Texture3.MinFilter = _minFilter;
                _material.Texture3.MagFilter = _magFilter;
            }
            _material.DepthTest = _depthTest;
            _material.AlphaBlend = _alphaBlend;
            _material.SmoothLines = _smoothLines;
            _material.Deactivate();
        }

        protected override void OnUnLoad()
        {
            if (_material != null)
            {
                _material.Dispose();
                _material = null;
            }
        }

        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            _shader = reader.GetAttribute("Shader");
            _color = ColorTranslator.FromHtml(reader.GetAttribute("Color"));
            _fileName0 = reader.GetAttribute("Texture0FileName");
            _fileName1 = reader.GetAttribute("Texture1FileName");
            _fileName2 = reader.GetAttribute("Texture2FileName");
            _fileName3 = reader.GetAttribute("Texture3FileName");
            if (reader.GetAttribute("DepthTest") != null) _depthTest = reader.GetAttribute("DepthTest").ToUpper() == "TRUE";
            if (reader.GetAttribute("PreMultipliedAlpha") != null) _preMultiplyAlpha = reader.GetAttribute("PreMultipliedAlpha").ToUpper() == "TRUE";
            if (reader.GetAttribute("AlphaBlend") != null) _alphaBlend = reader.GetAttribute("AlphaBlend").ToUpper() == "TRUE";
            if (reader.GetAttribute("SmoothLines") != null) _smoothLines = reader.GetAttribute("SmoothLines").ToUpper() == "TRUE";
            if (reader.GetAttribute("MinFilter") != null) _minFilter = (TextureMinFilter)Enum.Parse(typeof(TextureMinFilter), reader.GetAttribute("MinFilter"));
            if (reader.GetAttribute("MagFilter") != null) _magFilter = (TextureMagFilter)Enum.Parse(typeof(TextureMagFilter), reader.GetAttribute("MagFilter"));
        }

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteAttributeString("Shader", _shader);
            writer.WriteAttributeString("Color", ColorTranslator.ToHtml(_color));
            writer.WriteAttributeString("Texture0FileName", _fileName0);
            writer.WriteAttributeString("Texture1FileName", _fileName1);
            writer.WriteAttributeString("Texture2FileName", _fileName2);
            writer.WriteAttributeString("Texture3FileName", _fileName3);
            writer.WriteAttributeString("DepthTest", _depthTest ? "TRUE" : "FALSE");
            writer.WriteAttributeString("PreMultipliedAlpha", _preMultiplyAlpha ? "TRUE" : "FALSE");
            writer.WriteAttributeString("AlphaBlend", _alphaBlend ? "TRUE" : "FALSE");
            writer.WriteAttributeString("SmoothLines", _smoothLines ? "TRUE" : "FALSE");
            writer.WriteAttributeString("MinFilter", _minFilter.ToString());
            writer.WriteAttributeString("MagFilter", _magFilter.ToString());
        }
    }
}
