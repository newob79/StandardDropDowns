using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Models;

namespace StandardDropdowns.Builders
{
    /// <summary>
    /// Builder for creating customized Mexican state lists.
    /// </summary>
    public class MexicanStatesBuilder : BaseBuilder<MexicanStateInfo, MexicanStatesBuilder>
    {
        private bool _includeStates = true;
        private bool _includeCDMX = false;

        /// <summary>
        /// Gets the source data (all states and federal district).
        /// </summary>
        protected override IReadOnlyList<MexicanStateInfo> GetSourceData()
        {
            return DropdownData.MexicanStates.All;
        }

        /// <summary>
        /// Applies state-specific category filters (states, federal district).
        /// </summary>
        protected override IEnumerable<MexicanStateInfo> ApplyCategoryFilters(IEnumerable<MexicanStateInfo> source)
        {
            return source.Where(s =>
                (_includeStates && !s.IsFederalDistrict) ||
                (_includeCDMX && s.IsFederalDistrict));
        }

        /// <summary>
        /// Includes Ciudad de México (federal district) in the results.
        /// </summary>
        public MexicanStatesBuilder IncludeCDMX()
        {
            _includeCDMX = true;
            return this;
        }

        /// <summary>
        /// Excludes the 31 states, leaving only Ciudad de México if included.
        /// </summary>
        public MexicanStatesBuilder ExcludeStates()
        {
            _includeStates = false;
            return this;
        }

        #region Convenience Methods (map to base class for discoverability)

        /// <summary>
        /// Orders results by state name (ascending).
        /// </summary>
        public MexicanStatesBuilder OrderByName() => OrderByText();

        /// <summary>
        /// Orders results by state name (descending).
        /// </summary>
        public MexicanStatesBuilder OrderByNameDescending() => OrderByTextDescending();

        /// <summary>
        /// Orders results by abbreviation (ascending).
        /// </summary>
        public MexicanStatesBuilder OrderByAbbreviation() => OrderByValue();

        /// <summary>
        /// Orders results by abbreviation (descending).
        /// </summary>
        public MexicanStatesBuilder OrderByAbbreviationDescending() => OrderByValueDescending();

        #endregion
    }
}