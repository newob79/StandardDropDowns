using StandardDropdowns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StandardDropdowns.Data
{
    public class Months
    {
        private static readonly Lazy<IReadOnlyList<MonthInfo>> _all =
            new Lazy<IReadOnlyList<MonthInfo>>(LoadAllMonths);
        /// <summary>
        /// Gets all month names.
        /// </summary>
        public IReadOnlyList<MonthInfo> All => _all.Value;
        private static IReadOnlyList<MonthInfo> LoadAllMonths()
        {
            return new List<MonthInfo>
            {
                new MonthInfo { Name = "January", Abbreviation = "Jan", Number = 1 },
                new MonthInfo { Name = "February", Abbreviation = "Feb", Number = 2 },
                new MonthInfo { Name = "March", Abbreviation = "Mar", Number = 3 },
                new MonthInfo { Name = "April", Abbreviation = "Apr", Number = 4 },
                new MonthInfo { Name = "May", Abbreviation = "May", Number = 5 },
                new MonthInfo { Name = "June", Abbreviation = "Jun", Number = 6 },
                new MonthInfo { Name = "July", Abbreviation = "Jul", Number = 7 },
                new MonthInfo { Name = "August", Abbreviation = "Aug", Number = 8 },
                new MonthInfo { Name = "September", Abbreviation = "Sep", Number = 9 },
                new MonthInfo { Name = "October", Abbreviation = "Oct", Number = 10 },
                new MonthInfo { Name = "November", Abbreviation = "Nov", Number = 11 },
                new MonthInfo { Name = "December", Abbreviation = "Dec", Number = 12 }
            }.AsReadOnly();
        }

        /// <summary>
        /// Returns the month by its number (1-12)
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Month Info by the number</returns>
        public MonthInfo ByNumber(int number)
        {
            return _all.Value.FirstOrDefault(m => m.Number == number);
        }

        /// <summary>
        /// Find the month by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The matching Month</returns>
        public MonthInfo ByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return _all.Value.FirstOrDefault(m =>
                m.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Retrieves the month information corresponding to the specified abbreviation.    
        /// </summary>
        /// <param name="abbreviation">The three-letter abbreviation of the month to search for. Leading and trailing whitespace is ignored.
        /// Comparison is case-insensitive. If null, empty, or whitespace, no result is returned.</param>
        /// <returns>A <see cref="MonthInfo"/> representing the month with the specified abbreviation; or <see langword="null"/>
        /// if no matching month is found.</returns>
        public MonthInfo ByAbbreviation(string abbreviation)
        {
            if (string.IsNullOrWhiteSpace(abbreviation))
                return null;

            return _all.Value.FirstOrDefault(m =>
                m.Abbreviation.Equals(abbreviation.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }
}
