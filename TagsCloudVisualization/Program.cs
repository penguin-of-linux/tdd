using System.Drawing;
using Autofac;
using TagsCloudVisualization.FileReaders;
using TagsCloudVisualization.CloudBuilders;
using TagsCloudVisualization.Preprocessors;
using TagsCloudVisualization.Savers;
using System.IO;
using System.Text;
using TagsCloudVisualization.Layouters;

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
            var saver = container.Resolve<IImageSaver>();

            var visualizator = container.Resolve<ITagCloudVisualizator>();

            visualizator.SetSettings(reader, layouter, preprocessor, saver, cloudBilder);
            visualizator.Run();
        }

        static void AutofacSetUp() {
            var builder = new ContainerBuilder();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<TxtReader>().As<IFileReader>();
            builder.RegisterType<CloudBuilder>().As<ICloudBuilder>();
            builder.RegisterType<WinFormsVisualisator>().As<ITagCloudVisualizator>();
            builder.RegisterType<BoringPreprocessor>().As<IPreprocessor>();
            builder.RegisterType<BmpSaver>().As<IImageSaver>();

            container = builder.Build();
        }
    }
}
