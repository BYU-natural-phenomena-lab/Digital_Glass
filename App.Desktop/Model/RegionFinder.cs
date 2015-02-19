using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using C5;
using Point = System.Drawing.Point;

namespace Walle.Model
{
    public class RegionFinder
    {
        private Queue<Point> _queue = new Queue<Point>();
        private Bitmap _image;
        private readonly double _toleranceSquared;
        private Color _baseColor;
        private PixelTracker _considered;
        public event LineFound OnLineFound;
        public delegate void LineFound(Point[] line);

        private class PixelTracker
        {
            private bool[,] _bools;
            private int _height;
            private int _width;

            public PixelTracker(int width, int height)
            {
                _bools = new bool[width, height];
                _width = width;
                _height = height;
                OutsideDefault = true;
            }

            public bool OutsideDefault { get; set; }

            public void Add(int x, int y)
            {
                _bools[x, y] = true;
            }
            public void Remove(int x, int y)
            {
                _bools[x, y] = false;
            }

            public bool Contains(int x, int y)
            {
                if (x < 0 || y < 0 || x >= _width || y >= _height)
                    return OutsideDefault;
                return _bools[x, y];
            }
        }

        private List<Point> outside;

        public RegionFinder(Bitmap image, Point startPoint, uint tolerance)
        {
            try
            {
                _baseColor = image.GetPixel(startPoint.X, startPoint.Y);
                _image = image;
                _queue.Enqueue(startPoint);
                _considered = new PixelTracker(_image.Width, _image.Height) { OutsideDefault = true };
                _considered.Add(startPoint.X, startPoint.Y);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            _toleranceSquared = Math.Pow(tolerance, 2);
        }

        private System.Collections.Generic.IList<Point> FindBorder()
        {
            outside = new List<Point>();

            while (_queue.Count > 0)
            {
                var start = _queue.Dequeue();
                Explore(start.X - 1, start.Y - 1);
                Explore(start.X - 1, start.Y);
                Explore(start.X - 1, start.Y + 1);
                Explore(start.X, start.Y + 1);
                Explore(start.X + 1, start.Y + 1);
                Explore(start.X + 1, start.Y);
                Explore(start.X + 1, start.Y - 1);
                Explore(start.X, start.Y - 1);
            }
            return outside;
        }

        public void Process()
        {
            var outside = FindBorder();
            var ordered = FindPath(outside);
            OnLineFound(ordered);
        }

        private static double DistanceSqr(Point a, Point b)
        {
            return Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2);
        }

        private Point[] FindPath(System.Collections.Generic.IList<Point> points)
        {
            _considered = new PixelTracker(_image.Width, _image.Height) { OutsideDefault = false };
            if (points.Count < 3)
                return points.ToArray();
            foreach (var p in points)
                _considered.Add(p.X, p.Y);
            var ordered = new List<Point>();
            var start = points[0];
            Point? current = points[0];
            double direction = 0;
            while (current.HasValue)
            {
                ordered.Add(current.Value);
                if (current.Value != start)
                    _considered.Remove(current.Value.X, current.Value.Y);
                var last = current;
                current = FindNearest(current.Value, direction);
                if (current.HasValue)
                {
                    if (current.Value == start)
                    {
                        break;
                    }
                    direction = Direction(last.Value, current.Value);
                }
            }
            if (ordered.Count < points.Count)
            {
                var leftOut = points.Where(pt => _considered.Contains(pt.X, pt.Y));
                Debug.WriteLine("Left overs {0}", leftOut.Count());
            }

            return ordered.ToArray();
        }

        private double Direction(Point last, Point next)
        {
            var unit = new Vector(1, 0);
            return Vector.AngleBetween(unit, new Vector(last.X - next.X, last.Y - next.Y));
        }

        private Point? FindNearest(Point point, double direction)
        {
            _queue = new Queue<Point>();
            _queue.Enqueue(point);
            var searcher = new PixelTracker(_image.Width, _image.Height) { OutsideDefault = true };
            int startOctet = (int)Math.Round(direction / 45);
            while (_queue.Count > 0)
            {
                var start = _queue.Dequeue();
                if (_considered.Contains(start.X, start.Y) && !start.Equals(point))
                    return start;

                for (var i = 0; i < 8; i++)
                {
                    var octet = (startOctet + i) % 8;
                    var rad = octet * Math.PI / 4;
                    int adjX = (int)Math.Ceiling(Math.Cos(rad));
                    int adjY = (int)Math.Ceiling(Math.Sin(rad));
                    ConsiderNearest(searcher, start.X + adjX, start.Y + adjY);

                }
            }
            return null;
        }

        private void ConsiderNearest(PixelTracker searching, int x, int y)
        {
            if (searching.Contains(x, y))
                return;
            _queue.Enqueue(new Point(x, y));
            searching.Add(x, y);
        }
        private void Explore(int x, int y)
        {
            if (_considered.Contains(x, y))
                return;
            _considered.Add(x, y);
            var p = new Point(x, y);
            if (IsEdge(x, y))
            {
                outside.Add(p);
            }
            else
            {
                _queue.Enqueue(p);
            }
        }

        private bool IsEdge(int x, int y)
        {
            if (x < 0 || x >= _image.Width) return true;
            if (y < 0 || y >= _image.Height) return true;
            var color = _image.GetPixel(x, y);
            var distSqr = Math.Pow(_baseColor.R - color.R, 2) + Math.Pow(_baseColor.G - color.G, 2) +
                          Math.Pow(_baseColor.B - color.B, 2);
            return distSqr > _toleranceSquared;
        }

        protected virtual void OnOnLineFound(Point[] line)
        {
            var handler = OnLineFound;
            if (handler != null) handler(line);
        }
    }

}