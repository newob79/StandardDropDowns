using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Interfaces;

namespace StandardDropdowns.Builders
{
    /// <summary>
    /// Base builder class providing common filtering and ordering functionality for all dropdown builders.
    /// </summary>
    /// <typeparam name="TModel">The model type (must implement ISelectOption).</typeparam>
    /// <typeparam name="TBuilder">The derived builder type (for fluent chaining).</typeparam>
    public abstract class BaseBuilder<TModel, TBuilder>
        where TModel : class, ISelectOption
        where TBuilder : BaseBuilder<TModel, TBuilder>
    {
        protected readonly HashSet<string> _excludedValues = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        protected readonly HashSet<string> _includedValues = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        protected Func<IEnumerable<TModel>, IEnumerable<TModel>> _orderBy;

        /// <summary>
        /// Gets the source data to filter from.
        /// </summary>
        protected abstract IReadOnlyList<TModel> GetSourceData();

        /// <summary>
        /// Applies category-specific filters (e.g., IncludeDC, IncludeTerritories for states).
        /// Called before exclusions and ordering are applied.
        /// </summary>
        /// <param name="source">The source data.</param>
        /// <returns>Filtered data based on category-specific rules.</returns>
        protected abstract IEnumerable<TModel> ApplyCategoryFilters(IEnumerable<TModel> source);

        /// <summary>
        /// Excludes specific items by their value (e.g., abbreviation, code).
        /// </summary>
        /// <param name="values">The values to exclude.</param>
        public TBuilder Exclude(params string[] values)
        {
            if (values != null)
            {
                foreach (var value in values.Where(v => !string.IsNullOrWhiteSpace(v)))
                {
                    _excludedValues.Add(value.Trim());
                }
            }
            return (TBuilder)this;
        }

        /// <summary>
        /// Includes only specific items by their value. When called, only the specified values will be included.
        /// </summary>
        /// <param name="values">The values to include.</param>
        public TBuilder Only(params string[] values)
        {
            if (values != null)
            {
                foreach (var value in values.Where(v => !string.IsNullOrWhiteSpace(v)))
                {
                    _includedValues.Add(value.Trim());
                }
            }
            return (TBuilder)this;
        }

        /// <summary>
        /// Orders results by display text (ascending).
        /// </summary>
        public TBuilder OrderByText()
        {
            _orderBy = items => items.OrderBy(i => i.Text, StringComparer.Ordinal);
            return (TBuilder)this;
        }

        /// <summary>
        /// Orders results by display text (descending).
        /// </summary>
        public TBuilder OrderByTextDescending()
        {
            _orderBy = items => items.OrderByDescending(i => i.Text, StringComparer.Ordinal);
            return (TBuilder)this;
        }

        /// <summary>
        /// Orders results by value (ascending).
        /// </summary>
        public TBuilder OrderByValue()
        {
            _orderBy = items => items.OrderBy(i => i.Value, StringComparer.Ordinal);
            return (TBuilder)this;
        }

        /// <summary>
        /// Orders results by value (descending).
        /// </summary>
        public TBuilder OrderByValueDescending()
        {
            _orderBy = items => items.OrderByDescending(i => i.Value, StringComparer.Ordinal);
            return (TBuilder)this;
        }

        /// <summary>
        /// Builds the customized list based on configured options.
        /// </summary>
        /// <returns>A read-only list matching the configured criteria.</returns>
        public IReadOnlyList<TModel> Build()
        {
            var source = GetSourceData().AsEnumerable();

            // If specific values are requested via Only(), filter to those first
            if (_includedValues.Count > 0)
            {
                source = source.Where(item => _includedValues.Contains(item.Value));
            }
            else
            {
                // Apply category-specific filters (e.g., IncludeDC, IncludeTerritories)
                source = ApplyCategoryFilters(source);
            }

            // Apply exclusions
            if (_excludedValues.Count > 0)
            {
                source = source.Where(item => !_excludedValues.Contains(item.Value));
            }

            // Apply ordering
            if (_orderBy != null)
            {
                source = _orderBy(source);
            }

            return source.ToList().AsReadOnly();
        }
    }
}
