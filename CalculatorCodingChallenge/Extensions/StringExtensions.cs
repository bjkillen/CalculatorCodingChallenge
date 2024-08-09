using System;

namespace CalculatorCodingChallenge.Extensions
{
    public static class StringExtensions
    {
        public static string Sanitize(this string Source)
        {
            return Source.Replace("\\n", "\n");
        }

        public static string[] SplitOnce(this string? Source, string delimiter)
        {
            if (string.IsNullOrEmpty(Source))
            {
                return Array.Empty<string>();
            }

            return Source.Split(delimiter, 2, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}

