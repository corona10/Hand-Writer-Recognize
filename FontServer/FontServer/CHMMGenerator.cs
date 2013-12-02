/**
 @section LICENSE
 
 The MIT License (MIT)

Copyright (c) 2013 Dong-hee,Na <corona10@gmail.com> Jun-woo, Choi <choigo92@gmail.com>  Sun-min, Kim <mh5537@naver.com>

 Permission is hereby granted, free of charge, to any person obtaining a copy of 
this software and associated documentation files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is 
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included
in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, 
 ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * **/
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
