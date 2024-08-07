using CalculatorCodingChallenge.Controllers;
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
    public void MultipleValuesOneGreaterThan1000Return8()
    {
        int expected = 8;
        string input = "2,1001,6";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MultipleValuesOneEqual1000Return1008()
    {
        int expected = 1008;
        string input = "2,1000,6";

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

    [Fact]
    public void SupportsCustomOneCharDelimiterReturns7()
    {
        int expected = 7;
        string input = "//#\n2#5";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SupportsCustomOneCharDelimiterReturns102()
    {
        int expected = 102;
        string input = "//,\n2,ff,100";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesMixedDelimitersReturns20()
    {
        int expected = 20;
        string input = "//#\n2#5,10,3";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesInvalidDelimiterReturns13()
    {
        int expected = 13;
        string input = "/#\n2#5,10,3";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InvalidatesEmptyDelimiterReturns13()
    {
        int expected = 13;
        string input = "//\n2#5,10,3";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InvalidatesMultiCharacterDelimiterReturns13()
    {
        int expected = 13;
        string input = "//##\n2#5,10,3";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesMultiCharacterBracketedDelimiterReturns66()
    {
        int expected = 66;
        string input = "//[***]\n11***22***33";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InvalidatesEmptyBracketedDelimiterReturns0()
    {
        int expected = 0;
        string input = "//[]\n11***22***33";

        int actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }
}
