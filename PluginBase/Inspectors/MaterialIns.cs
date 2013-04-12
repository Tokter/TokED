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
    [Export("Material", typeof(GameObjectIns)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class MaterialIns : GameObjectIns
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

            var shader = Plugins.Container.ResolveNamed<TokED.ShaderDefinition>((this.GameObject as PluginBase.GameObjects.Material).Shader);
            if (shader.Texture0Enabled)
            {
                _fileBox0 = _details.AddLabeledFilename(100, "Texture 0:", ".png", "Texture (.png)|*.png", "Load Texture from file:");
                _fileBox0.Bind(this.GameObject, "Texture0FileName");
            }

            if (shader.Texture1Enabled)
            {
                _fileBox1 = _details.AddLabeledFilename(100, "Texture 1:", ".png", "Texture (.png)|*.png", "Load Texture from file:");
                _fileBox1.Bind(this.GameObject, "Texture1FileName");
            }

            if (shader.Texture2Enabled)
            {
                _fileBox2 = _details.AddLabeledFilename(100, "Texture 2:", ".png", "Texture (.png)|*.png", "Load Texture from file:");
                _fileBox2.Bind(this.GameObject, "Texture2FileName");
            }

            if (shader.Texture3Enabled)
            {
                _fileBox3 = _details.AddLabeledFilename(100, "Texture 3:", ".png", "Texture (.png)|*.png", "Load Texture from file:");
                _fileBox3.Bind(this.GameObject, "Texture3FileName");
            }

            var anyTextures = shader.Texture0Enabled || shader.Texture1Enabled || shader.Texture2Enabled || shader.Texture3Enabled;

            _depthTest = _details.AddLabeledCheckBox(100, "Depth Test:");
            _depthTest.Bind(this.GameObject, "DepthTest");

            if (anyTextures)
            {
                _multAlphaCheck = _details.AddLabeledCheckBox(100, "Pre Mul. Alpha:");
                _multAlphaCheck.Bind(this.GameObject, "PreMultiplyAlpha");
            }

            _alphaBlend = _details.AddLabeledCheckBox(100, "Alpha Blend:");
            _alphaBlend.Bind(this.GameObject, "AlphaBlend");

            _smoothLines = _details.AddLabeledCheckBox(100, "Smooth Lines:");
            _smoothLines.Bind(this.GameObject, "SmoothLines");

            if (anyTextures)
            {
                _minFilter = _details.AddEnumList(100, "Min Filter:", typeof(TextureMinFilter));
                _minFilter.Bind(this.GameObject, "MinFilter");

                _magFilter = _details.AddEnumList(100, "Mag Filter:", typeof(TextureMagFilter));
                _magFilter.Bind(this.GameObject, "MagFilter");
            }

            //Shader Parameters
            foreach (var p in shader.Parameters)
            {
                switch (p.Type)
                {
                    case ShaderParamType.Float:
                        var field = _details.AddLabeledTextBox(100, p.LongName + ":", p.FloatValue);
                        //field.Bind(this.Component, "PosX");
                        break;
                }
            }

            _details.Size = new Point(0, 20 * _details.Controls.Count);
        }

    }
}
