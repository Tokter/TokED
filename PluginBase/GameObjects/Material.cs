using Autofac;
using OpenTK;
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
        private bool _depthTest = true;
        private bool _alphaBlend = true;
        private bool _smoothLines = true;
        private TextureMinFilter _minFilter = TextureMinFilter.Linear;
        private TextureMagFilter _magFilter = TextureMagFilter.Linear;

        private string _parameterShader;
        private List<ShaderParam> _parameters = new List<ShaderParam>();

        private TokGL.Material _material = null;
        private static Bitmap _notTexture = null;

        public Material()
        {
            Name = "Material";

            if (_notTexture == null)
            {
                _notTexture = new Bitmap(256, 256, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using(var g = Graphics.FromImage(_notTexture))
                {
                    g.DrawLine(Pens.Red, new Point(0,0), new Point(256,256));
                    g.DrawLine(Pens.Red, new Point(256,0), new Point(0,256));
                }
            }
        }

        public string Shader
        {
            get { return _shader; }
            set { _shader = value; NotifyChange(); }
        }

        public bool DepthTest
        {
            get { return _depthTest; }
            set { _depthTest = value; NotifyChange(); }
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

            var shader = Plugins.Container.ResolveNamed<TokED.ShaderDefinition>(_shader);
            if (shader == null) throw new Exception(string.Format("Did not find shader {0}!", _shader));

            _material.Shader = new TokGL.Shader(shader.VertexProgram, shader.FragmentProgram, shader.Attributes, shader.Parameters);
            _material.Activate();

            if (_shader != _parameterShader)
            {
                //Get default parameters
                _parameters.Clear();
                foreach (var p in _material.Shader.Parameters)
                {
                    _parameters.Add(p.Clone());
                }
                _parameterShader = _shader;
            }
            else ApplyParameters();

            //Load Textures
            if (_material.TextureCount > 0) throw new Exception("Textures not cleaned up!");
            foreach (var param in _material.Shader.Parameters)
            {
                if (param.Type == ShaderParamType.Texture)
                {
                    Texture texture;
                    if (File.Exists(param.Filename))
                        texture = Texture.FromFile(param.Filename, true, false);
                    else
                        texture = Texture.FromBitmap(_notTexture, true, false);
                    texture.MinFilter = _minFilter;
                    texture.MagFilter = _magFilter;
                    _material.AddTexture(param.TexUnit, texture);
                }
            }
            _material.DepthTest = _depthTest;
            _material.AlphaBlend = _alphaBlend;
            _material.SmoothLines = _smoothLines;
            _material.Deactivate();
        }

        public void ApplyParameters()
        {
            if (_material != null)
            {
                foreach (var pc in _parameters)
                {
                    _material.Shader.CopyParameter(pc);
                }
            }
        }

        public IEnumerable<ShaderParam> Parameters
        {
            get { return _parameters; }
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
            _parameterShader = _shader;
            if (reader.GetAttribute("DepthTest") != null) _depthTest = reader.GetAttribute("DepthTest").ToUpper() == "TRUE";
            if (reader.GetAttribute("AlphaBlend") != null) _alphaBlend = reader.GetAttribute("AlphaBlend").ToUpper() == "TRUE";
            if (reader.GetAttribute("SmoothLines") != null) _smoothLines = reader.GetAttribute("SmoothLines").ToUpper() == "TRUE";
            if (reader.GetAttribute("MinFilter") != null) _minFilter = (TextureMinFilter)Enum.Parse(typeof(TextureMinFilter), reader.GetAttribute("MinFilter"));
            if (reader.GetAttribute("MagFilter") != null) _magFilter = (TextureMagFilter)Enum.Parse(typeof(TextureMagFilter), reader.GetAttribute("MagFilter"));
            while (reader.Name != "Parameters") reader.Read();
            _parameters.Clear();
            if (!reader.IsEmptyElement)
            {
                while (reader.Read() && (reader.NodeType != XmlNodeType.EndElement))
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        var p = new ShaderParam();
                        p.Type = (ShaderParamType)Enum.Parse(typeof(ShaderParamType), reader.Name); ;
                        p.Name = reader.GetAttribute("Name");
                        p.LongName = reader.GetAttribute("Desc");
                        switch (p.Type)
                        {
                            case ShaderParamType.Int:
                                p.IntValue = Convert.ToInt32(reader.GetAttribute("Value"));
                                break;
                            case ShaderParamType.Float:
                                p.FloatValue = Convert.ToSingle(reader.GetAttribute("Value"));
                                break;
                            case ShaderParamType.Vec2:
                                p.Vec2Value = new Vector2(
                                    Convert.ToSingle(reader.GetAttribute("X")),
                                    Convert.ToSingle(reader.GetAttribute("Y"))
                                );
                                break;
                            case ShaderParamType.Vec3:
                                p.Vec3Value = new Vector3(
                                    Convert.ToSingle(reader.GetAttribute("X")),
                                    Convert.ToSingle(reader.GetAttribute("Y")),
                                    Convert.ToSingle(reader.GetAttribute("Z"))
                                );
                                break;
                            case ShaderParamType.Vec4:
                                p.Vec4Value = new Vector4(
                                    Convert.ToSingle(reader.GetAttribute("X")),
                                    Convert.ToSingle(reader.GetAttribute("Y")),
                                    Convert.ToSingle(reader.GetAttribute("Z")),
                                    Convert.ToSingle(reader.GetAttribute("W"))
                                );
                                break;
                            case ShaderParamType.Texture:
                                p.Filename = reader.GetAttribute("Filename");
                                p.TexUnit = (TextureUnit)Enum.Parse(typeof(TextureUnit), reader.GetAttribute("TextureUnit"));
                                break;
                            case ShaderParamType.Color:
                                p.Vec4Value = new Vector4(
                                    Convert.ToSingle(reader.GetAttribute("R")),
                                    Convert.ToSingle(reader.GetAttribute("G")),
                                    Convert.ToSingle(reader.GetAttribute("B")),
                                    Convert.ToSingle(reader.GetAttribute("A"))
                                );
                                break;
                        }
                        _parameters.Add(p);
                    }
                }
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteAttributeString("Shader", _shader);
            writer.WriteAttributeString("DepthTest", _depthTest ? "TRUE" : "FALSE");
            writer.WriteAttributeString("AlphaBlend", _alphaBlend ? "TRUE" : "FALSE");
            writer.WriteAttributeString("SmoothLines", _smoothLines ? "TRUE" : "FALSE");
            writer.WriteAttributeString("MinFilter", _minFilter.ToString());
            writer.WriteAttributeString("MagFilter", _magFilter.ToString());
            writer.WriteStartElement("Parameters");
            foreach (var a in _parameters)
            {
                if (a.IsNotTransient)
                {
                    writer.WriteStartElement(a.Type.ToString());
                    writer.WriteAttributeString("Name", a.Name);
                    writer.WriteAttributeString("Desc", a.LongName);
                    switch (a.Type)
                    {
                        case ShaderParamType.Int:
                            writer.WriteAttributeString("Value", a.IntValue.ToString());
                            break;
                        case ShaderParamType.Float:
                            writer.WriteAttributeString("Value", a.FloatValue.ToString());
                            break;
                        case ShaderParamType.Vec2:
                            writer.WriteAttributeString("X", a.Vec2Value.X.ToString());
                            writer.WriteAttributeString("Y", a.Vec2Value.Y.ToString());
                            break;
                        case ShaderParamType.Vec3:
                            writer.WriteAttributeString("X", a.Vec3Value.X.ToString());
                            writer.WriteAttributeString("Y", a.Vec3Value.Y.ToString());
                            writer.WriteAttributeString("Z", a.Vec3Value.Z.ToString());
                            break;
                        case ShaderParamType.Vec4:
                            writer.WriteAttributeString("X", a.Vec4Value.X.ToString());
                            writer.WriteAttributeString("Y", a.Vec4Value.Y.ToString());
                            writer.WriteAttributeString("Z", a.Vec4Value.Z.ToString());
                            writer.WriteAttributeString("W", a.Vec4Value.W.ToString());
                            break;
                        case ShaderParamType.Texture:
                            writer.WriteAttributeString("TextureUnit", a.TexUnit.ToString());
                            writer.WriteAttributeString("Filename", a.Filename);
                            break;
                        case ShaderParamType.Color:
                            writer.WriteAttributeString("R", a.Vec4Value.X.ToString());
                            writer.WriteAttributeString("G", a.Vec4Value.Y.ToString());
                            writer.WriteAttributeString("B", a.Vec4Value.Z.ToString());
                            writer.WriteAttributeString("A", a.Vec4Value.W.ToString());
                            break;
                    }
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
        }
    }
}
