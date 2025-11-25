using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Models;

namespace StandardDropdowns.Builders
{
    /// <summary>
    /// Builder for creating customized US state lists.
    /// </summary>
    public class UsStatesBuilder : BaseBuilder<StateInfo, UsStatesBuilder>
    {
        private bool _includeStates = true;
        private bool _includeDC = false;
        private bool _includeTerritories = false;

        /// <summary>
        /// Gets the source data (all states, DC, and territories).
        /// </summary>
        protected override IReadOnlyList<StateInfo> GetSourceData()
        {
            return DropdownData.UsStates.All;
        }

        /// <summary>
        /// Applies state-specific category filters (states, DC, territories).
        /// </summary>
        protected override IEnumerable<StateInfo> ApplyCategoryFilters(IEnumerable<StateInfo> source)
        {
            return source.Where(s =>
                (_includeStates && !s.IsDC && !s.IsTerritory) ||
                (_includeDC && s.IsDC) ||
                (_includeTerritories && s.IsTerritory));
        }

        /// <summary>
        /// Includes the District of Columbia in the results.
        /// </summary>
        public UsStatesBuilder IncludeDC()
        {
            _includeDC = true;
            return this;
        }

        /// <summary>
        /// Includes US territories in the results.
        /// </summary>
        public UsStatesBuilder IncludeTerritories()
        {
            _includeTerritories = true;
            return this;
        }

        /// <summary>
        /// Includes both DC and territories in the results.
        /// </summary>
        public UsStatesBuilder IncludeAll()
        {
            _includeDC = true;
            _includeTerritories = true;
            return this;
        }

        /// <summary>
        /// Excludes the 50 states, leaving only DC and/or territories if included.
        /// </summary>
        public UsStatesBuilder ExcludeStates()
        {
            _includeStates = false;
            return this;
        }

        #region Convenience Methods (map to base class for discoverability)

        /// <summary>
        /// Orders results by state name (ascending).
        /// </summary>
        public UsStatesBuilder OrderByName() => OrderByText();

        /// <summary>
        /// Orders results by state name (descending).
        /// </summary>
        public UsStatesBuilder OrderByNameDescending() => OrderByTextDescending();

        /// <summary>
        /// Orders results by abbreviation (ascending).
        /// </summary>
        public UsStatesBuilder OrderByAbbreviation() => OrderByValue();

        /// <summary>
        /// Orders results by abbreviation (descending).
        /// </summary>
        public UsStatesBuilder OrderByAbbreviationDescending() => OrderByValueDescending();

        #endregion
    }
}