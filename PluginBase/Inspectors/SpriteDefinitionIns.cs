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
    [Export("SpriteDefinition", typeof(GameObjectIns)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class SpriteDefinitionIns : GameObjectIns
    {
        private TextBox _p1X;
        private TextBox _p1Y;
        private TextBox _p2X;
        private TextBox _p2Y;
        private TextBox _p3X;
        private TextBox _p3Y;
        private TextBox _p4X;
        private TextBox _p4Y;
        private TextBox _oX;
        private TextBox _oY;

        public SpriteDefinitionIns()
        {
            _p1X = this.AddLabeledTextBox(100, "P1 X:");
            _p1Y = this.AddLabeledTextBox(100, "P1 Y:");
            _p2X = this.AddLabeledTextBox(100, "P2 X:");
            _p2Y = this.AddLabeledTextBox(100, "P2 Y:");
            _p3X = this.AddLabeledTextBox(100, "P3 X:");
            _p3Y = this.AddLabeledTextBox(100, "P3 Y:");
            _p4X = this.AddLabeledTextBox(100, "P4 X:");
            _p4Y = this.AddLabeledTextBox(100, "P4 Y:");
            _oX = this.AddLabeledTextBox(100, "Origin X:");
            _oY = this.AddLabeledTextBox(100, "Origin Y:");
        }

        protected override void Build()
        {
            base.Build();
            _p1X.Bind(this.GameObject, "P1X");
            _p1Y.Bind(this.GameObject, "P1Y");
            _p2X.Bind(this.GameObject, "P2X");
            _p2Y.Bind(this.GameObject, "P2Y");
            _p3X.Bind(this.GameObject, "P3X");
            _p3Y.Bind(this.GameObject, "P3Y");
            _p4X.Bind(this.GameObject, "P4X");
            _p4Y.Bind(this.GameObject, "P4Y");
            _oX.Bind(this.GameObject, "OriginX");
            _oY.Bind(this.GameObject, "OriginY");
        }
    }
}
