using System;
using CalculatorCodingChallenge.Controllers;

namespace CalculatorCodingChallenge.Models.Calculator
{
    public interface ICalculator
    {
        ComputationResult Calculate(int[] nums);
    }

    public abstract class Calculator: ICalculator
    {
        public Calculator()
        {
        }

        public abstract ComputationResult Calculate(int[] nums);
    }
}