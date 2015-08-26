using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DigitalGlass.Annotations;
using DigitalGlass.Model;
using DigitalGlass.ViewModel;
using System.Windows.Media.Imaging;
using System.IO;

namespace DigitalGlass.View
{
    /// <summary>
    /// The UI element that draws a CanvasHostViewModel and responds to users clicks.
    /// </summary>
    public class CanvasHost : FrameworkElement
    {
        private VisualCollection _children;
        private CanvasHostViewModel _viewModel;
        private double _scale;
        private int idx = 0;
        private IList<DrawingVisual> _outlines = new List<DrawingVisual>();
        private IList<DrawingVisual> _leds = new List<DrawingVisual>();

        public CanvasHost()
        {
            _children = new VisualCollection(this);
            StartClick = null;
            this.MouseLeftButtonDown += CanvasHost_MouseLeftButtonDown;
            this.MouseLeftButtonUp += CanvasHost_MouseLeftButtonUp;
            this.DataContextChanged += CanvasHost_DataContextChanged;
        }


        /// <summary>
        /// Calculates the size of the image when rearranging the WPF layout
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            //finalSize.Height = 0;
            return MeasureArrangeHelper(finalSize);           
        }

        /// <summary>
        /// Calculates how much space to take when WPF is arranging layout
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            availableSize.Width = Math.Max(0, availableSize.Width - 150); // Provides room for animation bar (size 150)
            return MeasureArrangeHelper(availableSize);
        }

        /// <summary>
        /// Updates the view to match a new model. Updates event handlers and listeners.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasHost_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var oldDc = e.OldValue as CanvasHostViewModel;

            if (oldDc != null)
            {
                oldDc.PropertyChanged -= ViewModelPropertyChanged;
                oldDc.Cells.CollectionChanged -= DrawOutline;
                oldDc.Leds.CollectionChanged -= DrawLed;
                oldDc.TouchRegions.CollectionChanged -= DrawTouchRegion;
                //oldDc.Frames.CollectionChanged -= DrawFrames;

            }

            _viewModel = e.NewValue as CanvasHostViewModel;

            ClearCanvas();
            if (_viewModel == null)
                return;
            _viewModel.PropertyChanged += ViewModelPropertyChanged;
            _viewModel.Cells.CollectionChanged += DrawOutline;
            _viewModel.Leds.CollectionChanged += DrawLed;
            _viewModel.TouchRegions.CollectionChanged += DrawTouchRegion;
           // _viewModel.Frames.CollectionChanged += DrawFrames;
            UpdateImage();
        }

        private void DrawLed(object sender, NotifyCollectionChangedEventArgs e)
        {
            ModelToVisual<Led>(sender, e, CreateLedVisual, ref _leds);
        }

        private void DrawTouchRegion(object sender, NotifyCollectionChangedEventArgs e)
        {
            ModelToVisual<TouchRegion>(sender, e, CreateTouchRegtionVisual, ref _leds);
        }


        /// <summary>
        /// Create a visual representation of LEDs
        /// </summary>
        /// <param name="led"></param>
        /// <returns></returns>
        private DrawingVisual CreateLedVisual(Led led)
        {
            var color = Brushes.OrangeRed;
            var dv = new DrawingVisual();
            var dc = dv.RenderOpen();
            dc.DrawEllipse(color, null, new Point(led.X, led.Y), 3, 3);
            dc.Close();
            return dv;
        }

        /// <summary>
        /// Create a visual representation of LEDs
        /// </summary>
        /// <param name="touchRegion"></param>
        /// <returns></returns>
        private DrawingVisual CreateTouchRegtionVisual(TouchRegion touchRegion)
        {
            var color1 = Brushes.LightBlue;
            var color2 = Brushes.Black;
            var dv = new DrawingVisual();
            var dc = dv.RenderOpen();
            dc.DrawEllipse(color1, null, new Point(touchRegion.X, touchRegion.Y), 8, 8);
            dc.DrawText(new FormattedText(touchRegion.number + "", new System.Globalization.CultureInfo("en-US") , new FlowDirection(), new Typeface("arial"), 7, color2), new Point(touchRegion.X - 2.5, touchRegion.Y - 4));
            dc.Close();
            return dv;
        }

        private void DrawOutline(object sender, NotifyCollectionChangedEventArgs e)
        {
            ModelToVisual<Cell>(sender, e, CreateLineVisual, ref _outlines);
            CreateSaveBitmap(_outlines, "Frame" + _viewModel.currentFrame);
            updateThumbnails();
        }

        /// <summary>
        /// Updates the thumbnails with images to reflect the colors of each frame
        /// </summary>
        private void updateThumbnails()
        {

        }

        /// <summary>
        /// Creates a visual for cell boundaries.
        /// </summary>
        /// <param name="bnd">The cell boundaries</param>
        /// <returns></returns>
        private DrawingVisual CreateLineVisual(Cell bnd)
        {
            int frame = _viewModel.currentFrame;
            Color newColor = System.Windows.Media.Color.FromArgb(bnd.color[frame].A, bnd.color[frame].R, bnd.color[frame].G, bnd.color[frame].B); //_viewModel.getCellColor(bnd);
            var color = new SolidColorBrush(newColor); //Set to selected color
            var dv = new DrawingVisual();
            var dc = dv.RenderOpen();
            for (var i = 0; i < bnd.Points.Length; i++)
            {

                //Draw the points intsead of a line
                dc.DrawLine(new Pen(color, 1),
                   new Point(bnd.Points[i].X, bnd.Points[i].Y),
                   new Point(bnd.Points[i].X+1, bnd.Points[i].Y+1));

                //Code to draw a line - cange for to start with 1
                // dc.DrawLine(new Pen(color, 1),
                //     new Point(bnd.Points[i].X, bnd.Points[i].Y),
                //     new Point(bnd.Points[i - 1].X, bnd.Points[i - 1].Y));
            }

            dc.Close();
            return dv;
        }

        /// <summary>
        /// A generic method to update the visual children to match an ObservableCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="creator"></param>
        /// <param name="layer"></param>
        private void ModelToVisual<T>(object sender, NotifyCollectionChangedEventArgs e, Func<T, DrawingVisual> creator,
            ref IList<DrawingVisual> layer)
        {
            var collection = sender as ObservableCollection<T>;
            if (collection == null) return;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    var newVisuals = e.NewItems.Cast<T>().Select(creator);
                    foreach (var dv in newVisuals)
                    {
                        layer.Add(dv);
                        _children.Add(dv);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var rm = layer[e.OldStartingIndex];
                    layer.RemoveAt(e.OldStartingIndex);
                    _children.Remove(rm);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    foreach (var dv in _outlines)
                    {
                        _children.Remove(dv);
                    }
                    layer = new List<DrawingVisual>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            this.InvalidateVisual();
        }

        [NotNull]
        private ScaleTransform GetScaleTransform()
        {
            return new ScaleTransform(_scale, _scale, 0, 0);
        }

        /// <summary>
        /// Called by WPF when the window changes.
        /// </summary>
        /// <param name="sizeInfo"></param>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (_viewModel == null)
                return;
            _scale = sizeInfo.NewSize.Height/_viewModel.ImageHeight;
            this.RenderTransform = GetScaleTransform();
        }

        /// <summary>
        /// Deletes all children elements
        /// </summary>
        private void ClearCanvas()
        {
            _children.Clear();
            this.InvalidateVisual();
        }
        /// <summary>
        /// Triggered whenever a property changes. This allows the application to respond to a new application state.
        /// <seealso cref="UiServices"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
            {
                UiServices.SetBusyState(); 
            }
        }

        /// <summary>
        /// Redraws the image.
        /// </summary>
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


        private void CreateSaveBitmap(IList<DrawingVisual> visuals, string filename)
        {
            RenderTargetBitmap MyRenderTargetBitmap = new RenderTargetBitmap(_viewModel.ImageWidth, _viewModel.ImageHeight, 96, 96, PixelFormats.Default);
            
            //Add original image
            var originalImage = new DrawingVisual();
            var dc = originalImage.RenderOpen();
            dc.DrawImage(_viewModel.ImageSource, new Rect(new Size(_viewModel.ImageWidth, _viewModel.ImageHeight)));
            dc.Close();

            MyRenderTargetBitmap.Render(originalImage);

           
            //Add each drwaing element
            foreach (DrawingVisual dv in visuals)
                MyRenderTargetBitmap.Render(dv);


            //Encode
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(MyRenderTargetBitmap));
            

            //Save File
            using (FileStream file = File.Create(Path.Combine(Path.GetTempPath(), "DigitalGlass2_" + filename + ".png")))
            {
                encoder.Save(file);
            }
        }

            /*
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
             (int)canvas.Width, (int)canvas.Height,
             96d, 96d, PixelFormats.Pbgra32);
            // needed otherwise the image output is black
            canvas.Measure(new Size((int)canvas.Width, (int)canvas.Height));
            canvas.Arrange(new Rect(new Size((int)canvas.Width, (int)canvas.Height)));

            renderBitmap.Render(canvas);

            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (FileStream file = File.Create(filename))
            {
                encoder.Save(file);
            }
        }
        */

        private void CanvasHost_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.StartClick = e.GetPosition((UIElement) sender);
        }


        public Point? StartClick { get; set; }

        /// <summary>
        /// Required. WPF must know how many visual children this element has. Used in various WPF functionalities.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return _children.Count; }
        }

        /// <summary>
        /// Required. Allows WPF to access children directly
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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
        private void CanvasHost_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!StartClick.HasValue)
                return;
            if (_viewModel.Processing)
                return;
            // Retreive the coordinates of the mouse button event.
            var endClick = e.GetPosition((UIElement) sender);


            _viewModel.Act(StartClick.Value, endClick);


            //TODO Initiate the hit test by setting up a hit test result callback method.
            //            VisualTreeHelper.HitTest(this, null, result => ResultCallback(result, StartClick.Value, pt),
            //                new PointHitTestParameters(StartClick.Value));
        }

        //TODO implement hit testing.
        // This is how to implement hit testing with with a visual collection
        // private HitTestResultBehavior ResultCallback(HitTestResult result, Point startPoint, Point endPoint)
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

        /// <summary>
        /// Calculates the way to maximize alloated screenspace to show an image and preserve its aspect ratio.
        /// </summary>
        /// <param name="inputSize"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Compute how to scale the image without messing up its aspect ratio
        /// </summary>
        /// <param name="availableSize"></param>
        /// <param name="contentSize"></param>
        /// <returns></returns>
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