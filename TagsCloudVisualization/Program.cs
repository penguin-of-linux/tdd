using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace TagsCloudVisualization {
    static class Program {
        static void Main() {
            var layouter = new CircularCloudLayouter(new Point(200, 200));
            var lines = File.ReadAllLines("../../sizes_1.txt");
            foreach(var line in lines) {
                var x = int.Parse(line.Split(',')[0]);
                var y = int.Parse(line.Split(',')[1]);
                layouter.PushNextRectangle(new Size(x, y), true);
            }

            var visualizator = new TagsCloudVisualizator();
            visualizator.DrawCloud(layouter.GetRectangles());
            //visualizator.SaveImageToFile("1.bmp", layouter.GetRectangles());

            Application.Run(visualizator);
        }
    }
}
