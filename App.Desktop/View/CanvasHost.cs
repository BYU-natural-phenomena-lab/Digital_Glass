using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Walle.View
{
    public partial class CanvasHost : Image
    {
        private VisualCollection _children;

        /// <summary>
        /// Only for DI and testing. Don't use.
        /// </summary>
        /// <param name="children"></param>
        internal CanvasHost(VisualCollection children)
        {
            _children = children;
        }

        public CanvasHost()
        {
            _children = new VisualCollection(this);
            this.MouseLeftButtonUp += CanvasHost_MouseLeftButtonUp;
        }

        // Provide a required override for the VisualChildrenCount property. 
        protected override int VisualChildrenCount
        {
            get { return _children.Count; }
        }

        // Provide a required override for the GetVisualChild method. 
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _children[index];
        }

        // Capture the mouse event and hit test the coordinate point value against 
        // the child visual objects. 
        void CanvasHost_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Retreive the coordinates of the mouse button event.
            var pt = e.GetPosition((UIElement)sender);

            // Initiate the hit test by setting up a hit test result callback method.
            VisualTreeHelper.HitTest(this, null,result => ResultCallback(result,pt), new PointHitTestParameters(pt));
         
        }

        private DrawingVisual CreatePane(Point pt)
        {
            var v = new DrawingVisual();
            var dc = v.RenderOpen();
            dc.DrawEllipse(new SolidColorBrush(Color.FromArgb(60, 255, 0, 0)), new Pen(Brushes.DarkRed, 1), pt, 10, 10);
            dc.Close();
            return v;
        }

        private HitTestResultBehavior ResultCallback(HitTestResult result, Point pt)
        {
            if (result.VisualHit.GetType() == typeof (DrawingVisual))
            {
                // do something with visual
                var visual = (DrawingVisual) result.VisualHit;
                if (visual.Effect == null)
                {
                    visual.Effect = new BlurEffect()
                    {
                        Radius = 4
                    };
                }
                else
                {
                    visual.Effect = null;
                }
            }
            else
            {
                _children.Add(CreatePane(pt));
            }

            // Stop the hit test enumeration of objects in the visual tree. 
            return HitTestResultBehavior.Stop;
        }
    }
}
