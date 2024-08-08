﻿using CalculatorCodingChallenge.Controllers;
using CalculatorCodingChallenge.Exceptions;

public class Program
{
    public static void Main()
    {
        Console.Clear();

        Console.WriteLine("Welcome to my calculator!");
        Console.WriteLine("Please provide only positive numbers for me to add, " +
            "separated by a comma or \\n. Example 1,2\\n3");
        Console.WriteLine("Please note, values greater than 1000 " +
            "will be invalidated and turned to 0.");
        Console.WriteLine("You can provide custom delimiters in the following formats\n" +
            "-- list of multiple character delimiters /[{delimiter1}][{delimiter2}]...\\n{numbers}\n" +
            "-- single character delimiter //{delimiter}\\n{numbers}\n" +
            "-- empty or invalid delimiters will be invalidated");
        Console.WriteLine("");
        Console.WriteLine("You can process entries until Ctrl+C is pressed, " +
            "in which the application will exit");
        Console.WriteLine("");

        bool keepCalculating = true;

        Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs e) {
            keepCalculating = false;
            e.Cancel = true;

            Console.WriteLine("Ctrl+C detected, please press enter to close program");
        };

        while (keepCalculating)
        {
            string? inputText = Console.ReadLine();

            // ReadLine will block until user presses enter in console
            // and stream is read from. Need to break loop if exit key
            // has been previously pressed
            if (!keepCalculating)
            {
                break;
            }

            try
            {
                ComputationResult result = BaseController.Compute(inputText);

                Console.WriteLine($"Result: {result.Result}");
                Console.WriteLine($"Formula: {result.FullFormula}");
            }
            catch (NoNegativeNumbersException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Unknown error occurred");
            }

            Console.WriteLine("");
        }

        Console.WriteLine("Exiting now ... goodbye!");
    }
}