﻿using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace TokED
{
    public class GameObject : IXmlSerializable, INotifyPropertyChanged, IDisposable
    {
        private List<GameObject> _children = new List<GameObject>();
        private Dictionary<Type, Component> _components = new Dictionary<Type, Component>();
        private string _name;
        private GameObject _parent = null;
        private bool _expanded = true;
        private bool _visible = true;

        /// <summary>
        /// Helper Property to get to the name that the GameObject is exported as
        /// </summary>
        public string ExportName
        {
            get
            {
                var test = this.GetType().GetCustomAttributes(typeof(ExportAttribute), false);
                var test2 = test.Where(a => a is ExportAttribute && (a as ExportAttribute).ContractName != null).Select(a => (a as ExportAttribute).ContractName);
                return this.GetType().GetCustomAttributes(typeof(ExportAttribute), false)
                    .Where(a => a is ExportAttribute && (a as ExportAttribute).ContractName != null)
                    .Select(a => (a as ExportAttribute).ContractName)
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Name of this GameObject instance
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyChange(); }
        }

        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                _expanded = value;
                NotifyChange();
            }
        }

        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                foreach (var child in _children)
                {
                    child.Visible = _visible;
                }
                NotifyChange();
            }
        }

        #region GameObject Children/Parent

        public GameObject Parent
        {
            get { return _parent; }
            set { _parent = value; NotifyChange(); }
        }

        public GameObject Root
        {
            get
            {
                GameObject result = this;
                while (result.Parent != null) result = result.Parent;
                return result;
            }
        }

        public IEnumerable<GameObject> Children
        {
            get { return _children; }
        }

        public void AddChild(GameObject gameObject)
        {
            gameObject.Expanded = true;
            gameObject.Parent = this;
            _children.Add(gameObject);
        }

        public GameObject AddChild(string name)
        {
            GameObject result = null;
            if (name != null && name.Length > 0)
            {
                result = Plugins.Container.ResolveNamed<GameObject>(name);
            }
            AddChild(result);
            return result;
        }

        public T FindChild<T>(string name) where T : GameObject
        {
            foreach (var child in _children)
            {
                if (child is T && child.Name == name) return child as T;
                var result = child.FindChild<T>(name);
                if (result != null) return result;
            }
            return null;
        }

        public List<T> FindChildren<T>() where T : GameObject
        {
            var result = new List<T>();
            foreach (var child in _children)
            {
                if (child is T) result.Add(child as T);
                result.AddRange(child.FindChildren<T>());
            }
            return result;
        }

        public T FindParent<T>() where T : GameObject
        {
            if (this is T)
                return this as T;
            else
            {
                if (Parent != null)
                    return Parent.FindParent<T>();
                else
                    return null;
            }
        }

        public void RemoveChild<T>(string name) where T : GameObject
        {
            RemoveChild(FindChild<T>(name));
        }

        public void RemoveChild(GameObject child)
        {
            if (child != null) _children.Remove(child);
        }

        public int NumChildrens
        {
            get { return _children.Count; }
        }

        #endregion

        #region Components

        public int NumComponents
        {
            get { return _components.Count; }
        }

        public IEnumerable<Component> Components
        {
            get { return _components.Values; }
        }

        public T Component<T>() where T : Component
        {
            if (!_components.ContainsKey(typeof(T))) return default(T);
            return (T)_components[typeof(T)];
        }

        public void AddComponent(Component component)
        {
            component.Owner = this;
            _components.Add(component.GetType(), component);
        }

        public void AddComponent(string componentName)
        {
            AddComponent(Plugins.Container.ResolveNamed<Component>(componentName));
        }

        public void RemoveComponent<T>() where T : Component
        {
            _components.Remove(typeof(T));
        }

        public void RemoveComponent(string componentName)
        {
            foreach (var k in _components.Keys)
            {
                if (_components[k].ExportName == componentName)
                {
                    _components.Remove(k);
                    break;
                }
            }
        }

        public bool HasComponent(string componentName)
        {
            foreach (var c in _components.Values)
            {
                if (c.ExportName == componentName) return true;
            }
            return false;
        }

        #endregion

        #region XML Serialization

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
            Name = reader.GetAttribute("Name");
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Name", Name);
        }

        #endregion

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

        #region Save & Load

        public void Save(string defaultFilename)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = defaultFilename;
            dialog.DefaultExt = ".ted";
            dialog.Filter = "TokEd Files (.ted)|*.ted";
            dialog.Title = "Save TokEd Project as...";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var settings = new XmlWriterSettings();
                settings.Indent = true;

                using (var writer = XmlWriter.Create(dialog.FileName, settings))
                {
                    writer.WriteStartDocument();
                    WriteGameObject(this, writer);
                    writer.WriteEndDocument();
                }
            }
        }

        private void WriteGameObject(GameObject obj, XmlWriter writer)
        {
            if (!string.IsNullOrWhiteSpace(obj.ExportName))
            {
                writer.WriteStartElement(obj.ExportName);

                //Write GameObject
                obj.WriteXml(writer);

                //Write Children
                if (obj.NumChildrens > 0)
                {
                    writer.WriteStartElement("Children");
                    foreach (var child in obj.Children)
                    {
                        WriteGameObject(child, writer);
                    }
                    writer.WriteEndElement();
                }

                //Write Components
                if (obj.NumComponents > 0)
                {
                    writer.WriteStartElement("Components");
                    foreach (var component in obj.Components)
                    {
                        WriteComponent(component, writer);
                    }
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }

        private void WriteComponent(Component component, XmlWriter writer)
        {
            if (!string.IsNullOrWhiteSpace(component.ExportName))
            {
                writer.WriteStartElement(component.ExportName);
                component.WriteXml(writer);
                writer.WriteEndElement();
            }
        }

        public void Load(ref string defaultFilename)
        {
            var dialog = new OpenFileDialog();
            if (defaultFilename.Length > 0)
            {
                dialog.FileName = defaultFilename;
            }
            dialog.DefaultExt = ".ted";
            dialog.Filter = "TokEd Files (.ted)|*.ted";
            dialog.Title = "Load TokEd Project...";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                defaultFilename = dialog.FileName;
                var settings = new XmlReaderSettings();

                using (var reader = XmlReader.Create(dialog.FileName, settings))
                {
                    //reader.MoveToContent();
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                switch (reader.Name)
                                {
                                    case "Project":
                                        this.Clear();
                                        ReadGameObject(null, reader);
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
        }

        private void ReadGameObject(GameObject parent, XmlReader reader)
        {
            var isEmpty = reader.IsEmptyElement;
            GameObject obj;
            if (parent != null)
            {
                obj = parent.AddChild(reader.Name);
            }
            else
            {
                obj = this;
            }
            obj.ReadXml(reader);

            if (!isEmpty)
            {
                while (reader.Read() && (reader.NodeType != XmlNodeType.EndElement))
                {
                    if (reader.NodeType == XmlNodeType.Element && (!reader.IsEmptyElement))
                    {
                        switch (reader.Name)
                        {
                            case "Children":
                                while (reader.Read() && (reader.NodeType != XmlNodeType.EndElement))
                                {
                                    if (reader.NodeType == XmlNodeType.Element)
                                    {
                                        ReadGameObject(obj, reader);
                                    }
                                }
                                break;

                            case "Components":
                                while (reader.Read() && (reader.NodeType != XmlNodeType.EndElement))
                                {
                                    if (reader.NodeType == XmlNodeType.Element)
                                    {
                                        var component = Plugins.Container.ResolveNamed<Component>(reader.Name);
                                        if (!reader.IsEmptyElement || reader.HasAttributes)
                                        {
                                            component.ReadXml(reader);
                                        }
                                        obj.AddComponent(component);
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

        #endregion

        #region Loading, UnLoading, Clearing & Disposing

        private bool _loaded = false;
        public void Load()
        {
            if (!_loaded)
            {
                OnLoad();
                _loaded = true;
            }
            foreach (var comp in _components.Values)
            {
                comp.Load();
            }
            foreach (var child in _children)
            {
                child.Load();
            }
        }

        public void UnLoad()
        {
            if (_loaded)
            {
                OnUnLoad();
                _loaded = false;
            }
            foreach (var comp in _components.Values)
            {
                comp.UnLoad();
            }
            foreach (var child in _children)
            {
                child.UnLoad();
            }
        }

        public void Clear()
        {
            OnUnLoad();
            _components.Clear();
            foreach (var obj in _children)
            {
                obj.Clear();
            }
            _children.Clear();
        }

        protected virtual void OnLoad()
        {
        }

        protected virtual void OnUnLoad()
        {
        }

        public void Dispose()
        {
            Clear();
        }

        #endregion



    }
}

