using System;
using System.Linq;

using CalculatorCodingChallenge.Extensions;

namespace CalculatorCodingChallenge.Models
{
    public class StringInputParser
    {
        public StringInputParser()
        {
        }

        public static int[] ParseInput(string? text)
        {
            if (text == null)
            {
                return new int[] { 0 };
            }

            int[] numbers = text.Split(',')
                            .Select(numText => numText.Trim().TryParse())
                            .ToArray();

            return numbers;
        }
    }
}

