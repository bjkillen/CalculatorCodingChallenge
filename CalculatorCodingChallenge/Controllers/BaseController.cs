using System;

using CalculatorCodingChallenge.Models;
using CalculatorCodingChallenge.Models.Calculator;

namespace CalculatorCodingChallenge.Controllers
{
    public static class BaseController
    {
        static BaseController()
        {
        }

        public static int Compute(string? inputText)
        {
            int[] parsedInputText = StringInputParser.ParseInput(inputText);

            AddCalculator calculator = new();

            int result = calculator.Calculate(parsedInputText);

            return result;
        }
    }
}

