using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Models;

namespace StandardDropdowns.Builders
{
    /// <summary>
    /// Builder for creating customized yes/no option lists.
    /// </summary>
    public class YesNoOptionsBuilder : BaseBuilder<YesNoInfo, YesNoOptionsBuilder>
    {
        private readonly HashSet<string> _includedPresets = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _excludedPresets = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets the source data (all yes/no options).
        /// </summary>
        protected override IReadOnlyList<YesNoInfo> GetSourceData()
        {
            return DropdownData.YesNo.All;
        }

        /// <summary>
        /// Applies preset filters.
        /// </summary>
        protected override IEnumerable<YesNoInfo> ApplyCategoryFilters(IEnumerable<YesNoInfo> source)
        {
            if (_includedPresets.Count > 0)
            {
                source = source.Where(o => _includedPresets.Contains(o.Preset));
            }

            if (_excludedPresets.Count > 0)
            {
                source = source.Where(o => !_excludedPresets.Contains(o.Preset));
            }

            return source;
        }

        /// <summary>
        /// Includes only options from the specified preset(s).
        /// </summary>
        /// <param name="presets">The preset(s) to include (e.g., "Basic", "Extended").</param>
        public YesNoOptionsBuilder InPreset(params string[] presets)
        {
            if (presets != null)
            {
                foreach (var preset in presets.Where(p => !string.IsNullOrWhiteSpace(p)))
                {
                    _includedPresets.Add(preset.Trim());
                }
            }
            return this;
        }

        /// <summary>
        /// Excludes options from the specified preset(s).
        /// </summary>
        /// <param name="presets">The preset(s) to exclude.</param>
        public YesNoOptionsBuilder ExcludePreset(params string[] presets)
        {
            if (presets != null)
            {
                foreach (var preset in presets.Where(p => !string.IsNullOrWhiteSpace(p)))
                {
                    _excludedPresets.Add(preset.Trim());
                }
            }
            return this;
        }

        /// <summary>
        /// Includes the N/A option along with basic Yes/No.
        /// </summary>
        public YesNoOptionsBuilder IncludeNA()
        {
            return Only("Y", "N", "NA");
        }

        /// <summary>
        /// Includes the Unknown option along with basic Yes/No.
        /// </summary>
        public YesNoOptionsBuilder IncludeUnknown()
        {
            return Only("Y", "N", "U");
        }

        #region Convenience Methods

        /// <summary>
        /// Orders results by name (ascending).
        /// </summary>
        public YesNoOptionsBuilder OrderByName() => OrderByText();

        /// <summary>
        /// Orders results by name (descending).
        /// </summary>
        public YesNoOptionsBuilder OrderByNameDescending() => OrderByTextDescending();

        #endregion
    }
}