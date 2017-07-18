using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Model
{
    public class Condition
    {
        public string text { get; set; }
        public string icon { get; set; }
        public int code { get; set; }
        public ImageSource image
        {
            get
            {
                return new BitmapImage(new Uri("http:" + icon, UriKind.Absolute));
            }
            set { }
        }
    }
}
