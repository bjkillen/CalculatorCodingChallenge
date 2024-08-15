using System;
using Ninject;

namespace CalculatorCodingChallenge.Models.Calculator
{
    public static class CalculatorFactory
    {
        public static ICalculator Create(string? type)
        {
            StandardKernel kernel = KernelSingleton.Instance.kernel;

            return kernel.Get<ICalculator>(type);
        }
    }
}
