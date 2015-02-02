using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using Walle.Annotations;

namespace Walle.View
{
    public partial class CanvasHost : Image
    {
        private VisualCollection _children;
        private string _coordinates;

        public CanvasHost()
        {
            _children = new VisualCollection(this);
            StartClick = null;
            this.MouseLeftButtonDown +=CanvasHost_MouseLeftButtonDown;
            this.MouseLeftButtonUp += CanvasHost_MouseLeftButtonUp;
        }

        private void CanvasHost_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.StartClick = e.GetPosition((UIElement)sender);
;
        }


        public Point? StartClick { get; set; }

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
            if (!StartClick.HasValue)
                return;

            // Retreive the coordinates of the mouse button event.
            var pt = e.GetPosition((UIElement)sender);
          
            // Initiate the hit test by setting up a hit test result callback method.
            VisualTreeHelper.HitTest(this, null, result => ResultCallback(result, StartClick.Value, pt), new PointHitTestParameters(StartClick.Value));
         
        }

        private DrawingVisual CreateEllipse(Point pt, double radius)
        {
            var v = new DrawingVisual();
            var dc = v.RenderOpen();
            dc.DrawEllipse(new SolidColorBrush(Color.FromArgb(60, 255, 0, 0)), new Pen(Brushes.DarkRed, 1), pt, radius, radius);
            dc.Close();
            return v;
        }

        private HitTestResultBehavior ResultCallback(HitTestResult result,Point startPoint, Point endPoint)
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
                var distSqrd = Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));
                _children.Add(CreateEllipse(startPoint,distSqrd));
            }

            // Stop the hit test enumeration of objects in the visual tree. 
            return HitTestResultBehavior.Stop;
        }

    }
}
