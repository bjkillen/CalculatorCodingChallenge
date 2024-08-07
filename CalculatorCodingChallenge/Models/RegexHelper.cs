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

    public readonly struct RegexDelimitersResult
    {
        public RegexDelimitersResult(string cleanedText, string[] delimiters)
        {
            CleanedText = cleanedText;
            Delimiters = delimiters;
        }

        public string CleanedText { get; }
        public string[] Delimiters { get; }
    }

    public static class RegexHelper
    {
        private static readonly string startingSimpleDelimiterPattern = @"^//(.)\n";

        private static readonly string startingBracketedDelimiterPattern = @"^//(\[.+\])+?\n";
        private static readonly string valueInsideBracketedListDelimiterPattern = @"\[([^\]]+)\]+";

        public static RegexDelimiterResult MatchesSimpleDelimiterAndCleansIfMatch(string input)
        {
            return MatchesRegexAndCleansIfMatch(
                input, startingSimpleDelimiterPattern);
        }

        public static RegexDelimitersResult MatchesBracketedDelimitersAndCleansIfMatch(string input)
        {
            RegexDelimiterResult result = MatchesRegexAndCleansIfMatch(
                input, startingBracketedDelimiterPattern);

            string? delimiter = result.Delimiter;

            // We have our delimiter returned containing all bracket pairs and
            // must separate multiple delimiters grouped via brackets if applicable
            if (delimiter == null)
            {
                return new RegexDelimitersResult(result.CleanedText, Array.Empty<string>());
            }

            string pattern = valueInsideBracketedListDelimiterPattern;

            Regex regex = new(pattern);
            MatchCollection matchResults = regex.Matches(delimiter);

            List<string> returnDelimiters = new();

            foreach (Match matchResult in matchResults.ToList())
            {
                if (matchResult.Success)
                {
                    Group delimiterMatchingGroup = matchResult.Groups[1];
                    returnDelimiters.Add(delimiterMatchingGroup.Value);
                }
            }

            return new RegexDelimitersResult(result.CleanedText, returnDelimiters.ToArray());
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

