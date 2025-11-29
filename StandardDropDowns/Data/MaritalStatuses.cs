using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Models;

namespace StandardDropdowns.Data
{
    /// <summary>
    /// Provides marital status options for dropdown lists.
    /// </summary>
    public class MaritalStatuses
    {
        private static readonly Lazy<IReadOnlyList<MaritalStatusInfo>> _all =
            new Lazy<IReadOnlyList<MaritalStatusInfo>>(LoadAllStatuses);

        /// <summary>
        /// Gets all marital status options.
        /// </summary>
        public IReadOnlyList<MaritalStatusInfo> All => _all.Value;

        /// <summary>
        /// Finds a marital status by its code.
        /// </summary>
        /// <param name="code">The status code (case-insensitive).</param>
        /// <returns>The matching MaritalStatusInfo, or null if not found.</returns>
        public MaritalStatusInfo ByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;

            return _all.Value.FirstOrDefault(m =>
                m.Code.Equals(code.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds a marital status by its name.
        /// </summary>
        /// <param name="name">The status name (case-insensitive).</param>
        /// <returns>The matching MaritalStatusInfo, or null if not found.</returns>
        public MaritalStatusInfo ByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return _all.Value.FirstOrDefault(m =>
                m.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds a marital status by its number.
        /// </summary>
        /// <param name="number">The status number.</param>
        /// <returns>The matching MaritalStatusInfo, or null if not found.</returns>
        public MaritalStatusInfo ByNumber(int number)
        {
            return _all.Value.FirstOrDefault(m => m.Number == number);
        }

        private static IReadOnlyList<MaritalStatusInfo> LoadAllStatuses()
        {
            return new List<MaritalStatusInfo>
            {
                new MaritalStatusInfo { Name = "Single", Code = "S", Number = 1 },
                new MaritalStatusInfo { Name = "Married", Code = "M", Number = 2 },
                new MaritalStatusInfo { Name = "Divorced", Code = "D", Number = 3 },
                new MaritalStatusInfo { Name = "Widowed", Code = "W", Number = 4 },
                new MaritalStatusInfo { Name = "Separated", Code = "SP", Number = 5 },
                new MaritalStatusInfo { Name = "Domestic Partnership", Code = "DP", Number = 6 }
            }.AsReadOnly();
        }
    }
}