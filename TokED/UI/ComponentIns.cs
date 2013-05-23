using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TokED.UI
{
    public partial class ComponentIns : Inspector
    {
        private Component _component;

        public ComponentIns()
        {
            InitializeComponent();
        }

        public Component Component
        {
            get { return _component; }
            set { _component = value; Bind(); }
        }

    }
}
