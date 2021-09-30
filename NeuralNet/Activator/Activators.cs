using System;

namespace NeuralNet
{
    public class LeakyRelu : IActivator
    {
        public double Activate(double value) => (0 >= value) ? 0.01d * value : value;
        public double ActivateDer(double value) => (0 >= value) ? 0.01d : 1;
    }

    public class Relu : IActivator
    {
        public double Activate(double value) => (0 >= value) ? 0 : value;
        public double ActivateDer(double value) => (0 >= value) ? 0 : 1;
    }

    public class Sigmoid : IActivator
    {
        public double Activate(double value)
        {
            double exp = Math.Exp(value);
            return exp / (1.0d + exp);
        }
        public double ActivateDer(double value) => value * (1 - value);
    }

    public class TanH : IActivator
    {
        public double Activate(double value) => Math.Tanh(value);

        public double ActivateDer(double value) => 1 - (value * value);
    }
}
