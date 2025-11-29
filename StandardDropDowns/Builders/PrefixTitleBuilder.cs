using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Models;

namespace StandardDropdowns.Builders
{
    /// <summary>
    /// Builder for creating customized prefix title lists.
    /// </summary>
    public class PrefixTitleBuilder : BaseBuilder<PrefixTitleInfo, PrefixTitleBuilder>
    {
        private readonly HashSet<string> _includedCategories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _excludedCategories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets the source data (all prefix titles).
        /// </summary>
        protected override IReadOnlyList<PrefixTitleInfo> GetSourceData()
        {
            return DropdownData.PrefixTitles.All;
        }

        /// <summary>
        /// Applies category filters.
        /// </summary>
        protected override IEnumerable<PrefixTitleInfo> ApplyCategoryFilters(IEnumerable<PrefixTitleInfo> source)
        {
            if (_includedCategories.Count > 0)
            {
                source = source.Where(p => _includedCategories.Contains(p.Category));
            }

            if (_excludedCategories.Count > 0)
            {
                source = source.Where(p => !_excludedCategories.Contains(p.Category));
            }

            return source;
        }

        /// <summary>
        /// Includes only titles from the specified category(s).
        /// </summary>
        /// <param name="categories">The category(s) to include (e.g., "Civilian", "Professional").</param>
        public PrefixTitleBuilder InCategory(params string[] categories)
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
        /// Excludes titles from the specified category(s).
        /// </summary>
        /// <param name="categories">The category(s) to exclude.</param>
        public PrefixTitleBuilder ExcludeCategory(params string[] categories)
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
        /// Orders results by title name (ascending).
        /// </summary>
        public PrefixTitleBuilder OrderByName() => OrderByText();

        /// <summary>
        /// Orders results by title name (descending).
        /// </summary>
        public PrefixTitleBuilder OrderByNameDescending() => OrderByTextDescending();

        #endregion
    }
}