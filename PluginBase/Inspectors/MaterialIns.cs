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
using TokED;
using OpenTK.Graphics.OpenGL;
using TokGL;

namespace PluginBase.Inspectors
{
    [Export("Material", typeof(GameObjectIns)), HasIcon("Material.png"), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MaterialIns : GameObjectIns
    {
        private List<Control> _parameters = new List<Control>();

        public MaterialIns()
        {
            InitializeComponent();
        }

        protected override void Bind()
        {
            base.Bind();
            bsMaterial.DataSource = this.GameObject;
            bsShader.DataSource = Plugins.GetKeys<ShaderDefinition>();
            cbMinFilter.DataSource = Enum.GetValues(typeof(TextureMinFilter));
            cbMagFilter.DataSource = Enum.GetValues(typeof(TextureMagFilter));
        }

        private void bsShader_CurrentChanged(object sender, EventArgs e)
        {
            BindParameters();
        }

        private void BindParameters()
        {
            var material = this.GameObject as PluginBase.GameObjects.Material;

            foreach (var param in _parameters)
            {
                this.Controls.Remove(param);
                param.Dispose();
            }
            _parameters.Clear();
            this.Height = 177;
            if (this.Parent != null) this.Parent.Height = 202;

            foreach (var param in material.Parameters)
            {                
                switch (param.Type)
                {
                    case ShaderParamType.Int:
                        var pUC = new ParameterInt();
                        pUC.Bind(param, material);
                        _parameters.Add(pUC); 
                        break;
                }
            }

            int y = 177;
            foreach (var param in _parameters)
            {
                param.Location = new Point(0, y);
                param.Size = new Size(this.Width, param.Height);
                param.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                this.Controls.Add(param);
                y += param.Height;
                this.Height += param.Height;
                if (this.Parent != null) this.Parent.Height += param.Height;
            }
        }
    }
}
