using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Type: Squid.SplitContainer
// Assembly: Squid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=5ba804b1072f6e05
// Assembly location: E:\Projects\Squid\lib\Squid.dll

namespace Squid
{
    /// <summary>
    /// A SplitContainer. Can be used horizontally and vertically.
    ///             This is a Frame|Button|Frame combination.
    ///             The Button resizes Frame1.
    /// 
    /// </summary>
    [Toolbox]
    public class TestSplitContainer : Control
    {
        private Orientation _orientation;
        private Point ClickedPos;
        private Point OldSize;

        /// <summary>
        /// Gets the split frame1.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The split frame1.
        /// </value>
        public Frame SplitFrame1 { get; private set; }

        /// <summary>
        /// Gets the split frame2.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The split frame2.
        /// </value>
        public Frame SplitFrame2 { get; private set; }

        /// <summary>
        /// Gets the split button.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The split button.
        /// </value>
        public Button SplitButton { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether [retain aspect].
        /// 
        /// </summary>
        /// 
        /// <value>
        /// <c>true</c> if [retain aspect]; otherwise, <c>false</c>.
        /// </value>
        public bool RetainAspect { get; set; }

        /// <summary>
        /// Gets or sets the aspect ratio.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The aspect ratio.
        /// </value>
        public float AspectRatio { get; set; }

        /// <summary>
        /// Gets or sets the orientation.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The orientation.
        /// </value>
        public Orientation Orientation
        {
            get
            {
                return this._orientation;
            }
            set
            {
                if (value == this._orientation)
                    return;
                this._orientation = value;
                this.ChangeOrientation();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Squid.SplitContainer"/> class.
        /// 
        /// </summary>
        public TestSplitContainer()
        {
            this.Orientation = Orientation.Horizontal;
            this.Size = new Point(100, 100);
            this.SplitFrame1 = new Frame();
            this.SplitFrame1.Size = new Point();
            this.SplitFrame1.Dock = DockStyle.Left;
            this.SplitFrame1.Size = new Point(40, 40);
            this.Elements.Add((Control)this.SplitFrame1);
            this.SplitButton = new Button();
            this.SplitButton.Dock = DockStyle.Left;
            this.SplitButton.Size = new Point(10, 10);
            this.SplitButton.MousePress += new MouseEvent(this.SplitButton_MousePress);
            this.SplitButton.MouseDown += new MouseEvent(this.SplitButton_MouseDown);
            this.SplitButton.Cursor = Cursors.VSplit;
            this.Elements.Add((Control)this.SplitButton);
            this.SplitFrame2 = new Frame();
            this.SplitFrame2.Size = new Point();
            this.SplitFrame2.Dock = DockStyle.Fill;
            this.SplitFrame2.Size = new Point(50, 50);
            this.Elements.Add((Control)this.SplitFrame2);
            this.RetainAspect = true;
            this.AspectRatio = (float)this.SplitFrame1.Size.x / (float)this.Size.x;
        }

        private void ChangeOrientation()
        {
            if (this.Orientation == Orientation.Horizontal)
            {
                this.SplitFrame1.Dock = DockStyle.Left;
                this.SplitButton.Size = new Point(this.SplitButton.Size.y, 10);
                this.SplitButton.Dock = DockStyle.Left;
                this.SplitButton.Cursor = Cursors.VSplit;
            }
            else
            {
                this.SplitFrame1.Dock = DockStyle.Top;
                this.SplitButton.Size = new Point(10, this.SplitButton.Size.x);
                this.SplitButton.Dock = DockStyle.Top;
                this.SplitButton.Cursor = Cursors.HSplit;
            }
        }

        private void SplitButton_MouseDown(Control sender, MouseEventArgs args)
        {
            if (args.Button > 0)
                return;
            this.ClickedPos = GuiHost.MousePosition;
            this.OldSize = this.SplitFrame1.Size;
        }

        private void SplitButton_MousePress(Control sender, MouseEventArgs args)
        {
            if (args.Button > 0) return;
            if (this.Orientation == Orientation.Horizontal)
            {
                this.SplitFrame1.ResizeTo(this.OldSize + GuiHost.MousePosition - this.ClickedPos, AnchorStyles.Right);
                this.AspectRatio = (float)this.SplitFrame1.Size.x / (float)this.Size.x;
            }
            else
            {
                this.SplitFrame1.ResizeTo(this.OldSize + GuiHost.MousePosition - this.ClickedPos, AnchorStyles.Bottom);
                this.AspectRatio = (float)this.SplitFrame1.Size.y / (float)this.Size.y;
            }
        }

        protected override void OnLateUpdate()
        {
            if (!this.RetainAspect || this.Desktop.PressedControl == this.SplitButton)
                return;
            if (this.Orientation == Orientation.Horizontal)
                this.SplitFrame1.Size = new Point((int)((double)this.AspectRatio * (double)this.Size.x), this.SplitFrame1.Size.y);
            else
                this.SplitFrame1.Size = new Point(this.SplitFrame1.Size.x, (int)((double)this.AspectRatio * (double)this.Size.y));
        }
    }
}
