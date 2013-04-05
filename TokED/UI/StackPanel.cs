using Squid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokED.UI
{
    public class StackPanel : Control
    {
        protected Frame _currentCateg;

        public Panel Panel;
        public Frame Content;

        public StackPanel()
        {
            Panel = new Panel();
            Panel.Dock = DockStyle.Fill;
            Panel.VScroll.ButtonUp.Visible = false;
            Panel.VScroll.ButtonDown.Visible = false;
            Panel.VScroll.Size = new Point(13, 13);
            Panel.VScroll.Slider.Style = "vscrollTrack";
            Panel.VScroll.Slider.Button.Style = "vscrollButton";
            Panel.VScroll.Slider.Margin = new Squid.Margin(0, 2, 0, 2);
            Panel.VScroll.Dock = DockStyle.Right;
            Panel.HScroll.Size = new Point(0, 0);
            Elements.Add(Panel);

            Content = new Frame();
            Content.AutoSize = AutoSize.Vertical;
            Content.Dock = DockStyle.Top;
            Panel.Content.Controls.Add(Content);
        }

        public void AddControl(Control control)
        {
            if (control == null) return;
            control.Dock = DockStyle.Top;

            if (_currentCateg != null)
                _currentCateg.Controls.Add(control);
            else
                Content.Controls.Add(control);
        }

        public CategoryButton AddCategory(string text)
        {
            var f = Content.AddFrame(0, 20, DockStyle.Top);
            var img = f.AddImage(text, 14, 14, DockStyle.Left);
            img.Margin = new Squid.Margin(0, 3, 0, 3);
            CategoryButton categ = new CategoryButton();
            categ.Text = text;
            categ.Style = "category";
            categ.Dock = DockStyle.Top;
            categ.Size = new Point(20, 20);
            f.Controls.Add(categ);

            _currentCateg = new Frame();
            _currentCateg.AutoSize = Squid.AutoSize.Vertical;
            _currentCateg.Dock = DockStyle.Top;

            categ.Tag = _currentCateg;
            categ.MouseClick += delegate(Control sender, MouseEventArgs args)
            {
                CategoryButton head = sender as CategoryButton;
                head.Expanded = !head.Expanded;

                Frame frame = sender.Tag as Frame;

                if (head.Expanded)
                {
                    Point size = frame.Size;
                    frame.AutoSize = Squid.AutoSize.Vertical;
                    frame.PerformLayout();

                    Point nsize = frame.Size;
                    frame.Size = size;

                    frame.Animation.Stop();
                    frame.Animation.Size(new Point(frame.Size.x, nsize.y), 250);
                }
                else
                {
                    frame.AutoSize = Squid.AutoSize.None;
                    frame.Animation.Stop();
                    frame.Animation.Size(new Point(frame.Size.x, 0), 250);
                }
            };

            Content.Controls.Add(_currentCateg);

            return categ;
        }
    }

    public class CategoryButton : Button
    {
        private ImageControl Image;
        private bool _expanded = true;

        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                _expanded = value;
                Image.Texture = _expanded ? "ArrowHead-Down" : "ArrowHead-Right";
            }
        }

        public CategoryButton()
        {
            Image = new ImageControl();
            Image.NoEvents = true;
            Image.Size = new Point(14, 14);
            Image.Dock = DockStyle.Right;
            Image.Margin = new Margin(0,3,0,3);
            Image.Texture = "ArrowHead-Down";
            //Image.Opacity = 0.25f;
            Elements.Add(Image);
        }
    }

}
