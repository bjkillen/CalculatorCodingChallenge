using System;
using CalculatorCodingChallenge.Controllers;

namespace CalculatorCodingChallenge.Models.Calculator
{
    public abstract class Calculator
    {
        public Calculator()
        {
        }

        public abstract ComputationResult Calculate(int[] nums);
    }
}