using Autofac;
using Squid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED.Editors;
using TokED.Properties;

namespace TokED.UI
{
    public class EditorDesktop : Desktop
    {
        private GameObject _project;
        private GameObject _selectedGameObject;
        private Frame _frameLeft;
        private DropDownList _gameObjectDropDown;
        private DropDownList _componentsDropDown;
        private Button _addGameObject;
        private Button _addComponent;
        private Button _removeGameObject;
        private Button _removeComponent;
        private Button _saveButton;
        private Button _loadButton;
        private TreeView _gameObjectTree;
        private TestSplitContainer _splitContainer;
        private StackPanel _inspectorPanel;
        private string _projectFilename = "";
        private Editor _editor;

        public EditorDesktop()
        {
            _project = Plugins.Container.ResolveNamed<GameObject>("Project");
            _project.Expanded = true;
            GuiHost.SetSkin(EditorSkin.CreateSkin());

            //Setup Tooltip
            var tt = new SimpleTooltip();
            tt.Delay = 2000;
            this.TooltipControl = tt;

            //Left Frame
            _frameLeft = this.AddFrame("frame", Width, 600, DockStyle.Left);
            _frameLeft.Opacity = 0.8f;


            var _toolbarFrame = _frameLeft.AddFrame(Width, 24, DockStyle.Top);

            _saveButton = _toolbarFrame.AddButton("save", 2, 2, 20, 20);
            _saveButton.Tooltip = Properties.Resources.SaveButtonToolTip;
            _saveButton.MouseClick += SaveButton_MouseClick;
            _loadButton = _toolbarFrame.AddButton("load", 24, 2, 20, 20);
            _loadButton.Tooltip = Properties.Resources.LoadButtonToolTip;
            _loadButton.MouseClick += LoadButton_MouseClick;

            _gameObjectDropDown = _toolbarFrame.AddDropDownList(46, 2, Width - 88);
            _gameObjectDropDown.SelectedItemChanged += GameObjectDropDown_SelectedItemChanged;
            _addGameObject = _toolbarFrame.AddDropDownButton(Width - 42, 2, DropDownButtonType.Plus);
            _addGameObject.Tooltip = Resources.AddGameObject;
            _addGameObject.MouseClick += AddGameObject_MouseClick;
            _removeGameObject = _toolbarFrame.AddDropDownButton(Width - 22, 2, DropDownButtonType.Minus);
            _removeGameObject.Tooltip = Resources.RemoveGameObject;
            _removeGameObject.MouseClick += RemoveGameObject_MouseClick;

            _splitContainer = _frameLeft.AddSplitContainer(Orientation.Vertical);

            _gameObjectTree = _splitContainer.SplitFrame1.AddTreeView(2, 42, 246, 330);
            _gameObjectTree.Dock = DockStyle.Fill;
            _gameObjectTree.SelectedNodeChanged += GameObjectTree_SelectedNodeChanged;

            var componentFrame = _splitContainer.SplitFrame2.AddFrame(0, 20, DockStyle.Top);
            _componentsDropDown = componentFrame.AddDropDownList(2, 2, Width - 44);
            _componentsDropDown.SelectedItemChanged += ComponentsDropDown_SelectedItemChanged;
            _addComponent = componentFrame.AddDropDownButton(Width - 42, 2, DropDownButtonType.Plus);
            _addComponent.Enabled = false;
            _addComponent.MouseClick += AddComponent_MouseClick;
            _removeComponent = componentFrame.AddDropDownButton(Width - 22, 2, DropDownButtonType.Minus);
            _removeComponent.Enabled = false;
            _removeComponent.MouseClick += RemoveComponent_MouseClick;

            _inspectorPanel = _splitContainer.SplitFrame2.AddStackPanel();

            RefreshGameObjectTree();
            RefreshGameObjectList();
            GameObjectTree_SelectedNodeChanged(this, null);
        }

        public int Width
        {
            get { return 250; }
        }

        #region Editor

        public Editor Editor
        {
            get { return _editor; }
        }

        #endregion

        #region Load & Save

        void SaveButton_MouseClick(Control sender, MouseEventArgs args)
        {
            _project.Save(_projectFilename);
        }

        void LoadButton_MouseClick(Control sender, MouseEventArgs args)
        {
            _project.Load(ref _projectFilename);
            RefreshGameObjectTree();
        }

        #endregion

        #region GameObject Tree & Drop Down


        void AddGameObject_MouseClick(Control sender, MouseEventArgs args)
        {
            if (_selectedGameObject != null)
            {
                _selectedGameObject.AddChild(_gameObjectDropDown.SelectedItem.Text);
                RefreshGameObjectTree();
            }
        }

