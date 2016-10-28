using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization {
    public interface ICloudLayouter {
        Rectangle PushNextRectangle(Size size);
        List<Rectangle> GetRectangles();
    }
}
