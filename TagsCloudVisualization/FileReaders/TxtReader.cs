using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.FileReaders {
    public class TxtReader : IFileReader {
        public IEnumerable<string> GetWords(string fileName) {
            return File.ReadAllLines(fileName);
        }
    }
}
