using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Models;

namespace StandardDropdowns.Data
{
    /// <summary>
    /// Provides gender options for dropdown lists.
    /// </summary>
    public class Genders
    {
        private static readonly Lazy<IReadOnlyList<GenderInfo>> _all =
            new Lazy<IReadOnlyList<GenderInfo>>(LoadAllGenders);

        /// <summary>
        /// Gets all gender options.
        /// </summary>
        public IReadOnlyList<GenderInfo> All => _all.Value;

        /// <summary>
        /// Finds a gender by its code.
        /// </summary>
        /// <param name="code">The gender code (case-insensitive).</param>
        /// <returns>The matching GenderInfo, or null if not found.</returns>
        public GenderInfo ByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;

            return _all.Value.FirstOrDefault(g =>
                g.Code.Equals(code.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds a gender by its name.
        /// </summary>
        /// <param name="name">The gender name (case-insensitive).</param>
        /// <returns>The matching GenderInfo, or null if not found.</returns>
        public GenderInfo ByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return _all.Value.FirstOrDefault(g =>
                g.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds a gender by its number.
        /// </summary>
        /// <param name="number">The gender number.</param>
        /// <returns>The matching GenderInfo, or null if not found.</returns>
        public GenderInfo ByNumber(int number)
        {
            return _all.Value.FirstOrDefault(g => g.Number == number);
        }

        private static IReadOnlyList<GenderInfo> LoadAllGenders()
        {
            return new List<GenderInfo>
            {
                new GenderInfo { Name = "Male", Code = "M", Number = 1 },
                new GenderInfo { Name = "Female", Code = "F", Number = 2 },
                new GenderInfo { Name = "Prefer Not to Say", Code = "U", Number = 3 }
            }.AsReadOnly();
        }
    }
}