Deep Belief Training Routine (Accord.net)
======
**DeepBe** Deep Belief Training routine. Required Accord.net: Math, IO and Statistical Routines, saves a 
trained Deep Belief model to disk.

LRTrain:
Loads a training file specified from the CL
Loads a label file specified from the CL
Need to programatically specify the number of layers, number of outputs and inputs are calculated
from the training files 

Input files are required to be in csv format, conversion to the appropriate format is done internally to the program.
The programs then proceeds to build a deep belife network, intializes (randomly) the weights and train using contrastive
divergence learning.

  
## Accord Routines

DeepBeliefNetwork
GaussianWeights
ContrastiveDivergenceLearning 
               

                
## Contributors
Bob Hildreth

### Third party libraries
* Uses Accord.Net 
* http://accord-framework.net/

## Version 
* Version 0.1

## Contact
#### Bob/HAL/R
* Homepage: 
* e-mail: Bobh@thehildreths.com

