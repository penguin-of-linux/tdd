using System.Collections.Generic;
using NUnit.Framework;
using System.Drawing;
using TagsCloudVisualization;
using System.Linq;
using System;

namespace CircularCloudLayouterTests {
    [TestFixture]
    public class CircularCloudLayouterTest {
        public CircularCloudLayouter cloudLayouter;

        [SetUp]
        public void CircularCloudLayouter_Init() {
            cloudLayouter = new CircularCloudLayouter(new Point(0, 0));
        }

        [Test]
        public void PushNextRectangle_EmptyList() {
            var size = new Size(10, 10);
            var nextRectangle = cloudLayouter.PushNextRectangle(size);
            Assert.AreEqual(cloudLayouter.center, nextRectangle.GetCenter());
        }

        [Test]
        public void PushNextRectangle_ManyItems() {
            for (int i = 0; i < 30; i++)
                cloudLayouter.PushNextRectangle(new Size(10, 5));

            Assert.AreEqual(0, cloudLayouter.CloudHeight - cloudLayouter.CloudWidth, 20);
        }

        [Test]
        public void IsRectanglesCrossing() {
            var isCrossing = false;
            for (int i = 0; i < 50; i++) {
                var rect = cloudLayouter.PushNextRectangle(new Size(50, 25));
                isCrossing = isCrossing || rect.IsCrossing(cloudLayouter.GetRectangles()
                    .Where(r => !r.Equals(rect))
                    .ToList());
            }

            Assert.False(isCrossing);
        }

        [Test]
        public void Cloud_IsCompact() {
            for (int i = 0; i < 20; i++)
                cloudLayouter.PushNextRectangle(new Size(10, 5));

            var oldSummDistToCenter = cloudLayouter.GetRectangles().Sum(r => {
                    var x = cloudLayouter.center.X - r.Location.X;
                    var y = cloudLayouter.center.Y - r.Location.Y;
                    return Math.Sqrt(x * x + y * y);
                }
            );

            cloudLayouter = new CircularCloudLayouter(cloudLayouter.center);
            for (int i = 0; i < 20; i++) {
                cloudLayouter.PushNextRectangle(new Size(10, 5), true);
            }

            var newSummDistToCenter = cloudLayouter.GetRectangles().Sum(r => {
                    var x = cloudLayouter.center.X - r.Location.X;
                    var y = cloudLayouter.center.Y - r.Location.Y;
                    return Math.Sqrt(x * x + y * y);
                }
            );
            Assert.Greater(oldSummDistToCenter, newSummDistToCenter);

        }
    }

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