﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Preprocessors {
    class BoringPreprocessor : IPreprocessor {
        public IEnumerable<string> GetProcessedWords(IEnumerable<string> words) {
            return words;
        }
    }
}
