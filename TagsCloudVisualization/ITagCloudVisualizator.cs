namespace TagsCloudVisualization {
    public interface ITagCloudVisualizator {
        TagCloud Cloud { set; }
        void SetSettings(FileReaders.IFileReader reader,
                         Layouters.ICloudLayouter layouter,
                         Preprocessors.IPreprocessor preprocessor,
                         Savers.IImageSaver saver,
                         CloudBuilders.ICloudBuilder builder);
        void Run();
    }
}
