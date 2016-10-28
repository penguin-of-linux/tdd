using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace TagsCloudVisualization {
    public class TagsCloudVisualizator : Form {
        private List<Rectangle> rectangles;
        public TagsCloudVisualizator() {
            Size = new Size(1300, 700);
        }

        public void SaveImageToFile(string fileName, List<Rectangle> rectangles) {
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

        public void DrawCloud(List<Rectangle> rectangles) {
            this.rectangles = rectangles;
        }

        protected override void OnPaint(PaintEventArgs e) {
            var graphics = e.Graphics;

            if (rectangles != null)
                graphics.DrawRectangles(new Pen(Color.Black), rectangles.ToArray());
        }
    }
}
