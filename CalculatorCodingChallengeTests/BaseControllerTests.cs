﻿using CalculatorCodingChallenge.Controllers;
using CalculatorCodingChallenge.Exceptions;

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
    public void PositiveAndNegativeValuesThrowsNoNegativeNumbersException()
    {
        string input = "4,-3,0,-10";

        Action act = () => BaseController.Compute(input);

        var ex = Assert.Throws<NoNegativeNumbersException>(act);

        int[] negativeNumbers = new int[] { -3, -10 };
        string expectedExceptionMessage =
            new NoNegativeNumbersException(negativeNumbers).Message;

        Assert.Equal(
            expectedExceptionMessage,
            ex.Message
        );
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
    public void MultipleValuesReturn78()
    {
        int expected = 78;
        string input = "1,2,3,4,5,6,7,8,9,10,11,12";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SupportsSplitByNewline()
    {
        int expected = 6;
        string input = "1\n2\n3";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SupportsSplitByNewlineAndCommaMixed()
    {
        int expected = 6;
        string input = "1\n2,3";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }
}
