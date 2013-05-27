namespace PluginBase.Inspectors
{
    partial class ParameterVec4
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bsParameter = new System.Windows.Forms.BindingSource(this.components);
            this.lZ = new System.Windows.Forms.Label();
            this.tbZValue = new System.Windows.Forms.TextBox();
            this.lY = new System.Windows.Forms.Label();
            this.lX = new System.Windows.Forms.Label();
            this.tbYValue = new System.Windows.Forms.TextBox();
            this.lLabel = new System.Windows.Forms.Label();
            this.tbXValue = new System.Windows.Forms.TextBox();
            this.lW = new System.Windows.Forms.Label();
            this.tbWValue = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.bsParameter)).BeginInit();
            this.SuspendLayout();
            // 
            // bsParameter
            // 
            this.bsParameter.DataSource = typeof(TokGL.ShaderParam);
            this.bsParameter.CurrentItemChanged += new System.EventHandler(this.bsParameter_CurrentItemChanged);
            // 
            // lZ
            // 
            this.lZ.AutoSize = true;
            this.lZ.Location = new System.Drawing.Point(80, 58);
            this.lZ.Name = "lZ";
            this.lZ.Size = new System.Drawing.Size(14, 13);
            this.lZ.TabIndex = 22;
            this.lZ.Text = "Z";
            // 
            // tbZValue
            // 
            this.tbZValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbZValue.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsParameter, "Z", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N2"));
            this.tbZValue.Location = new System.Drawing.Point(100, 55);
            this.tbZValue.Name = "tbZValue";
            this.tbZValue.Size = new System.Drawing.Size(141, 20);
            this.tbZValue.TabIndex = 21;
            this.tbZValue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbZValue_MouseDown);
            this.tbZValue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tbZValue_MouseMove);
            // 
            // lY
            // 
            this.lY.AutoSize = true;
            this.lY.Location = new System.Drawing.Point(80, 32);
            this.lY.Name = "lY";
            this.lY.Size = new System.Drawing.Size(14, 13);
            this.lY.TabIndex = 20;
            this.lY.Text = "Y";
            // 
            // lX
            // 
            this.lX.AutoSize = true;
            this.lX.Location = new System.Drawing.Point(80, 6);
            this.lX.Name = "lX";
            this.lX.Size = new System.Drawing.Size(14, 13);
            this.lX.TabIndex = 19;
            this.lX.Text = "X";
            // 
            // tbYValue
            // 
            this.tbYValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbYValue.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsParameter, "Y", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N2"));
            this.tbYValue.Location = new System.Drawing.Point(100, 29);
            this.tbYValue.Name = "tbYValue";
            this.tbYValue.Size = new System.Drawing.Size(141, 20);
            this.tbYValue.TabIndex = 18;
            this.tbYValue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbYValue_MouseDown);
            this.tbYValue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tbYValue_MouseMove);
            // 
            // lLabel
            // 
            this.lLabel.AutoSize = true;
            this.lLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsParameter, "LongName", true));
            this.lLabel.Location = new System.Drawing.Point(3, 6);
            this.lLabel.Name = "lLabel";
            this.lLabel.Size = new System.Drawing.Size(35, 13);
            this.lLabel.TabIndex = 17;
            this.lLabel.Text = "label1";
            // 
            // tbXValue
            // 
            this.tbXValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbXValue.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsParameter, "X", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N2"));
            this.tbXValue.Location = new System.Drawing.Point(100, 3);
            this.tbXValue.Name = "tbXValue";
            this.tbXValue.Size = new System.Drawing.Size(141, 20);
            this.tbXValue.TabIndex = 16;
            this.tbXValue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbXValue_MouseDown);
            this.tbXValue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tbXValue_MouseMove);
            // 
            // lW
            // 
            this.lW.AutoSize = true;
            this.lW.Location = new System.Drawing.Point(80, 84);
            this.lW.Name = "lW";
            this.lW.Size = new System.Drawing.Size(18, 13);
            this.lW.TabIndex = 24;
            this.lW.Text = "W";
            // 
            // tbWValue
            // 
            this.tbWValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWValue.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsParameter, "W", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N2"));
            this.tbWValue.Location = new System.Drawing.Point(100, 81);
            this.tbWValue.Name = "tbWValue";
            this.tbWValue.Size = new System.Drawing.Size(141, 20);
            this.tbWValue.TabIndex = 23;
            this.tbWValue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbWValue_MouseDown);
            this.tbWValue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tbWValue_MouseMove);
            // 
            // ParameterVec4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lW);
            this.Controls.Add(this.tbWValue);
            this.Controls.Add(this.lZ);
            this.Controls.Add(this.tbZValue);
            this.Controls.Add(this.lY);
            this.Controls.Add(this.lX);
            this.Controls.Add(this.tbYValue);
            this.Controls.Add(this.lLabel);
            this.Controls.Add(this.tbXValue);
            this.Name = "ParameterVec4";
            this.Size = new System.Drawing.Size(244, 104);
            ((System.ComponentModel.ISupportInitialize)(this.bsParameter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsParameter;
        private System.Windows.Forms.Label lZ;
        private System.Windows.Forms.TextBox tbZValue;
        private System.Windows.Forms.Label lY;
        private System.Windows.Forms.Label lX;
        private System.Windows.Forms.TextBox tbYValue;
        private System.Windows.Forms.Label lLabel;
        private System.Windows.Forms.TextBox tbXValue;
        private System.Windows.Forms.Label lW;
        private System.Windows.Forms.TextBox tbWValue;
    }
}
