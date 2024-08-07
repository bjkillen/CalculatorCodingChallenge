using System;
using System.Collections.Generic;
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

        private static readonly string startingDelimiterPattern = @"^(//)(.)(\n)";
        private static readonly Regex startingDelimiterRegex = new(StringInputParser.startingDelimiterPattern);

        private static HashSet<string> separators = new() { ",", "\n" };

        public static int[] ParseInput(string? text)
        {
            if (text == null)
            {
                return new int[] { 0 };
            }

            // inputting \n via console received an escape while unit tests
            // input don't. We must sanitize correctly for user input
            string sanitizedInputText = text.Replace("\\n", "\n");

            Match matchResult = startingDelimiterRegex.Match(sanitizedInputText);

            if (matchResult.Success)
            {
                Group middleMatchingGroup = matchResult.Groups[2];

                string delimeter = middleMatchingGroup.Value;
                separators.Add(delimeter);

                sanitizedInputText = Regex.Replace(
                    sanitizedInputText,
                    startingDelimiterPattern,
                    ""
                 );
            }

            return ParseInputNoDelimiter(sanitizedInputText);
        }

        private static int[] ParseInputNoDelimiter(string text)
        {
            // TODO: Refactor this block to make one pass looking at each char,
            // parsing previous chars when delimeter found and performing
            // negative check in the same loop pass
            //
            // This will improve performance in the case requirements change
            int[] numbers = text.Split(separators.ToArray(), StringSplitOptions.TrimEntries)
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

