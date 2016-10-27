using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace TagsCloudVisualization {
    public class TagsCloudVisualizator : Form {
        readonly List<Rectangle> rectangles;
        public TagsCloudVisualizator(List<Rectangle> rectangles) {
            Size = new Size(1300, 700);
            this.rectangles = rectangles;
        }

        public void SaveImageToFile(string fileName) {
            var center = rectangles.First();
            var left = rectangles.Min(r => r.Left);
            var top = rectangles.Min(r => r.Top);
            var height = rectangles.Max(r => r.Bottom) - rectangles.Min(r => r.Top);
            var width = rectangles.Max(r => r.Right) - rectangles.Min(r => r.Left);

            var bitmap = new Bitmap(width + 100, height + 100);
            var graphics = Graphics.FromImage(bitmap);

            graphics.FillRectangle(Brushes.White, 0, 0, width, height);
            graphics.DrawRectangles(new Pen(Color.Black), rectangles
                .Select(r => new Rectangle(new Point(r.Location.X - left, r.Location.Y - top), r.Size))
                .ToArray());

            bitmap.Save(fileName);
        }

        protected override void OnPaint(PaintEventArgs e) {
            var graphics = e.Graphics;

            graphics.DrawRectangles(new Pen(Color.Black), rectangles.ToArray());
        }
    }
}
