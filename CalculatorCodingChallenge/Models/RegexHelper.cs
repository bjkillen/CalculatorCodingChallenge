using System;
using System.Text.RegularExpressions;

namespace CalculatorCodingChallenge.Models
{
    public readonly struct RegexDelimiterResult
    {
        public RegexDelimiterResult(string cleanedText, string? delimiter)
        {
            CleanedText = cleanedText;
            Delimiter = delimiter;
        }

        public string CleanedText { get; }
        public string? Delimiter { get; }
    }

    public static class RegexHelper
    {
        private static readonly string startingSimpleDelimiterPattern = @"^//(.)\n";
        private static readonly string startingBracketedDelimiterPattern = @"^//\[(.+)\]\n";

        public static RegexDelimiterResult MatchesSimpleDelimiterAndCleansIfMatch(string input)
        {
            return MatchesRegexAndCleansIfMatch(input, startingSimpleDelimiterPattern);
        }

        public static RegexDelimiterResult MatchesBracketedDelimiterAndCleansIfMatch(string input)
        {
            return MatchesRegexAndCleansIfMatch(input, startingBracketedDelimiterPattern);
        }

        public static RegexDelimiterResult MatchesRegexAndCleansIfMatch(
                string input,
                string pattern
            )
        {
            Regex regex = new(pattern);

            Match matchResult = regex.Match(input);
            string? returnDelimiter = null;

            if (matchResult.Success)
            {
                Group middleMatchingGroup = matchResult.Groups[1];
                returnDelimiter = middleMatchingGroup.Value;

                input = Regex.Replace(
                    input,
                    pattern,
                    ""
                );
            }

            return new RegexDelimiterResult(input, returnDelimiter);
        }
    }
}

