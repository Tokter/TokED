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
    public partial class ParameterVec4 : UserControl
    {
        private PluginBase.GameObjects.Material _mat;
        private float _mouseStartValue;
        private int _mouseStartX, _mouseStartY;

        public ParameterVec4()
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

        private void tbXValue_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                _mouseStartValue = (bsParameter.Current as ShaderParam).X;
                _mouseStartX = e.X;
                _mouseStartY = e.Y;
            }
        }

        private void tbYValue_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                _mouseStartValue = (bsParameter.Current as ShaderParam).Y;
                _mouseStartX = e.X;
                _mouseStartY = e.Y;
            }
        }

        private void tbZValue_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                _mouseStartValue = (bsParameter.Current as ShaderParam).Z;
                _mouseStartX = e.X;
                _mouseStartY = e.Y;
            }
        }

        private void tbWValue_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                _mouseStartValue = (bsParameter.Current as ShaderParam).W;
                _mouseStartX = e.X;
                _mouseStartY = e.Y;
            }
        }

        private void tbXValue_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                var delta = Math.Sqrt((e.X - _mouseStartX) * (e.X - _mouseStartX) + (e.Y - _mouseStartY) * (e.Y - _mouseStartY));
                if (e.X < _mouseStartX) delta = -delta;
                (bsParameter.Current as ShaderParam).X = _mouseStartValue + (float)delta;
            }
        }

        private void tbYValue_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                var delta = Math.Sqrt((e.X - _mouseStartX) * (e.X - _mouseStartX) + (e.Y - _mouseStartY) * (e.Y - _mouseStartY));
                if (e.X < _mouseStartX) delta = -delta;
                (bsParameter.Current as ShaderParam).Y = _mouseStartValue + (float)delta;
            }
        }

        private void tbZValue_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                var delta = Math.Sqrt((e.X - _mouseStartX) * (e.X - _mouseStartX) + (e.Y - _mouseStartY) * (e.Y - _mouseStartY));
                if (e.X < _mouseStartX) delta = -delta;
                (bsParameter.Current as ShaderParam).Z = _mouseStartValue + (float)delta;
            }
        }

        private void tbWValue_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                var delta = Math.Sqrt((e.X - _mouseStartX) * (e.X - _mouseStartX) + (e.Y - _mouseStartY) * (e.Y - _mouseStartY));
                if (e.X < _mouseStartX) delta = -delta;
                (bsParameter.Current as ShaderParam).W = _mouseStartValue + (float)delta;
            }
        }
    }
}
