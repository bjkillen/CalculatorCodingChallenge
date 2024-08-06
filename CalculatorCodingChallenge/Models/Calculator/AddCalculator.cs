using System;

namespace CalculatorCodingChallenge.Models.Calculator
{
    public class AddCalculator : Calculator
    {
        public AddCalculator()
        {
        }

        public override int Calculate(int[] nums)
        {
            return nums.Sum();
        }
    }
}

