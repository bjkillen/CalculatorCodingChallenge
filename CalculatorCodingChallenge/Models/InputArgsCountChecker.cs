using System;

using CalculatorCodingChallenge.Models.Exceptions;

namespace CalculatorCodingChallenge.Models
{
    public class InputArgsCountChecker
    {
        public required int MaxNumbersAllowed { get; set; }

        public void ValidateInput(int[] nums)
        {
            if (nums.Length > this.MaxNumbersAllowed)
            {
                throw new TooManyNumbersProvidedException(this.MaxNumbersAllowed);
            }
        }
    }
}

