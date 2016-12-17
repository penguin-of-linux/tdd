using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace TagsCloudVisualization {
    public class TagCloud {
        public List<Tag> Tags { get; private set; }

        public int Height {
            get {
                return Tags.Select(t => new Rectangle(t.Location, t.Form)).Max(r => r.Bottom) - 
                    Tags.Select(t => new Rectangle(t.Location, t.Form)).Min(r => r.Top);
            }
        }
        public int Width {
            get {
                return Tags.Select(t => new Rectangle(t.Location, t.Form)).Max(r => r.Right) -
                    Tags.Select(t => new Rectangle(t.Location, t.Form)).Min(r => r.Left);
            }
        }

        public Point Center;
        public void AddTag(Tag tag) {
            Tags.Add(tag);
        }

        public void AddTags(IEnumerable<Tag> tags) {
            Tags = new List<Tag>(tags);
        }
    }
}
