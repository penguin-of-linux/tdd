using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualization.CloudBuilders {
    public class CloudBuilder : ICloudBuilder {
        private const double minFontSize = 5;
        private const double maxFontSize = 20;

        public TagCloud CreateCloud(IEnumerable<string> words, ICloudLayouter layouter) {
            var tags = new List<Tag>();
            var frequencyDictionary = new Dictionary<string, int>();
            foreach (var word in words) {
                if (!frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word] = 0;
                frequencyDictionary[word]++;
            }

            var minFrequency = frequencyDictionary.Values.Min();
            var maxFrequency = frequencyDictionary.Values.Max();
            var sizesDifferent = maxFontSize != minFontSize ? (maxFontSize - minFontSize) : 1;
            var fontSizePerOneFrequency = sizesDifferent / (maxFrequency - minFrequency);

            foreach (var pair in frequencyDictionary) {
                var font = new Font("fantasy", (float)(minFontSize + fontSizePerOneFrequency * pair.Value));
                var size = TextRenderer.MeasureText(pair.Key, font);
                tags.Add(new Tag(pair.Key, font, size, new Point(0, 0), Color.Red));
            }
            var result = new TagCloud();
            result.AddTags(tags);

            LocateTags(result, layouter);

            return result;
        }

        private void LocateTags(TagCloud cloud, ICloudLayouter layouter) {
            for (int i = 0; i < cloud.Tags.Count; i++) {
                var location = layouter.PushNextRectangle(cloud.Tags[i].Form).Location;
                cloud.Tags[i].Location = location;
            }
        }
    }
}
