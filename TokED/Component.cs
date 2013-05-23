using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TokED
{
    public class Component : IXmlSerializable, INotifyPropertyChanged
    {
        public GameObject Owner { get; set; }

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

        public static string IconName(string componentName)
        {
            var parentMeta = Plugins.GetMetadata<Component>(componentName);
            if (parentMeta.ContainsKey("IconName")) return (string)parentMeta["IconName"]; else return null;
        }

        #region XML Serialization

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
        }

        public virtual void WriteXml(XmlWriter writer)
        {
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyChange([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Loading & UnLoading

        private bool _loaded = false;
        public void Load()
        {
            if (!_loaded)
            {
                OnLoad();
                _loaded = true;
            }
        }

        public void UnLoad()
        {
            if (_loaded)
            {
                OnUnLoad();
                _loaded = false;
            }
        }

        protected virtual void OnLoad()
        {
        }

        protected virtual void OnUnLoad()
        {
        }

        #endregion
    }
}
