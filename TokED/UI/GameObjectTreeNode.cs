using Squid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokED.UI
{
    public class GameObjectTreeNode : TreeNode, IDisposable
    {
        private Button _label;
        private GameObject _gameObject;

        public GameObject GameObject
        {
            get { return _gameObject; }
        }

        public GameObjectTreeNode(GameObject gameObject)
        {
            _gameObject = gameObject;
            _gameObject.PropertyChanged += GameObject_PropertyChanged;

            this.Expanded = gameObject.Expanded;

            Margin = new Margin(1);
            Size = new Point(100, 18);

            var buttonFrame = this.AddFrame(9, 9, DockStyle.Left);
            Elements.Add(buttonFrame);

            this.Bind("Expanded", _gameObject, "Expanded");
            buttonFrame.AddCheckBox("treeButton", 9, 9)
                .Bind(_gameObject, "Expanded")
                .Bind("Opacity", _gameObject, "NumChildrens", (num) => { return (int)num > 0 ? 1.0f : 0.0f; });

            var imageFrame = this.AddFrame(18, 18, DockStyle.Left);
            imageFrame.AddImage(gameObject.TextureName, 14, 14, DockStyle.Center);
            Elements.Add(imageFrame);

            _label = this.AddButton("item", 1, 1, DockStyle.Fill);
            _label.AllowDrop = true;
            _label.Text = gameObject.Name;
            _label.MouseClick += label_MouseClick;
            _label.Update += label_Update;
            _label.DragDrop += label_DragDrop;
            _label.MouseDrag += label_MouseDrag;
            Elements.Add(_label);

            var eyeButtonFrame = this.AddFrame(13, 14, DockStyle.Right);
            eyeButtonFrame.AddCheckBox("eyeButton", 13, 9).Bind(_gameObject, "Visible");
            Elements.Add(eyeButtonFrame);
        }

        void GameObject_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _label.Text = GameObject.Name;
        }

        void label_MouseClick(Control sender, MouseEventArgs args)
        {
            ((sender as Button).Parent as GameObjectTreeNode).Selected = true;
        }

        void label_Update(Control sender)
        {
            (sender as Button).Selected = ((sender as Button).Parent as GameObjectTreeNode).Selected;
        }

        void label_MouseDrag(Control sender, MouseEventArgs args)
        {
            var label = new Label();
            label.Text = (sender as Button).Text;
            label.Position = GuiHost.MousePosition;
            label.Tag = sender.Parent;
            label.Style = "item";
            DoDragDrop(label);
        }

        void label_DragDrop(Control sender, DragDropEventArgs e)
        {
            GameObjectTreeNode target = sender.Parent as GameObjectTreeNode;
            GameObjectTreeNode source = e.Source as GameObjectTreeNode;
            if (target.Parent != null && (target != source))
            {
                var targetIndex = target.Parent.Nodes.IndexOf(target);
                source.Parent.Nodes.Remove(source);
                target.Parent.Nodes.Insert(targetIndex, source);
            }
        }

        public void Dispose()
        {
            this.Tag = null;
            _label.MouseClick -= label_MouseClick;
            _label.Update -= label_Update;
            _label.DragDrop -= label_DragDrop;
            _label.MouseDrag -= label_MouseDrag;
            _gameObject.PropertyChanged -= GameObject_PropertyChanged;
            this.UnBind();
        }
    }
}
