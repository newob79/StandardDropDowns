using System;
using System.Collections.Generic;
using StandardDropdowns.Models;

namespace StandardDropdowns.Data
{
    /// <summary>
    /// Provides numeric ranges for dropdown lists.
    /// </summary>
    public class Numbers
    {
        /// <summary>
        /// Creates a range of numbers from start to end (inclusive).
        /// </summary>
        /// <param name="start">The starting number (inclusive).</param>
        /// <param name="end">The ending number (inclusive).</param>
        /// <param name="step">The increment between numbers (default: 1).</param>
        /// <param name="descending">If true, returns numbers in descending order (default: false).</param>
        /// <returns>A read-only list of NumberInfo objects.</returns>
        /// <exception cref="ArgumentException">Thrown when step is less than or equal to zero.</exception>
        /// <example>
        /// <code>
        /// // Basic range: 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
        /// var numbers = DropdownData.Numbers.Range(1, 10);
        /// 
        /// // With step: 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100
        /// var tens = DropdownData.Numbers.Range(0, 100, step: 10);
        /// 
        /// // Descending: 10, 9, 8, 7, 6, 5, 4, 3, 2, 1
        /// var countdown = DropdownData.Numbers.Range(1, 10, descending: true);
        /// </code>
        /// </example>
        public IReadOnlyList<NumberInfo> Range(int start, int end, int step = 1, bool descending = false)
        {
            if (step <= 0)
                throw new ArgumentException("Step must be greater than zero.", nameof(step));

            var numbers = new List<NumberInfo>();

            int min = Math.Min(start, end);
            int max = Math.Max(start, end);

            for (int i = min; i <= max; i += step)
            {
                numbers.Add(new NumberInfo { Number = i });
            }

            if (descending)
            {
                numbers.Reverse();
            }

            return numbers.AsReadOnly();
        }
    }
}