using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System;

namespace TagsCloudVisualization {
    public class TagsCloudVisualizator : Form {
        public TagCloud Cloud { private get; set; }
        public TagsCloudVisualizator(Size size) {
            Size = size;
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

        protected override void OnPaint(PaintEventArgs e) {
            var graphics = e.Graphics;

            if (Cloud != null) {
                foreach(var tag in Cloud.Tags) {
                    var rect = new Rectangle(tag.Location, tag.Form);
                    graphics.DrawRectangle(new Pen(Color.Black), rect);
                    //graphics.FillRectangle()
                    //var fontSize_em = (int)(rect.Height / 0.35);
                    var font = new Font("fantasy", 10);
                    TextRenderer.DrawText(graphics, tag.Text, font, rect, Color.Red);
                }
            }
        }
    }
}
