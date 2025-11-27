using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Models;

namespace StandardDropdowns.Builders
{
    /// <summary>
    /// Builder for creating customized country lists.
    /// </summary>
    public class CountriesBuilder : BaseBuilder<CountryInfo, CountriesBuilder>
    {
        private readonly HashSet<string> _includedContinents = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _excludedContinents = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets the source data (all countries).
        /// </summary>
        protected override IReadOnlyList<CountryInfo> GetSourceData()
        {
            return DropdownData.Countries.All;
        }

        /// <summary>
        /// Applies country-specific category filters (continents).
        /// </summary>
        protected override IEnumerable<CountryInfo> ApplyCategoryFilters(IEnumerable<CountryInfo> source)
        {
            // Filter by included continents if specified
            if (_includedContinents.Count > 0)
            {
                source = source.Where(c => _includedContinents.Contains(c.Continent));
            }

            // Filter out excluded continents
            if (_excludedContinents.Count > 0)
            {
                source = source.Where(c => !_excludedContinents.Contains(c.Continent));
            }

            return source;
        }

        /// <summary>
        /// Includes only countries from the specified continent(s).
        /// </summary>
        /// <param name="continents">The continent(s) to include (e.g., "Europe", "Asia").</param>
        public CountriesBuilder InContinent(params string[] continents)
        {
            if (continents != null)
            {
                foreach (var continent in continents.Where(c => !string.IsNullOrWhiteSpace(c)))
                {
                    _includedContinents.Add(continent.Trim());
                }
            }
            return this;
        }

        /// <summary>
        /// Excludes countries from the specified continent(s).
        /// </summary>
        /// <param name="continents">The continent(s) to exclude.</param>
        public CountriesBuilder ExcludeContinent(params string[] continents)
        {
            if (continents != null)
            {
                foreach (var continent in continents.Where(c => !string.IsNullOrWhiteSpace(c)))
                {
                    _excludedContinents.Add(continent.Trim());
                }
            }
            return this;
        }

        #region Convenience Methods (map to base class for discoverability)

        /// <summary>
        /// Orders results by country name (ascending).
        /// </summary>
        public CountriesBuilder OrderByName() => OrderByText();

        /// <summary>
        /// Orders results by country name (descending).
        /// </summary>
        public CountriesBuilder OrderByNameDescending() => OrderByTextDescending();

        /// <summary>
        /// Orders results by alpha-2 code (ascending).
        /// </summary>
        public CountriesBuilder OrderByCode() => OrderByValue();

        /// <summary>
        /// Orders results by alpha-2 code (descending).
        /// </summary>
        public CountriesBuilder OrderByCodeDescending() => OrderByValueDescending();

        #endregion
    }
}