using System;
using System.Reflection;

using Ninject;
using Ninject.Parameters;

using CalculatorCodingChallenge.Extensions;
using CalculatorCodingChallenge.Models;
using CalculatorCodingChallenge.Models.Calculator;

namespace CalculatorCodingChallenge.Controllers
{
    public readonly struct ComputationResult
    {
        public ComputationResult(int result, string formulaBeforeEquals)
        {
            Result = result;
            FormulaBeforeEquals = formulaBeforeEquals;
        }

        public int Result { get; }
        public string FormulaBeforeEquals { get; }

        public string FullFormula {
            get { return $"{FormulaBeforeEquals} = {Result}"; }
        }
    }

    public static class BaseController
    {
        static BaseController()
        {
        }

        public static ComputationResult Compute(string? inputText)
        {
            StandardKernel kernel = new();
            kernel.Load(Assembly.GetExecutingAssembly());

            string[] inputTextSplitOnceBySpace = inputText.SplitOnce(" ");

            string? inputTextWithoutArgs = inputTextSplitOnceBySpace.TryGetElement(0);

            string? potentialArgs = inputTextSplitOnceBySpace.TryGetElement(1);

            ICommandLineArgParser commandLineArgParser = kernel.Get<ICommandLineArgParser>();
            CommandLineArgsResult parsedCommandLineArgs = commandLineArgParser.ParseArgs(potentialArgs);

            IStringInputParser inputParser = kernel.Get<IStringInputParser>(
                new ConstructorArgument("args", parsedCommandLineArgs));

            int[] parsedInputText = inputParser.ParseInput(inputTextWithoutArgs);

            ICalculator calculator = kernel.Get<ICalculator>();
            ComputationResult result = calculator.Calculate(parsedInputText);

            return result;
        }
    }
}

