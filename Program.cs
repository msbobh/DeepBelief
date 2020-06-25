using System;
using Accord.IO;
using System.IO;
using UtilityFuncs;
using resources;
using Accord.Math;
using Accord.Statistics.Analysis;

namespace NNAccord
{
    class Program
    {
        static void Main(string[] args)
        {
                 
            /*
             * Process command line arguments and input training and label files converting
             * to jagged arrays expected by the learning program.
             * 
             */
            
            string trainingfile = null;
            string labelfile = null;
            string ModelSave = "DeepBeliefModel.sav";


            // Process the command line and error out if not acceptable

            Utility.CommandLine process = new Utility.CommandLine(args);
            if (process.usage)
            {
                Console.WriteLine(strings.usage);
                System.Environment.Exit(1);
            }

            
            labelfile = process.arg2;
            trainingfile = process.arg1;
            string path = labelfile;
            if (process.numargs == 3) // bug bug needs to be not and checked for valid file not a number

                ModelSave = Path.ChangeExtension(labelfile, (".sav"));
                    //string changed = Path.ChangeExtension(path, ".sav")

            Console.WriteLine(strings.StartingUp);
            
            // 
            // Read in the training file an convert to a Matrix
            //
            CsvReader training_samples = new CsvReader(trainingfile, false);
            double[,] MatrixIn = training_samples.ToMatrix<double>();

            int rows = MatrixIn.Rows();
            int cols = MatrixIn.Columns();

            // 
            // Read in the label file an convert to a Matrix
            //
            CsvReader labelscsv = new CsvReader(labelfile, false);
            double[,] labels = labelscsv.ToMatrix<double>();

            // Labels need to be doubles for the NN routines and need to be jagged arrays
            double[][] output1 = externalFunc.convertToJaggedArray(labels);

            if (rows != labels.Rows()) // number of samples must match
            {
                Console.WriteLine(strings.SampleMisMatch, cols, 4);
                System.Environment.Exit(1);
            }

            // For Accord.net learning routines require input data Jagged Arrays         
            double[][] input1 = MatrixIn.ToJagged<double>();

                      
            int NumInputs = input1[1].GetLength(0);
            int Layer2 = NumInputs * 2;

            
            Console.WriteLine("\nStarting Deep Belief");
            // Lets run a DBN with same data
            RBM.deepBelief DBN = new RBM.deepBelief(NumInputs, hidden: 4, outputs: 1, inputs: input1, labels: output1);
            int [] DBNPredictions = externalFunc.Predict(input1, DBN.DBNetwork);  
            
            ConfusionMatrix cmDeepbelief = new ConfusionMatrix( expected: externalFunc.convetToJaggedArray(labels),
                predicted: DBNPredictions);
            DBN.DBNetwork.Save(ModelSave);

            Utility.output.Printstats(cmDeepbelief);
            Console.WriteLine("DBN Learning Algorithm: {0}", DBN.GetType().Name);
            
            if (input1.Length <= 4)
                for (int i = 0; i <= input1.Length - 1; i++)
                {
                    Console.WriteLine
                        ("Predicited {0}, Actual {1}", DBNPredictions[i], output1[i][0]);
                }
        }
    }
}

