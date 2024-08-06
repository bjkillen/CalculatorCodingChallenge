using CalculatorCodingChallenge.Controllers;
using CalculatorCodingChallenge.Models.Exceptions;

namespace CalculatorCodingChallengeTests;

public class BaseControllerTests
{
    [Fact]
    public void OneValueReturn20()
    {
        int expected = 20;
        string input = "20";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NoValuesReturn0()
    {
        int expected = 0;
        string input = "";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void OnlyCommaReturn0()
    {
        int expected = 0;
        string input = ",";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TwoPositiveValuesReturn5001()
    {
        int expected = 5001;
        string input = "5000,1";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PositiveAndNegativeValueReturn1()
    {
        int expected = 1;
        string input = "4,-3";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InvalidInputConvertsTo0Return5()
    {
        int expected = 5;
        string input = "5,tytyt";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MoreThanTwoNumbersThrowsTooManyNumbersProvidedException()
    {
        string input = "1,2,3";

        void act() => BaseController.Compute(input);

        Assert.Throws<TooManyNumbersProvidedException>(act);
    }
}