        void RemoveGameObject_MouseClick(Control sender, MouseEventArgs args)
        {
            if (_selectedGameObject != null)
            {
                _selectedGameObject.Parent.RemoveChild(_selectedGameObject);
                var parentGameObject = (_gameObjectTree.SelectedNode.Parent as GameObjectTreeNode).GameObject;
                _gameObjectTree.SelectedNode = null;
                RefreshGameObjectTree();
                var parent = FindGOTreeNode(_gameObjectTree.Nodes, parentGameObject);
                if (parent.Nodes.Count > 0)
                {
                    parent.Nodes[0].Selected = true;
                }
                else
                {
                    parent.Selected = true;
                }
            }
        }

        private GameObjectTreeNode FindGOTreeNode(ActiveList<TreeNode> nodes, GameObject gameObject)
        {
            foreach (var node in nodes)
            {
                var goNode = (node as GameObjectTreeNode);
                if (goNode.GameObject == gameObject)
                    return goNode;
                else
                {
                    var result = FindGOTreeNode(goNode.Nodes, gameObject);
                    if (result != null) return result;
                }
            }
            return null;
        }

        void GameObjectDropDown_SelectedItemChanged(Control sender, ListBoxItem value)
        {
            _addGameObject.Enabled = true;
        }

        void GameObjectTree_SelectedNodeChanged(Control sender, TreeNode value)
        {
            if (value != null && value is GameObjectTreeNode)
            {
                var newObject = (value as GameObjectTreeNode).GameObject;
                if (newObject != null && newObject != _selectedGameObject)
                {
                    _selectedGameObject = newObject;
                    RefreshGameObjectList();
                    RefreshGameObjectEditors();
                    _removeGameObject.Enabled = (_selectedGameObject != null && _selectedGameObject != _project);

                    _project.UnLoad();
                    if (_editor != null) _editor.Dispose();

                    var go = _selectedGameObject;
                    _editor = null;
                    //Look for an editor
                    while (go != null && _editor == null)
                    {
                        _editor = Plugins.Container.ResolveOptionalNamed<Editor>(go.ExportName);
                        if (_editor == null) go = go.Parent;
                    }
                    //If we did not find an editor, show empty default editor;
                    if (_editor == null) _editor = Plugins.Container.ResolveOptionalNamed<Editor>("Editor");

                    _editor.SelectedGameObject = _selectedGameObject;
                }
            }
        }

        private void ClearGameObjectTree(ActiveList<TreeNode> nodes)
        {
            foreach (var node in nodes)
            {
                ClearGameObjectTree(node.Nodes);
                if (node is IDisposable) (node as IDisposable).Dispose();
            }
            nodes.Clear();
        }

        private void AddGameObjectNode(ActiveList<TreeNode> nodes, GameObject gameObject)
        {
            var node = new GameObjectTreeNode(gameObject);
            //if (gameObject == _selectedGameObject) node.Selected = true;
            nodes.Add(node);
            if (gameObject == _selectedGameObject) _gameObjectTree.SelectedNode = node;            
            foreach (var go in gameObject.Children)
            {
                AddGameObjectNode(node.Nodes, go);
            }
        }

        public void RefreshGameObjectTree()
        {
            ClearGameObjectTree(_gameObjectTree.Nodes);
            AddGameObjectNode(_gameObjectTree.Nodes, _project);
            if (_gameObjectTree.SelectedNode == null) _gameObjectTree.SelectedNode = _gameObjectTree.Nodes[0];
        }

        private bool DoesNotAllowChildren(GameObject parent, string objName)
        {
            if (parent == null || parent.ExportName == null) return false;
            var parentMeta = Plugins.GetMetadata<GameObject>(parent.ExportName);
            var meta = Plugins.GetMetadata<GameObject>(objName);
            if (parentMeta.ContainsKey("IsNotAllowingChildred") && (bool)parentMeta["IsNotAllowingChildred"] == true)
            {
                return true;
            }
            return DoesNotAllowChildren(parent.Parent, objName);
        }

        private bool DoesNotAllowChild(GameObject parent, string objName)
        {
            if (parent == null || parent.ExportName == null) return false;
            var parentMeta = Plugins.GetMetadata<GameObject>(parent.ExportName);
            var meta = Plugins.GetMetadata<GameObject>(objName);

            if (parentMeta.ContainsKey("IsNotAllowingChild"))
            {
                foreach (var unacceptable in (parentMeta["IsNotAllowingChild"] as string[]))
                {
                    if (objName == unacceptable) return true;
                }
            }
            return DoesNotAllowChild(parent.Parent, objName);
        }

        private bool AllowsChild(GameObject parent, string objName)
        {
            if (parent == null || parent.ExportName == null) return true;
            var parentMeta = Plugins.GetMetadata<GameObject>(parent.ExportName);
            var meta = Plugins.GetMetadata<GameObject>(objName);

            if (parentMeta.ContainsKey("IsAllowingChild"))
            {
                foreach (var required in (parentMeta["IsAllowingChild"] as string[]))
                {
                    if (objName != required) return false;
                }
            }
            return AllowsChild(parent.Parent, objName);
        }

