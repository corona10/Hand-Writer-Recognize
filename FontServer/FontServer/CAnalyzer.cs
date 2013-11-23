using Accord.Statistics.Models.Markov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FontServer
{
    class CAnalyzer
    {
        public int func_analyze(int[] newSquence, HiddenMarkovClassifier Classifier)
        {
            int tmpClass = Classifier.Compute(newSquence);

            return tmpClass;
        }
    }
}
