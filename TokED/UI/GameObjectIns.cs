﻿using Squid;
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
    [Export("GameObject", typeof(GameObjectIns)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class GameObjectIns : Frame, IDisposable
    {
        private TextBox _textBox;
        private GameObject _gameObject;
        private List<Binding> _bindings = new List<Binding>();

        public GameObject GameObject
        {
            get { return _gameObject; }
            set
            {
                _gameObject = value;
                Bind();
            }
        }

        public GameObjectIns()
        {
            this.ResetTabIndex();
            this.AutoSize = Squid.AutoSize.Vertical;
            this.Dock = DockStyle.Top;
            _textBox = this.AddLabeledTextBox(100, "Name:");
        }

        protected void AddBinding(Binding binding)
        {
            _bindings.Add(binding);
        }

        protected virtual void Bind()
        {
            _textBox.Bind(this.GameObject, "Name");
        }

        public virtual void Dispose()
        {
            this.UnBind();
        }
    }
}