        private bool HasRequiredParent(GameObject parent, string objName)
        {
            if (parent == null || parent.ExportName == null) return false;
            var parentMeta = Plugins.GetMetadata<GameObject>(parent.ExportName);
            var meta = Plugins.GetMetadata<GameObject>(objName);

            if (meta.ContainsKey("IsRequiringParent"))
            {
                if (parent.ExportName == meta["IsRequiringParent"].ToString()) return true;
                else return HasRequiredParent(parent.Parent, objName);
            }
            else return true;
        }

        private bool IsAcceptable(GameObject parent, string objName)
        {
            if (DoesNotAllowChildren(parent, objName)) return false;
            if (DoesNotAllowChild(parent, objName)) return false;
            if (!AllowsChild(parent, objName)) return false;
            if (!HasRequiredParent(parent, objName)) return false;
            return true;
        }

        public List<string> AvailableGameObject
        {
            get
            {
                if (_selectedGameObject == null) return null;
                var result = Plugins.GetKeys<GameObject>().Where(k => IsAcceptable(_selectedGameObject, k)).ToList<string>();
                result.Sort();
                return result;
            }
        }

        public void RefreshGameObjectList()
        {
            _gameObjectDropDown.Label.Text = "Game Objects";
            _gameObjectDropDown.Items.Clear();
            _addGameObject.Enabled = false;
            if (_selectedGameObject != null)
            {
                foreach (var go in AvailableGameObject)
                {
                    var item = new ImageItem(go);
                    item.Text = go;
                    item.Size = new Squid.Point(200, 15);
                    item.Style = "comboItem";
                    _gameObjectDropDown.Items.Add(item);
                }
            }

            _componentsDropDown.Label.Text = "Components";
            _componentsDropDown.Items.Clear();
            foreach (var c in Plugins.GetKeys<Component>())
            {
                var item = new ImageItem(c);
                item.Text = c;
                item.Size = new Squid.Point(200, 15);
                item.Style = "comboItem";
                _componentsDropDown.Items.Add(item);
            }
        }

        public void RefreshGameObjectEditors()
        {
            foreach (var c in _inspectorPanel.Content.Controls)
            {
                if (c is GameObjectIns) (c as GameObjectIns).Dispose();
            }
            _inspectorPanel.Content.Controls.Clear();

            if (_selectedGameObject != null)
            {
                _inspectorPanel.AddCategory(_selectedGameObject.ExportName);
                var frame = Plugins.Container.ResolveOptionalNamed<GameObjectIns>(_selectedGameObject.ExportName);
                if (frame == null) frame = new GameObjectIns();
                frame.GameObject = _selectedGameObject;
                _inspectorPanel.AddControl(frame);

                foreach (var c in _selectedGameObject.Components)
                {
                    var cframe = Plugins.Container.ResolveOptionalNamed<ComponentIns>(c.ExportName);
                    if (cframe != null)
                    {
                        cframe.Component = c;
                        _inspectorPanel.AddCategory(c.ExportName);
                        _inspectorPanel.AddControl(cframe);
                    }
                }
            }
        }

        #endregion

        #region Components

        private void RefreshComponentsButton()
        {
            if (_selectedGameObject != null)
            {
                if (_selectedGameObject.HasComponent(_componentsDropDown.SelectedItem.Text))
                {
                    _addComponent.Enabled = false;
                    _removeComponent.Enabled = true;
                }
                else
                {
                    _addComponent.Enabled = true;
                    _removeComponent.Enabled = false;
                }
            }
            else
            {
                _addComponent.Enabled = false;
                _removeComponent.Enabled = false;
            }
        }

        private void ComponentsDropDown_SelectedItemChanged(Control sender, ListBoxItem value)
        {
            RefreshComponentsButton();
        }

        private void AddComponent_MouseClick(Control sender, MouseEventArgs args)
        {
            if (_selectedGameObject != null && !_selectedGameObject.HasComponent(_componentsDropDown.SelectedItem.Text))
            {
                _selectedGameObject.AddComponent(_componentsDropDown.SelectedItem.Text);
                RefreshGameObjectEditors();
                RefreshComponentsButton();
            }
        }

        private void RemoveComponent_MouseClick(Control sender, MouseEventArgs args)
        {
            if (_selectedGameObject != null && _selectedGameObject.HasComponent(_componentsDropDown.SelectedItem.Text))
            {
                _selectedGameObject.RemoveComponent(_componentsDropDown.SelectedItem.Text);
                RefreshGameObjectEditors();
                RefreshComponentsButton();
            }
        }

        #endregion
    }
}
