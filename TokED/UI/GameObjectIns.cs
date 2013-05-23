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
    [Export("GameObject", typeof(GameObjectIns)), HasIcon("GameObject.png"), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class GameObjectIns : Inspector
    {
        private GameObject _gameObject;

        public GameObjectIns()
        {
            InitializeComponent();
        }

        public GameObject GameObject
        {
            get { return _gameObject; }
            set { _gameObject = value; Bind(); }
        }

        protected override void Bind()
        {
            bsGameObject.DataSource = _gameObject;
        }
    }
}
