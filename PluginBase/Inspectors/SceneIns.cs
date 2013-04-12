using Squid;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED.UI;

namespace PluginBase.Inspectors
{
    [Export("Scene", typeof(GameObjectIns)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class SceneIns : GameObjectIns
    {
        private TextBox _transitionIn;
        private TextBox _transitionOut;

        public SceneIns()
        {
            _transitionIn = this.AddLabeledTextBox(100, "Transition In:");
            _transitionOut = this.AddLabeledTextBox(100, "Transition Out:");
        }

        protected override void Build()
        {
            base.Build();
            _transitionIn.Bind(this.GameObject, "TransitionInTime");
            _transitionOut.Bind(this.GameObject, "TransitionOutTime");
        }
    }
}
