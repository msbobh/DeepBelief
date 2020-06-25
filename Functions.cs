using UtilityFuncs;
using System;
namespace Utility
{
    

    public class output
    {
        static public void Printstats (in Accord.Statistics.Analysis.ConfusionMatrix cm)
        {
            // Prediction accuracy of training set = 99.51%
            Console.Write(resources.strings.Predictions);
            externalFunc.Printcolor(Math.Round(cm.Accuracy*100,2), ConsoleColor.Red, true);
            // Sensitivity(true Positive rate) = 86
            // Specificity(true Negative rate) = 100
            Console.Write("Sensitivity (Recall - true Positive rate) = ");
            externalFunc.Printcolor (Math.Round(cm.Sensitivity * 100,2), ConsoleColor.Yellow, true);
            Console.Write("Specificity(true Negative rate) = ");
            externalFunc.Printcolor(Math.Round(cm.Specificity * 100, 2), ConsoleColor.Yellow, true);
            Console.Write("Precision (TP / TP + FP) = ");
            externalFunc.Printcolor(Math.Round(Math.Round(cm.Precision * 100, 2)), ConsoleColor.Yellow, true);
            Console.WriteLine("Precision (TP / TP + FP)  = {0}", Math.Round(cm.Precision * 100, 2));
            Console.WriteLine("Fscore (harmonic mean of Precision and Recall.) = {0}", Math.Round(cm.FScore * 100), 2);
            
        }
    }

    public class CommandLine
    {
        public int numargs;
        public string arg1, arg2, arg3;
                
        public bool usage = false; //True if improper usage 
        public bool FileOpenError = false;

        public CommandLine(string[] args)
        {
            /* 
             * Process command line arguments, return variables and validate existence of files
             */

            /* Max of 3 parameters:
             * Training set
             * Labels
             * <opt> file name for saving the model file
             *
             */
            numargs = args.Length;
            if (numargs > 3 | numargs < 2) { usage = true; return; }

            
            arg1 = args[0]; // Training file
            arg2 = args[1]; // Labels file
            if (numargs == 3)
                arg3 = args[2];

            // Perform some file checking                        
            
            if (!externalFunc.checkFile(arg1))
            {
                Console.WriteLine("Error opening file{0}", arg1);
                FileOpenError = true;

            }
            if (!externalFunc.checkFile(arg2))
            {
                Console.WriteLine("Error opening file {0}", arg2);
                FileOpenError = true;
            }
            
        }

    }
}

    