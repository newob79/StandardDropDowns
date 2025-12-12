using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Builders;
using StandardDropdowns.Models;

namespace StandardDropdowns.Data
{
    /// <summary>
    /// Provides a curated list of common time zones for dropdown lists.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is a curated list of ~40 commonly used time zones suitable for dropdown selection,
    /// not a comprehensive IANA time zone database. It covers approximately 90% of real-world
    /// form use cases.
    /// </para>
    /// <para>
    /// For comprehensive time zone handling, consider using <see cref="System.TimeZoneInfo"/>
    /// or the NodaTime library.
    /// </para>
    /// </remarks>
    public class TimeZones
    {
        private static readonly Lazy<IReadOnlyList<TimeZoneData>> _all =
            new Lazy<IReadOnlyList<TimeZoneData>>(LoadAllTimeZones);

        /// <summary>
        /// Gets all curated time zones.
        /// </summary>
        public IReadOnlyList<TimeZoneData> All => _all.Value;

        /// <summary>
        /// Gets time zones in a specific region.
        /// </summary>
        /// <param name="region">The region name (e.g., "Americas", "Europe", "Asia").</param>
        public IReadOnlyList<TimeZoneData> ByRegion(string region)
        {
            if (string.IsNullOrWhiteSpace(region))
                return new List<TimeZoneData>().AsReadOnly();

            return _all.Value
                .Where(tz => tz.Region.Equals(region.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList()
                .AsReadOnly();
        }

        /// <summary>
        /// Finds a time zone by its IANA identifier.
        /// </summary>
        /// <param name="ianaId">The IANA identifier (e.g., "America/New_York").</param>
        /// <returns>The matching TimeZoneData, or null if not found.</returns>
        public TimeZoneData ByIanaId(string ianaId)
        {
            if (string.IsNullOrWhiteSpace(ianaId))
                return null;

            return _all.Value.FirstOrDefault(tz =>
                tz.IanaId.Equals(ianaId.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds a time zone by its Windows identifier.
        /// </summary>
        /// <param name="windowsId">The Windows identifier (e.g., "Eastern Standard Time").</param>
        /// <returns>The matching TimeZoneData, or null if not found.</returns>
        public TimeZoneData ByWindowsId(string windowsId)
        {
            if (string.IsNullOrWhiteSpace(windowsId))
                return null;

            return _all.Value.FirstOrDefault(tz =>
                tz.WindowsId.Equals(windowsId.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds a time zone by its standard time abbreviation.
        /// </summary>
        /// <param name="abbreviation">The abbreviation (e.g., "EST", "PST").</param>
        /// <returns>The matching TimeZoneData, or null if not found. Note: abbreviations may not be unique.</returns>
        public TimeZoneData ByAbbreviation(string abbreviation)
        {
            if (string.IsNullOrWhiteSpace(abbreviation))
                return null;

            return _all.Value.FirstOrDefault(tz =>
                tz.Abbreviation.Equals(abbreviation.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets all time zones that observe daylight saving time.
        /// </summary>
        public IReadOnlyList<TimeZoneData> WithDst =>
            _all.Value.Where(tz => tz.ObservesDst).ToList().AsReadOnly();

        /// <summary>
        /// Gets all time zones that do not observe daylight saving time.
        /// </summary>
        public IReadOnlyList<TimeZoneData> WithoutDst =>
            _all.Value.Where(tz => !tz.ObservesDst).ToList().AsReadOnly();

        /// <summary>
        /// Creates a builder for customizing the time zone list.
        /// </summary>
        /// <returns>A new TimeZonesBuilder instance.</returns>
        public TimeZonesBuilder Builder() => new TimeZonesBuilder();

        private static IReadOnlyList<TimeZoneData> LoadAllTimeZones()
        {
            return new List<TimeZoneData>
            {
                // Universal
                new TimeZoneData
                {
                    IanaId = "Etc/UTC",
                    WindowsId = "UTC",
                    DisplayName = "Coordinated Universal Time",
                    Abbreviation = "UTC",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.Zero,
                    Region = "Universal",
                    ObservesDst = false
                },

                // Americas - ordered roughly west to east
                new TimeZoneData
                {
                    IanaId = "Pacific/Honolulu",
                    WindowsId = "Hawaiian Standard Time",
                    DisplayName = "Hawaii",
                    Abbreviation = "HST",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(-10),
                    Region = "Americas",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "America/Anchorage",
                    WindowsId = "Alaskan Standard Time",
                    DisplayName = "Alaska",
                    Abbreviation = "AKST",
                    AbbreviationDst = "AKDT",
                    UtcOffset = TimeSpan.FromHours(-9),
                    Region = "Americas",
                    ObservesDst = true
                },
                new TimeZoneData
                {
                    IanaId = "America/Los_Angeles",
                    WindowsId = "Pacific Standard Time",
                    DisplayName = "Pacific Time (US & Canada)",
                    Abbreviation = "PST",
                    AbbreviationDst = "PDT",
                    UtcOffset = TimeSpan.FromHours(-8),
                    Region = "Americas",
                    ObservesDst = true
                },
                new TimeZoneData
                {
                    IanaId = "America/Phoenix",
                    WindowsId = "US Mountain Standard Time",
                    DisplayName = "Arizona",
                    Abbreviation = "MST",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(-7),
                    Region = "Americas",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "America/Denver",
                    WindowsId = "Mountain Standard Time",
                    DisplayName = "Mountain Time (US & Canada)",
                    Abbreviation = "MST",
                    AbbreviationDst = "MDT",
                    UtcOffset = TimeSpan.FromHours(-7),
                    Region = "Americas",
                    ObservesDst = true
                },
                new TimeZoneData
                {
                    IanaId = "America/Chicago",
                    WindowsId = "Central Standard Time",
                    DisplayName = "Central Time (US & Canada)",
                    Abbreviation = "CST",
                    AbbreviationDst = "CDT",
                    UtcOffset = TimeSpan.FromHours(-6),
                    Region = "Americas",
                    ObservesDst = true
                },
                new TimeZoneData
                {
                    IanaId = "America/Mexico_City",
                    WindowsId = "Central Standard Time (Mexico)",
                    DisplayName = "Mexico City",
                    Abbreviation = "CST",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(-6),
                    Region = "Americas",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "America/New_York",
                    WindowsId = "Eastern Standard Time",
                    DisplayName = "Eastern Time (US & Canada)",
                    Abbreviation = "EST",
                    AbbreviationDst = "EDT",
                    UtcOffset = TimeSpan.FromHours(-5),
                    Region = "Americas",
                    ObservesDst = true
                },
                new TimeZoneData
                {
                    IanaId = "America/Toronto",
                    WindowsId = "Eastern Standard Time",
                    DisplayName = "Eastern Time (Canada)",
                    Abbreviation = "EST",
                    AbbreviationDst = "EDT",
                    UtcOffset = TimeSpan.FromHours(-5),
                    Region = "Americas",
                    ObservesDst = true
                },
                new TimeZoneData
                {
                    IanaId = "America/Halifax",
                    WindowsId = "Atlantic Standard Time",
                    DisplayName = "Atlantic Time (Canada)",
                    Abbreviation = "AST",
                    AbbreviationDst = "ADT",
                    UtcOffset = TimeSpan.FromHours(-4),
                    Region = "Americas",
                    ObservesDst = true
                },
                new TimeZoneData
                {
                    IanaId = "America/Sao_Paulo",
                    WindowsId = "E. South America Standard Time",
                    DisplayName = "Brasilia",
                    Abbreviation = "BRT",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(-3),
                    Region = "Americas",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "America/Argentina/Buenos_Aires",
                    WindowsId = "Argentina Standard Time",
                    DisplayName = "Buenos Aires",
                    Abbreviation = "ART",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(-3),
                    Region = "Americas",
                    ObservesDst = false
                },

                // Atlantic
                new TimeZoneData
                {
                    IanaId = "Atlantic/Azores",
                    WindowsId = "Azores Standard Time",
                    DisplayName = "Azores",
                    Abbreviation = "AZOT",
                    AbbreviationDst = "AZOST",
                    UtcOffset = TimeSpan.FromHours(-1),
                    Region = "Atlantic",
                    ObservesDst = true
                },

                // Europe
                new TimeZoneData
                {
                    IanaId = "Atlantic/Reykjavik",
                    WindowsId = "Greenwich Standard Time",
                    DisplayName = "Reykjavik",
                    Abbreviation = "GMT",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.Zero,
                    Region = "Europe",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "Europe/London",
                    WindowsId = "GMT Standard Time",
                    DisplayName = "London, Dublin, Edinburgh",
                    Abbreviation = "GMT",
                    AbbreviationDst = "BST",
                    UtcOffset = TimeSpan.Zero,
                    Region = "Europe",
                    ObservesDst = true
                },
                new TimeZoneData
                {
                    IanaId = "Europe/Paris",
                    WindowsId = "Romance Standard Time",
                    DisplayName = "Paris, Brussels, Madrid",
                    Abbreviation = "CET",
                    AbbreviationDst = "CEST",
                    UtcOffset = TimeSpan.FromHours(1),
                    Region = "Europe",
                    ObservesDst = true
                },
                new TimeZoneData
                {
                    IanaId = "Europe/Berlin",
                    WindowsId = "W. Europe Standard Time",
                    DisplayName = "Berlin, Amsterdam, Vienna",
                    Abbreviation = "CET",
                    AbbreviationDst = "CEST",
                    UtcOffset = TimeSpan.FromHours(1),
                    Region = "Europe",
                    ObservesDst = true
                },
                new TimeZoneData
                {
                    IanaId = "Europe/Helsinki",
                    WindowsId = "FLE Standard Time",
                    DisplayName = "Helsinki, Kyiv, Riga",
                    Abbreviation = "EET",
                    AbbreviationDst = "EEST",
                    UtcOffset = TimeSpan.FromHours(2),
                    Region = "Europe",
                    ObservesDst = true
                },
                new TimeZoneData
                {
                    IanaId = "Europe/Athens",
                    WindowsId = "GTB Standard Time",
                    DisplayName = "Athens, Bucharest",
                    Abbreviation = "EET",
                    AbbreviationDst = "EEST",
                    UtcOffset = TimeSpan.FromHours(2),
                    Region = "Europe",
                    ObservesDst = true
                },
                new TimeZoneData
                {
                    IanaId = "Europe/Moscow",
                    WindowsId = "Russian Standard Time",
                    DisplayName = "Moscow, St. Petersburg",
                    Abbreviation = "MSK",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(3),
                    Region = "Europe",
                    ObservesDst = false
                },

                // Africa
                new TimeZoneData
                {
                    IanaId = "Africa/Lagos",
                    WindowsId = "W. Central Africa Standard Time",
                    DisplayName = "West Central Africa",
                    Abbreviation = "WAT",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(1),
                    Region = "Africa",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "Africa/Cairo",
                    WindowsId = "Egypt Standard Time",
                    DisplayName = "Cairo",
                    Abbreviation = "EET",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(2),
                    Region = "Africa",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "Africa/Johannesburg",
                    WindowsId = "South Africa Standard Time",
                    DisplayName = "Johannesburg, Pretoria",
                    Abbreviation = "SAST",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(2),
                    Region = "Africa",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "Africa/Nairobi",
                    WindowsId = "E. Africa Standard Time",
                    DisplayName = "Nairobi",
                    Abbreviation = "EAT",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(3),
                    Region = "Africa",
                    ObservesDst = false
                },

                // Asia
                new TimeZoneData
                {
                    IanaId = "Asia/Dubai",
                    WindowsId = "Arabian Standard Time",
                    DisplayName = "Dubai, Abu Dhabi",
                    Abbreviation = "GST",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(4),
                    Region = "Asia",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "Asia/Karachi",
                    WindowsId = "Pakistan Standard Time",
                    DisplayName = "Karachi",
                    Abbreviation = "PKT",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(5),
                    Region = "Asia",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "Asia/Kolkata",
                    WindowsId = "India Standard Time",
                    DisplayName = "Chennai, Kolkata, Mumbai, New Delhi",
                    Abbreviation = "IST",
                    AbbreviationDst = null,
                    UtcOffset = new TimeSpan(5, 30, 0),
                    Region = "Asia",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "Asia/Dhaka",
                    WindowsId = "Bangladesh Standard Time",
                    DisplayName = "Dhaka",
                    Abbreviation = "BST",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(6),
                    Region = "Asia",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "Asia/Bangkok",
                    WindowsId = "SE Asia Standard Time",
                    DisplayName = "Bangkok, Hanoi, Jakarta",
                    Abbreviation = "ICT",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(7),
                    Region = "Asia",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "Asia/Singapore",
                    WindowsId = "Singapore Standard Time",
                    DisplayName = "Singapore, Kuala Lumpur",
                    Abbreviation = "SGT",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(8),
                    Region = "Asia",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "Asia/Hong_Kong",
                    WindowsId = "China Standard Time",
                    DisplayName = "Hong Kong",
                    Abbreviation = "HKT",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(8),
                    Region = "Asia",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "Asia/Shanghai",
                    WindowsId = "China Standard Time",
                    DisplayName = "Beijing, Shanghai",
                    Abbreviation = "CST",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(8),
                    Region = "Asia",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "Asia/Taipei",
                    WindowsId = "Taipei Standard Time",
                    DisplayName = "Taipei",
                    Abbreviation = "CST",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(8),
                    Region = "Asia",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "Asia/Tokyo",
                    WindowsId = "Tokyo Standard Time",
                    DisplayName = "Tokyo, Osaka, Sapporo",
                    Abbreviation = "JST",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(9),
                    Region = "Asia",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "Asia/Seoul",
                    WindowsId = "Korea Standard Time",
                    DisplayName = "Seoul",
                    Abbreviation = "KST",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(9),
                    Region = "Asia",
                    ObservesDst = false
                },

                // Pacific / Oceania
                new TimeZoneData
                {
                    IanaId = "Australia/Perth",
                    WindowsId = "W. Australia Standard Time",
                    DisplayName = "Perth",
                    Abbreviation = "AWST",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(8),
                    Region = "Pacific",
                    ObservesDst = false
                },
                new TimeZoneData
                {
                    IanaId = "Australia/Adelaide",
                    WindowsId = "Cen. Australia Standard Time",
                    DisplayName = "Adelaide",
                    Abbreviation = "ACST",
                    AbbreviationDst = "ACDT",
                    UtcOffset = new TimeSpan(9, 30, 0),
                    Region = "Pacific",
                    ObservesDst = true
                },
                new TimeZoneData
                {
                    IanaId = "Australia/Sydney",
                    WindowsId = "AUS Eastern Standard Time",
                    DisplayName = "Sydney, Melbourne, Canberra",
                    Abbreviation = "AEST",
                    AbbreviationDst = "AEDT",
                    UtcOffset = TimeSpan.FromHours(10),
                    Region = "Pacific",
                    ObservesDst = true
                },
                new TimeZoneData
                {
                    IanaId = "Pacific/Auckland",
                    WindowsId = "New Zealand Standard Time",
                    DisplayName = "Auckland, Wellington",
                    Abbreviation = "NZST",
                    AbbreviationDst = "NZDT",
                    UtcOffset = TimeSpan.FromHours(12),
                    Region = "Pacific",
                    ObservesDst = true
                },
                new TimeZoneData
                {
                    IanaId = "Pacific/Fiji",
                    WindowsId = "Fiji Standard Time",
                    DisplayName = "Fiji",
                    Abbreviation = "FJT",
                    AbbreviationDst = null,
                    UtcOffset = TimeSpan.FromHours(12),
                    Region = "Pacific",
                    ObservesDst = false
                }
            }.AsReadOnly();
        }
    }
}