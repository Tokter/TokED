using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokGL;

namespace PluginBase.Inspectors
{
    public partial class ParameterInt : UserControl
    {
        private PluginBase.GameObjects.Material _mat;

        public ParameterInt()
        {
            InitializeComponent();
        }

        public void Bind(ShaderParam param, PluginBase.GameObjects.Material mat)
        {
            _mat = mat;
            bsParameter.DataSource = param;
        }

        private void bsParameter_CurrentItemChanged(object sender, EventArgs e)
        {
            _mat.ApplyParameters();
        }
    }
}
