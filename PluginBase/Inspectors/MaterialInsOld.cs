using Autofac;
using OpenTK.Graphics.OpenGL;
using PluginBase.GameObjects;
using Squid;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED;
using TokED.UI;
using TokGL;


namespace PluginBase.Inspectors
{
    [Export("Material", typeof(GameObjectInsOld)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class MaterialInsOld : GameObjectInsOld
    {
        private DropDownList _shader;
        private Frame _details;
        private TextBox _fileBox0;
        private TextBox _fileBox1;
        private TextBox _fileBox2;
        private TextBox _fileBox3;
        private CheckBox _depthTest;
        private CheckBox _multAlphaCheck;
        private CheckBox _alphaBlend;
        private CheckBox _smoothLines;
        private DropDownList _minFilter;
        private DropDownList _magFilter;

        protected override void Build()
        {
            base.Build();

            _shader = this.AddStringList(100, "Shader", Plugins.GetKeys<ShaderDefinition>());
            _shader.Bind(this.GameObject, "Shader");

            _details = this.AddFrame(0, 0, DockStyle.Top);

            _shader.SelectedItemChanged += (s, e) =>
                {
                    BuildDetails();
                };

            BuildDetails();
        }

        private void BuildDetails()
        {
            _details.UnBind();
            _details.Controls.Clear();

            var material = this.GameObject as PluginBase.GameObjects.Material;
            //var shader = Plugins.Container.ResolveNamed<TokED.ShaderDefinition>(material.Shader);

            _depthTest = _details.AddLabeledCheckBox(100, "Depth Test:");
            _depthTest.Bind(this.GameObject, "DepthTest");

            _alphaBlend = _details.AddLabeledCheckBox(100, "Alpha Blend:");
            _alphaBlend.Bind(this.GameObject, "AlphaBlend");

            _smoothLines = _details.AddLabeledCheckBox(100, "Smooth Lines:");
            _smoothLines.Bind(this.GameObject, "SmoothLines");

            _minFilter = _details.AddEnumList(100, "Min Filter:", typeof(TextureMinFilter));
            _minFilter.Bind(this.GameObject, "MinFilter");

            _magFilter = _details.AddEnumList(100, "Mag Filter:", typeof(TextureMagFilter));
            _magFilter.Bind(this.GameObject, "MagFilter");

            //Shader Parameters
            foreach (var p in material.Parameters)
            {
                switch (p.Type)
                {
                    case ShaderParamType.Int:
                        _details.AddLabeledTextBox(100, p.LongName + ":", 1.0f).Bind(p, "IntValue").Bind(this.GameObject, "ApplyParameters");
                        break;

                    case ShaderParamType.Float:
                        _details.AddLabeledTextBox(100, p.LongName + ":", 0.02f).Bind(p, "FloatValue").Bind(this.GameObject, "ApplyParameters");
                        break;

                    case ShaderParamType.Vec2:
                        _details.AddLabeledTextBox(100, p.LongName + " X:", 0.02f).Bind(p, "X").Bind(this.GameObject, "ApplyParameters");
                        _details.AddLabeledTextBox(100, p.LongName + " Y:", 0.02f).Bind(p, "Y").Bind(this.GameObject, "ApplyParameters");
                        break;

                    case ShaderParamType.Vec3:
                        _details.AddLabeledTextBox(100, p.LongName + " X:", 0.02f).Bind(p, "X").Bind(this.GameObject, "ApplyParameters");
                        _details.AddLabeledTextBox(100, p.LongName + " Y:", 0.02f).Bind(p, "Y").Bind(this.GameObject, "ApplyParameters");
                        _details.AddLabeledTextBox(100, p.LongName + " Z:", 0.02f).Bind(p, "Z").Bind(this.GameObject, "ApplyParameters");
                        break;

                    case ShaderParamType.Vec4:
                        _details.AddLabeledTextBox(100, p.LongName + " X:", 0.02f).Bind(p, "X").Bind(this.GameObject, "ApplyParameters");
                        _details.AddLabeledTextBox(100, p.LongName + " Y:", 0.02f).Bind(p, "Y").Bind(this.GameObject, "ApplyParameters");
                        _details.AddLabeledTextBox(100, p.LongName + " Z:", 0.02f).Bind(p, "Z").Bind(this.GameObject, "ApplyParameters");
                        _details.AddLabeledTextBox(100, p.LongName + " W:", 0.02f).Bind(p, "W").Bind(this.GameObject, "ApplyParameters");
                        break;

                    case ShaderParamType.Color:
                        _details.AddLabeledColorButton(100, p.LongName + ":").Bind(p, "Color").Bind(this.GameObject, "ApplyParameters");
                        break;

                    case ShaderParamType.Texture:
                        _details.AddLabeledFilename(100, p.LongName + ":", ".png", "Texture (.png)|*.png", "Load Texture from file:")
                            .Bind(p, "Filename")
                            .Bind(this.GameObject, "NotifyChange");
                        break;
                }
            }

            _details.Size = new Point(0, 20 * _details.Controls.Count);
        }

    }
}
