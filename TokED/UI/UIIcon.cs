using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TokED.UI
{
    public class UIIcon
    {
        public static ImageList ImageList;

        public static void RegisterIcon(string resourceName)
        {
            var image = Image.FromStream(Plugins.LoadResourceStream(resourceName));
            ImageList.Images.Add(resourceName, image);
        }
    }
}
