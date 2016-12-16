using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization {
    public interface ITagCloudVisualizator {
        TagCloud Cloud { set; }
        void SaveImageToFile(string fileName);
        void DrawCloud();
    }
}
