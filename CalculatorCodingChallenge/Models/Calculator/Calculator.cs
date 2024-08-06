using System;
namespace CalculatorCodingChallenge.Models.Calculator
{
    public abstract class Calculator
    {
        public Calculator()
        {
        }

        public abstract int Calculate(int[] nums);
    }
}