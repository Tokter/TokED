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


namespace PluginBase.Inspectors
{
    [Export("Material", typeof(GameObjectIns)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class MaterialIns : GameObjectIns
    {
        private DropDownList _shader;
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

        public MaterialIns()
        {
            _shader = this.AddStringList(100, "Shader", Plugins.GetKeys<Shader>());

            _fileBox0 = this.AddLabeledFilename(100, "Texture 0:", ".png", "Texture (.png)|*.png", "Load Texture from file:");
            _fileBox1 = this.AddLabeledFilename(100, "Texture 1:", ".png", "Texture (.png)|*.png", "Load Texture from file:");
            _fileBox2 = this.AddLabeledFilename(100, "Texture 2:", ".png", "Texture (.png)|*.png", "Load Texture from file:");
            _fileBox3 = this.AddLabeledFilename(100, "Texture 3:", ".png", "Texture (.png)|*.png", "Load Texture from file:");

            _depthTest = this.AddLabeledCheckBox(100, "Depth Test:");
            _multAlphaCheck = this.AddLabeledCheckBox(100, "Pre Mul. Alpha:");
            _alphaBlend = this.AddLabeledCheckBox(100, "Alpha Blend:");
            _smoothLines = this.AddLabeledCheckBox(100, "Smooth Lines:");
            _minFilter = this.AddEnumList(100, "Min Filter:", typeof(TextureMinFilter));
            _magFilter = this.AddEnumList(100, "Mag Filter:", typeof(TextureMagFilter));
        }

        protected override void Bind()
        {
            base.Bind();
            _fileBox0.Bind(this.GameObject, "Texture0FileName");
            _fileBox1.Bind(this.GameObject, "Texture1FileName");
            _fileBox2.Bind(this.GameObject, "Texture2FileName");
            _fileBox3.Bind(this.GameObject, "Texture3FileName");
            _depthTest.Bind(this.GameObject, "DepthTest");
            _multAlphaCheck.Bind(this.GameObject, "PreMultiplyAlpha");
            _alphaBlend.Bind(this.GameObject, "AlphaBlend");
            _smoothLines.Bind(this.GameObject, "SmoothLines");
            _minFilter.Bind(this.GameObject, "MinFilter");
            _magFilter.Bind(this.GameObject, "MagFilter");
            _shader.Bind(this.GameObject, "Shader");
        }
    }
}
