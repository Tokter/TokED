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
    [Export("TextureAtlas", typeof(GameObjectIns)), HasIcon("TextureAtlas.png"), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TextureAtlasIns : GameObjectIns
    {
        public TextureAtlasIns()
        {
            InitializeComponent();
        }

        protected override void Bind()
        {
            base.Bind();
            bsTextureAtlasItems.DataSource = (this.GameObject as TextureAtlas).Items;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in openFileDialog.FileNames)
                {
                    (this.GameObject as TextureAtlas).Items.Add(new TextureAtlasItem { Filename = file });
                }
            }
        }
    }
}
