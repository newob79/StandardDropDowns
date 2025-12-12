using System;
using System.Collections.Generic;
using StandardDropdowns.Models;

namespace StandardDropdowns.Data
{
    /// <summary>
    /// Provides year ranges for dropdown lists with rolling window support.
    /// </summary>
    public class Years
    {
        /// <summary>
        /// Gets the last n years including the current year.
        /// Default order is descending (most recent first) - ideal for birth year dropdowns.
        /// </summary>
        /// <param name="count">The number of years to include.</param>
        /// <param name="descending">If true (default), returns years in descending order.</param>
        /// <returns>A read-only list of YearInfo objects.</returns>
        /// <exception cref="ArgumentException">Thrown when count is less than or equal to zero.</exception>
        /// <example>
        /// <code>
        /// // Birth year dropdown - last 100 years, descending (2025, 2024, 2023, ... 1926)
        /// var birthYears = DropdownData.Years.Last(100);
        /// 
        /// // Same but ascending (1926, 1927, ... 2025)
        /// var birthYearsAsc = DropdownData.Years.Last(100, descending: false);
        /// </code>
        /// </example>
        public IReadOnlyList<YearInfo> Last(int count, bool descending = true)
        {
            if (count <= 0)
                throw new ArgumentException("Count must be greater than zero.", nameof(count));

            int currentYear = DateTime.Now.Year;
            int startYear = currentYear - count + 1;

            return GenerateYears(startYear, currentYear, descending);
        }

        /// <summary>
        /// Gets the next n years including the current year.
        /// Default order is ascending (soonest first) - ideal for expiration year dropdowns.
        /// </summary>
        /// <param name="count">The number of years to include.</param>
        /// <param name="descending">If true, returns years in descending order (default: false).</param>
        /// <returns>A read-only list of YearInfo objects.</returns>
        /// <exception cref="ArgumentException">Thrown when count is less than or equal to zero.</exception>
        /// <example>
        /// <code>
        /// // Credit card expiration - next 10 years (2025, 2026, 2027, ... 2034)
        /// var expirationYears = DropdownData.Years.Next(10);
        /// 
        /// // Same but descending (2034, 2033, ... 2025)
        /// var expirationYearsDesc = DropdownData.Years.Next(10, descending: true);
        /// </code>
        /// </example>
        public IReadOnlyList<YearInfo> Next(int count, bool descending = false)
        {
            if (count <= 0)
                throw new ArgumentException("Count must be greater than zero.", nameof(count));

            int currentYear = DateTime.Now.Year;
            int endYear = currentYear + count - 1;

            return GenerateYears(currentYear, endYear, descending);
        }

        /// <summary>
        /// Gets a range of years from start to end (inclusive).
        /// </summary>
        /// <param name="startYear">The starting year (inclusive).</param>
        /// <param name="endYear">The ending year (inclusive).</param>
        /// <param name="descending">If true, returns years in descending order (default: false).</param>
        /// <returns>A read-only list of YearInfo objects.</returns>
        /// <example>
        /// <code>
        /// // Explicit range ascending (1990, 1991, ... 2030)
        /// var years = DropdownData.Years.Range(1990, 2030);
        /// 
        /// // Explicit range descending (2030, 2029, ... 1990)
        /// var yearsDesc = DropdownData.Years.Range(1990, 2030, descending: true);
        /// </code>
        /// </example>
        public IReadOnlyList<YearInfo> Range(int startYear, int endYear, bool descending = false)
        {
            return GenerateYears(startYear, endYear, descending);
        }

        private static IReadOnlyList<YearInfo> GenerateYears(int start, int end, bool descending)
        {
            var years = new List<YearInfo>();

            int min = Math.Min(start, end);
            int max = Math.Max(start, end);

            for (int year = min; year <= max; year++)
            {
                years.Add(new YearInfo { Year = year });
            }

            if (descending)
            {
                years.Reverse();
            }

            return years.AsReadOnly();
        }
    }
}