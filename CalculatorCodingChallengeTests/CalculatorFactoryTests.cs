using System;

using CalculatorCodingChallenge.Models;
using CalculatorCodingChallenge.Models.Calculator;

namespace CalculatorCodingChallengeTests;

public class CalculatorFactoryTests
{
    [Fact]
    public void CreatesAddCalculator()
    {
        string input = "a";

        ICalculator result = CalculatorFactory.Create(input);

        Assert.IsType<AddCalculator>(result);
    }

    [Fact]
    public void CreatesSubtractionCalculator()
    {
        string input = "s";

        ICalculator result = CalculatorFactory.Create(input);

        Assert.IsType<SubtractionCalculator>(result);
    }

    [Fact]
    public void CreatesMultiplicationCalculator()
    {
        string input = "m";

        ICalculator result = CalculatorFactory.Create(input);

        Assert.IsType<MultiplicationCalculator>(result);
    }

    [Fact]
    public void CreatesDivisionCalculator()
    {
        string input = "d";

        ICalculator result = CalculatorFactory.Create(input);

        Assert.IsType<DivisionCalculator>(result);
    }

    [Fact]
    public void CreatesDefaultAddCalculator()
    {
        ICalculator result = CalculatorFactory.Create();

        Assert.IsType<AddCalculator>(result);
    }

    [Fact]
    public void InvalidInputCreatesAddCalculator()
    {
        string input = "f";

        ICalculator result = CalculatorFactory.Create(input);

        Assert.IsType<AddCalculator>(result);
    }
}


