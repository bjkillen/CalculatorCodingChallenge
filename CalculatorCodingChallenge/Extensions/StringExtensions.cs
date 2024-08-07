using System;

namespace CalculatorCodingChallenge.Extensions
{
    public static class StringExtensions
    {
        public static string Sanitize(this string Source)
        {
            return Source.Replace("\\n", "\n");
        }
    }
}

