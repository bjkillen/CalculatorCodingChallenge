using System;

namespace CalculatorCodingChallenge.Extensions
{
    public static class IntExtensions
    {
        public static int TryParseWithLimit(this string Source, int limit = 1000)
        {
            if (int.TryParse(Source, out int result) && result <= limit)
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
    }
}

