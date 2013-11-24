using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Topology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FontServer
{
    class CHMMGenerator
    {
        private HiddenMarkovClassifier _Classifier;
        private CTrainer _Trainer;
        private CAnalyzer _Analyzer;

        public CHMMGenerator()
        {

        }

        public CHMMGenerator(int States, int ClassCount)
        {
            ITopology forward = new Forward(States);
            this._Trainer = new CTrainer();
            this._Analyzer = new CAnalyzer();
            this._Classifier = new HiddenMarkovClassifier(classes: ClassCount, topology: forward, symbols: 8);
        }

        public HiddenMarkovClassifier func_getClassifier()
        {
            return this._Classifier;
        }
        public void func_train(int[][] InputSequences, int[] OutputClass)
        {
            this._Trainer.func_train(InputSequences, OutputClass, _Classifier);
        }
        public int func_analyze(int[] newSequence)
        {
            
            return this._Analyzer.func_analyze(newSequence, _Classifier);
        }
        public double func_getProb(int[] newSequence)
        {
            return this._Classifier.LogLikelihood(newSequence);
        }
    }
}
