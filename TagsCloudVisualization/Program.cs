using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using Autofac;
using TagsCloudVisualization.FileReaders;

namespace TagsCloudVisualization {
    static class Program {
        static IContainer container;
        static void Main() {
            AutofacSetUp();

            var words = container.Resolve<IFileReader>().GetWords("../../words1.txt");
            var cloud = TagCloud.CreateTagsCloud(words);
            var layouter = container.Resolve<ICloudLayouter>(
                new TypedParameter(typeof(Point), new Point(200, 200)));

            for(int i = 0; i < cloud.Tags.Count; i++) {
                var location = layouter.PushNextRectangle(cloud.Tags[i].Form).Location;
                cloud.Tags[i].Location = location;
            }

            var visualizator = new TagsCloudVisualizator(new Size(layouter.CloudWidth, layouter.CloudHeight));

            visualizator.Cloud = cloud;
            //visualizator.SaveImageToFile("1.bmp", layouter.GetRectangles());

            Application.Run(visualizator);
        }

        static void AutofacSetUp() {
            var builder = new ContainerBuilder();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<TxtReader>().As<IFileReader>();

            container = builder.Build();
        }
    }
}
