using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace TagsCloudVisualization.Preprocessors {
    public class BoringPreprocessor : IPreprocessor {
        public IEnumerable<string> GetProcessedWords(IEnumerable<string> words) {
            var boringWords = new HashSet<string>();
            foreach (var word in File.ReadAllLines("boring.txt")) boringWords.Add(word);
            return words.Where(w => !boringWords.Contains(w)).Select(w => w.ToLower());
        }
    }
}
