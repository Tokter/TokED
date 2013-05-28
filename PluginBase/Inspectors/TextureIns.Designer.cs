namespace PluginBase.Inspectors
{
    partial class TextureIns
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
            this.bsTexture = new System.Windows.Forms.BindingSource(this.components);
            this.lFilename = new System.Windows.Forms.Label();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.bLoad = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.bsTexture)).BeginInit();
            this.SuspendLayout();
            // 
            // bsTexture
            // 
            this.bsTexture.DataSource = typeof(PluginBase.GameObjects.Texture);
            // 
            // lFilename
            // 
            this.lFilename.AutoSize = true;
            this.lFilename.Location = new System.Drawing.Point(3, 32);
            this.lFilename.Name = "lFilename";
            this.lFilename.Size = new System.Drawing.Size(49, 13);
            this.lFilename.TabIndex = 2;
            this.lFilename.Text = "Filename";
            // 
            // tbFilename
            // 
            this.tbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilename.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTexture, "Filename", true));
            this.tbFilename.Location = new System.Drawing.Point(100, 29);
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.Size = new System.Drawing.Size(230, 20);
            this.tbFilename.TabIndex = 3;
            // 
            // bLoad
            // 
            this.bLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bLoad.Location = new System.Drawing.Point(336, 29);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(24, 20);
            this.bLoad.TabIndex = 4;
            this.bLoad.Text = "...";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "png";
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "PNG Files|*.png";
            this.openFileDialog.Title = "Load Texture";
            // 
            // TextureIns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bLoad);
            this.Controls.Add(this.tbFilename);
            this.Controls.Add(this.lFilename);
            this.Name = "TextureIns";
            this.Size = new System.Drawing.Size(363, 53);
            this.Controls.SetChildIndex(this.lFilename, 0);
            this.Controls.SetChildIndex(this.tbFilename, 0);
            this.Controls.SetChildIndex(this.bLoad, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bsTexture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsTexture;
        private System.Windows.Forms.Label lFilename;
        private System.Windows.Forms.TextBox tbFilename;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}
