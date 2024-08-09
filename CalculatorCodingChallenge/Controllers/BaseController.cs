using System;

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
            string[] inputTextSplitOnceBySpace = inputText.SplitOnce(" ");

            string? inputTextWithoutArgs = inputTextSplitOnceBySpace.TryGetElement(0);

            string? potentialArgs = inputTextSplitOnceBySpace.TryGetElement(1);

            CommandLineArgsResult parsedCommandLineArgs = CommandLineArgParser.ParseArgs(potentialArgs);

            StringInputParser inputParser = new(parsedCommandLineArgs);

            int[] parsedInputText = inputParser.ParseInput(inputTextWithoutArgs);

            AddCalculator calculator = new();

            ComputationResult result = calculator.Calculate(parsedInputText);

            return result;
        }
    }
}

