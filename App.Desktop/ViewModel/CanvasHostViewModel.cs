using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Walle.ViewModel
{
    public class CanvasHostViewModel : ViewModelBase
    {
        private ImageSource _source;

        public CanvasHostViewModel(ImageSource imageSource)
        {
            this.Source = imageSource;
        }

        public ImageSource Source
        {
            get { return _source; }
            set
            {
                if (Equals(value, _source)) return;
                _source = value;
                OnPropertyChanged();
            }
        }
    }
}
