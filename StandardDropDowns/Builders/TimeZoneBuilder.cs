using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Models;

namespace StandardDropdowns.Builders
{
    /// <summary>
    /// Builder for creating customized time zone lists.
    /// </summary>
    public class TimeZonesBuilder : BaseBuilder<TimeZoneData, TimeZonesBuilder>
    {
        private readonly HashSet<string> _includedRegions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _excludedRegions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private bool? _observesDstFilter = null;

        /// <summary>
        /// Gets the source data (all time zones).
        /// </summary>
        protected override IReadOnlyList<TimeZoneData> GetSourceData()
        {
            return DropdownData.TimeZones.All;
        }

        /// <summary>
        /// Applies time zone-specific category filters (regions, DST).
        /// </summary>
        protected override IEnumerable<TimeZoneData> ApplyCategoryFilters(IEnumerable<TimeZoneData> source)
        {
            // Filter by included regions if specified
            if (_includedRegions.Count > 0)
            {
                source = source.Where(tz => _includedRegions.Contains(tz.Region));
            }

            // Filter out excluded regions
            if (_excludedRegions.Count > 0)
            {
                source = source.Where(tz => !_excludedRegions.Contains(tz.Region));
            }

            // Filter by DST observance
            if (_observesDstFilter.HasValue)
            {
                source = source.Where(tz => tz.ObservesDst == _observesDstFilter.Value);
            }

            return source;
        }

        /// <summary>
        /// Includes only time zones from the specified region(s).
        /// </summary>
        /// <param name="regions">The region(s) to include (e.g., "Americas", "Europe", "Asia", "Pacific", "Africa", "Atlantic", "Universal").</param>
        public TimeZonesBuilder InRegion(params string[] regions)
        {
            if (regions != null)
            {
                foreach (var region in regions.Where(r => !string.IsNullOrWhiteSpace(r)))
                {
                    _includedRegions.Add(region.Trim());
                }
            }
            return this;
        }

        /// <summary>
        /// Excludes time zones from the specified region(s).
        /// </summary>
        /// <param name="regions">The region(s) to exclude.</param>
        public TimeZonesBuilder ExcludeRegion(params string[] regions)
        {
            if (regions != null)
            {
                foreach (var region in regions.Where(r => !string.IsNullOrWhiteSpace(r)))
                {
                    _excludedRegions.Add(region.Trim());
                }
            }
            return this;
        }

        /// <summary>
        /// Includes only time zones that observe daylight saving time.
        /// </summary>
        public TimeZonesBuilder WithDstOnly()
        {
            _observesDstFilter = true;
            return this;
        }

        /// <summary>
        /// Includes only time zones that do not observe daylight saving time.
        /// </summary>
        public TimeZonesBuilder WithoutDst()
        {
            _observesDstFilter = false;
            return this;
        }

        /// <summary>
        /// Orders results by UTC offset ascending (west to east, most negative to most positive).
        /// </summary>
        public TimeZonesBuilder OrderByOffset()
        {
            _orderBy = items => items.OrderBy(tz => tz.UtcOffset);
            return this;
        }

        /// <summary>
        /// Orders results by UTC offset descending (east to west, most positive to most negative).
        /// </summary>
        public TimeZonesBuilder OrderByOffsetDescending()
        {
            _orderBy = items => items.OrderByDescending(tz => tz.UtcOffset);
            return this;
        }

        #region Convenience Methods (map to base class for discoverability)

        /// <summary>
        /// Orders results by display name (ascending).
        /// </summary>
        public TimeZonesBuilder OrderByName() => OrderByText();

        /// <summary>
        /// Orders results by display name (descending).
        /// </summary>
        public TimeZonesBuilder OrderByNameDescending() => OrderByTextDescending();

        /// <summary>
        /// Orders results by IANA identifier (ascending).
        /// </summary>
        public TimeZonesBuilder OrderByIanaId() => OrderByValue();

        /// <summary>
        /// Orders results by IANA identifier (descending).
        /// </summary>
        public TimeZonesBuilder OrderByIanaIdDescending() => OrderByValueDescending();

        #endregion
    }
}