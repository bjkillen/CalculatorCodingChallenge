﻿using System;

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
                currentResult -= num;

                if (idx == 0)
                {
                    formulaBeforeEquals += currentResult.ToString();
                }
                else
                {
                    formulaBeforeEquals += $"-{num}";
                }
            });

            return new ComputationResult(currentResult, formulaBeforeEquals);
        }
    }
}

