using Accord.Neuro.Networks;
using Accord.Neuro;
using Accord.Neuro.Learning;

namespace RBM
{
    public class deepBelief
    {
        public DeepBeliefNetwork DBNetwork;
        public DeepBeliefNetworkLearning Teacher;
        public int Epochs;
        public deepBelief (in int NumInputs, in int hidden, in int outputs, in double[][] inputs,
            in double[][] labels)
        {
            /*
             * Deep belief: mulitple layers of hidden units connected as a bipartite graph. The
             * hidden units are trained on a set of examples without supervision in prepartion for
             * supevised training on the input set.
             * 
             * Each of the sub layers are trained using contrastive divergence in turn 
             */
            Epochs = 5000;

            DBNetwork = new 
                DeepBeliefNetwork(NumInputs, hidden, outputs);
            // Initialize the network with Gaussian weights
            new GaussianWeights(DBNetwork, 0.1).Randomize();

            // Update the visible layer with the new weights
            DBNetwork.UpdateVisibleWeights();


            // Setup the learning algorithm.
            DeepBeliefNetworkLearning Teacher = new DeepBeliefNetworkLearning(DBNetwork)
            {
                Algorithm = (h, v, i) => new ContrastiveDivergenceLearning(hidden: h,visible: v)
                {
                    LearningRate = 0.1,
                    Momentum = 0.5,
                    Decay = 0.001,
                }
            };

            // Unsupervised learning on each hidden layer, except for the output.
            for (int i = 0; i < DBNetwork.Layers.Length - 1; i++)
            {
                Teacher.LayerIndex = i;

                // Compute the learning data  should be used
                var layerInput = Teacher.GetLayerInput(inputs);

                // Train the layer iteratively
                for (int j = 0; j < 5000; j++)
                    Teacher.RunEpoch(layerInput);
            }

            // Supervised learning on entire network, to provide output classification.
            var backpropagation = new BackPropagationLearning(DBNetwork)
            {
                LearningRate = 0.1,
                Momentum = 0.5
            };

            // Run supervised learning.
            for (int i = 0; i < Epochs; i++)
                backpropagation.RunEpoch(inputs, labels);            
        }
        

    }
}