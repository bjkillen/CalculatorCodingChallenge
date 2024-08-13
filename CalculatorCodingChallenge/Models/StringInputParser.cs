using System;
using System.Text.RegularExpressions;

using CalculatorCodingChallenge.Exceptions;
using CalculatorCodingChallenge.Extensions;

namespace CalculatorCodingChallenge.Models
{
    public interface IStringInputParser
    {
        int[] ParseInput(string? text, CommandLineArgsResult args);

    }

    public class StringInputParser: IStringInputParser
    {
        private readonly HashSet<string> defaultSeparators = new() { ",", "\n" };
        private int ValueUpperBound { get; set; }
        private bool AllowNegatives { get; set; }

        public int[] ParseInput(string? text, CommandLineArgsResult args)
        {
            HashSet<string> separators = new(defaultSeparators);

            if (args.AlternateDelimiter != null)
            {
                separators.Add(args.AlternateDelimiter);
            }

            ValueUpperBound = args.ValuesUpperBound ?? 1000;
            AllowNegatives = args.AllowNegatives ?? false;

            if (text == null)
            {
                return new int[] { 0 };
            }

            string sanitizedInputText = text.Sanitize();

            RegexDelimiterResult matchedSimpleDelimiter =
                RegexHelper.MatchesSimpleDelimiterAndCleansIfMatch(sanitizedInputText);

            if (matchedSimpleDelimiter.Delimiter != null)
            {
                separators.Add(matchedSimpleDelimiter.Delimiter);

                return ParseInputNoDelimiter(
                    matchedSimpleDelimiter.CleanedText,
                    separators.ToArray()
                );
            }

            RegexDelimitersResult matchedBracketedDelimiters =
                RegexHelper.MatchesBracketedDelimitersAndCleansIfMatch(sanitizedInputText);

            if (matchedBracketedDelimiters.Delimiters.Length > 0)
            {
                separators.UnionWith(matchedBracketedDelimiters.Delimiters.ToHashSet());

                return ParseInputNoDelimiter(
                    matchedBracketedDelimiters.CleanedText,
                    separators.ToArray()
                );
            }

            return ParseInputNoDelimiter(sanitizedInputText, separators.ToArray());
        }

        private int[] ParseInputNoDelimiter(string text, string[] separators)
        {
            // TODO: Refactor this block to make one pass looking at each char,
            // parsing previous chars when delimeter found and performing
            // negative check in the same loop pass
            //
            // This will improve performance in the case requirements change
            int[] numbers = text.Split(separators, StringSplitOptions.TrimEntries)
                            .Select(numText => numText.TryParseWithLimit(ValueUpperBound))
                            .ToArray();

            if (!AllowNegatives)
            {
                int[] negativeNumbers = numbers.Where(n => n < 0).ToArray();

                if (negativeNumbers.Length > 0)
                {
                    throw new NoNegativeNumbersException(negativeNumbers);
                }
            }

            return numbers;
        }
    }
}

