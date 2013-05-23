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
    [Export("Project", typeof(GameObjectIns)), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ProjectIns : GameObjectIns
    {
        public ProjectIns()
        {
            InitializeComponent();
        }

        protected override void Bind()
        {
            base.Bind();
            bsProject.DataSource = this.GameObject as Project;
        }
    }
}
