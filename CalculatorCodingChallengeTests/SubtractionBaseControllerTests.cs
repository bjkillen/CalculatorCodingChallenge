using System;

using Ninject;

using CalculatorCodingChallenge.Controllers;
using CalculatorCodingChallenge.Models;
using CalculatorCodingChallenge.Models.Calculator;

namespace CalculatorCodingChallengeTests;

public class SubtractionBaseControllerTests
{
    readonly ICommandLineArgParser CommandLineArgParser;
    readonly IStringInputParser StringInputParser;
    readonly ICalculator Calculator;

    public SubtractionBaseControllerTests()
    {
        StandardKernel kernel = KernelSingleton.Instance.kernel;

        CommandLineArgParser = kernel.Get<ICommandLineArgParser>();
        StringInputParser = kernel.Get<IStringInputParser>();
        Calculator = CalculatorFactory.Create("s");
    }

    private BaseController CreateDefaultBaseController()
    {
        return new(CommandLineArgParser, StringInputParser, Calculator);
    }

    [Fact]
    public void MultipleValuesReturnNegative76()
    {
        ComputationResult expected = new(
            -76,
            "1-2-3-4-5-6-7-8-9-10-11-12"
        );

        string input = "1,2,3,4,5,6,7,8,9,10,11,12";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MultipleValuesHandlesNegativesReturnNegative52()
    {
        ComputationResult expected = new(
            -52,
            "1-2-3-4-5-6-7-8-9-10-11--12"
        );

        string input = "1,2,3,4,5,6,7,8,9,10,11,-12 --allowNegatives";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }
}
