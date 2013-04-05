using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TokED;

namespace PluginBase.GameObjects
{
    [Export("Scene", typeof(GameObject)), PartCreationPolicy(CreationPolicy.NonShared), DoesNotAllowChild("Scene")]
    public class Scene : GameObject
    {
        private int _transitionInTime = 0;
        private int _transitionOutTime = 0;

        public Scene()
        {
            Name = "Scene";
            TextureName = "Scene";
        }

        public int TransitionInTime
        {
            get { return _transitionInTime; }
            set { _transitionInTime = value; NotifyChange(); }
        }

        public int TransitionOutTime
        {
            get { return _transitionOutTime; }
            set { _transitionOutTime = value; NotifyChange(); }
        }

        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            _transitionInTime = Convert.ToInt32(reader.GetAttribute("TransitionInTime"));
            _transitionOutTime = Convert.ToInt32(reader.GetAttribute("TransitionOutTime"));
        }

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteAttributeString("TransitionInTime", _transitionInTime.ToString());
            writer.WriteAttributeString("TransitionOutTime", _transitionOutTime.ToString());
        }
    }
}
