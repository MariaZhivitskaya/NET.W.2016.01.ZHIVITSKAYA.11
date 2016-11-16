using System;
using System.Collections.Generic;

namespace Task1.Logic
{
    public class FibonacciNumbers
    {
        /// <summary>
        /// Generates a specified number of Fibonacci numbers.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the number of numbers is less or equal than 0.
        /// </exception>
        /// <param name="number">The number of numbers.</param>
        /// <returns>Returns the next Fibonacci number.</returns>
        public static IEnumerable<int> GenerateFibonacciNumber(int number)
        {
            if (number <= 0)
                throw new ArgumentOutOfRangeException("Wrong number!");

            int first = 1;
            int second = 1;

            yield return first;
            yield return second;

            for (int i = 0; i < number - 2; i++)
            {
                yield return first + second;
                var tmp = second;
                second += first;
                first = tmp;
            }
        }
    }
}
