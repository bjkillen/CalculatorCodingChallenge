using CalculatorCodingChallenge.Models.Calculator;

namespace CalculatorCodingChallengeTests;

public class AddCaclulatorTest
{
    [Fact]
    public void OneValueReturn20()
    {
        int expected = 20;

        int[] input = new int[] { 20 };

        int actual = new AddCalculator().Calculate(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NoValuesReturn0()
    {
        int expected = 0;

        int[] input = new int[] { 0 };
        int actual = new AddCalculator().Calculate(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TwoPositiveValuesReturn5001()
    {
        int expected = 5001;

        int[] input = new int[] { 5000, 1 };
        int actual = new AddCalculator().Calculate(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PositiveAndNegativeValueReturn1()
    {
        int expected = 1;

        int[] input = new int[] { 4, -3 };
        int actual = new AddCalculator().Calculate(input);

        Assert.Equal(expected, actual);
    }
}
