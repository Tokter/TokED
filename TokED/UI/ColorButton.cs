using AvengersUtd.ColorChooserTest;
using Squid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokED.UI
{
    public class ColorButton : Control
    {
        private ImageControl _image;
        private Button _button;
        private Color _color = Color.White;

        public ColorButton()
        {
            _image = new ImageControl();
            _image.Dock = DockStyle.Fill;
            _image.NoEvents = true;
            _image.Texture = "white";
            Elements.Add(_image);

            _button = new Button();
            _button.Dock = DockStyle.Fill;
            _button.Style = "border";
            Elements.Add(_button);

            _button.MouseClick += Button_MouseClick;
        }

        public event EventHandler ColorChanged;

        private void Button_MouseClick(Control sender, MouseEventArgs args)
        {
            var colorChooser = new ColorChooser();
            colorChooser.Color = Color;
            if (colorChooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Color = colorChooser.Color;
                if (ColorChanged != null) ColorChanged(this, new EventArgs());
            }
        }

        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                _image.Color = _color.ToArgb();
            }
        }
    }
}
