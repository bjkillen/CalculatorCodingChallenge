using System;

using Ninject.Modules;

using CalculatorCodingChallenge.Models.Calculator;

namespace CalculatorCodingChallenge.Models
{
    public class DIBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ICalculator>().To<AddCalculator>().Named("a");
            Bind<ICalculator>().To<SubtractionCalculator>().Named("s");
            Bind<ICalculator>().To<MultiplicationCalculator>().Named("m");
            Bind<ICalculator>().To<DivisionCalculator>().Named("d");
            Bind<ICalculator>().To<AddCalculator>();

            Bind<ICommandLineArgParser>().To<CommandLineArgParser>();
            Bind<IStringInputParser>().To<StringInputParser>();
        }
    }
}

