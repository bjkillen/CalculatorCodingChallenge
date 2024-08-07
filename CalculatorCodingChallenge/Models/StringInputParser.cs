using System;
using System.Text.RegularExpressions;

using CalculatorCodingChallenge.Exceptions;
using CalculatorCodingChallenge.Extensions;

namespace CalculatorCodingChallenge.Models
{
    public class StringInputParser
    {
        public StringInputParser()
        {
        }

        private static readonly HashSet<string> baseSeparators = new() { ",", "\n" };

        public static int[] ParseInput(string? text)
        {
            if (text == null)
            {
                return new int[] { 0 };
            }

            string sanitizedInputText = text.Sanitize();

            RegexDelimiterResult matchedSimpleDelimiter =
                RegexHelper.MatchesSimpleDelimiterAndCleansIfMatch(sanitizedInputText);

            HashSet<string> combinedSeparators = new (baseSeparators);

            if (matchedSimpleDelimiter.Delimiter != null)
            {
                combinedSeparators.Add(matchedSimpleDelimiter.Delimiter);

                return ParseInputNoDelimiter(
                    matchedSimpleDelimiter.CleanedText,
                    combinedSeparators.ToArray()
                );
            }

            RegexDelimiterResult matchedBracketedDelimiter =
                RegexHelper.MatchesBracketedDelimiterAndCleansIfMatch(sanitizedInputText);

            if (matchedBracketedDelimiter.Delimiter != null)
            {
                combinedSeparators.Add(matchedBracketedDelimiter.Delimiter);

                return ParseInputNoDelimiter(
                    matchedBracketedDelimiter.CleanedText,
                    combinedSeparators.ToArray()
                );
            }

            return ParseInputNoDelimiter(sanitizedInputText, combinedSeparators.ToArray());
        }

        private static int[] ParseInputNoDelimiter(string text, string[] separators)
        {
            // TODO: Refactor this block to make one pass looking at each char,
            // parsing previous chars when delimeter found and performing
            // negative check in the same loop pass
            //
            // This will improve performance in the case requirements change
            int[] numbers = text.Split(separators, StringSplitOptions.TrimEntries)
                            .Select(numText => numText.TryParseWithLimit())
                            .ToArray();

            int[] negativeNumbers = numbers.Where(n => n < 0).ToArray();

            if (negativeNumbers.Length > 0)
            {
                throw new NoNegativeNumbersException(negativeNumbers);
            }

            return numbers;
        }
    }
}

