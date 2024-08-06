using System;

namespace CalculatorCodingChallenge.Extensions
{
    public static class IntExtensions
    {
        public static int TryParse(this string Source)
        {
            if (int.TryParse(Source, out int result) && result <= 1000)
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

