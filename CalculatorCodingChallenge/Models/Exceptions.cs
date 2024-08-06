using System;

namespace CalculatorCodingChallenge.Models.Exceptions
{
    public class TooManyNumbersProvidedException : Exception
    {
        public TooManyNumbersProvidedException(int maxNums)
            : base($"Too many numbers provided. Please provide at most {maxNums} numbers for us to calculate.")
        {
        }
    }
}
