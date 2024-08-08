using System;

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
            StringInputParser inputParser = new();

            int[] parsedInputText = inputParser.ParseInput(inputText);

            AddCalculator calculator = new();

            ComputationResult result = calculator.Calculate(parsedInputText);

            return result;
        }
    }
}

