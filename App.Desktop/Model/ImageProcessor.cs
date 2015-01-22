using System.Drawing;
using System.Windows.Media;

namespace Walle.Model
{
    public class ImageProcessor
    {
        private Image _image;

        public ImageProcessor(Image image)
        {
            _image = image;
        }

        public Image Image { get { return _image; } }

        public void SelectRegion(Point startPoint, uint tolerance)
        {
            
        }
    }
}
