using System;

namespace Squid
{
    public class CanCommitEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
    }

    public class TextBoxEx : TextBox
    {
        public event EventHandler<CanCommitEventArgs> CanTextCommit;

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
    }
}
