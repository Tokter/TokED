using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TokED.UI
{
    public class ImageComboBoxItem
    {
        public string Name { get; set; }
        public string IconName { get; set; }
    }

    public class ImageComboBox : ComboBox
    {
        public ImageList ImageList { get; set; }

        public ImageComboBox()
        {
            DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            ItemHeight = 16;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index < 0) return;

            // Get the item.
            var item = this.Items[e.Index] as ImageComboBoxItem;
            if(item == null) return;

            // Draw image
            if (item.IconName != null)
            {
                e.Graphics.DrawImage(ImageList.Images[item.IconName], new Point(e.Bounds.X + 5, e.Bounds.Y + 1));
            }

            // Draw text
            e.Graphics.DrawString(item.Name, this.Font, new SolidBrush(Color.Black), new PointF(e.Bounds.X + 22, e.Bounds.Y + 3));

            e.DrawFocusRectangle();
        }
    }
}
