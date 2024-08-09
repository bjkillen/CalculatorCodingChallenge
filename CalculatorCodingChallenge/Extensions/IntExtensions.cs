using System;

namespace CalculatorCodingChallenge.Extensions
{
    public static class IntExtensions
    {
        public static int TryParse(this string Source)
        {
            if (int.TryParse(Source, out int result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        public static int TryParseWithLimit(this string Source, int limit)
        {
            int result = Source.TryParse();

            if (result <= limit)
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

