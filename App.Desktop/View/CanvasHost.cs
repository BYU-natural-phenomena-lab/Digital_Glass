using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using Walle.Annotations;
using Walle.ViewModel;

namespace Walle.View
{
    public partial class CanvasHost : FrameworkElement
    {
        private VisualCollection _children;
        private CanvasHostViewModel _viewModel;
        private double _scale;

        public CanvasHost()
        {
            _children = new VisualCollection(this);
            StartClick = null;
            this.MouseLeftButtonDown += CanvasHost_MouseLeftButtonDown;
            this.MouseLeftButtonUp += CanvasHost_MouseLeftButtonUp;
            this.DataContextChanged += CanvasHost_DataContextChanged;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return MeasureArrangeHelper(finalSize);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return MeasureArrangeHelper(availableSize);
        }

        private void CanvasHost_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var oldDc = e.OldValue as CanvasHostViewModel;

            if (oldDc != null)
            {
                oldDc.PropertyChanged -= ViewModelPropertyChanged;
                oldDc.OutlineDiscovered -= DrawOutline;
            }

            _viewModel = e.NewValue as CanvasHostViewModel;

            ClearCanvas();
            if (_viewModel == null)
                return;
            _viewModel.PropertyChanged += ViewModelPropertyChanged;
            _viewModel.OutlineDiscovered += DrawOutline;
            UpdateImage();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (_viewModel == null)
                return;
            _scale = sizeInfo.NewSize.Height/_viewModel.ImageHeight;
            this.RenderTransform = GetScaleTransform();
        }

        private void DrawOutline(object sender, System.Drawing.Point[] points)
        {
            var dv = new DrawingVisual();
            var dc = dv.RenderOpen();
            foreach (var pt in points)
            {
                dc.DrawRectangle(Brushes.Red, null, new Rect(new Point(pt.X, pt.Y), new Vector(1, 1)));
            }
            dc.Close();
            _children.Add(dv);
            this.InvalidateVisual();
        }


        [NotNull]
        private ScaleTransform GetScaleTransform()
        {
            return new ScaleTransform(_scale, _scale, 0, 0);
        }


        private void ClearCanvas()
        {
            _children.Clear();
            this.InvalidateVisual();
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
            {
                UiServices.SetBusyState();
            }
        }

        private void UpdateImage()
        {
            if (_viewModel == null || _viewModel.ImageSource == null)
                return;
            var bg = new DrawingVisual();
            var dc = bg.RenderOpen();
            dc.DrawImage(_viewModel.ImageSource, new Rect(new Size(_viewModel.ImageWidth, _viewModel.ImageHeight)));
            dc.Close();
            _children.Add(bg);
            this.InvalidateVisual();
        }

        private void CanvasHost_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.StartClick = e.GetPosition((UIElement) sender);
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
        private void CanvasHost_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!StartClick.HasValue)
                return;
            if (_viewModel.Processing)
                return;
            // Retreive the coordinates of the mouse button event.
            var endClick = e.GetPosition((UIElement) sender);


            _viewModel.Act(StartClick.Value, endClick);


            // Initiate the hit test by setting up a hit test result callback method.
            //            VisualTreeHelper.HitTest(this, null, result => ResultCallback(result, StartClick.Value, pt),
            //                new PointHitTestParameters(StartClick.Value));
        }

        //        private HitTestResultBehavior ResultCallback(HitTestResult result, Point startPoint, Point endPoint)
        //        {
        //            if (result.VisualHit.GetType() == typeof (DrawingVisual))
        //            {
        //                // do something with visual
        //                var visual = (DrawingVisual) result.VisualHit;
        //                if (visual.Effect == null)
        //                {
        //                    visual.Effect = new BlurEffect()
        //                    {
        //                        Radius = 4
        //                    };
        //                }
        //                else
        //                {
        //                    visual.Effect = null;
        //                }
        //            }
        //            else
        //            {
        //                var distSqrd = Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));
        //                _children.Add(CreateEllipse(startPoint, distSqrd));
        //            }
        //
        //            // Stop the hit test enumeration of objects in the visual tree. 
        //            return HitTestResultBehavior.Stop;
        //        }
        private Size MeasureArrangeHelper(Size inputSize)
        {
            if (_viewModel == null || _viewModel.ImageSource == null)
                return new Size();
            var naturalSize = new Size(_viewModel.ImageWidth, _viewModel.ImageHeight);


            //get computed scale factor
            Size scaleFactor = ComputeScaleFactor(inputSize, naturalSize);

            // Returns our minimum size & sets DesiredSize.
            return new Size(naturalSize.Width*scaleFactor.Width, naturalSize.Height*scaleFactor.Height);
        }

        private static Size ComputeScaleFactor(Size availableSize,
            Size contentSize)
        {
            // Compute scaling factors to use for axes
            double scaleX = 1.0;
            double scaleY = 1.0;

            bool isConstrainedWidth = !Double.IsPositiveInfinity(availableSize.Width);
            bool isConstrainedHeight = !Double.IsPositiveInfinity(availableSize.Height);


            // Compute scaling factors for both axes
            scaleX = (contentSize.Width == 0) ? 0.0 : availableSize.Width/contentSize.Width;
            scaleY = contentSize.Height == 0 ? 0.0 : availableSize.Height/contentSize.Height;

            if (!isConstrainedWidth) scaleX = scaleY;
            else if (!isConstrainedHeight) scaleY = scaleX;
            else
            {
                double minscale = scaleX < scaleY ? scaleX : scaleY;
                scaleX = scaleY = minscale;
            }

            return new Size(scaleX, scaleY);
        }
    }
}