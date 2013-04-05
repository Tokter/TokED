using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokGL
{
    public class BitmapPacker
    {
        public BitmapPacker A = null;
        public BitmapPacker B = null;
        public Rectangle Rect;
        public Bitmap image = null;

        public void Render(Bitmap target)
        {
            if (image != null)
            {
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(image, Rect);
                }
            }
            if (A != null) A.Render(target);
            if (B != null) B.Render(target);
        }

        public BitmapPacker Insert(Bitmap texture)
        {
            //image = new Bitmap(filename);

            if (A != null && B != null)
            {
                BitmapPacker newNode = A.Insert(texture);
                if (newNode != null) return newNode;
                return B.Insert(texture);
            }
            else
            {
                if (image != null) return null;

                //if we're too small, return
                if (Rect.Width < texture.Width || Rect.Height < texture.Height) return null;

                //if we're just right, accept
                if (Rect.Width == texture.Width && Rect.Height == texture.Height)
                {
                    image = texture;
                    return this;
                }

                //otherwise, gotta split this node and create some kids
                A = new BitmapPacker();
                B = new BitmapPacker();

                //decide which way to split
                int dw = Rect.Width - texture.Width;
                int dh = Rect.Height - texture.Height;

                if (dw > dh)
                {
                    A.Rect = new System.Drawing.Rectangle(Rect.Left, Rect.Top, texture.Width, Rect.Bottom - Rect.Top);
                    B.Rect = new System.Drawing.Rectangle(Rect.Left + texture.Width, Rect.Top, Rect.Right - (Rect.Left + texture.Width), Rect.Bottom - Rect.Top);
                }
                else
                {
                    A.Rect = new System.Drawing.Rectangle(Rect.Left, Rect.Top, Rect.Right - Rect.Left, texture.Height);
                    B.Rect = new System.Drawing.Rectangle(Rect.Left, Rect.Top + texture.Height, Rect.Right - Rect.Left, Rect.Bottom - (Rect.Top + texture.Height));
                }

                //insert into first child we created
                return A.Insert(texture);
            }
        }
    }
}
