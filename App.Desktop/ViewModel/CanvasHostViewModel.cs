using System;
using System.Windows;
using System.Drawing;
using System.Security.AccessControl;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Walle.Model;
using Point = System.Drawing.Point;

namespace Walle.ViewModel
{
    public class CanvasHostViewModel : ViewModelBase
    {
        private ImageSource _imageSource;
        private Bitmap _image;

        public CanvasHostViewModel(Uri uri)
        {
            this.ImageSource = new BitmapImage(uri);
            _image = new Bitmap(uri.LocalPath);
        }

        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                if (Equals(value, _imageSource)) return;
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        public event OutlineDiscoveredHandler OutlineDiscovered;

        public void Act(System.Windows.Point startClick, System.Windows.Point endClick)
        {
            var pt = new System.Drawing.Point((int) startClick.X, (int) startClick.Y);
            var finder = new RegionFinder(_image, pt, 20);
            var outline = finder.Process();
            OnOutlineDiscovered(outline);
        }

        protected virtual void OnOutlineDiscovered(Point[] points)
        {
            var handler = OutlineDiscovered;
            if (handler != null) handler(this, points);
        }
    }

    public delegate void OutlineDiscoveredHandler(object sender, System.Drawing.Point[] points);
}
