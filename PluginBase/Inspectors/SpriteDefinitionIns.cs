using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokED.UI;
using System.ComponentModel.Composition;
using PluginBase.GameObjects;

namespace PluginBase.Inspectors
{
    [Export("SpriteDefinition", typeof(GameObjectIns)), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SpriteDefinitionIns : GameObjectIns
    {
        public SpriteDefinitionIns()
        {
            InitializeComponent();
        }

        protected override void Bind()
        {
            base.Bind();
            bsSpriteDefinition.DataSource = this.GameObject as SpriteDefinition;
        }
    }
}
