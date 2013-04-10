using Squid;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED.UI;

namespace PluginBase.Inspectors
{
    [Export("Transformation", typeof(ComponentIns)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class TransformationIns : ComponentIns
    {
        private TextBox _posX;
        private TextBox _posY;
        private TextBox _posZ;
        private TextBox _rotX;
        private TextBox _rotY;
        private TextBox _rotZ;
        private TextBox _scaleX;
        private TextBox _scaleY;
        private TextBox _scaleZ;

        public TransformationIns()
        {
            _posX = this.AddLabeledTextBox(100, "X Position:");
            _posY = this.AddLabeledTextBox(100, "Y Position:");
            _posZ = this.AddLabeledTextBox(100, "Z Position:");
            _rotX = this.AddLabeledTextBox(100, "X Rotation:", 0.5f);
            _rotY = this.AddLabeledTextBox(100, "Y Rotation:", 0.5f);
            _rotZ = this.AddLabeledTextBox(100, "Z Rotation:", 0.5f);
            _scaleX = this.AddLabeledTextBox(100, "X Scale:", 0.01f);
            _scaleY = this.AddLabeledTextBox(100, "Y Scale:", 0.01f);
            _scaleZ = this.AddLabeledTextBox(100, "Z Scale:", 0.01f);
        }

        protected override void Bind()
        {
            base.Bind();
            _posX.Bind(this.Component, "PosX");
            _posY.Bind(this.Component, "PosY");
            _posZ.Bind(this.Component, "PosZ");
            _rotX.Bind(this.Component, "RotX");
            _rotY.Bind(this.Component, "RotY");
            _rotZ.Bind(this.Component, "RotZ");
            _scaleX.Bind(this.Component, "ScaleX");
            _scaleY.Bind(this.Component, "ScaleY");
            _scaleZ.Bind(this.Component, "ScaleZ");
        }
    }
}
