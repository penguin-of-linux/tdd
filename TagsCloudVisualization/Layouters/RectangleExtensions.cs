using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace TagsCloudVisualization.Layouters {
    public static class RectangleExtensions {
        public static bool IsCrossing(this Rectangle targetRect, List<Rectangle> rectangles) {
            foreach (var rect in rectangles)
                if (targetRect.IntersectsWith(rect))
                    return true;
            return false;
        }

        public static Point GetCenter(this Rectangle rect) {
            return new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
        }
    }
}
