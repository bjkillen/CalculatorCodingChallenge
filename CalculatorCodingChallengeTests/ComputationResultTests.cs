using System;
using CalculatorCodingChallenge.Controllers;

namespace CalculatorCodingChallengeTests
{
	public class ComputationResultTests
	{
        [Fact]
        public void FullFormulaComputesCorrectly()
        {
            string expected = "1+2+3 = 6";

            ComputationResult input = new(
                6,
                "1+2+3"
            );

            string actual = input.FullFormula;

            Assert.Equal(expected, actual);
        }
    }
}

