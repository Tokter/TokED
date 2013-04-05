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
    [Export("Texture", typeof(GameObjectIns)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class TextureIns : GameObjectIns
    {
        private TextBox _fileBox;
        private CheckBox _multAlphaCheck;

        public TextureIns()
        {
            _fileBox = this.AddLabeledFilename(100, "File Name:", ".png", "Texture (.png)|*.png", "Load Texture from file:");
            _multAlphaCheck = this.AddLabeledCheckBox(100, "Pre Mul. Alpha:");
        }

        protected override void Bind()
        {
            base.Bind();
            _fileBox.Bind(this.GameObject, "FileName");
            _multAlphaCheck.Bind(this.GameObject, "PreMultiplyAlpha");
        }
    }
}
