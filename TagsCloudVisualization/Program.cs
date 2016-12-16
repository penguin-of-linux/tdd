using System.Windows.Forms;
using System.Drawing;
using Autofac;
using TagsCloudVisualization.FileReaders;
using TagsCloudVisualization.CloudBuilders;
using TagsCloudVisualization.Preprocessors;

namespace TagsCloudVisualization {
    static class Program {
        static IContainer container;
        static void Main() {
            AutofacSetUp();

            var reader = container.Resolve<IFileReader>();
            var preprocessor = container.Resolve<IPreprocessor>();
            var layouter = container.Resolve<ICloudLayouter>(
                    new TypedParameter(typeof(Point), new Point(300, 300)));
            var cloudBilder = container.Resolve<ICloudBuilder>();

            var words = reader.GetWords("../../words1.txt");
            words = preprocessor.GetProcessedWords(words);

            var cloud = cloudBilder.CreateCloud(words, layouter);

            var visualizator = container.Resolve<ITagCloudVisualizator>(
                new TypedParameter(typeof(Size), (
                    new Size(
                        layouter.CloudWidth + layouter.Center.X, layouter.CloudHeight + layouter.Center.Y))));

            visualizator.Cloud = cloud;
            visualizator.DrawCloud();
        }

        static void AutofacSetUp() {
            var builder = new ContainerBuilder();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<TxtReader>().As<IFileReader>();
            builder.RegisterType<CloudBuilder>().As<ICloudBuilder>();
            builder.RegisterType<WinFormsVisualisator>().As<ITagCloudVisualizator>();
            builder.RegisterType<BoringPreprocessor>().As<IPreprocessor>();

            container = builder.Build();
        }
    }
}
