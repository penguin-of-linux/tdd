using System.Collections.Generic;


namespace TagsCloudVisualization.CloudBuilders {
    public interface ICloudBuilder {
        TagCloud CreateCloud(IEnumerable<string> words, ICloudLayouter layouter);
    }
}
