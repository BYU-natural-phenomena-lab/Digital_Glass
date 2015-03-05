using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Walle.Commands;
using Walle.Model;

namespace Walle.ViewModel
{
    public class CanvasHostViewModel : ViewModelBase
    {
        private ImageSource _imageSource;
        private Bitmap _image;
        private CanvasHostMode _canvasMode;

        public CanvasHostViewModel(Uri uri)
        {
            Cells = new ObservableCollection<CellBoundaries>();
            Leds = new ObservableCollection<Led>();
            ImageSource = new BitmapImage(uri);
            _image = new Bitmap(uri.LocalPath);
            Tolerance = 30;
            _canvasMode = CanvasHostMode.None;
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

        public void Act(System.Windows.Point startClick, System.Windows.Point endClick)
        {
            var command = CanvasHostCommandFactory.Create(this, _canvasMode);
            if (command == null) return;
            Processing = true;
            command.Execute(startClick,endClick);
            Processing = false;
        }

        private bool _processing;

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

        public Bitmap Image
        {
            get
            {
                return _image;
            }
        }

        public ObservableCollection<CellBoundaries> Cells { get; private set; }
        public ObservableCollection<Led> Leds { get; private set; }

        public CanvasHostMode CanvasMode
        {
            get { return _canvasMode; }
            set
            {
                if (value.Equals(_canvasMode)) return;
                _canvasMode = value;
                OnPropertyChanged();
            }
        }

        public uint BoardWidth
        {
            get
            {
                //TODO scale the board to custom size
                return (uint) this.ImageWidth;
            }
        }

        public uint BoardHeight
        {
            get
            {
                //TODO scale the board to custom size
                return (uint) this.ImageHeight;
            }
        }
    }

    public enum CanvasHostMode
    {
        None,
        FindCell,
        PlaceLED
    }
}