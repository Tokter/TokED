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
using TokED;

namespace PluginBase.Inspectors
{
    [Export("Scene", typeof(GameObjectIns)), HasIcon("Scene.png"), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SceneIns : GameObjectIns
    {
        public SceneIns()
        {
            InitializeComponent();
        }

        protected override void Bind()
        {
            base.Bind();
            bsScene.DataSource = this.GameObject as Scene;
        }
    }
}
