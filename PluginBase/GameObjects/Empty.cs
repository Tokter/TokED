using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED;

namespace PluginBase.GameObjects
{
    [Export("Empty", typeof(GameObject)), PartCreationPolicy(CreationPolicy.NonShared), RequiresParent("Scene")]
    public class Empty : GameObject
    {
        public Empty()
        {
            Name = "Empty";
        }
    }
}
