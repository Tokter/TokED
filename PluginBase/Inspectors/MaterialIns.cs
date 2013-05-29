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
                        var paramInt = new ParameterInt();
                        paramInt.Bind(param, material);
                        _parameters.Add(paramInt); 
                        break;

                    case ShaderParamType.Float:
                        var paramFloat = new ParameterFloat();
                        paramFloat.Bind(param, material);
                        _parameters.Add(paramFloat);
                        break;

                    case ShaderParamType.Color:
                        var paramColor = new ParameterColor();
                        paramColor.Bind(param, material);
                        _parameters.Add(paramColor);
                        break;

                    case ShaderParamType.Vec2:
                        var paramVec2 = new ParameterVec2();
                        paramVec2.Bind(param, material);
                        _parameters.Add(paramVec2);
                        break;

                    case ShaderParamType.Vec3:
                        var paramVec3 = new ParameterVec3();
                        paramVec3.Bind(param, material);
                        _parameters.Add(paramVec3);
                        break;

                    case ShaderParamType.Vec4:
                        var paramVec4 = new ParameterVec4();
                        paramVec4.Bind(param, material);
                        _parameters.Add(paramVec4);
                        break;

                    case ShaderParamType.Texture:
                        var paramTex = new ParameterTexture();
                        paramTex.Bind(param, material);
                        _parameters.Add(paramTex);
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
