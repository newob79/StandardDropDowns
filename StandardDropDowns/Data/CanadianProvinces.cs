using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Builders;
using StandardDropdowns.Models;

namespace StandardDropdowns.Data
{
    /// <summary>
    /// Provides Canadian province and territory data for dropdown lists.
    /// </summary>
    public class CanadianProvinces
    {
        private static readonly Lazy<IReadOnlyList<ProvinceInfo>> _all =
            new Lazy<IReadOnlyList<ProvinceInfo>>(LoadAllProvinces);

        /// <summary>
        /// Gets all Canadian provinces and territories.
        /// </summary>
        public IReadOnlyList<ProvinceInfo> All => _all.Value;

        /// <summary>
        /// Gets only the 10 Canadian provinces (excludes territories).
        /// </summary>
        public IReadOnlyList<ProvinceInfo> Provinces =>
            _all.Value.Where(p => !p.IsTerritory).ToList().AsReadOnly();

        /// <summary>
        /// Gets only the 3 Canadian territories.
        /// </summary>
        public IReadOnlyList<ProvinceInfo> Territories =>
            _all.Value.Where(p => p.IsTerritory).ToList().AsReadOnly();

        /// <summary>
        /// Finds a province or territory by its two-letter abbreviation.
        /// </summary>
        /// <param name="abbreviation">The two-letter Canada Post abbreviation (case-insensitive).</param>
        /// <returns>The matching ProvinceInfo, or null if not found.</returns>
        public ProvinceInfo ByAbbreviation(string abbreviation)
        {
            if (string.IsNullOrWhiteSpace(abbreviation))
                return null;

            return _all.Value.FirstOrDefault(p =>
                p.Abbreviation.Equals(abbreviation.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds a province or territory by its full name.
        /// </summary>
        /// <param name="name">The full province or territory name (case-insensitive).</param>
        /// <returns>The matching ProvinceInfo, or null if not found.</returns>
        public ProvinceInfo ByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return _all.Value.FirstOrDefault(p =>
                p.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Creates a builder for customizing the province/territory list.
        /// </summary>
        /// <returns>A new CanadianProvincesBuilder instance.</returns>
        public CanadianProvincesBuilder Builder() => new CanadianProvincesBuilder();

        private static IReadOnlyList<ProvinceInfo> LoadAllProvinces()
        {
            return new List<ProvinceInfo>
            {
                // 10 Provinces
                new ProvinceInfo { Name = "Alberta", Abbreviation = "AB" },
                new ProvinceInfo { Name = "British Columbia", Abbreviation = "BC" },
                new ProvinceInfo { Name = "Manitoba", Abbreviation = "MB" },
                new ProvinceInfo { Name = "New Brunswick", Abbreviation = "NB" },
                new ProvinceInfo { Name = "Newfoundland and Labrador", Abbreviation = "NL" },
                new ProvinceInfo { Name = "Nova Scotia", Abbreviation = "NS" },
                new ProvinceInfo { Name = "Ontario", Abbreviation = "ON" },
                new ProvinceInfo { Name = "Prince Edward Island", Abbreviation = "PE" },
                new ProvinceInfo { Name = "Quebec", Abbreviation = "QC" },
                new ProvinceInfo { Name = "Saskatchewan", Abbreviation = "SK" },

                // 3 Territories
                new ProvinceInfo { Name = "Northwest Territories", Abbreviation = "NT", IsTerritory = true },
                new ProvinceInfo { Name = "Nunavut", Abbreviation = "NU", IsTerritory = true },
                new ProvinceInfo { Name = "Yukon", Abbreviation = "YT", IsTerritory = true }
            }.AsReadOnly();
        }
    }
}