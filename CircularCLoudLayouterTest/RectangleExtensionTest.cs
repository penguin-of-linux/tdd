using System.Collections.Generic;
using NUnit.Framework;
using System.Drawing;
using TagsCloudVisualization;

namespace Tests{
    [TestFixture]
    public class RectangleExtensionTest {

        [TestCase(25, 5, 2, 2, ExpectedResult = false)]
        [TestCase(5, 5, 5, 5, ExpectedResult = true)]
        public bool IsCrossing(int x, int y, int w, int h) {
            var rectangles = new List<Rectangle>() {
                new Rectangle(0, 0, 10, 10),
                new Rectangle(11, 11, 10, 10),
                new Rectangle(-5, -5, 1, 1)
            };

            return new Rectangle(x, y, w, h).IsCrossing(rectangles);
        }

        [Test]
        public void GetCenter() {
            var rect = new Rectangle(0, 0, 10, 10);
            var center = rect.GetCenter();
            Assert.AreEqual(new Point(5, 5), center);
        }
    }
}
