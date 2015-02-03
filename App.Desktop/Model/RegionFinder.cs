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
        private HashSet<ulong> _considered = new HashSet<ulong>();

        public RegionFinder(Bitmap image, Point startPoint, uint tolerance)
        {
            try
            {
                _baseColor = image.GetPixel(startPoint.X, startPoint.Y);
                _image = image;
                _queue.Enqueue(startPoint);
                _considered.Add(F(startPoint.X, startPoint.Y));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            _toleranceSquared = Math.Pow(tolerance, 2);
        }

        private ulong F(int x, int y)
        {
            return (uint) y | ((ulong) x << 32);
        }


        public Point[] Process()
        {
            var outside = new List<Point>();

            while (_queue.Count > 0)
            {
                var start = _queue.Dequeue();

                if (IsEdge(start.X - 1, start.Y - 1))
                    outside.Add(new Point(start.X - 1, start.Y - 1));

                if (IsEdge(start.X - 1, start.Y))
                    outside.Add(new Point(start.X - 1, start.Y));

                if (IsEdge(start.X - 1, start.Y + 1))
                    outside.Add(new Point(start.X - 1, start.Y + 1));

                if (IsEdge(start.X, start.Y + 1))
                    outside.Add(new Point(start.X, start.Y + 1));

                if (IsEdge(start.X + 1, start.Y + 1))
                    outside.Add(new Point(start.X + 1, start.Y + 1));

                if (IsEdge(start.X + 1, start.Y))
                    outside.Add(new Point(start.X + 1, start.Y));

                if (IsEdge(start.X + 1, start.Y - 1))
                    outside.Add(new Point(start.X + 1, start.Y - 1));

                if (IsEdge(start.X, start.Y - 1))
                    outside.Add(new Point(start.X, start.Y - 1));

            }
            return outside.ToArray();
        }

        private bool IsEdge(int x, int y)
        {
            var h = F(x, y);
            if (_considered.Contains(h))
                return false;
            _considered.Add(h);
            if (x < 0 || x >= _image.Width) return true;
            if (y < 0 || y >= _image.Height) return true;
            var color = _image.GetPixel(x, y);
            var distSqr = Math.Pow(_baseColor.R - color.R, 2) + Math.Pow(_baseColor.G - color.G, 2) + Math.Pow(_baseColor.B - color.B, 2);
            if (distSqr > _toleranceSquared)
                return true;
            _queue.Enqueue(new Point(x, y));
            return false;
        }
    }
}
