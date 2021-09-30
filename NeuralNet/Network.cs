using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace NeuralNet
{
    [Serializable]
    public class Network
    {
        public Layer[] Layers { get; set; }
        public double[][] Neurons { get; set; }
        public double[][] Biases { get; set; }
        public double[][][] Weights { get; set; }

        public double Cost = 0;

        private readonly Random _r = new Random();
        private readonly double _learningRate = 0.0001d;

        public Network(Layer[] layers)
        {
            Layers = new Layer[layers.Length];
            Array.Copy(layers, Layers, layers.Length);
            InitNeurons();
            InitBiases();
            InitWeights();
        }

        public Network() { }

        private void InitNeurons()
        {
            List<double[]> neuronsList = new List<double[]>();
            for (int i = 0; i < Layers.Length; i++)
            {
                neuronsList.Add(new double[Layers[i].NodeCount]);
            }
            Neurons = neuronsList.ToArray();
        }

        private void InitBiases()
        {
            List<double[]> biasList = new List<double[]>();
            for (int i = 1; i < Layers.Length; i++)
            {
                double[] bias = new double[Layers[i].NodeCount];
                for (int j = 0; j < Layers[i].NodeCount; j++)
                {
                    bias[j] = _r.Next(-50, 50) / 100d;
                }
                biasList.Add(bias);
            }
            Biases = biasList.ToArray();
        }

        private void InitWeights()
        {
            List<double[][]> weightsList = new List<double[][]>();
            for (int i = 1; i < Layers.Length; i++)
            {
                List<double[]> layerWeightsList = new List<double[]>();
                int neuronsInPreviousLayer = Layers[i - 1].NodeCount;
                for (int j = 0; j < Layers[i].NodeCount; j++)
                {
                    double[] neuronWeights = new double[neuronsInPreviousLayer];
                    for (int k = 0; k < neuronsInPreviousLayer; k++)
                    {
                        neuronWeights[k] = _r.Next(-50, 50) / 100d;
                    }
                    layerWeightsList.Add(neuronWeights);
                }
                weightsList.Add(layerWeightsList.ToArray());
            }
            Weights = weightsList.ToArray();
        }

        public double[] FeedForward(double[] inputs)
        {
            //populate input layer
            for (int i = 0; i < inputs.Length; i++)
            {
                Neurons[0][i] = inputs[i];
            }

            //move values through the network
            for (int i = 1; i < Layers.Length; i++)
            {
                int preLayer = i - 1;
                for (int j = 0; j < Layers[i].NodeCount; j++)
                {
                    double value = 0d;
                    for (int k = 0; k < Layers[preLayer].NodeCount; k++)
                    {
                        value += Weights[preLayer][j][k] * Neurons[preLayer][k];
                    }
                    Neurons[i][j] = Layers[preLayer].Activate(value + Biases[preLayer][j]);
                }
            }
            return Neurons[Layers.Length - 1];
        }

        public void BackPropagate(double[] inputs, double[] expected)
        {
            //run feed forward to ensure neurons are populated correctly
            double[] output = FeedForward(inputs);

            //Cost is not used in calculions, its used to identify the performance of the network
            Cost = 0;
            for (int i = 0; i < output.Length; i++) 
                Cost += Math.Pow(output[i] - expected[i], 2);


            //gamma initialization
            //gamma is the value used to update the network and bring it inline with expected output.
            double[][] gamma;
            List<double[]> gammaList = new List<double[]>();
            for (int i = 0; i < Layers.Length; i++)
            {
                gammaList.Add(new double[Layers[i].NodeCount]);
            }
            gamma = gammaList.ToArray();


            int lastHiddenLayerIndex = Layers.Length - 2; //last hidden layer
            int outputLayerIndex = Layers.Length - 1; //last node (output node)

            //Loop of 1 since I only have 1 output
            for (int i = 0; i < output.Length; i++)
                gamma[outputLayerIndex][i] = (output[i] - expected[i]) * Layers[lastHiddenLayerIndex].ActivateDer(output[i]);//Gamma calculation

            //calculates the w' and b' for the last hidden layer in the network
            for (int i = 0; i < Layers[^1].NodeCount; i++)
            {
                Biases[lastHiddenLayerIndex][i] -= gamma[outputLayerIndex][i] * _learningRate;
                for (int j = 0; j < Layers[lastHiddenLayerIndex].NodeCount; j++)
                {
                    Weights[lastHiddenLayerIndex][i][j] -= gamma[outputLayerIndex][i] * Neurons[lastHiddenLayerIndex][j] * _learningRate;//*learning 
                }
            }


            //update w' and b' for hidden layers, from back to front
            for (int i = lastHiddenLayerIndex; i > 0; i--)
            {
                int preLayer = i - 1;
                //calculate gamma for layer
                for (int j = 0; j < Layers[i].NodeCount; j++)
                {
                    gamma[i][j] = 0;
                    for (int k = 0; k < gamma[i + 1].Length; k++)
                    {
                        gamma[i][j] = gamma[i + 1][k] * Weights[i][k][j];
                    }

                    gamma[i][j] *= Layers[preLayer].ActivateDer(Neurons[i][j]);
                }

                //apply gamma changes to w' and b' for layer.
                for (int j = 0; j < Layers[i].NodeCount; j++)
                {
                    //learning rate determined how large each update is.
                    Biases[preLayer][j] -= gamma[i][j] * _learningRate;
                    for (int k = 0; k < Layers[preLayer].NodeCount; k++)
                    {
                        Weights[preLayer][j][k] -= gamma[i][j] * Neurons[preLayer][k] * _learningRate;
                    }
                }
            }
        }

        public static void SaveNetwork(Network network)
        {
            File.WriteAllText("Network.json", JsonSerializer.Serialize(network));
        }

        public static Network LoadNetwork()
        {
            try
            {
                string networkString = File.ReadAllText("Network.json");
                return JsonSerializer.Deserialize<Network>(networkString);
            }
            catch(IOException)
            {
                return null;
            }
            catch(JsonException)
            {
                return null;
            }
        }
    }
}
