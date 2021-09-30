using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNet
{
    public interface IActivator
    {
        double Activate(double value);
        double ActivateDer(double value);
    }
}
