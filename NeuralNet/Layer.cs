using System;

namespace NeuralNet
{
    public enum Activation : byte
    {
        Sigmoid = 0,
        TanH = 1,
        Relu = 2,
        LeakyRelu = 3
    }

    [Serializable]
    public struct Layer
    {
        public readonly int NodeCount { get; }
        public readonly double Dropout { get; }
        public readonly Activation Activation { get; }

        private readonly IActivator _activator;

        public Layer(int nodeCount, Activation activation, float dropout = 0)
        {
            if (dropout < 0 || dropout > 1)
                throw new ArgumentOutOfRangeException(nameof(dropout), dropout, "Dropout should have a value from 0 to 1");

            NodeCount = nodeCount;
            Dropout = dropout;
            Activation = activation;
            _activator = activation switch
            {
                Activation.Sigmoid => new Sigmoid(),
                Activation.TanH => new TanH(),
                Activation.Relu => new Relu(),
                Activation.LeakyRelu => new LeakyRelu(),
                _ => throw new NotSupportedException($"No implementation registered for activation {Enum.GetName(typeof(Activation), activation)}.")
            };
        }

        public double Activate(double value) => _activator.Activate(value);
        public double ActivateDer(double value) => _activator.ActivateDer(value);
    }
}
