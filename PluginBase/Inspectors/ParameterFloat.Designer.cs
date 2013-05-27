namespace PluginBase.Inspectors
{
    partial class ParameterFloat
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
            this.lLabel = new System.Windows.Forms.Label();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.bsParameter = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsParameter)).BeginInit();
            this.SuspendLayout();
            // 
            // lLabel
            // 
            this.lLabel.AutoSize = true;
            this.lLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsParameter, "LongName", true));
            this.lLabel.Location = new System.Drawing.Point(3, 6);
            this.lLabel.Name = "lLabel";
            this.lLabel.Size = new System.Drawing.Size(35, 13);
            this.lLabel.TabIndex = 3;
            this.lLabel.Text = "label1";
            // 
            // tbValue
            // 
            this.tbValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbValue.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsParameter, "FloatValue", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N2"));
            this.tbValue.Location = new System.Drawing.Point(100, 3);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(141, 20);
            this.tbValue.TabIndex = 2;
            this.tbValue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbValue_MouseDown);
            this.tbValue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tbValue_MouseMove);
            // 
            // bsParameter
            // 
            this.bsParameter.DataSource = typeof(TokGL.ShaderParam);
            this.bsParameter.CurrentItemChanged += new System.EventHandler(this.bsParameter_CurrentItemChanged);
            // 
            // ParameterFloat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lLabel);
            this.Controls.Add(this.tbValue);
            this.Name = "ParameterFloat";
            this.Size = new System.Drawing.Size(244, 27);
            ((System.ComponentModel.ISupportInitialize)(this.bsParameter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsParameter;
        private System.Windows.Forms.Label lLabel;
        private System.Windows.Forms.TextBox tbValue;
    }
}
