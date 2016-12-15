using NUnit.Framework;
using System.Drawing;
using TagsCloudVisualization;
using System.Linq;
using System;

namespace Tests {
    [TestFixture]
    public class CircularCloudLayouterTest {
        public CircularCloudLayouter cloudLayouter;

        [SetUp]
        public void CircularCloudLayouter_Init() {
            cloudLayouter = new CircularCloudLayouter(new Point(0, 0));
        }

        [Test]
        public void PushNextRectangle_PushFirstRectangle() {
            var size = new Size(10, 10);
            var nextRectangle = cloudLayouter.PushNextRectangle(size);
            Assert.AreEqual(cloudLayouter.Center, nextRectangle.GetCenter());
        }

        [Test]
        public void CloudHasCircularForm() {
            for (int i = 0; i < 30; i++)
                cloudLayouter.PushNextRectangle(new Size(10, 5));

            Assert.AreEqual(0, cloudLayouter.CloudHeight - cloudLayouter.CloudWidth, 20);
        }

        [Test]
        public void RectanglesIsNotCrossing() {
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
                    var x = cloudLayouter.Center.X - r.Location.X;
                    var y = cloudLayouter.Center.Y - r.Location.Y;
                    return Math.Sqrt(x * x + y * y);
                }
            );

            cloudLayouter = new CircularCloudLayouter(cloudLayouter.Center);
            for (int i = 0; i < 20; i++) {
                cloudLayouter.PushNextRectangle(new Size(10, 5));
            }

            var newSummDistToCenter = cloudLayouter.GetRectangles().Sum(r => {
                    var x = cloudLayouter.Center.X - r.Location.X;
                    var y = cloudLayouter.Center.Y - r.Location.Y;
                    return Math.Sqrt(x * x + y * y);
                }
            );
            Assert.GreaterOrEqual(oldSummDistToCenter, newSummDistToCenter);

        }
    }
}