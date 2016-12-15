using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization {
    public class TagCloud {
        private const int maxTagWidth = 200;
        private const double heightWidthRelation = 1.4;
        public List<Tag> Tags { get; private set; }

        public void AddTag(Tag tag) {
            Tags.Add(tag);
        }

        public static TagCloud CreateTagsCloud(IEnumerable<string> words) {
            var tags = new List<Tag>();
            var frequencyDictionary = new Dictionary<string, int>();

            foreach (var word in words) {
                if (!frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word] = 0;
                frequencyDictionary[word]++;
            }

            var minFrequency = frequencyDictionary.Values.Min();
            var maxFrequency = frequencyDictionary.Values.Max();
            var pixelPerFrequency = maxTagWidth / maxFrequency;

            foreach (var pair in frequencyDictionary) {
                var width = pixelPerFrequency * pair.Value;
                var height = (int)(width / heightWidthRelation);
                tags.Add(new Tag(pair.Key, new Size(width, height), new Point(0, 0)));
            }
            var result = new TagCloud();
            result.Tags = tags;
            return result;
        }
    }
}
