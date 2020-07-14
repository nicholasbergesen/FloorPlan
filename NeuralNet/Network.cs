using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeuralNet
{
    [Serializable]
    public class Network
    {
        public int[] _layers { get; set; }
        public float[][] _neurons { get; set; }
        public float[][] _biases { get; set; }
        public float[][][] _weights { get; set; }
        public int[] _activations { get; set; }

        private Random _r = new Random();

        private float learningRate = 0.01f;
        public float cost = 0;

        public Network(int[] layers, string[] layerActivations)
        {
            _layers = new int[layers.Length];
            for (int i = 0; i < layers.Length; i++)
                _layers[i] = layers[i];

            _activations = new int[layers.Length - 1];
            for (int i = 0; i < layers.Length - 1; i++)
            {
                string action = layerActivations[i];
                switch (action)
                {
                    case "sigmoid":
                        _activations[i] = 0;
                        break;
                    case "tanh":
                        _activations[i] = 1;
                        break;
                    case "relu":
                        _activations[i] = 2;
                        break;
                    case "leakyrelu":
                        _activations[i] = 3;
                        break;
                    default:
                        _activations[i] = 2;
                        break;
                }
            }
            InitNeurons();
            InitBiases();
            InitWeights();
        }

        public Network() { }

        private void InitNeurons()
        {
            List<float[]> neuronsList = new List<float[]>();
            for (int i = 0; i < _layers.Length; i++)
            {
                neuronsList.Add(new float[_layers[i]]);
            }
            _neurons = neuronsList.ToArray();
        }

        private void InitBiases()
        {
            List<float[]> biasList = new List<float[]>();
            for (int i = 1; i < _layers.Length; i++)
            {
                float[] bias = new float[_layers[i]];
                for (int j = 0; j < _layers[i]; j++)
                {
                    bias[j] = _r.Next(-50, 50) / 100f;
                }
                biasList.Add(bias);
            }
            _biases = biasList.ToArray();
        }

        private void InitWeights()
        {
            List<float[][]> weightsList = new List<float[][]>();
            for (int i = 1; i < _layers.Length; i++)
            {
                List<float[]> layerWeightsList = new List<float[]>();
                int neuronsInPreviousLayer = _layers[i - 1];
                for (int j = 0; j < _layers[i]; j++)
                {
                    float[] neuronWeights = new float[neuronsInPreviousLayer];
                    for (int k = 0; k < neuronsInPreviousLayer; k++)
                    {
                        neuronWeights[k] = _r.Next(-50, 50) / 100f;
                    }
                    layerWeightsList.Add(neuronWeights);
                }
                weightsList.Add(layerWeightsList.ToArray());
            }
            _weights = weightsList.ToArray();
        }

        public float[] FeedForward(float[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                _neurons[0][i] = inputs[i];
            }
            for (int i = 1; i < _layers.Length; i++)
            {
                int layer = i - 1;
                for (int j = 0; j < _layers[i]; j++)
                {
                    float value = 0f;
                    for (int k = 0; k < _layers[i - 1]; k++)
                    {
                        value += _weights[i - 1][j][k] * _neurons[i - 1][k];
                    }
                    _neurons[i][j] = activate(value + _biases[i - 1][j], layer);
                }
            }
            return _neurons[_layers.Length - 1];
        }

        private float activate(float value, int layer)
        {
            switch (_activations[layer])
            {
                case 0:
                    return sigmoid(value);
                case 1:
                    return tanh(value);
                case 2:
                    return relu(value);
                case 3:
                    return leakyrelu(value);
                default:
                    return relu(value);
            }
        }
        private float activateDer(float value, int layer)
        {
            switch (_activations[layer])
            {
                case 0:
                    return sigmoidDer(value);
                case 1:
                    return tanhDer(value);
                case 2:
                    return reluDer(value);
                case 3:
                    return leakyreluDer(value);
                default:
                    return reluDer(value);
            }
        }

        private float sigmoid(float x)
        {
            float k = (float)Math.Exp(x);
            return k / (1.0f + k);
        }
        private float tanh(float x)
        {
            return (float)Math.Tanh(x);
        }
        private float relu(float x)
        {
            return (0 >= x) ? 0 : x;
        }
        private float leakyrelu(float x)
        {
            return (0 >= x) ? 0.01f * x : x;
        }
        private float sigmoidDer(float x)
        {
            return x * (1 - x);
        }
        private float tanhDer(float x)
        {
            return 1 - (x * x);
        }
        private float reluDer(float x)
        {
            return (0 >= x) ? 0 : 1;
        }
        private float leakyreluDer(float x)
        {
            return (0 >= x) ? 0.01f : 1;
        }

        public void BackPropagate(float[] inputs, float[] expected)
        {
            float[] output = FeedForward(inputs);//runs feed forward to ensure neurons are populated correctly

            cost = 0;
            for (int i = 0; i < output.Length; i++) cost += (float)Math.Pow(output[i] - expected[i], 2);
            cost = cost / 2;//this value is not used in calculions, rather used to identify the performance of the network

            float[][] gamma;


            List<float[]> gammaList = new List<float[]>();
            for (int i = 0; i < _layers.Length; i++)
            {
                gammaList.Add(new float[_layers[i]]);
            }
            gamma = gammaList.ToArray();//gamma initialization

            int layer = _layers.Length - 2;
            for (int i = 0; i < output.Length; i++) gamma[_layers.Length - 1][i] = (output[i] - expected[i]) * activateDer(output[i], layer);//Gamma calculation
            for (int i = 0; i < _layers[_layers.Length - 1]; i++)//calculates the w' and b' for the last layer in the network
            {
                _biases[_layers.Length - 2][i] -= gamma[_layers.Length - 1][i] * learningRate;
                for (int j = 0; j < _layers[_layers.Length - 2]; j++)
                {

                    _weights[_layers.Length - 2][i][j] -= gamma[_layers.Length - 1][i] * _neurons[_layers.Length - 2][j] * learningRate;//*learning 
                }
            }

            for (int i = _layers.Length - 2; i > 0; i--)//runs on all hidden layers
            {
                layer = i - 1;
                for (int j = 0; j < _layers[i]; j++)//outputs
                {
                    gamma[i][j] = 0;
                    for (int k = 0; k < gamma[i + 1].Length; k++)
                    {
                        gamma[i][j] = gamma[i + 1][k] * _weights[i][k][j];
                    }
                    gamma[i][j] *= activateDer(_neurons[i][j], layer);//calculate gamma
                }
                for (int j = 0; j < _layers[i]; j++)//itterate over outputs of layer
                {
                    _biases[i - 1][j] -= gamma[i][j] * learningRate;//modify biases of network
                    for (int k = 0; k < _layers[i - 1]; k++)//itterate over inputs to layer
                    {
                        _weights[i - 1][j][k] -= gamma[i][j] * _neurons[i - 1][k] * learningRate;//modify weights of network
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
            string networkString = File.ReadAllText("Network.json");
            return JsonSerializer.Deserialize<Network>(networkString);
        }
    }
}
