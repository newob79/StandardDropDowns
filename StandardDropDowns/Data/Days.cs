using StandardDropDowns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StandardDropDowns.Data
{
    public class Days
    {
        private static readonly Lazy<IReadOnlyList<DaysOfWeekInfo>> _all =
            new Lazy<IReadOnlyList<DaysOfWeekInfo>>(LoadAllDays);
        /// <summary>
        /// Gets all days of the week.
        /// </summary>
        public IReadOnlyList<DaysOfWeekInfo> All => _all.Value;
        private static IReadOnlyList<DaysOfWeekInfo> LoadAllDays()
        {
            return new List<DaysOfWeekInfo>
            {
                new DaysOfWeekInfo { Name = "Sunday", Abbreviation = "Sun", ShortAbbreviation = "Su", Number = 0 },
                new DaysOfWeekInfo { Name = "Monday", Abbreviation = "Mon", ShortAbbreviation = "M", Number = 1 },
                new DaysOfWeekInfo { Name = "Tuesday", Abbreviation = "Tue", ShortAbbreviation = "Tu", Number = 2 },
                new DaysOfWeekInfo { Name = "Wednesday", Abbreviation = "Wed", ShortAbbreviation = "We", Number = 3 },
                new DaysOfWeekInfo { Name = "Thursday", Abbreviation = "Thu", ShortAbbreviation = "Th", Number = 4 },
                new DaysOfWeekInfo { Name = "Friday", Abbreviation = "Fri", ShortAbbreviation = "F", Number = 5 },
                new DaysOfWeekInfo { Name = "Saturday", Abbreviation = "Sat", ShortAbbreviation = "Sa", Number = 6 }
            }.AsReadOnly();
        }

        /// <summary>
        /// Retrieves day information by its number (0 for Sunday through 6 for Saturday).
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public DaysOfWeekInfo ByNumber(int number)
        {
            return _all.Value.FirstOrDefault(d => d.Number == number);
        }

        /// <summary>
        /// Retrieves day information by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DaysOfWeekInfo ByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;
            return _all.Value.FirstOrDefault(d =>
                d.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Retrieves day information by its abbreviation.
        /// </summary>
        /// <param name="abbreviation"></param>
        /// <returns></returns>
        public DaysOfWeekInfo ByAbbreviation(string abbreviation)
        {
            if (string.IsNullOrWhiteSpace(abbreviation))
                return null;
            return _all.Value.FirstOrDefault(d =>
                d.Abbreviation.Equals(abbreviation.Trim(), StringComparison.OrdinalIgnoreCase));
        }
        
        /// <summary>
        /// Retrieves the day of the week information that matches the specified short abbreviation, using a
        /// case-insensitive comparison.
        /// </summary>
        /// <param name="shortAbbreviation">The short abbreviation of the day to search for. Leading and trailing whitespace is ignored. If null, empty,
        /// or whitespace, no result is returned.</param>
        /// <returns>A <see cref="DaysOfWeekInfo"/> instance that corresponds to the specified short abbreviation; or null if no
        /// matching day is found.</returns>
        public DaysOfWeekInfo ByShortAbbreviation(string shortAbbreviation)
        {
            if (string.IsNullOrWhiteSpace(shortAbbreviation))
                return null;
            return _all.Value.FirstOrDefault(d =>
                d.ShortAbbreviation.Equals(shortAbbreviation.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }
}
