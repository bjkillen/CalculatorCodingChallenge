using System;
using Ninject.Modules;

using CalculatorCodingChallenge.Models.Calculator;

namespace CalculatorCodingChallenge.Models
{
    public class DIBindings : NinjectModule
    {
        public DIBindings()
        {
        }

        public override void Load()
        {
            Bind<ICalculator>().To<AddCalculator>();
            Bind<ICommandLineArgParser>().To<CommandLineArgParser>();
            Bind<IStringInputParser>().To<StringInputParser>();
        }
    }
}

