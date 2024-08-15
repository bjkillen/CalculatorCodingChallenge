using System;

using CalculatorCodingChallenge.Controllers;
using CalculatorCodingChallenge.Extensions;

namespace CalculatorCodingChallenge.Models.Calculator
{
    public class SubtractionCalculator : Calculator
    {
        public SubtractionCalculator()
        {
        }

        public override ComputationResult Calculate(int[] nums)
        {
            int currentResult = 0;
            string formulaBeforeEquals = "";

            // Choosing to create formula in same loop as computing sum to
            // save an extra for loop with method like Join
            nums.forEach((num, idx) =>
            {
                if (idx == 0)
                {
                    currentResult = num;
                    formulaBeforeEquals += num.ToString();
                }
                else
                {
                    currentResult -= num;
                    formulaBeforeEquals += $"-{num}";
                }
            });

            return new ComputationResult(currentResult, formulaBeforeEquals);
        }
    }
}

