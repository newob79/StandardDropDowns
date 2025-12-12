using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Models;

namespace StandardDropdowns.Builders
{
    /// <summary>
    /// Builder for creating customized Canadian province/territory lists.
    /// </summary>
    public class CanadianProvincesBuilder : BaseBuilder<ProvinceInfo, CanadianProvincesBuilder>
    {
        private bool _includeProvinces = true;
        private bool _includeTerritories = false;

        /// <summary>
        /// Gets the source data (all provinces and territories).
        /// </summary>
        protected override IReadOnlyList<ProvinceInfo> GetSourceData()
        {
            return DropdownData.CanadianProvinces.All;
        }

        /// <summary>
        /// Applies province/territory-specific category filters.
        /// </summary>
        protected override IEnumerable<ProvinceInfo> ApplyCategoryFilters(IEnumerable<ProvinceInfo> source)
        {
            return source.Where(p =>
                (_includeProvinces && !p.IsTerritory) ||
                (_includeTerritories && p.IsTerritory));
        }

        /// <summary>
        /// Includes the 3 Canadian territories in the results.
        /// </summary>
        public CanadianProvincesBuilder IncludeTerritories()
        {
            _includeTerritories = true;
            return this;
        }

        /// <summary>
        /// Includes both provinces and territories in the results.
        /// </summary>
        public CanadianProvincesBuilder IncludeAll()
        {
            _includeTerritories = true;
            return this;
        }

        /// <summary>
        /// Excludes the 10 provinces, leaving only territories if included.
        /// </summary>
        public CanadianProvincesBuilder ExcludeProvinces()
        {
            _includeProvinces = false;
            return this;
        }

        #region Convenience Methods (map to base class for discoverability)

        /// <summary>
        /// Orders results by province/territory name (ascending).
        /// </summary>
        public CanadianProvincesBuilder OrderByName() => OrderByText();

        /// <summary>
        /// Orders results by province/territory name (descending).
        /// </summary>
        public CanadianProvincesBuilder OrderByNameDescending() => OrderByTextDescending();

        /// <summary>
        /// Orders results by abbreviation (ascending).
        /// </summary>
        public CanadianProvincesBuilder OrderByAbbreviation() => OrderByValue();

        /// <summary>
        /// Orders results by abbreviation (descending).
        /// </summary>
        public CanadianProvincesBuilder OrderByAbbreviationDescending() => OrderByValueDescending();

        #endregion
    }
}