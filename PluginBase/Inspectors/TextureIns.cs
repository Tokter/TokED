using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using TokED.UI;
using TokED;
using PluginBase.GameObjects;

namespace PluginBase.Inspectors
{
    [Export("Texture", typeof(GameObjectIns)), HasIcon("Texture.png"), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TextureIns : GameObjectIns
    {
        public TextureIns()
        {
            InitializeComponent();
        }

        protected override void Bind()
        {
            base.Bind();
            bsTexture.DataSource = this.GameObject as Texture;
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                (this.GameObject as Texture).Filename = openFileDialog.FileName;
            }
        }
    }
}
