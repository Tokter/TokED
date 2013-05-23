using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TokED.Editors;
using TokED.UI;

namespace TokED
{
    public class EditorModel : INotifyPropertyChanged
    {
        private GameObject _project;
        private GameObject _selectedGameObject;
        private Editor _editor;
        private BindingList<ImageComboBoxItem> _availableGameObjects = new BindingList<ImageComboBoxItem>();
        private BindingList<ImageComboBoxItem> _availableComponents = new BindingList<ImageComboBoxItem>();
        private BindingList<Inspector> _inspectors = new BindingList<Inspector>();

        public EditorModel()
        {
            var root = new GameObject();
            root.Model = new GameObjectTreeModel();
            _project = Plugins.Container.ResolveNamed<GameObject>("Project");
            root.AddChild(_project);
            SelectedGameObject = _project;
            RefreshGameObjectList();
            RefreshGameObjectInspectors();
        }

        #region Editor

        public Editor Editor
        {
            get { return _editor; }
        }

        public BindingList<ImageComboBoxItem> AvailableGameObjects
        {
            get { return _availableGameObjects; }
        }

        public BindingList<ImageComboBoxItem> AvailableComponents
        {
            get { return _availableComponents; }
        }

        public BindingList<Inspector> Inspectors
        {
            get { return _inspectors; }
        }

        public GameObject Project
        {
            get { return _project; }
        }

        public GameObject SelectedGameObject
        {
            get { return _selectedGameObject; }
            set
            {
                if (value != null && _selectedGameObject != value)
                {
                    _selectedGameObject = value;

                    //Walking up the chain, trying to find an editor
                    Editor newEditor = null;
                    GameObject go = _selectedGameObject;
                    while (go != null && newEditor == null)
                    {
                        if (go.ExportName != null) newEditor = Plugins.Container.ResolveOptionalNamed<Editor>(go.ExportName);
                        if (newEditor == null) go = go.Parent;
                    }

                    if (_editor != newEditor)
                    {
                        _project.UnLoad();
                        if (_editor != null) _editor.Dispose();
                        _editor = newEditor;
                    }

                    //If we did not find an editor, show empty default editor;
                    if (_editor == null) _editor = Plugins.Container.ResolveOptionalNamed<Editor>("Editor");

                    _editor.SelectedGameObject = _selectedGameObject;

                    RefreshGameObjectList();
                    RefreshGameObjectInspectors();
                }
            }
        }

        #endregion

        public void AddGameObject(string gameObjectName)
        {
            if (_selectedGameObject != null)
            {
                _selectedGameObject.AddChild(gameObjectName);
            }
        }

        public void RemoveGameObject()
        {
            var go = _selectedGameObject;
            _selectedGameObject = go.Parent;
            go.Parent.RemoveChild(go);
            go.Dispose();
        }

        public void RefreshGameObjectList()
        {
            _availableGameObjects.Clear();
            _availableComponents.Clear();
            if (_selectedGameObject == null) return;

            var availableGameObjects = Plugins.GetKeys<GameObject>().Where(k => _selectedGameObject.IsAcceptableChild(k)).ToList<string>();
            availableGameObjects.Sort();
            foreach (var availableGameObject in availableGameObjects)
            {
                var item = new ImageComboBoxItem();
                item.Name = availableGameObject;
                item.IconName = GameObject.IconName(availableGameObject);
                _availableGameObjects.Add(item);
            }

            var availableComponents = Plugins.GetKeys<Component>().ToList<string>();
            availableComponents.Sort();
            foreach (var availableComponent in availableComponents)
            {
                var item = new ImageComboBoxItem();
                item.Name = availableComponent;
                item.IconName = Component.IconName(availableComponent);
                _availableComponents.Add(item);                
            }

            NotifyChange("AvailableGameObjects");
            NotifyChange("AvailableComponents");
        }

        public void RefreshGameObjectInspectors()
        {
            _inspectors.Clear();
            var ins = Plugins.Container.ResolveOptionalNamed<GameObjectIns>(_selectedGameObject.ExportName);
            if (ins == null) ins = new GameObjectIns();
            ins.GameObject = _selectedGameObject;
            ins.Tag = _selectedGameObject.ExportName;
            _inspectors.Add(ins);

            foreach (var c in _selectedGameObject.Components)
            {
                var cIns = Plugins.Container.ResolveOptionalNamed<ComponentIns>(c.ExportName);
                cIns.Component = c;
                cIns.Tag = c.ExportName;
                _inspectors.Add(cIns);
            }
        }

        public void AddComponent(string componentName)
        {
            if (_selectedGameObject != null && !_selectedGameObject.HasComponent(componentName))
            {
                _selectedGameObject.AddComponent(componentName);
                RefreshGameObjectInspectors();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyChange([CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(propertyName);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual void OnPropertyChanged(string name)
        {
        }

        #endregion


    }
}
