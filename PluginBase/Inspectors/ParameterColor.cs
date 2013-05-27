using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AvengersUtd.ColorChooserTest;
using TokGL;

namespace PluginBase.Inspectors
{
    public partial class ParameterColor : UserControl
    {
        private PluginBase.GameObjects.Material _mat;

        public ParameterColor()
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

        private void button1_Click(object sender, EventArgs e)
        {
            var colorChooser = new ColorChooser();
            colorChooser.Color = (bsParameter.Current as ShaderParam).Color;
            if (colorChooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                (bsParameter.Current as ShaderParam).Color = colorChooser.Color;
            }
        }
    }
}
