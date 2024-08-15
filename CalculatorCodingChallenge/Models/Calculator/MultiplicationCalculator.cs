using System;

using CalculatorCodingChallenge.Controllers;
using CalculatorCodingChallenge.Extensions;

namespace CalculatorCodingChallenge.Models.Calculator
{
    public class MultiplicationCalculator : Calculator
    {
        public MultiplicationCalculator()
        {
        }

        public override ComputationResult Calculate(int[] nums)
        {
            int currentResult = 1;
            string formulaBeforeEquals = "";

            // Choosing to create formula in same loop as computing sum to
            // save an extra for loop with method like Join
            nums.forEach((num, idx) =>
            {
                currentResult *= num;

                if (idx == 0)
                {
                    formulaBeforeEquals += num.ToString();
                }
                else
                {
                    formulaBeforeEquals += $"*{num}";
                }
            });

            return new ComputationResult(currentResult, formulaBeforeEquals);
        }
    }
}

