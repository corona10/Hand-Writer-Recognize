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

        private CHMMGenerator _hmmGenerator = new CHMMGenerator(5, 30);
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
                Console.WriteLine("Train 할 수 없습니다.");
                return;
            }
            Console.WriteLine("Train을 시작합니다.");
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
