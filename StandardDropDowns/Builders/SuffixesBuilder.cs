using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Models;

namespace StandardDropdowns.Builders
{
    /// <summary>
    /// Builder for creating customized suffix lists.
    /// </summary>
    public class SuffixesBuilder : BaseBuilder<SuffixInfo, SuffixesBuilder>
    {
        private readonly HashSet<string> _includedCategories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _excludedCategories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets the source data (all suffixes).
        /// </summary>
        protected override IReadOnlyList<SuffixInfo> GetSourceData()
        {
            return DropdownData.Suffixes.All;
        }

        /// <summary>
        /// Applies category filters.
        /// </summary>
        protected override IEnumerable<SuffixInfo> ApplyCategoryFilters(IEnumerable<SuffixInfo> source)
        {
            if (_includedCategories.Count > 0)
            {
                source = source.Where(s => _includedCategories.Contains(s.Category));
            }

            if (_excludedCategories.Count > 0)
            {
                source = source.Where(s => !_excludedCategories.Contains(s.Category));
            }

            return source;
        }

        /// <summary>
        /// Includes only suffixes from the specified category(s).
        /// </summary>
        /// <param name="categories">The category(s) to include (e.g., "Generational", "Academic").</param>
        public SuffixesBuilder InCategory(params string[] categories)
        {
            if (categories != null)
            {
                foreach (var category in categories.Where(c => !string.IsNullOrWhiteSpace(c)))
                {
                    _includedCategories.Add(category.Trim());
                }
            }
            return this;
        }

        /// <summary>
        /// Excludes suffixes from the specified category(s).
        /// </summary>
        /// <param name="categories">The category(s) to exclude.</param>
        public SuffixesBuilder ExcludeCategory(params string[] categories)
        {
            if (categories != null)
            {
                foreach (var category in categories.Where(c => !string.IsNullOrWhiteSpace(c)))
                {
                    _excludedCategories.Add(category.Trim());
                }
            }
            return this;
        }

        #region Convenience Methods

        /// <summary>
        /// Orders results by suffix name (ascending).
        /// </summary>
        public SuffixesBuilder OrderByName() => OrderByText();

        /// <summary>
        /// Orders results by suffix name (descending).
        /// </summary>
        public SuffixesBuilder OrderByNameDescending() => OrderByTextDescending();

        /// <summary>
        /// Orders results by abbreviation (ascending).
        /// </summary>
        public SuffixesBuilder OrderByAbbreviation() => OrderByValue();

        /// <summary>
        /// Orders results by abbreviation (descending).
        /// </summary>
        public SuffixesBuilder OrderByAbbreviationDescending() => OrderByValueDescending();

        #endregion
    }
}
