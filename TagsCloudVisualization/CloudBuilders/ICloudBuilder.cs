using System.Collections.Generic;
using TagsCloudVisualization.Layouters;


namespace TagsCloudVisualization.CloudBuilders {
    public interface ICloudBuilder {
        TagCloud CreateCloud(IEnumerable<string> words, ICloudLayouter layouter);
    }
}
