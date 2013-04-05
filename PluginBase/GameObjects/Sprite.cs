using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED;

namespace PluginBase.GameObjects
{
    [Export("Sprite", typeof(GameObject)), PartCreationPolicy(CreationPolicy.NonShared), RequiresParent("Texture")]
    public class Sprite : GameObject
    {
        public Sprite()
        {
            Name = "Sprite";
            TextureName = "Sprite";
        }
    }
}
