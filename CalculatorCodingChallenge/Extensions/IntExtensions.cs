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
    }
}

