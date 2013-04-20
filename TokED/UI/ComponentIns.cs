using Squid;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokED.UI
{
    [Export("GameObject", typeof(ComponentIns)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ComponentIns : Frame, IDisposable
    {
        private Component _component;

        public Component Component
        {
            get { return _component; }
            set
            {
                _component = value;
                Build();
            }
        }

        public ComponentIns()
        {
            this.ResetTabIndex();
            this.AutoSize = Squid.AutoSize.Vertical;
            this.Dock = DockStyle.Top;
        }

        protected virtual void Build()
        {
        }

        public virtual void Dispose()
        {
            this.UnBind();
        }
    }
}
