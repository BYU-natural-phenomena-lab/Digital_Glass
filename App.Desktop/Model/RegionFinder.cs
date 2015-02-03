using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Walle.Model
{
    public class RegionFinder
    {
        private Queue<Point> _queue = new Queue<Point>();
        private Bitmap _image;
        private readonly double _toleranceSquared;
        private Color _baseColor;
        private PixelTracker _considered;

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
            }

            public void Add(int x, int y)
            {
                _bools[x, y] = true;
            }

            public bool Contains(int x, int y)
            {
                if (x < 0 || y < 0 || x >= _width || y >= _height)
                    return true;
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
                _considered = new PixelTracker(_image.Width, _image.Height);
                _considered.Add(startPoint.X, startPoint.Y);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            _toleranceSquared = Math.Pow(tolerance, 2);
        }


        public Point[] Process()
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
            return outside.ToArray();
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
    }
}