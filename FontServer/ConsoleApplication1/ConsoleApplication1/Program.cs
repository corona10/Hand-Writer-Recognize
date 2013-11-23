using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Fields;
using Accord.Statistics.Models.Fields.Functions;
using Accord.Statistics.Models.Fields.Learning;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Models.Markov.Topology;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            

            ArrayList trainingSet = new ArrayList();
            ArrayList outputLabels = new ArrayList();

            int[][] _trainingSet;
            int[] _outputLabels;

            trainingSet.Add(new[] { 0, 1, 1, 1, 0 });
            trainingSet.Add(new[] { 0, 0, 1, 1, 0, 0 });
            trainingSet.Add(new[] { 2, 2, 2, 2, 1, 1, 1, 1, 1 });
            trainingSet.Add(new[] { 0, 0, 0, 3, 3, 3, 3 });
            trainingSet.Add(new[] { 1, 0, 1, 2, 2, 2, 3, 3 });
            outputLabels.Add(0);
            outputLabels.Add(0);
            outputLabels.Add(1);
            outputLabels.Add(2);
            outputLabels.Add(2);

            _trainingSet = (int[][])trainingSet.ToArray(typeof(int[]));
            _outputLabels = (int[])outputLabels.ToArray(typeof(int));

            CHMMGenerator hmmGenerator = new CHMMGenerator(8, 3);
            CTrainer hmmTrainer = new CTrainer();

            hmmGenerator.func_train(_trainingSet, _outputLabels);
            int tmpClass1 = hmmGenerator.func_analyze(new[] { 0, 0, 1, 1, 1 });
            int tmpClass2 = hmmGenerator.func_analyze(new[] { 1, 2, 3, 0, 0 });
            int tmpClass3 = hmmGenerator.func_analyze(new[] { 0, 1, 1, 2, 3 });
            int tmpClass4 = hmmGenerator.func_analyze(new[] { 0, 1, 1, 0, 1 });
            int tmpClass5 = hmmGenerator.func_analyze(new[] { 2, 0, 1, 1, 1 });
            double prob1 = hmmGenerator.func_getProb(new[] { 2, 0, 1, 1, 1 });


            System.Console.WriteLine(tmpClass1);
            System.Console.WriteLine(tmpClass2);
            System.Console.WriteLine(tmpClass3);
            System.Console.WriteLine(tmpClass4);
            System.Console.WriteLine(tmpClass5);
            System.Console.WriteLine(prob1);

            //hmmGenerator.func_generate(inputSequences, outputLabels);


        }
    }

}
