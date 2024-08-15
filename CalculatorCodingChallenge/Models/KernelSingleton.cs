using System;
using System.Reflection;

using Ninject;

namespace CalculatorCodingChallenge.Models
{
    public class KernelSingleton
    {
        private static KernelSingleton? instance = null;
        private static readonly object padlock = new();

        public readonly StandardKernel kernel = new();

        private KernelSingleton()
        {
            kernel.Load(Assembly.GetExecutingAssembly());
        }

        public static KernelSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    return instance ??= new KernelSingleton();
                }
            }
        }
    }
}
