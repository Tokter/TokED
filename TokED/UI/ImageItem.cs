using Squid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokED.UI
{
    public class ImageItem : ListBoxItem
    {
        public ImageItem(string name)
        {
            var image = this.AddImage(name, 2, 1, 14, 15);
            Elements.Add(image);
        }
    }
}
