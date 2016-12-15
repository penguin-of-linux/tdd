using System.Collections.Generic;


namespace TagsCloudVisualization.FileReaders {
    public interface IFileReader {
        IEnumerable<string> GetWords(string fileName);
    }
}
