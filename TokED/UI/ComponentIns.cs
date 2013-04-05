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
        private List<Binding> _bindings = new List<Binding>();

        public Component Component
        {
            get { return _component; }
            set
            {
                _component = value;
                Bind();
            }
        }

        public ComponentIns()
        {
            this.ResetTabIndex();
            this.AutoSize = Squid.AutoSize.Vertical;
            this.Dock = DockStyle.Top;
        }

        protected void AddBinding(Binding binding)
        {
            _bindings.Add(binding);
        }

        protected virtual void Bind()
        {
        }

        public virtual void Dispose()
        {
            this.UnBind();
        }
    }
}
