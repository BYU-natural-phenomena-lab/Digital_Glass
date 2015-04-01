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
        private Point _start;
        public event LineFoundHandler LineFound;

        public delegate void LineFoundHandler(Point[] line);

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

        public RegionFinder(Bitmap image, Point startPoint, uint tolerance)
        {
            try
            {
                _start = startPoint;
                _baseColor = image.GetPixel(startPoint.X, startPoint.Y);
                _image = image;
                _queue.Enqueue(startPoint);
                _considered = new PixelTracker(_image.Width, _image.Height) {OutsideDefault = true};
                _considered.Add(startPoint.X, startPoint.Y);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            _toleranceSquared = Math.Pow(tolerance, 2);
        }

        private ISet<Point> _outside = new System.Collections.Generic.HashSet<Point>(); 
        private void FloodFill()
        {
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
        }

        public void Process()
        {
            FloodFill();
            var edgePoints = new List<Point>
            {
                FindEdge(_start, 1, 0), //E
                FindEdge(_start, 1, 1), //SE
                FindEdge(_start, 0, 1), //S
                FindEdge(_start, -1, 1), //SW
                FindEdge(_start, -1, 0), //W
                FindEdge(_start, -1, -1), //NW
                FindEdge(_start, 0, -1), //N
            };
            var points = new List<Point>();
            for (var i = 1; i < edgePoints.Count; i++)
            {
                points.AddRange(LeastCostPath(edgePoints[i],edgePoints[i-1]));
            }
            points.AddRange(LeastCostPath(edgePoints[0], edgePoints[edgePoints.Count-1]));

           
            OnLineFound(points.ToArray());
        }

        private System.Collections.Generic.IList<Point> LeastCostPath(Point start, Point end)
        {
            var h = new IntervalHeap<Node>(new NodeCmpr());
            var n = new Node
            {
                Back = null,
                Cost = 0,
                Point = start
            };
            h.Add(n);
            _considered = new PixelTracker(_image.Width, _image.Height) { OutsideDefault = true };
            while (!h.IsEmpty)
            {
                var node = h.DeleteMin();
                if (node.Point.X == end.X && node.Point.Y == end.Y)
                {
                    return Backtrace(node);
                }
                Follow(h,node,node.Point.X - 1, node.Point.Y - 1);
                Follow(h,node,node.Point.X - 1, node.Point.Y);
                Follow(h,node,node.Point.X - 1, node.Point.Y + 1);
                Follow(h,node,node.Point.X, node.Point.Y + 1);
                Follow(h,node,node.Point.X + 1, node.Point.Y + 1);
                Follow(h,node,node.Point.X + 1, node.Point.Y);
                Follow(h,node,node.Point.X + 1, node.Point.Y - 1);
                Follow(h,node,node.Point.X, node.Point.Y - 1);
            }
            return new Point[]{};
        }

        private System.Collections.Generic.IList<Point> Backtrace(Node node)
        {
            var points = new List<Point>();
            while (node != null)
            {
                points.Add(node.Point);
                node = node.Back;
            }
            return points;
        }

        private void Follow(IPriorityQueue<Node> heap,Node previous, int x, int y)
        {
            if (_considered.Contains(x,y))
                return;
            _considered.Add(x,y);
            var point = new Point(x, y);
            if (!_outside.Contains(point))
                return;
            var node = new Node
            {
                Back = previous,
                Point = point,
                Cost = previous.Cost + Distance(point, previous.Point)
            };
            heap.Add(node);
        }

        private static double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }


        private class NodeCmpr : IComparer<Node>
        {
            public int Compare(Node x, Node y)
            {
                return x.Cost.CompareTo(y.Cost);
            }
        }

        private class Node
        {
            public double Cost { get; set; }
            public Point Point { get; set; }
            public Node Back { get; set; }
        }

        private Point FindEdge(Point start,int xDirection, int yDirection)
        {
            var x = start.X;
            var y = start.Y;
            while (!IsEdge(x, y))
            {
                x += xDirection;
                y += yDirection;
            }
            return new Point(x, y);
        }

        private void Explore(int x, int y)
        {
            if (_considered.Contains(x, y))
                return;
            _considered.Add(x, y);
                var p = new Point(x, y);
            if (IsEdge(x, y))
            {
                _outside.Add(p);
            }
            else {
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

        protected virtual void OnLineFound(Point[] line)
        {
            var handler = LineFound;
            if (handler != null) handler(line);
        }
    }
}