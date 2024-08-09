using System;
using CalculatorCodingChallenge.Extensions;

namespace CalculatorCodingChallenge.Models
{
    public readonly struct CommandLineArgsResult
    {
        public CommandLineArgsResult()
        {
        }

        public CommandLineArgsResult(
            string? alternateDelimiter,
            bool? allowNegatives,
            int? valuesUpperBound)
        {
            AlternateDelimiter = alternateDelimiter;
            AllowNegatives = allowNegatives;
            ValuesUpperBound = valuesUpperBound;
        }

        public string? AlternateDelimiter { get; }
        public bool? AllowNegatives { get; }
        public int? ValuesUpperBound { get; }
    }

    public static class CommandLineArgParser
    {
        static CommandLineArgParser()
        {
        }

        public static CommandLineArgsResult ParseArgs(string? text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new();
            }

            string[] commandLineArgsSplitBySpaces = text.Split(
                " ",
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries
            );

            string? alternateDelimiter = null;
            bool? allowNegatives = null;
            int? valuesUpperBound = null;

            foreach (string arg in commandLineArgsSplitBySpaces)
            {
                RegexDelimiterResult alternateDelimiterMatch =
                    RegexHelper.MatchesStartsWithAlternateDelimiterFlag(arg, "-ad=");

                if (alternateDelimiter == null && alternateDelimiterMatch.Delimiter != null)
                {
                    alternateDelimiter = alternateDelimiterMatch.Delimiter;
                    continue;
                }
                else if (allowNegatives == null && arg.Contains("--allowNegatives"))
                {
                    allowNegatives = true;
                    continue;
                }

                RegexDelimiterResult valueUpperBoundMatch =
                        RegexHelper.MatchesStartsWithValueUpperBoundFlag(arg, "-ub=");

                if (valuesUpperBound == null && valueUpperBoundMatch.Delimiter != null)
                {
                    valuesUpperBound = valueUpperBoundMatch.Delimiter.TryParse();
                }
            }

            return new(alternateDelimiter, allowNegatives, valuesUpperBound);
        }
    }
}

