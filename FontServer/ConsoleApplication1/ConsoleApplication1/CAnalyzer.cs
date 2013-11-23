using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Fields;
using Accord.Statistics.Models.Fields.Functions;
using Accord.Statistics.Models.Fields.Learning;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Models.Markov.Topology;

namespace ConsoleApplication1
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
