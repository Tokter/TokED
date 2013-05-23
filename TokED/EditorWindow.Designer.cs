namespace TokED
{
    partial class EditorWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorWindow));
            this.treeColumn1 = new TokED.UI.Tree.TreeColumn();
            this.treeColumn2 = new TokED.UI.Tree.TreeColumn();
            this.spliterHorizontal = new System.Windows.Forms.SplitContainer();
            this.spliterVertical = new System.Windows.Forms.SplitContainer();
            this.tvGameObjects = new TokED.UI.Tree.TreeViewAdv();
            this.nodeIcon1 = new TokED.UI.Tree.NodeControls.NodeIcon();
            this.nodeTextBox2 = new TokED.UI.Tree.NodeControls.NodeTextBox();
            this._visible = new TokED.UI.Tree.NodeControls.NodeCheckBox();
            this.pGameObjectHeader = new System.Windows.Forms.Panel();
            this.bRemoveGameObject = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.bAddGameObject = new System.Windows.Forms.Button();
            this.cbAvailableGameObjects = new TokED.UI.ImageComboBox();
            this.bsAvailableGameObjects = new System.Windows.Forms.BindingSource(this.components);
            this.bSaveProject = new System.Windows.Forms.Button();
            this.bLoadProject = new System.Windows.Forms.Button();
            this.pContent = new System.Windows.Forms.Panel();
            this.pComponentHeader = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.bAddGameComponent = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAvailableComponents = new TokED.UI.ImageComboBox();
            this.bsAvailableComponents = new System.Windows.Forms.BindingSource(this.components);
            this.glControl = new OpenTK.GLControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.slFPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.slGameObjectCount = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.spliterHorizontal)).BeginInit();
            this.spliterHorizontal.Panel1.SuspendLayout();
            this.spliterHorizontal.Panel2.SuspendLayout();
            this.spliterHorizontal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spliterVertical)).BeginInit();
            this.spliterVertical.Panel1.SuspendLayout();
            this.spliterVertical.Panel2.SuspendLayout();
            this.spliterVertical.SuspendLayout();
            this.pGameObjectHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsAvailableGameObjects)).BeginInit();
            this.pComponentHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsAvailableComponents)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeColumn1
            // 
            this.treeColumn1.Header = "Game Object";
            this.treeColumn1.SortOrder = System.Windows.Forms.SortOrder.None;
            this.treeColumn1.TooltipText = null;
            this.treeColumn1.Width = 240;
            // 
            // treeColumn2
            // 
            this.treeColumn2.Header = "Visible";
            this.treeColumn2.SortOrder = System.Windows.Forms.SortOrder.None;
            this.treeColumn2.TooltipText = null;
            // 
            // spliterHorizontal
            // 
            this.spliterHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spliterHorizontal.Location = new System.Drawing.Point(0, 0);
            this.spliterHorizontal.Name = "spliterHorizontal";
            // 
            // spliterHorizontal.Panel1
            // 
            this.spliterHorizontal.Panel1.Controls.Add(this.spliterVertical);
            // 
            // spliterHorizontal.Panel2
            // 
            this.spliterHorizontal.Panel2.Controls.Add(this.glControl);
            this.spliterHorizontal.Size = new System.Drawing.Size(882, 548);
            this.spliterHorizontal.SplitterDistance = 294;
            this.spliterHorizontal.TabIndex = 0;
            // 
            // spliterVertical
            // 
            this.spliterVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spliterVertical.Location = new System.Drawing.Point(0, 0);
            this.spliterVertical.Name = "spliterVertical";
            this.spliterVertical.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spliterVertical.Panel1
            // 
            this.spliterVertical.Panel1.Controls.Add(this.tvGameObjects);
            this.spliterVertical.Panel1.Controls.Add(this.pGameObjectHeader);
            // 
            // spliterVertical.Panel2
            // 
            this.spliterVertical.Panel2.AutoScroll = true;
            this.spliterVertical.Panel2.Controls.Add(this.pContent);
            this.spliterVertical.Panel2.Controls.Add(this.pComponentHeader);
            this.spliterVertical.Size = new System.Drawing.Size(294, 548);
            this.spliterVertical.SplitterDistance = 270;
            this.spliterVertical.TabIndex = 0;
            // 
            // tvGameObjects
            // 
            this.tvGameObjects.AllowColumnReorder = true;
            this.tvGameObjects.AllowDrop = true;
            this.tvGameObjects.BackColor = System.Drawing.SystemColors.Window;
            this.tvGameObjects.Columns.Add(this.treeColumn1);
            this.tvGameObjects.Columns.Add(this.treeColumn2);
            this.tvGameObjects.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvGameObjects.DefaultToolTipProvider = null;
            this.tvGameObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvGameObjects.DragDropMarkColor = System.Drawing.Color.Black;
            this.tvGameObjects.FullRowSelect = true;
            this.tvGameObjects.LineColor = System.Drawing.SystemColors.ControlDark;
            this.tvGameObjects.LoadOnDemand = true;
            this.tvGameObjects.Location = new System.Drawing.Point(0, 26);
            this.tvGameObjects.Model = null;
            this.tvGameObjects.Name = "tvGameObjects";
            this.tvGameObjects.NodeControls.Add(this.nodeIcon1);
            this.tvGameObjects.NodeControls.Add(this.nodeTextBox2);
            this.tvGameObjects.NodeControls.Add(this._visible);
            this.tvGameObjects.SelectedNode = null;
            this.tvGameObjects.Size = new System.Drawing.Size(294, 244);
            this.tvGameObjects.TabIndex = 2;
            this.tvGameObjects.Text = "treeViewAdv1";
            this.tvGameObjects.UseColumns = true;
            this.tvGameObjects.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvGameObjects_ItemDrag);
            this.tvGameObjects.SelectionChanged += new System.EventHandler(this.tvGameObjects_SelectionChanged);
            this.tvGameObjects.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvGameObjects_DragDrop);
            this.tvGameObjects.DragOver += new System.Windows.Forms.DragEventHandler(this.tvGameObjects_DragOver);
            // 
            // nodeIcon1
            // 
            this.nodeIcon1.DataPropertyName = "Icon";
            this.nodeIcon1.LeftMargin = 1;
            this.nodeIcon1.ParentColumn = this.treeColumn1;
            this.nodeIcon1.ScaleMode = TokED.UI.Tree.ImageScaleMode.Clip;
            // 
            // nodeTextBox2
            // 
            this.nodeTextBox2.DataPropertyName = "Name";
            this.nodeTextBox2.IncrementalSearchEnabled = true;
            this.nodeTextBox2.LeftMargin = 3;
            this.nodeTextBox2.ParentColumn = this.treeColumn1;
            // 
            // _visible
            // 
            this._visible.DataPropertyName = "Visible";
            this._visible.LeftMargin = 0;
            this._visible.ParentColumn = this.treeColumn2;
            // 
            // pGameObjectHeader
            // 
            this.pGameObjectHeader.Controls.Add(this.bRemoveGameObject);
            this.pGameObjectHeader.Controls.Add(this.bAddGameObject);
            this.pGameObjectHeader.Controls.Add(this.cbAvailableGameObjects);
            this.pGameObjectHeader.Controls.Add(this.bSaveProject);
            this.pGameObjectHeader.Controls.Add(this.bLoadProject);
            this.pGameObjectHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pGameObjectHeader.Location = new System.Drawing.Point(0, 0);
            this.pGameObjectHeader.Name = "pGameObjectHeader";
            this.pGameObjectHeader.Size = new System.Drawing.Size(294, 26);
            this.pGameObjectHeader.TabIndex = 1;
            // 
            // bRemoveGameObject
            // 
            this.bRemoveGameObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bRemoveGameObject.FlatAppearance.BorderSize = 0;
            this.bRemoveGameObject.ImageKey = "Delete";
            this.bRemoveGameObject.ImageList = this.imageList;
            this.bRemoveGameObject.Location = new System.Drawing.Point(271, 0);
            this.bRemoveGameObject.Margin = new System.Windows.Forms.Padding(0);
            this.bRemoveGameObject.Name = "bRemoveGameObject";
            this.bRemoveGameObject.Size = new System.Drawing.Size(23, 25);
            this.bRemoveGameObject.TabIndex = 4;
            this.bRemoveGameObject.UseVisualStyleBackColor = true;
            this.bRemoveGameObject.Click += new System.EventHandler(this.bRemoveGameObject_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Add");
            this.imageList.Images.SetKeyName(1, "Delete");
            this.imageList.Images.SetKeyName(2, "Load");
            this.imageList.Images.SetKeyName(3, "Save");
            this.imageList.Images.SetKeyName(4, "Collapse");
            this.imageList.Images.SetKeyName(5, "Expand");
            this.imageList.Images.SetKeyName(6, "NotVisible");
            this.imageList.Images.SetKeyName(7, "Visible");
            this.imageList.Images.SetKeyName(8, "GameObject.png");
            // 
            // bAddGameObject
            // 
            this.bAddGameObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bAddGameObject.FlatAppearance.BorderSize = 0;
            this.bAddGameObject.ImageKey = "Add";
            this.bAddGameObject.ImageList = this.imageList;
            this.bAddGameObject.Location = new System.Drawing.Point(248, 0);
            this.bAddGameObject.Margin = new System.Windows.Forms.Padding(0);
            this.bAddGameObject.Name = "bAddGameObject";
            this.bAddGameObject.Size = new System.Drawing.Size(23, 25);
            this.bAddGameObject.TabIndex = 3;
            this.bAddGameObject.UseVisualStyleBackColor = true;
            this.bAddGameObject.Click += new System.EventHandler(this.bAddGameObject_Click);
            // 
            // cbAvailableGameObjects
            // 
            this.cbAvailableGameObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAvailableGameObjects.DataSource = this.bsAvailableGameObjects;
            this.cbAvailableGameObjects.DisplayMember = "Name";
            this.cbAvailableGameObjects.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbAvailableGameObjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAvailableGameObjects.FormattingEnabled = true;
            this.cbAvailableGameObjects.ImageList = this.imageList;
            this.cbAvailableGameObjects.ItemHeight = 18;
            this.cbAvailableGameObjects.Location = new System.Drawing.Point(47, 1);
            this.cbAvailableGameObjects.Name = "cbAvailableGameObjects";
            this.cbAvailableGameObjects.Size = new System.Drawing.Size(201, 24);
            this.cbAvailableGameObjects.TabIndex = 2;
            // 
            // bsAvailableGameObjects
            // 
            this.bsAvailableGameObjects.DataSource = typeof(TokED.UI.ImageComboBoxItem);
            // 
            // bSaveProject
            // 
            this.bSaveProject.FlatAppearance.BorderSize = 0;
            this.bSaveProject.ImageKey = "Save";
            this.bSaveProject.ImageList = this.imageList;
            this.bSaveProject.Location = new System.Drawing.Point(23, 0);
            this.bSaveProject.Name = "bSaveProject";
            this.bSaveProject.Size = new System.Drawing.Size(23, 25);
            this.bSaveProject.TabIndex = 1;
            this.bSaveProject.UseVisualStyleBackColor = true;
            // 
            // bLoadProject
            // 
            this.bLoadProject.FlatAppearance.BorderSize = 0;
            this.bLoadProject.ImageKey = "Load";
            this.bLoadProject.ImageList = this.imageList;
            this.bLoadProject.Location = new System.Drawing.Point(0, 0);
            this.bLoadProject.Name = "bLoadProject";
            this.bLoadProject.Size = new System.Drawing.Size(23, 25);
            this.bLoadProject.TabIndex = 0;
            this.bLoadProject.UseVisualStyleBackColor = true;
            // 
            // pContent
            // 
            this.pContent.AutoScroll = true;
            this.pContent.AutoSize = true;
            this.pContent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pContent.Location = new System.Drawing.Point(0, 26);
            this.pContent.Name = "pContent";
            this.pContent.Size = new System.Drawing.Size(294, 248);
            this.pContent.TabIndex = 1;
            // 
            // pComponentHeader
            // 
            this.pComponentHeader.Controls.Add(this.button1);
            this.pComponentHeader.Controls.Add(this.bAddGameComponent);
            this.pComponentHeader.Controls.Add(this.label1);
            this.pComponentHeader.Controls.Add(this.cbAvailableComponents);
            this.pComponentHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pComponentHeader.Location = new System.Drawing.Point(0, 0);
            this.pComponentHeader.Name = "pComponentHeader";
            this.pComponentHeader.Size = new System.Drawing.Size(294, 26);
            this.pComponentHeader.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.ImageKey = "Delete";
            this.button1.ImageList = this.imageList;
            this.button1.Location = new System.Drawing.Point(271, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 25);
            this.button1.TabIndex = 6;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // bAddGameComponent
            // 
            this.bAddGameComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bAddGameComponent.FlatAppearance.BorderSize = 0;
            this.bAddGameComponent.ImageKey = "Add";
            this.bAddGameComponent.ImageList = this.imageList;
            this.bAddGameComponent.Location = new System.Drawing.Point(248, 0);
            this.bAddGameComponent.Margin = new System.Windows.Forms.Padding(0);
            this.bAddGameComponent.Name = "bAddGameComponent";
            this.bAddGameComponent.Size = new System.Drawing.Size(23, 25);
            this.bAddGameComponent.TabIndex = 5;
            this.bAddGameComponent.UseVisualStyleBackColor = true;
            this.bAddGameComponent.Click += new System.EventHandler(this.bAddComponent_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Components";
            // 
            // cbAvailableComponents
            // 
            this.cbAvailableComponents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAvailableComponents.DataSource = this.bsAvailableComponents;
            this.cbAvailableComponents.DisplayMember = "Name";
            this.cbAvailableComponents.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbAvailableComponents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAvailableComponents.FormattingEnabled = true;
            this.cbAvailableComponents.ImageList = this.imageList;
            this.cbAvailableComponents.ItemHeight = 18;
            this.cbAvailableComponents.Location = new System.Drawing.Point(75, 1);
            this.cbAvailableComponents.Name = "cbAvailableComponents";
            this.cbAvailableComponents.Size = new System.Drawing.Size(172, 24);
            this.cbAvailableComponents.TabIndex = 0;
            // 
            // bsAvailableComponents
            // 
            this.bsAvailableComponents.DataSource = typeof(TokED.UI.ImageComboBoxItem);
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl.Location = new System.Drawing.Point(0, 0);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(584, 548);
            this.glControl.TabIndex = 0;
            this.glControl.VSync = false;
            this.glControl.Load += new System.EventHandler(this.glControl_Load);
            this.glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl_Paint);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slFPS,
            this.slGameObjectCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 548);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(882, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // slFPS
            // 
            this.slFPS.Name = "slFPS";
            this.slFPS.Size = new System.Drawing.Size(29, 17);
            this.slFPS.Text = "FPS:";
            // 
            // slGameObjectCount
            // 
            this.slGameObjectCount.Name = "slGameObjectCount";
            this.slGameObjectCount.Size = new System.Drawing.Size(118, 17);
            this.slGameObjectCount.Text = "toolStripStatusLabel1";
            // 
            // EditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 570);
            this.Controls.Add(this.spliterHorizontal);
            this.Controls.Add(this.statusStrip1);
            this.Name = "EditorWindow";
            this.Text = "TokED";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorWindow_FormClosing);
            this.spliterHorizontal.Panel1.ResumeLayout(false);
            this.spliterHorizontal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spliterHorizontal)).EndInit();
            this.spliterHorizontal.ResumeLayout(false);
            this.spliterVertical.Panel1.ResumeLayout(false);
            this.spliterVertical.Panel2.ResumeLayout(false);
            this.spliterVertical.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spliterVertical)).EndInit();
            this.spliterVertical.ResumeLayout(false);
            this.pGameObjectHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsAvailableGameObjects)).EndInit();
            this.pComponentHeader.ResumeLayout(false);
            this.pComponentHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsAvailableComponents)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer spliterHorizontal;
        private System.Windows.Forms.SplitContainer spliterVertical;
        private OpenTK.GLControl glControl;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel slFPS;
        private System.Windows.Forms.Panel pGameObjectHeader;
        private System.Windows.Forms.Button bLoadProject;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button bSaveProject;
        private UI.ImageComboBox cbAvailableGameObjects;
        private System.Windows.Forms.Button bRemoveGameObject;
        private System.Windows.Forms.Button bAddGameObject;
        private System.Windows.Forms.BindingSource bsAvailableGameObjects;
        private System.Windows.Forms.ToolStripStatusLabel slGameObjectCount;
        private System.Windows.Forms.Panel pComponentHeader;
        private System.Windows.Forms.Label label1;
        private UI.ImageComboBox cbAvailableComponents;
        private System.Windows.Forms.Panel pContent;
        private UI.Tree.TreeViewAdv tvGameObjects;
        private UI.Tree.NodeControls.NodeIcon nodeIcon1;
        private UI.Tree.NodeControls.NodeCheckBox _visible;
        private UI.Tree.TreeColumn treeColumn1;
        private UI.Tree.TreeColumn treeColumn2;
        private UI.Tree.NodeControls.NodeTextBox nodeTextBox2;
        private System.Windows.Forms.BindingSource bsAvailableComponents;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bAddGameComponent;
    }
}