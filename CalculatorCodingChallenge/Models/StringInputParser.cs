﻿using System;
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

        private readonly HashSet<string> separators = new() { ",", "\n" };

        public int[] ParseInput(string? text)
        {
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

