using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using TokGL;
using TokED.UI;
using TokED.UI.Tree;

namespace TokED
{
    public partial class EditorWindow : Form
    {
        private Stopwatch _sw = new Stopwatch();
        private EditorModel _model;

        public EditorWindow()
        {
            InitializeComponent();
            Plugins.LoadPlugins();

            //Load Icons
            UIIcon.ImageList = imageList;
            var icons = Plugins.GetKeys<UIIcon>();
            foreach (var icon in icons)
            {
                Plugins.Container.ResolveNamed<UIIcon>(icon);
            }

            //Load Tools
            var tools = Plugins.GetKeys<EditorTool>();
            foreach (var tool in tools)
            {
                Tools.AddTool(Plugins.Container.ResolveKeyed<EditorTool>(tool));
            }

            glControl.MouseMove += glControl_MouseMove;
            glControl.MouseWheel += glControl_MouseWheel;
            glControl.MouseDown += glControl_MouseDown;
            glControl.MouseUp += glControl_MouseUp;
            glControl.KeyDown += glControl_KeyDown;
            glControl.KeyUp += glControl_KeyUp;
        }

        private double ComputeTimeSlice()
        {
            _sw.Stop();
            double timeslice = _sw.Elapsed.TotalMilliseconds;
            _sw.Reset();
            _sw.Start();
            return timeslice;
        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            Application.Idle += Application_Idle;
            _sw.Start();

            _model = new EditorModel();
            bsAvailableGameObjects.DataSource = _model.AvailableGameObjects;
            bsAvailableComponents.DataSource = _model.AvailableComponents;
            _model.Project.Parent.Model.ImageList = imageList;
            tvGameObjects.Model = _model.Project.Parent.Model;
            _model.Inspectors.ListChanged += Inspectors_ListChanged;
            Inspectors_ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemAdded, 0));
        }

        private void Inspectors_ListChanged(object sender, ListChangedEventArgs e)
        {
            pContent.Controls.Clear();
            Inspector ins;
            for (int i = _model.Inspectors.Count - 1; i >= 0; i--)
            {
                ins = _model.Inspectors[i];
                var expander = new Expander();
                expander.Dock = DockStyle.Top;
                Image icon = null;
                if (ins.ExportName != null)
                {
                    var iconName = Inspector.IconName(ins.ExportName);
                    if (iconName != null) icon = imageList.Images[iconName];
                }
                ExpanderHelper.CreateLabelHeader(expander, ins.Tag.ToString(), SystemColors.ActiveBorder, imageList.Images["Collapse"], imageList.Images["Expand"], icon);
                ins.Dock = DockStyle.Top;
                expander.Content = ins;
                pContent.Controls.Add(expander);
            }
        }

        private void EditorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            RenderObject.FreeAll();
        }

        private int frameCounter = 0;
        private double frameTime = 0;
        private void Application_Idle(object sender, EventArgs e)
        {
            double milliseconds = ComputeTimeSlice();
            Update(milliseconds / 1000.0d);
            frameCounter++;
            frameTime += milliseconds;
            if (frameTime > 1000)
            {
                slFPS.Text = string.Format("FPS: {0}", frameCounter);
                slGameObjectCount.Text = string.Format("GO Count: {0}", GameObject.Count);
                frameTime -= 1000;
                frameCounter = 0;
            }
            glControl.Invalidate();
        }

        private void Update(double deltaSeconds)
        {
            _model.Editor.Update(deltaSeconds);
        }

        private void Draw()
        {
            GL.Viewport(ClientRectangle.Size);
            GL.ClearColor(0.20f, 0.20f, 0.20f, 0.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _model.Editor.Resize(ClientRectangle.Width, ClientRectangle.Height);
            _model.Editor.Draw();
            glControl.SwapBuffers();
        }

        private void gameObjectTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _model.SelectedGameObject = e.Node.Tag as GameObject;
        }

        private void bAddGameObject_Click(object sender, EventArgs e)
        {
            _model.AddGameObject((cbAvailableGameObjects.SelectedItem as ImageComboBoxItem).Name);
        }

        private void bRemoveGameObject_Click(object sender, EventArgs e)
        {
             //var parent = gameObjectTree.SelectedNode.Parent;
            //_model.RemoveGameObject();
            //gameObjectTree.SelectedNode = parent;
        }

        #region Input

        void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            _model.Editor.MouseMove(e);
        }

        void glControl_MouseWheel(object sender, MouseEventArgs e)
        {
            _model.Editor.MouseWheel(e);
        }

        void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            _model.Editor.MouseUp(e);
        }

        void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            _model.Editor.MouseDown(e);
        }

        void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            _model.Editor.KeyUp(e);
        }

        void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            _model.Editor.KeyDown(e);
        }


        #endregion

        private void tvGameObjects_SelectionChanged(object sender, EventArgs e)
        {
            if (tvGameObjects.SelectedNode != null)
                _model.SelectedGameObject = tvGameObjects.SelectedNode.Tag as GameObject;
            else
                _model.SelectedGameObject = null;
        }

        #region Drag & Drop

        private void tvGameObjects_ItemDrag(object sender, ItemDragEventArgs e)
        {
            tvGameObjects.DoDragDropSelectedNodes(DragDropEffects.Move);
        }

        private void tvGameObjects_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNodeAdv[])) && tvGameObjects.DropPosition.Node != null)
            {
                TreeNodeAdv[] nodes = e.Data.GetData(typeof(TreeNodeAdv[])) as TreeNodeAdv[];
                GameObject dropNode = tvGameObjects.DropPosition.Node.Tag as GameObject;

                if (dropNode == _model.Project && tvGameObjects.DropPosition.Position != NodePosition.Inside)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }

                if (tvGameObjects.DropPosition.Position != NodePosition.Inside) dropNode = dropNode.Parent;

                foreach (TreeNodeAdv n in nodes)
                {
                    var go = n.Tag as GameObject;
                    if (CheckNodeParent(dropNode, go) == false || dropNode.IsAcceptableChild(go.ExportName) == false)
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }
                }

                e.Effect = e.AllowedEffect;
            }
        }

        private bool CheckNodeParent(GameObject parent, GameObject node)
        {
            while (parent != null)
            {
                if (node == parent)
                    return false;
                else
                    parent = parent.Parent;
            }
            return true;
        }

        private void tvGameObjects_DragDrop(object sender, DragEventArgs e)
        {
            tvGameObjects.BeginUpdate();

            TreeNodeAdv[] nodes = (TreeNodeAdv[])e.Data.GetData(typeof(TreeNodeAdv[]));
            GameObject dropNode = tvGameObjects.DropPosition.Node.Tag as GameObject;
            if (tvGameObjects.DropPosition.Position == NodePosition.Inside)
            {
                foreach (TreeNodeAdv n in nodes)
                {
                    var go = n.Tag as GameObject;
                    go.Parent.RemoveChild(go);
                    dropNode.AddChild(go);
                }
                tvGameObjects.DropPosition.Node.IsExpanded = true;
            }
            else
            {
                GameObject parent = dropNode.Parent;
                int index = dropNode.Index;
                foreach (TreeNodeAdv n in nodes)
                {
                    var go = n.Tag as GameObject;
                    go.Parent.RemoveChild(go);
                    parent.InsertChild(index, go);
                }
                tvGameObjects.DropPosition.Node.IsExpanded = true;
            }

            tvGameObjects.EndUpdate();
        }

        #endregion

        private void bAddComponent_Click(object sender, EventArgs e)
        {
            _model.AddComponent((cbAvailableComponents.SelectedItem as ImageComboBoxItem).Name);
        }

    }
}
