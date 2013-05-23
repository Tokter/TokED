namespace PluginBase.Inspectors
{
    partial class MaterialIns
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
            this.cbShaders = new System.Windows.Forms.ComboBox();
            this.bsMaterial = new System.Windows.Forms.BindingSource(this.components);
            this.bsShader = new System.Windows.Forms.BindingSource(this.components);
            this.lShader = new System.Windows.Forms.Label();
            this.cbDepthTest = new System.Windows.Forms.CheckBox();
            this.cbAlphaBlend = new System.Windows.Forms.CheckBox();
            this.cbSmoothLines = new System.Windows.Forms.CheckBox();
            this.cbMinFilter = new System.Windows.Forms.ComboBox();
            this.cbMagFilter = new System.Windows.Forms.ComboBox();
            this.lMinFilter = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bsMaterial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsShader)).BeginInit();
            this.SuspendLayout();
            // 
            // cbShaders
            // 
            this.cbShaders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShaders.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsMaterial, "Shader", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbShaders.DataSource = this.bsShader;
            this.cbShaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShaders.FormattingEnabled = true;
            this.cbShaders.Location = new System.Drawing.Point(100, 29);
            this.cbShaders.Name = "cbShaders";
            this.cbShaders.Size = new System.Drawing.Size(260, 21);
            this.cbShaders.TabIndex = 2;
            // 
            // bsMaterial
            // 
            this.bsMaterial.DataSource = typeof(PluginBase.GameObjects.Material);
            // 
            // bsShader
            // 
            this.bsShader.CurrentChanged += new System.EventHandler(this.bsShader_CurrentChanged);
            // 
            // lShader
            // 
            this.lShader.AutoSize = true;
            this.lShader.Location = new System.Drawing.Point(3, 32);
            this.lShader.Name = "lShader";
            this.lShader.Size = new System.Drawing.Size(41, 13);
            this.lShader.TabIndex = 3;
            this.lShader.Text = "Shader";
            // 
            // cbDepthTest
            // 
            this.cbDepthTest.AutoSize = true;
            this.cbDepthTest.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsMaterial, "DepthTest", true));
            this.cbDepthTest.Location = new System.Drawing.Point(100, 56);
            this.cbDepthTest.Name = "cbDepthTest";
            this.cbDepthTest.Size = new System.Drawing.Size(79, 17);
            this.cbDepthTest.TabIndex = 4;
            this.cbDepthTest.Text = "Depth Test";
            this.cbDepthTest.UseVisualStyleBackColor = true;
            // 
            // cbAlphaBlend
            // 
            this.cbAlphaBlend.AutoSize = true;
            this.cbAlphaBlend.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsMaterial, "AlphaBlend", true));
            this.cbAlphaBlend.Location = new System.Drawing.Point(100, 79);
            this.cbAlphaBlend.Name = "cbAlphaBlend";
            this.cbAlphaBlend.Size = new System.Drawing.Size(83, 17);
            this.cbAlphaBlend.TabIndex = 5;
            this.cbAlphaBlend.Text = "Alpha Blend";
            this.cbAlphaBlend.UseVisualStyleBackColor = true;
            // 
            // cbSmoothLines
            // 
            this.cbSmoothLines.AutoSize = true;
            this.cbSmoothLines.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsMaterial, "SmoothLines", true));
            this.cbSmoothLines.Location = new System.Drawing.Point(100, 102);
            this.cbSmoothLines.Name = "cbSmoothLines";
            this.cbSmoothLines.Size = new System.Drawing.Size(90, 17);
            this.cbSmoothLines.TabIndex = 6;
            this.cbSmoothLines.Text = "Smooth Lines";
            this.cbSmoothLines.UseVisualStyleBackColor = true;
            // 
            // cbMinFilter
            // 
            this.cbMinFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMinFilter.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsMaterial, "MinFilter", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbMinFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMinFilter.FormattingEnabled = true;
            this.cbMinFilter.Location = new System.Drawing.Point(100, 125);
            this.cbMinFilter.Name = "cbMinFilter";
            this.cbMinFilter.Size = new System.Drawing.Size(260, 21);
            this.cbMinFilter.TabIndex = 7;
            // 
            // cbMagFilter
            // 
            this.cbMagFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMagFilter.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsMaterial, "MagFilter", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbMagFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMagFilter.FormattingEnabled = true;
            this.cbMagFilter.Location = new System.Drawing.Point(100, 152);
            this.cbMagFilter.Name = "cbMagFilter";
            this.cbMagFilter.Size = new System.Drawing.Size(260, 21);
            this.cbMagFilter.TabIndex = 8;
            // 
            // lMinFilter
            // 
            this.lMinFilter.AutoSize = true;
            this.lMinFilter.Location = new System.Drawing.Point(3, 128);
            this.lMinFilter.Name = "lMinFilter";
            this.lMinFilter.Size = new System.Drawing.Size(49, 13);
            this.lMinFilter.TabIndex = 9;
            this.lMinFilter.Text = "Min Filter";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Mag Filter";
            // 
            // MaterialIns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lMinFilter);
            this.Controls.Add(this.cbMagFilter);
            this.Controls.Add(this.cbMinFilter);
            this.Controls.Add(this.cbSmoothLines);
            this.Controls.Add(this.cbAlphaBlend);
            this.Controls.Add(this.cbDepthTest);
            this.Controls.Add(this.lShader);
            this.Controls.Add(this.cbShaders);
            this.Name = "MaterialIns";
            this.Size = new System.Drawing.Size(363, 177);
            this.Controls.SetChildIndex(this.cbShaders, 0);
            this.Controls.SetChildIndex(this.lShader, 0);
            this.Controls.SetChildIndex(this.cbDepthTest, 0);
            this.Controls.SetChildIndex(this.cbAlphaBlend, 0);
            this.Controls.SetChildIndex(this.cbSmoothLines, 0);
            this.Controls.SetChildIndex(this.cbMinFilter, 0);
            this.Controls.SetChildIndex(this.cbMagFilter, 0);
            this.Controls.SetChildIndex(this.lMinFilter, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bsMaterial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsShader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbShaders;
        private System.Windows.Forms.Label lShader;
        private System.Windows.Forms.BindingSource bsShader;
        private System.Windows.Forms.BindingSource bsMaterial;
        private System.Windows.Forms.CheckBox cbDepthTest;
        private System.Windows.Forms.CheckBox cbAlphaBlend;
        private System.Windows.Forms.CheckBox cbSmoothLines;
        private System.Windows.Forms.ComboBox cbMinFilter;
        private System.Windows.Forms.ComboBox cbMagFilter;
        private System.Windows.Forms.Label lMinFilter;
        private System.Windows.Forms.Label label2;
    }
}
