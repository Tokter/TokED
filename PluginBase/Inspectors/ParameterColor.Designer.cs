namespace PluginBase.Inspectors
{
    partial class ParameterColor
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
            this.bColor = new System.Windows.Forms.Button();
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
            // bColor
            // 
            this.bColor.BackColor = System.Drawing.Color.Salmon;
            this.bColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsParameter, "Color", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.bColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bColor.Location = new System.Drawing.Point(100, 3);
            this.bColor.Name = "bColor";
            this.bColor.Size = new System.Drawing.Size(141, 20);
            this.bColor.TabIndex = 4;
            this.bColor.UseVisualStyleBackColor = false;
            this.bColor.Click += new System.EventHandler(this.button1_Click);
            // 
            // bsParameter
            // 
            this.bsParameter.DataSource = typeof(TokGL.ShaderParam);
            this.bsParameter.CurrentItemChanged += new System.EventHandler(this.bsParameter_CurrentItemChanged);
            // 
            // ParameterColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bColor);
            this.Controls.Add(this.lLabel);
            this.Name = "ParameterColor";
            this.Size = new System.Drawing.Size(244, 27);
            ((System.ComponentModel.ISupportInitialize)(this.bsParameter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsParameter;
        private System.Windows.Forms.Label lLabel;
        private System.Windows.Forms.Button bColor;
    }
}
