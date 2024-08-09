using System;

namespace CalculatorCodingChallenge.Extensions
{
    public static class ArrayExtensions
    {
        public static T? TryGetElement<T>(this T[] array, int index)
        {
            if (array == null || index < 0 || index >= array.Length)
            {
                return default;
            }

            return array[index];
        }
    }
}

