using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Builders;
using StandardDropdowns.Models;

namespace StandardDropdowns.Data
{
    /// <summary>
    /// Provides name suffix data for dropdown lists.
    /// </summary>
    public class Suffixes
    {
        private static readonly Lazy<IReadOnlyList<SuffixInfo>> _all =
            new Lazy<IReadOnlyList<SuffixInfo>>(LoadAllSuffixes);

        /// <summary>
        /// Gets all suffixes.
        /// </summary>
        public IReadOnlyList<SuffixInfo> All => _all.Value;

        /// <summary>
        /// Gets suffixes in a specific category.
        /// </summary>
        /// <param name="category">The category name (e.g., "Generational", "Academic").</param>
        public IReadOnlyList<SuffixInfo> ByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return new List<SuffixInfo>().AsReadOnly();

            return _all.Value
                .Where(s => s.Category.Equals(category.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList()
                .AsReadOnly();
        }

        /// <summary>
        /// Finds a suffix by its number.
        /// </summary>
        /// <param name="number">The suffix number.</param>
        /// <returns>The matching SuffixInfo, or null if not found.</returns>
        public SuffixInfo ByNumber(int number)
        {
            return _all.Value.FirstOrDefault(s => s.Number == number);
        }

        /// <summary>
        /// Finds a suffix by its abbreviation.
        /// </summary>
        /// <param name="abbreviation">The suffix abbreviation (case-insensitive).</param>
        /// <returns>The matching SuffixInfo, or null if not found.</returns>
        public SuffixInfo ByAbbreviation(string abbreviation)
        {
            if (string.IsNullOrWhiteSpace(abbreviation))
                return null;

            return _all.Value.FirstOrDefault(s =>
                s.Abbreviation.Equals(abbreviation.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds a suffix by its full name.
        /// </summary>
        /// <param name="name">The suffix name (case-insensitive).</param>
        /// <returns>The matching SuffixInfo, or null if not found.</returns>
        public SuffixInfo ByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return _all.Value.FirstOrDefault(s =>
                s.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Creates a builder for customizing the suffix list.
        /// </summary>
        /// <returns>A new SuffixesBuilder instance.</returns>
        public SuffixesBuilder Builder() => new SuffixesBuilder();

        private static IReadOnlyList<SuffixInfo> LoadAllSuffixes()
        {
            return new List<SuffixInfo>
            {
                // Generational
                new SuffixInfo { Name = "Junior", Abbreviation = "Jr.", Category = "Generational", Number = 1 },
                new SuffixInfo { Name = "Senior", Abbreviation = "Sr.", Category = "Generational", Number = 2 },
                new SuffixInfo { Name = "The Second", Abbreviation = "II", Category = "Generational", Number = 3 },
                new SuffixInfo { Name = "The Third", Abbreviation = "III", Category = "Generational", Number = 4 },
                new SuffixInfo { Name = "The Fourth", Abbreviation = "IV", Category = "Generational", Number = 5 },
                new SuffixInfo { Name = "The Fifth", Abbreviation = "V", Category = "Generational", Number = 6 },

                // Academic
                new SuffixInfo { Name = "Doctor of Philosophy", Abbreviation = "PhD", Category = "Academic", Number = 7 },
                new SuffixInfo { Name = "Doctor of Medicine", Abbreviation = "MD", Category = "Academic", Number = 8 },
                new SuffixInfo { Name = "Doctor of Dental Surgery", Abbreviation = "DDS", Category = "Academic", Number = 9 },
                new SuffixInfo { Name = "Doctor of Education", Abbreviation = "EdD", Category = "Academic", Number = 10 },
                new SuffixInfo { Name = "Juris Doctor", Abbreviation = "JD", Category = "Academic", Number = 11 },
                new SuffixInfo { Name = "Master of Business Administration", Abbreviation = "MBA", Category = "Academic", Number = 12 },
                new SuffixInfo { Name = "Master of Science", Abbreviation = "MS", Category = "Academic", Number = 13 },
                new SuffixInfo { Name = "Master of Arts", Abbreviation = "MA", Category = "Academic", Number = 14 },
                new SuffixInfo { Name = "Bachelor of Science", Abbreviation = "BS", Category = "Academic", Number = 15 },
                new SuffixInfo { Name = "Bachelor of Arts", Abbreviation = "BA", Category = "Academic", Number = 16 },

                // Professional
                new SuffixInfo { Name = "Esquire", Abbreviation = "Esq.", Category = "Professional", Number = 17 },
                new SuffixInfo { Name = "Certified Public Accountant", Abbreviation = "CPA", Category = "Professional", Number = 18 },
                new SuffixInfo { Name = "Professional Engineer", Abbreviation = "PE", Category = "Professional", Number = 19 },
                new SuffixInfo { Name = "Registered Nurse", Abbreviation = "RN", Category = "Professional", Number = 20 },
                new SuffixInfo { Name = "Licensed Practical Nurse", Abbreviation = "LPN", Category = "Professional", Number = 21 },
                new SuffixInfo { Name = "Certified Financial Planner", Abbreviation = "CFP", Category = "Professional", Number = 22 }
            }.AsReadOnly();
        }
    }
}