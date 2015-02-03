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
            Tolerance = 30;
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

        public uint Tolerance
        {
            get { return _tolerance; }
            set
            {
                if (value == _tolerance) return;
                _tolerance = value;
                OnPropertyChanged();
            }
        }

        public int ImageWidth
        {
            get { return _image.Width; }
        }

        public int ImageHeight
        {
            get { return _image.Height; }
        }

        private uint _tolerance;
        private bool _processing;

        public event OutlineDiscoveredHandler OutlineDiscovered;

        public void Act(System.Windows.Point startClick, System.Windows.Point endClick)
        {
            var pt = new System.Drawing.Point((int) startClick.X, (int) startClick.Y);
            var finder = new RegionFinder(_image, pt, Tolerance);
            Processing = true;
            var outline = finder.Process();
            Processing = false;
            OnOutlineDiscovered(outline);
        }

        public bool Processing
        {
            get { return _processing; }
            set
            {
                if (value.Equals(_processing)) return;
                _processing = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnOutlineDiscovered(Point[] points)
        {
            var handler = OutlineDiscovered;
            if (handler != null) handler(this, points);
        }
    }

    public delegate void OutlineDiscoveredHandler(object sender, System.Drawing.Point[] points);
}