﻿using Ninject;
using Moq;

using CalculatorCodingChallenge.Controllers;
using CalculatorCodingChallenge.Exceptions;
using CalculatorCodingChallenge.Models;
using CalculatorCodingChallenge.Models.Calculator;

namespace CalculatorCodingChallengeTests;

public class BaseControllerTests
{
    readonly ICommandLineArgParser CommandLineArgParser;
    readonly IStringInputParser StringInputParser;
    readonly ICalculator Calculator;

    public BaseControllerTests()
    {
        StandardKernel kernel = new();
        kernel.Load(new DIBindings());

        CommandLineArgParser = kernel.Get<ICommandLineArgParser>();
        StringInputParser = kernel.Get<IStringInputParser>();
        Calculator = kernel.Get<ICalculator>();
    }

    private BaseController CreateDefaultBaseController()
    {
        return new(CommandLineArgParser, StringInputParser, Calculator);
    }

    [Fact]
    public void OneValueReturn20()
    {
        ComputationResult expected = new (
            20,
            "20"
        );

        string input = "20";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PositiveAndNegativeValuesThrowsNoNegativeNumbersException()
    {
        string input = "4,-3,0,-10";

        BaseController baseController = CreateDefaultBaseController();
        void act() => baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

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

    [Fact]
    public void HandlesAlternateDelimiterFlagReturns1008()
    {
        ComputationResult expected = new(
            1008,
            "2+1000+6"
        );

        string input = "2&1000&6 -ad=&";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesValueUpperBoundFlagReturns1009()
    {
        ComputationResult expected = new(
            1009,
            "2+1001+6"
        );

        string input = "2,1001,6 -ub=2000";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesAllowNegativesFlagReturns1()
    {
        ComputationResult expected = new(
            1,
            "4+-3"
        );

        string input = "4,-3 --allowNegatives";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandlesAllFlagsReturns1002()
    {
        ComputationResult expected = new(
            1002,
            "4+-3+1001"
        );

        string input = "4,-3&1001 -ad=& --allowNegatives -ub=2000";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ChoosesFirstValidDelimiterFlagReturns2009()
    {
        ComputationResult expected = new(
            2009,
            "3+2000+0+0+6"
        );

        string input = "3,2000,2001,4&5%6 -ub=2000 -ub=3000 -ad=% -ad=&";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InvalidatesAlternateDelimiterFlagReturns316()
    {
        ComputationResult expected = new(
            316,
            "4+312"
        );

        string input = "4,312 -ad=a1";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InvalidatesAllowNegativesFlagThrowsNoNegativeNumbersException()
    {
        string input = "4,1001,-3 --allowNegativesa";

        BaseController baseController = CreateDefaultBaseController();
        void act() => baseController.Compute(input);

        var ex = Assert.Throws<NoNegativeNumbersException>(act);

        int[] negativeNumbers = new int[] { -3 };
        string expectedExceptionMessage =
            new NoNegativeNumbersException(negativeNumbers).Message;

        Assert.Equal(
            expectedExceptionMessage,
            ex.Message
        );
    }

    [Fact]
    public void InvalidatesUpperBoundFlagReturns4()
    {
        ComputationResult expected = new(
            4,
            "4+0"
        );

        string input = "4,1001 -ub=2000a";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ComputeCallsParseArgsAndParseInputAndCalculateOnce()
    {
        // Arrange
        string input = "1,1001,3 -ub=1001 --allowNegatives";

        Mock<ICommandLineArgParser> mockCommandLineArgParser = new();
        Mock<IStringInputParser> mockStringInputParser = new();
        Mock<ICalculator> mockCalculator = new();

        BaseController mockBaseController = new (
            mockCommandLineArgParser.Object,
            mockStringInputParser.Object,
            mockCalculator.Object
        );

        string expectedArgsText = "-ub=1001 --allowNegatives";

        string expectedCalculationText = "1,1001,3";
        CommandLineArgsResult expectedParsedArgs = new(null, true, 1001);

        int[] parsedInputNums = new[] { 1, 1001, 3 };

        // Act
        _ = mockBaseController.Compute(input);

        // Assert
        mockCommandLineArgParser.Verify(p => p.ParseArgs(expectedArgsText), Times.Exactly(1));
        mockStringInputParser.Verify(p => p.ParseInput(expectedCalculationText, expectedParsedArgs), Times.Exactly(1));
        mockCalculator.Verify(c => c.Calculate(parsedInputNums), Times.Exactly(1));
    }
}
