using PluginBase.Components;
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
    [Export("Sprite", typeof(ComponentIns)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class SpriteIns : ComponentIns
    {
        private TextBox _spriteDef;

        public SpriteIns()
        {
            _spriteDef = this.AddLabeledTextBox(100, "Sprite Def:");
        }

        protected override void Bind()
        {
            GameObject root = this.Component.Owner;
            while (root.Parent != null) root = root.Parent;
            var definitions = root.FindChildren<SpriteDefinition>();
            (this.Component as Sprite).Definition = (SpriteDefinition)definitions[0];

            _spriteDef.Text = ((SpriteDefinition)definitions[0]).Name;
        }
    }
}
