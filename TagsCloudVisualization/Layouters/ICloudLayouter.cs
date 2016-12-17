using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Layouters {
    public interface ICloudLayouter {
        Rectangle PushNextRectangle(Size size);
        List<Rectangle> GetRectangles();
        int CloudHeight { get; }
        int CloudWidth { get; }
        Point Center { get; }
    }
}
