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
using PluginBase.GameObjects;

namespace PluginBase.Inspectors
{
    public partial class ParameterTexture : UserControl
    {
        private PluginBase.GameObjects.Material _mat;

        public ParameterTexture()
        {
            InitializeComponent();
        }

        public void Bind(ShaderParam param, PluginBase.GameObjects.Material mat)
        {
            _mat = mat;
            bsParameter.DataSource = param;

            var textures = _mat.Root.FindChildrenWithInterface<ITexture>();
            var names = new List<string>();
            foreach (var texture in textures)
            {
                names.Add(texture.Name);
            }
            bsTextures.DataSource = names;
        }

        private void bsParameter_CurrentItemChanged(object sender, EventArgs e)
        {
            _mat.ApplyParameters();
        }
    }
}
