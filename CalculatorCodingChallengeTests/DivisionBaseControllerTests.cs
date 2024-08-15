using System;
using CalculatorCodingChallenge.Controllers;
using CalculatorCodingChallenge.Exceptions;
using CalculatorCodingChallenge.Models;
using CalculatorCodingChallenge.Models.Calculator;
using Ninject;

namespace CalculatorCodingChallengeTests;

public class DivisionBaseControllerTests
{
    readonly ICommandLineArgParser CommandLineArgParser;
    readonly IStringInputParser StringInputParser;
    readonly ICalculator Calculator;

    public DivisionBaseControllerTests()
    {
        StandardKernel kernel = KernelSingleton.Instance.kernel;

        CommandLineArgParser = kernel.Get<ICommandLineArgParser>();
        StringInputParser = kernel.Get<IStringInputParser>();
        Calculator = CalculatorFactory.Create("d");
    }

    private BaseController CreateDefaultBaseController()
    {
        return new(CommandLineArgParser, StringInputParser, Calculator);
    }

    [Fact]
    public void MultipleValuesReturn20()
    {
        ComputationResult expected = new(
            20,
            "120/3/2"
        );

        string input = "120,3,2";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MultipleValuesHandlesNegativesReturnNegative20()
    {
        ComputationResult expected = new(
            -20,
            "120/3/-2"
        );

        string input = "120,3,-2 --allowNegatives";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MultipleValuesDividesBelowZeroReturn0()
    {
        ComputationResult expected = new(
            0,
            "1/2/3"
        );

        string input = "1,2,3";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MultipleValuesFirstValueZeroReturn0()
    {
        ComputationResult expected = new(
            0,
            "0/1/2/3"
        );

        string input = "0,1,2,3";

        BaseController baseController = CreateDefaultBaseController();
        ComputationResult actual = baseController.Compute(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DivideByZeroThrowsDivideByZeroException()
    {
        string input = "1,2,0,4";

        BaseController baseController = CreateDefaultBaseController();
        void act() => baseController.Compute(input);

        _ = Assert.Throws<DivideByZeroException>(act);
    }
}