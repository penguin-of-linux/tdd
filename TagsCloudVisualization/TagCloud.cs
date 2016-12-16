using System.Collections.Generic;

namespace TagsCloudVisualization {
    public class TagCloud {
        public List<Tag> Tags { get; private set; }

        public void AddTag(Tag tag) {
            Tags.Add(tag);
        }

        public void AddTags(IEnumerable<Tag> tags) {
            Tags = new List<Tag>(tags);
        }
    }
}
