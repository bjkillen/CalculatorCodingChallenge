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

    public class BaseController
    {
        public BaseController(
            ICommandLineArgParser commandLineArgParser,
            IStringInputParser stringInputParser,
            ICalculator calculator)
        {
            CommandLineArgParser = commandLineArgParser;
            StringInputParser = stringInputParser;
            Calculator = calculator;
        }

        private ICommandLineArgParser CommandLineArgParser { get; set; }
        private IStringInputParser StringInputParser { get; set; }
        private ICalculator Calculator{ get; set; }

        public ComputationResult Compute(string? inputText)
        {
            string[] inputTextSplitOnceBySpace = inputText.SplitOnce(" ");

            string? inputTextWithoutArgs = inputTextSplitOnceBySpace.TryGetElement(0);

            string? potentialArgs = inputTextSplitOnceBySpace.TryGetElement(1);

            CommandLineArgsResult parsedCommandLineArgs = CommandLineArgParser.ParseArgs(potentialArgs);

            int[] parsedInputText = StringInputParser.ParseInput(inputTextWithoutArgs, parsedCommandLineArgs);

            ComputationResult result = Calculator.Calculate(parsedInputText);

            return result;
        }
    }
}

