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
using PluginBase.Components;
using TokED;

namespace PluginBase.Inspectors
{
    [Export("Transformation", typeof(ComponentIns)), HasIcon("Transformation.png"), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TransformationIns : ComponentIns
    {
        public TransformationIns()
        {
            InitializeComponent();
        }

        protected override void Bind()
        {
            base.Bind();
            bsTransformation.DataSource = this.Component as Transformation;
        }
    }
}
