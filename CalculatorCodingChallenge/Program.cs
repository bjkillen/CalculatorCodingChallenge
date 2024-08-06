using CalculatorCodingChallenge.Controllers;
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

        try
        {
            int result = BaseController.Compute(inputText);

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