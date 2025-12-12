using StandardDropdowns.Interfaces;
using System;

namespace StandardDropdowns.Models
{
    /// <summary>
    /// Represents a time zone option for dropdown lists.
    /// This is a curated list of common time zones, not a comprehensive database.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="UtcOffset"/> property represents the standard time (non-DST) offset.
    /// For DST-aware applications, use <see cref="System.TimeZoneInfo"/> with the 
    /// <see cref="WindowsId"/> property, or NodaTime with the <see cref="IanaId"/> property.
    /// </para>
    /// <para>
    /// Time zone rules can change (governments modify DST rules). This is static reference
    /// data intended for dropdown selection, not a live time zone database.
    /// </para>
    /// </remarks>
    public class TimeZoneData : ISelectOption
    {
        /// <summary>
        /// The IANA time zone identifier (e.g., "America/New_York", "Europe/London").
        /// This is the industry standard identifier used by most platforms and libraries.
        /// </summary>
        public string IanaId { get; set; }

        /// <summary>
        /// The Windows time zone identifier (e.g., "Eastern Standard Time").
        /// Use this with <see cref="System.TimeZoneInfo.FindSystemTimeZoneById"/> for .NET interop.
        /// </summary>
        public string WindowsId { get; set; }

        /// <summary>
        /// The human-readable display name (e.g., "Eastern Time (US &amp; Canada)").
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The standard time abbreviation (e.g., "EST", "GMT", "JST").
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// The daylight saving time abbreviation (e.g., "EDT", "BST").
        /// Null for time zones that do not observe DST.
        /// </summary>
        public string AbbreviationDst { get; set; }

        /// <summary>
        /// The UTC offset during standard time (non-DST).
        /// For DST-aware offset calculations, use <see cref="System.TimeZoneInfo"/> or NodaTime.
        /// </summary>
        public TimeSpan UtcOffset { get; set; }

        /// <summary>
        /// The geographic region for filtering (e.g., "Americas", "Europe", "Asia", "Pacific", "Africa", "Atlantic", "Universal").
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Indicates whether this time zone observes daylight saving time.
        /// </summary>
        public bool ObservesDst { get; set; }

        /// <summary>
        /// Gets the IANA identifier as the value for dropdown lists.
        /// </summary>
        public string Value => IanaId;

        /// <summary>
        /// Gets the formatted display text showing UTC offset and display name.
        /// </summary>
        public string Text => FormatDisplayText();

        /// <summary>
        /// Returns the time zone display name with IANA identifier.
        /// </summary>
        public override string ToString() => $"{DisplayName} ({IanaId})";

        private string FormatDisplayText()
        {
            var sign = UtcOffset >= TimeSpan.Zero ? "+" : "";
            var offsetStr = $"{sign}{UtcOffset.Hours:00}:{Math.Abs(UtcOffset.Minutes):00}";
            return $"(UTC{offsetStr}) {DisplayName}";
        }
    }
}