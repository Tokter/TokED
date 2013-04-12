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
    [Export("Project", typeof(GameObjectIns)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectIns : GameObjectIns
    {
        private TextBox _widthBox;
        private TextBox _heightBox;

        public ProjectIns()
        {
            _widthBox = this.AddLabeledTextBox(100, "Screen Width:");
            _heightBox = this.AddLabeledTextBox(100, "Screen Height:");
        }

        protected override void Build()
        {
            base.Build();
            _widthBox.Bind(this.GameObject, "Width");
            _heightBox.Bind(this.GameObject, "Height");
        }

    }
}
