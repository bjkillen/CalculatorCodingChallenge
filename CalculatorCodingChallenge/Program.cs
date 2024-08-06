using CalculatorCodingChallenge.Models;
using CalculatorCodingChallenge.Models.Calculator;
using CalculatorCodingChallenge.Models.Exceptions;

public class Program
{
    public static void Main()
    {
        Console.Clear();

        Console.WriteLine("Welcome to my calculator!");
        Console.WriteLine("Please provide up to two numbers for me to add, " +
            "separated by a comma. Example 1,2");

        string? inputText = Console.ReadLine();

        int[] parsedInputText = StringInputParser.ParseInput(inputText);

        InputArgsCountChecker inputChecker = new() { MaxNumbersAllowed = 2 };

        // Method will throw exception if provided numbers exceed MaxNumbersAllowed
        try
        {
            inputChecker.ValidateInput(parsedInputText);

            AddCalculator calculator = new();

            int result = calculator.Calculate(parsedInputText);

            Console.WriteLine($"Result: {result}");
        }
        catch (TooManyNumbersProvidedException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception)
        {
            Console.WriteLine("Unknown error occured");
        }

        // Wait for the user to respond before closing.
        Console.Write("Press any key to close the Calculator console app...");
        Console.ReadKey();
    }
}