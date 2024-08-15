using System;
using Ninject;

using CalculatorCodingChallenge.Extensions;

namespace CalculatorCodingChallenge.Models.Calculator
{
    public static class CalculatorFactory
    {
        public static ICalculator Create(string? type = null)
        {
            StandardKernel kernel = KernelSingleton.Instance.kernel;

            return kernel.GetNamedOrDefault<ICalculator>(type);
        }
    }
}
