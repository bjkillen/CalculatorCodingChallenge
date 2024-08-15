using System;

using CalculatorCodingChallenge.Controllers;
using CalculatorCodingChallenge.Extensions;

namespace CalculatorCodingChallenge.Models.Calculator
{
    public class AddCalculator : Calculator
    {
        public AddCalculator()
        {
        }

        public override ComputationResult Calculate(int[] nums)
        {
            int currentSum = 0;
            string formulaBeforeEquals = "";

            // Choosing to create formula in same loop as computing sum to
            // save an extra for loop with method like Join
            nums.forEach((num, idx) =>
            {
                currentSum += num;

                if (idx == 0)
                {
                    formulaBeforeEquals += num.ToString();
                }
                else
                {
                    formulaBeforeEquals += $"+{num}";
                }
            });

            return new ComputationResult(currentSum, formulaBeforeEquals);
        }
    }
}

