using System;
namespace CalculatorCodingChallenge.Extensions
{
	public static class EnumerableExtensions
	{
        public static void forEach<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie) action(e, i++);
        }
    }
}

