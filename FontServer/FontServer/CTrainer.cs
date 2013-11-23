using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FontServer
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
