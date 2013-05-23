using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace TokED.UI
{
    public partial class Inspector : UserControl
    {
        public Inspector()
        {
            InitializeComponent();
        }

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

        public static string IconName(string objectName)
        {
            if (Plugins.Has<GameObjectIns>(objectName))
            {
                var parentMeta = Plugins.GetMetadata<GameObjectIns>(objectName);
                if (parentMeta.ContainsKey("IconName"))
                {
                    return (string)parentMeta["IconName"];
                }
                else return null;
            }
            if (Plugins.Has<ComponentIns>(objectName))
            {
                var parentMeta = Plugins.GetMetadata<ComponentIns>(objectName);
                if (parentMeta.ContainsKey("IconName"))
                {
                    return (string)parentMeta["IconName"];
                }
                else return null;
            }
            return null;
        }

        protected virtual void Bind()
        {
        }
    }
}
