namespace PluginBase.Inspectors
{
    partial class ProjectIns
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
            this.lWidth = new System.Windows.Forms.Label();
            this.lHeight = new System.Windows.Forms.Label();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.tbHeight = new System.Windows.Forms.TextBox();
            this.bsProject = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsProject)).BeginInit();
            this.SuspendLayout();
            // 
            // lWidth
            // 
            this.lWidth.AutoSize = true;
            this.lWidth.Location = new System.Drawing.Point(3, 32);
            this.lWidth.Name = "lWidth";
            this.lWidth.Size = new System.Drawing.Size(72, 13);
            this.lWidth.TabIndex = 2;
            this.lWidth.Text = "Screen Width";
            // 
            // lHeight
            // 
            this.lHeight.AutoSize = true;
            this.lHeight.Location = new System.Drawing.Point(3, 58);
            this.lHeight.Name = "lHeight";
            this.lHeight.Size = new System.Drawing.Size(75, 13);
            this.lHeight.TabIndex = 3;
            this.lHeight.Text = "Screen Height";
            // 
            // tbWidth
            // 
            this.tbWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWidth.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsProject, "Width", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N0"));
            this.tbWidth.Location = new System.Drawing.Point(100, 29);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(260, 20);
            this.tbWidth.TabIndex = 4;
            // 
            // tbHeight
            // 
            this.tbHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHeight.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsProject, "Height", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N0"));
            this.tbHeight.Location = new System.Drawing.Point(100, 55);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.Size = new System.Drawing.Size(260, 20);
            this.tbHeight.TabIndex = 5;
            // 
            // bsProject
            // 
            this.bsProject.DataSource = typeof(PluginBase.GameObjects.Project);
            // 
            // ProjectIns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbHeight);
            this.Controls.Add(this.tbWidth);
            this.Controls.Add(this.lHeight);
            this.Controls.Add(this.lWidth);
            this.Name = "ProjectIns";
            this.Size = new System.Drawing.Size(363, 81);
            this.Controls.SetChildIndex(this.lWidth, 0);
            this.Controls.SetChildIndex(this.lHeight, 0);
            this.Controls.SetChildIndex(this.tbWidth, 0);
            this.Controls.SetChildIndex(this.tbHeight, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bsProject)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lWidth;
        private System.Windows.Forms.Label lHeight;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.TextBox tbHeight;
        private System.Windows.Forms.BindingSource bsProject;
    }
}
