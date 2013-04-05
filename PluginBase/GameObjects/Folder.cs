using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED;

namespace PluginBase.GameObjects
{
    [Export("Folder", typeof(GameObject)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class Folder : GameObject
    {
        public Folder()
        {
            Name = "Folder";
            TextureName = "Folder";
        }

    }
}
