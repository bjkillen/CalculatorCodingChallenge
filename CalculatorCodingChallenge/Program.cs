using CalculatorCodingChallenge.Controllers;

public class Program
{
    public static void Main()
    {
        Console.Clear();

        Console.WriteLine("Welcome to my calculator!");
        Console.WriteLine("Please provide numbers for me to add, " +
            "separated by a comma. Example 1,2");

        string? inputText = Console.ReadLine();

        try
        {
            int result = BaseController.Compute(inputText);

            Console.WriteLine($"Result: {result}");
        }
        catch (Exception)
        {
            Console.WriteLine("Unknown error occurred");
        }

        // Wait for the user to respond before closing.
        Console.Write("Press any key to close the Calculator console app...");
        Console.ReadKey();
    }
}