using Squid;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED;
using TokED.UI;

namespace TokED.UI
{
    [Export("GameObject", typeof(GameObjectInsOld)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class GameObjectInsOld : Frame, IDisposable
    {
        private TextBox _textBox;
        private GameObject _gameObject;

        public GameObject GameObject
        {
            get { return _gameObject; }
            set
            {
                _gameObject = value;
                Build();
            }
        }

        protected virtual void Build()
        {
            this.ResetTabIndex();
            this.AutoSize = Squid.AutoSize.Vertical;
            this.Dock = DockStyle.Top;
            _textBox = this.AddLabeledTextBox(100, "Name:");             
            _textBox.Bind(this.GameObject, "Name");
        }

        protected void ReBuild()
        {
            this.UnBind();
            Build();
        }

        public virtual void Dispose()
        {
            this.UnBind();
        }
    }
}
