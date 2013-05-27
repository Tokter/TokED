namespace PluginBase.Inspectors
{
    partial class ParameterVec3
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
            this.lY = new System.Windows.Forms.Label();
            this.lX = new System.Windows.Forms.Label();
            this.tbYValue = new System.Windows.Forms.TextBox();
            this.bsParameter = new System.Windows.Forms.BindingSource(this.components);
            this.lLabel = new System.Windows.Forms.Label();
            this.tbXValue = new System.Windows.Forms.TextBox();
            this.lZ = new System.Windows.Forms.Label();
            this.tbZValue = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.bsParameter)).BeginInit();
            this.SuspendLayout();
            // 
            // lY
            // 
            this.lY.AutoSize = true;
            this.lY.Location = new System.Drawing.Point(80, 32);
            this.lY.Name = "lY";
            this.lY.Size = new System.Drawing.Size(14, 13);
            this.lY.TabIndex = 13;
            this.lY.Text = "Y";
            // 
            // lX
            // 
            this.lX.AutoSize = true;
            this.lX.Location = new System.Drawing.Point(80, 6);
            this.lX.Name = "lX";
            this.lX.Size = new System.Drawing.Size(14, 13);
            this.lX.TabIndex = 12;
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
            this.tbYValue.TabIndex = 11;
            this.tbYValue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbYValue_MouseDown);
            this.tbYValue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tbYValue_MouseMove);
            // 
            // bsParameter
            // 
            this.bsParameter.DataSource = typeof(TokGL.ShaderParam);
            this.bsParameter.CurrentItemChanged += new System.EventHandler(this.bsParameter_CurrentItemChanged);
            // 
            // lLabel
            // 
            this.lLabel.AutoSize = true;
            this.lLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsParameter, "LongName", true));
            this.lLabel.Location = new System.Drawing.Point(3, 6);
            this.lLabel.Name = "lLabel";
            this.lLabel.Size = new System.Drawing.Size(35, 13);
            this.lLabel.TabIndex = 10;
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
            this.tbXValue.TabIndex = 9;
            this.tbXValue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbXValue_MouseDown);
            this.tbXValue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tbXValue_MouseMove);
            // 
            // lZ
            // 
            this.lZ.AutoSize = true;
            this.lZ.Location = new System.Drawing.Point(80, 58);
            this.lZ.Name = "lZ";
            this.lZ.Size = new System.Drawing.Size(14, 13);
            this.lZ.TabIndex = 15;
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
            this.tbZValue.TabIndex = 14;
            this.tbZValue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbZValue_MouseDown);
            this.tbZValue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tbZValue_MouseMove);
            // 
            // ParameterVec3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lZ);
            this.Controls.Add(this.tbZValue);
            this.Controls.Add(this.lY);
            this.Controls.Add(this.lX);
            this.Controls.Add(this.tbYValue);
            this.Controls.Add(this.lLabel);
            this.Controls.Add(this.tbXValue);
            this.Name = "ParameterVec3";
            this.Size = new System.Drawing.Size(244, 78);
            ((System.ComponentModel.ISupportInitialize)(this.bsParameter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsParameter;
        private System.Windows.Forms.Label lY;
        private System.Windows.Forms.Label lX;
        private System.Windows.Forms.TextBox tbYValue;
        private System.Windows.Forms.Label lLabel;
        private System.Windows.Forms.TextBox tbXValue;
        private System.Windows.Forms.Label lZ;
        private System.Windows.Forms.TextBox tbZValue;
    }
}
