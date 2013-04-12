using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED.UI;

namespace PluginBase
{
    [Export("PluginBaseTextures", typeof(UITexture)), PartCreationPolicy(CreationPolicy.Shared)]
    public class PluginBaseTextures : UITexture
    {
        public PluginBaseTextures()
        {
            UITexture.RegisterTexture("PluginBaseUI.png");            
            UITexture.RegisterTextureInfo("Folder", 0, 0, 14, 14);
            UITexture.RegisterTextureInfo("Scene", 14, 0, 14, 14);
            UITexture.RegisterTextureInfo("Material", 28, 0, 14, 14);
            UITexture.RegisterTextureInfo("Sprite", 42, 0, 14, 14);
            UITexture.RegisterTextureInfo("Transformation", 56, 0, 14, 14);
            UITexture.RegisterTextureInfo("SpriteDefinition", 70, 0, 14, 14);
            UITexture.RegisterTextureInfo("Empty", 84, 0, 14, 14);
        }
    }
}
