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
        protected override void Build()
        {
            var sprites = new List<String>();
            foreach (var child in this.Component.Owner.Root.FindChildren<SpriteDefinition>())
            {
                sprites.Add(child.Name);
            }
            this.AddStringList(100, "Sprite Def.", sprites).Bind(this.Component, "SpriteDefName");
        }
    }
}
