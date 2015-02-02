using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Walle.Model
{
    public class RegionFinder
    {
        private Queue<Point> _queue;
        private Bitmap _image;
        private readonly double _toleranceSquared;
        private Color _baseColor;
        private HashSet<Point> _considered;

        public RegionFinder(Bitmap image, Point startPoint, uint tolerance)
        {
            _image = image;
            _queue = new Queue<Point>();
            _considered = new HashSet<Point>(new PointComparer());
            _baseColor = image.GetPixel(startPoint.X, startPoint.Y);
            _queue.Enqueue(startPoint);
            _considered.Add(startPoint);
            _toleranceSquared = Math.Pow(tolerance, 2);
        }
        public Point[] Process()
        {
            var outside = new List<Point>();
            
            while (_queue.Count > 0)
            {
                var start = _queue.Dequeue();

                var tl = new Point(start.X - 1, start.Y - 1);
                if (IsEdge(tl)) 
                    outside.Add(tl); 

                var left = new Point(start.X - 1, start.Y);
                if (IsEdge(left)) 
                    outside.Add(left); 

                var bl = new Point(start.X - 1, start.Y + 1);
                if (IsEdge(bl)) 
                    outside.Add(bl);

                var btm = new Point(start.X, start.Y + 1);
                if (IsEdge(btm)) 
                    outside.Add(btm); 

                var br = new Point(start.X + 1, start.Y + 1);
                if (IsEdge(br)) 
                    outside.Add(br); 

                var right = new Point(start.X + 1, start.Y);
                if (IsEdge(right)) 
                    outside.Add(right); 

                var tr = new Point(start.X + 1, start.Y - 1);
                if (IsEdge(tr)) 
                    outside.Add(tr);

                var top = new Point(start.X, start.Y - 1);
                if (IsEdge(top)) 
                    outside.Add(top);

            }
            return outside.ToArray();
        }

        public class PointComparer : IEqualityComparer<Point>
        {
            public bool Equals(Point x, Point y)
            {
                return x.Equals(y);
            }

            public int GetHashCode(Point obj)
            {
                return obj.X.GetHashCode()*31+obj.Y.GetHashCode();
            }
        }

        private bool IsEdge(Point point)
        {
            if (_considered.Contains(point))
                return false;
            _considered.Add(point);
            if (point.X < 0 || point.X >= _image.Width) return true;
            if (point.Y < 0 || point.Y >= _image.Height) return true;
            var color = _image.GetPixel(point.X, point.Y);
            var distSqr = Math.Pow(_baseColor.R - color.R, 2) + Math.Pow(_baseColor.G - color.G, 2) + Math.Pow(_baseColor.B - color.B, 2);
            if (distSqr > _toleranceSquared)
                return true;
            _queue.Enqueue(point);
            return false;
        }
    }
}
