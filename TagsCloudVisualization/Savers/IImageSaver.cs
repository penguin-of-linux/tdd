using System.Drawing;


namespace TagsCloudVisualization.Savers {
    public interface IImageSaver {
        void SaveToFile(string fileName, TagCloud cloud, Size size);
    }
}
