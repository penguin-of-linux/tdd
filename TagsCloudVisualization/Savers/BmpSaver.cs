using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TagsCloudVisualization.Savers {
    public class BmpSaver : IImageSaver {
        public void SaveToFile(string fileName, TagCloud cloud, Size size) {

            var bitmap = new Bitmap(size.Width, size.Height);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var tag in cloud.Tags) {
                var rect = new Rectangle(tag.Location, tag.Form);
                TextRenderer.DrawText(graphics, tag.Text, tag.Font, rect, tag.Color);
            }

            bitmap.Save(fileName);
        }
    }
}
