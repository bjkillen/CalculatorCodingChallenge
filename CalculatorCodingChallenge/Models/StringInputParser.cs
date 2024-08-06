using System;

using CalculatorCodingChallenge.Exceptions;
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

            // TODO: Refactor this block to make one pass looking at each char,
            // parsing previous chars when delimeter found and performing
            // negative check in the same loop pass
            //
            // This will improve performance in the case requirements change
            int[] numbers = text.Split(separators)
                            .Select(numText => numText.Trim().TryParseWithLimit())
                            .ToArray();

            int[] negativeNumbers = numbers.Where(n => n < 0).ToArray();

            if (negativeNumbers.Length > 0)
            {
                throw new NoNegativeNumbersException(negativeNumbers);
            }

            return numbers;
        }
    }
}

