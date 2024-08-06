using System;

using CalculatorCodingChallenge.Extensions;

namespace CalculatorCodingChallenge.Models
{
    public class StringInputParser
    {
        public StringInputParser()
        {
        }

        private static readonly char[] separators = new Char[] { ',', '\n' };

        public static int[] ParseInput(string? text)
        {
            if (text == null)
            {
                return new int[] { 0 };
            }

            int[] numbers = text.Split(separators)
                            .Select(numText => numText.Trim().TryParse())
                            .ToArray();

            return numbers;
        }
    }
}

