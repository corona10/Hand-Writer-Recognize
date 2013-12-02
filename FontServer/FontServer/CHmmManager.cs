/**
 @section LICENSE
 
 The MIT License (MIT)

Copyright (c) 2013 Dong-hee,Na <corona10@gmail.com> 
                   Jun-woo, Choi <choigo92@gmail.com>  
                   Sun-min, Kim <mh5537@naver.com>

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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Thread-safe Sigletone Pettern
namespace FontServer
{
    class CHmmManager
    {
        private static CHmmManager instance = null;
        private static readonly object padlock = new object();

        private ArrayList _trainingSet;
        private ArrayList _outputLabels;

        private CHMMGenerator _hmmGenerator = new CHMMGenerator(8, 256);
        private CTrainer _hmmTrainer = new CTrainer();

        private CHmmManager()
        {
            this._trainingSet = new ArrayList();
            this._outputLabels = new ArrayList();
            Console.Write("* CHmmManager Initialized.......... ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" [SUCCESS]");
            Console.ResetColor();

        }

        public static CHmmManager func_Instance()
        {
            
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new CHmmManager();
                    }
                    return instance;
                }
            
        }

        public void func_addTrainingSet(int outputLabels, int[] train)
        {
            this._outputLabels.Add(outputLabels);
            this._trainingSet.Add(train);
        }

        public void func_train()
        {
            int[][] trainset_array = (int[][])this._trainingSet.ToArray(typeof(int[]));
            int[] outputLabel_array = (int[])this._outputLabels.ToArray(typeof(int));

            if (trainset_array.Length == 0 || outputLabel_array.Length == 0)
            {
                Console.WriteLine("[Error] Because of Wrong data  It can not be trained!");
                return;
            }
            Console.WriteLine("[System] Train for new sequence. ");
            this._hmmGenerator.func_train(trainset_array, outputLabel_array);
        }

        public int func_analyze(int[] sequence)
        {
           
            return this._hmmGenerator.func_analyze(sequence);
        }

        public int[][] func_getTrainingSet()
        {
            return (int[][])this._trainingSet.ToArray(typeof(int[]));
        }

        public int[] func_getOutputLabels()
        {
            return (int[])this._outputLabels.ToArray(typeof(int));
        }
      
    }
}
