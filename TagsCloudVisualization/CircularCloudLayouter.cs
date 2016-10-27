using System.Collections.Generic;
using System.Drawing;
using System;
using System.Linq;

namespace TagsCloudVisualization {
    public class CircularCloudLayouter : ICloudLayouter {
        public readonly Point center;
        private List<Rectangle> rectangles = new List<Rectangle>();

        public int CloudHeight { get {
                var top = rectangles.Min(r => r.Top);
                var bottom = rectangles.Max(r => r.Bottom);
                return bottom - top;
            }
        }

        public int CloudWidth {
            get {
                var left = rectangles.Min(r => r.Left);
                var right = rectangles.Max(r => r.Right);
                return right - left;
            }
        }

        public List<Rectangle> GetRectangles() {
            return new List<Rectangle>(rectangles);
        }
        public CircularCloudLayouter(Point center) {
            this.center = center;
        }

        public Rectangle PushNextRectangle(Size size) {
            return PushNextRectangle(size, false);
        }

        public Rectangle PushNextRectangle(Size size, bool isCompact) {
            var result = new Rectangle(center, size);
            if (rectangles.Count == 0) {
                result.Location = new Point(center.X - result.Width / 2, center.Y - result.Height / 2);
                rectangles.Add(result);
                return result;
            }

            foreach (var location in Geometry.GetNewSpiralPoint()) {
                result.Location = new Point(center.X + location.X, center.Y + location.Y);
                if (!result.IsCrossing(rectangles)) {
                    Geometry.GetNewSpiralPoint(true);
                    break;
                }
            }
            if (isCompact) {
                result.Location = GetCompactLocation(result);
            }
            rectangles.Add(result);
            return result;
        }

        public Point GetCompactLocation(Rectangle target) {
            if (target.GetCenter() == center) return target.Location;
            var tempRect = target;
            var previousLocation = tempRect.Location;
            double x = tempRect.Location.X;
            double y = tempRect.Location.Y;
            var deltaVector = new System.Windows.Vector(center.X - x, center.Y - y);
            deltaVector.Normalize();

            while (!tempRect.IsCrossing(rectangles.Where(r => !r.Equals(target)).ToList())) {
                previousLocation = tempRect.Location;
                x += deltaVector.X;
                y += deltaVector.Y;
                tempRect.Location = new Point((int)x, (int)y);
            }

            return previousLocation;
        }
    }
}
