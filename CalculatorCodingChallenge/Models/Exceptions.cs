using System;

namespace CalculatorCodingChallenge.Exceptions
{
    public class NoNegativeNumbersException : Exception
    {
        public NoNegativeNumbersException(int[] numbers)
            : base($"Please provide only positive numbers. " +
                  $"These numbers are in violation: {
                      string.Join(",", numbers.Select(item => item.ToString()))
                  }")
        {
        }
    }
}

