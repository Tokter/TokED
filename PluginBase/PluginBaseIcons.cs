using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED.UI;

namespace PluginBase
{
    [Export("PluginBaseIcons", typeof(UIIcon)), PartCreationPolicy(CreationPolicy.Shared)]
    public class PluginBaseIcons : UIIcon
    {
        public PluginBaseIcons()
        {
            RegisterIcon("Folder.png");
            RegisterIcon("Material.png");
            RegisterIcon("Object.png");
            RegisterIcon("Scene.png");
            RegisterIcon("SpriteDef.png");
            RegisterIcon("Transformation.png");
            RegisterIcon("Project.png");
            RegisterIcon("Sprite.png");
            RegisterIcon("Texture.png");
        }
    }
}
