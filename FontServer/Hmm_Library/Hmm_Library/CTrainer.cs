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
    class CTrainer
    {
        private HiddenMarkovClassifierLearning _Teacher;

        public void func_train(int[][] InputSequences, int[] OutputClass, HiddenMarkovClassifier Classifier)
        {
            _Teacher = new HiddenMarkovClassifierLearning(Classifier,
                modelIndex => new BaumWelchLearning(Classifier.Models[modelIndex])
                {
                    Tolerance = 0.001,
                    Iterations = 0
                });

            double error = _Teacher.Run(InputSequences, OutputClass);
        }
    }
}
