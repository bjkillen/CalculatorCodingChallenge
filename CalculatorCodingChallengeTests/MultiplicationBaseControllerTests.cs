using System;
using Ninject;

using CalculatorCodingChallenge.Controllers;
using CalculatorCodingChallenge.Models;
using CalculatorCodingChallenge.Models.Calculator;

namespace CalculatorCodingChallengeTests;

public class MultiplicationBaseControllerTests
{
    readonly ICommandLineArgParser CommandLineArgParser;
    readonly IStringInputParser StringInputParser;
    readonly ICalculator Calculator;

    public MultiplicationBaseControllerTests()
    {
        StandardKernel kernel = KernelSingleton.Instance.kernel;

        CommandLineArgParser = kernel.Get<ICommandLineArgParser>();
        StringInputParser = kernel.Get<IStringInputParser>();
        Calculator = CalculatorFactory.Create("m");
    }

    private BaseController CreateDefaultBaseController()
    {
        return new(CommandLineArgParser, StringInputParser, Calculator);
    }

    [Fact]
    public void MultipleValuesReturn24()
    {
        ComputationResult expected = new(
            24,
            "1*2*3*4"
        );

        string input = "1,2,3,4";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MultipleValuesHandlesNegativesReturnNegative24()
    {
        ComputationResult expected = new(
            -24,
            "1*2*3*-4"
        );

        string input = "1,2,3,-4 --allowNegatives";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MultipleValuesHandlesZeroReturn0()
    {
        ComputationResult expected = new(
            0,
            "1*2*3*0"
        );

        string input = "1,2,3,0";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }
}
