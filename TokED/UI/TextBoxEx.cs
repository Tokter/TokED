using System;

namespace Squid
{
    public class CanCommitEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
    }

    public class TextBoxEx : TextBox
    {
        private Point _startPosition;
        private float _startValue;
        private bool _active = false;
        private float _scale = 1.0f;

        public event EventHandler<CanCommitEventArgs> CanTextCommit;

        public TextBoxEx()
        {
            this.MouseDown += TextBoxEx_MouseDown;
            this.MouseUp += TextBoxEx_MouseUp;
        }

        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        void TextBoxEx_MouseUp(Control sender, MouseEventArgs args)
        {
            if (args.Button == 1) _active = false;
        }

        void TextBoxEx_MouseDown(Control sender, MouseEventArgs args)
        {
            if (args.Button == 1) //Right
            {
                _startPosition = GuiHost.MousePosition;
                try
                {
                    _startValue = Convert.ToSingle(this.Text);
                    _active = true;
                }
                catch (FormatException)
                {
                    _active = false;
                }
            }
            else _active = false;
        }

        protected override void OnKeyDown(KeyEventArgs args)
        {
            if (CanTextCommit != null && (args.Key == Keys.RETURN || args.Key == Keys.NUMPADENTER))
            {
                var canArgs = new CanCommitEventArgs { Cancel = false };
                CanTextCommit(this, canArgs);
                if (canArgs.Cancel) return;
            }
            base.OnKeyDown(args);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            if (_active)
            {
                var delta = GuiHost.MousePosition - _startPosition;
                var distance = (float)Math.Sqrt(delta.x * delta.x + delta.y * delta.y) * Math.Sign(delta.x);
                this.Text = (_startValue + distance * _scale).ToString();
            }
        }
    }
}
