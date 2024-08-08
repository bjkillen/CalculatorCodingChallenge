using CalculatorCodingChallenge.Controllers;
using CalculatorCodingChallenge.Exceptions;

namespace CalculatorCodingChallengeTests;

public class BaseControllerTests
{
    [Fact]
    public void OneValueReturn20()
    {
        ComputationResult expected = new (
            20,
            "20"
        );

        string input = "20";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NoValuesReturn0()
    {
        ComputationResult expected = new (
            0,
            "0"
        );

        string input = "";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void OnlyCommaReturn0()
    {
        ComputationResult expected = new (
            0,
            "0+0"
        );

        string input = ",";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MultipleValuesOneGreaterThan1000Return8()
    {
        ComputationResult expected = new(
            8,
            "2+0+6"
        );

        string input = "2,1001,6";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MultipleValuesOneEqual1000Return1008()
    {
        ComputationResult expected = new (
            1008,
            "2+1000+6"
        );

        string input = "2,1000,6";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PositiveAndNegativeValuesThrowsNoNegativeNumbersException()
    {
        string input = "4,-3,0,-10";

        void act() => BaseController.Compute(input);

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
        ComputationResult expected = new(
            5,
            "5+0"
        );

        string input = "5,tytyt";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MultipleValuesReturn78()
    {
        ComputationResult expected = new(
            78,
            "1+2+3+4+5+6+7+8+9+10+11+12"
        );

        string input = "1,2,3,4,5,6,7,8,9,10,11,12";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MultipleValuesWithMixedInvalidAndEmptyNumbersReturns12()
    {
        ComputationResult expected = new(
            12,
            "2+0+4+0+0+6"
        );

        string input = "2,,4,rrrr,1001,6";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SupportsSplitByNewline()
    {
        ComputationResult expected = new(
            6,
            "1+2+3"
        );

        string input = "1\n2\n3";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SupportsSplitByNewlineAndCommaMixed()
    {
        ComputationResult expected = new(
            6,
            "1+2+3"
        );

        string input = "1\n2,3";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SupportsCustomOneCharDelimiterReturns7()
    {
        ComputationResult expected = new(
            7,
            "2+5"
        );

        string input = "//#\n2#5";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SupportsCustomOneCharDelimiterReturns102()
    {
        ComputationResult expected = new(
            102,
            "2+0+100"
        );

        string input = "//,\n2,ff,100";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesMixedDelimitersReturns20()
    {
        ComputationResult expected = new(
            20,
            "2+5+10+3"
        );

        string input = "//#\n2#5,10,3";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesInvalidDelimiterReturns13()
    {
        ComputationResult expected = new(
            13,
            "0+0+10+3"
        );

        string input = "/#\n2#5,10,3";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InvalidatesEmptyDelimiterReturns13()
    {
        ComputationResult expected = new(
            13,
            "0+0+10+3"
        );

        string input = "//\n2#5,10,3";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InvalidatesMultiCharacterDelimiterReturns13()
    {
        ComputationResult expected = new(
            13,
            "0+0+10+3"
        );

        string input = "//##\n2#5,10,3";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesMultiCharacterBracketedDelimiterReturns66()
    {
        ComputationResult expected = new(
            66,
            "11+22+33"
        );

        string input = "//[***]\n11***22***33";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InvalidatesEmptyBracketedDelimiterReturns0()
    {
        ComputationResult expected =new(
            0,
            "0+0"
        );

        string input = "//[]\n11***22***33";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesMultiCharacterMultiBracketedDelimiterReturns110()
    {
        ComputationResult expected = new(
            110,
            "11+22+0+33+44"
        );

        string input = "//[*][!!][r9r]\n11r9r22*hh*33!!44";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesMultiCharacterMultiBracketedAndInvalidatesEmptyBracketedDelimiterReturns110()
    {
        ComputationResult expected = new(
            110,
            "11+22+0+33+44"
        );

        string input = "//[*][!!][][r9r]\n11r9r22*hh*33!!44";

        ComputationResult actual = BaseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    // This test simulates a user entering multiple inputs before using Ctrl+C to exit
    // The purpose of the test is to catch any mishandling of state
    // and/ or class instantiation when called multiple times
    [Fact]
    public void HandlesMultipleInputsEnteredSequentially()
    {
        MultipleValuesReturn78();
        HandlesMultiCharacterBracketedDelimiterReturns66();
        InvalidatesEmptyBracketedDelimiterReturns0();
    }
}
