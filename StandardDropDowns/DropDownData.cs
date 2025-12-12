using System;
using StandardDropdowns.Data;

namespace StandardDropdowns
{
    /// <summary>
    /// Main entry point for accessing all dropdown data.
    /// Provides static access to common reference data for dropdown/select lists.
    /// </summary>
    public static class DropdownData
    {
        private static readonly Lazy<UsStates> _usStates =
            new Lazy<UsStates>(() => new UsStates());

        private static readonly Lazy<CanadianProvinces> _canadianProvinces =
            new Lazy<CanadianProvinces>(() => new CanadianProvinces());

        private static readonly Lazy<MexicanStates> _mexicanStates =
    new Lazy<MexicanStates>(() => new MexicanStates());

        private static readonly Lazy<Countries> _countries =
            new Lazy<Countries>(() => new Countries());

        private static readonly Lazy<Months> _months =
            new Lazy<Months>(() => new Months());

        private static readonly Lazy<Days> _daysOfWeek =
            new Lazy<Days>(() => new Days());

        private static readonly Lazy<PrefixTitle> _prefixTitles =
            new Lazy<PrefixTitle>(() => new PrefixTitle());

        private static readonly Lazy<Suffixes> _suffixes =
            new Lazy<Suffixes>(() => new Suffixes());

        private static readonly Lazy<Genders> _genders =
            new Lazy<Genders>(() => new Genders());

        private static readonly Lazy<MaritalStatuses> _maritalStatuses =
            new Lazy<MaritalStatuses>(() => new MaritalStatuses());

        private static readonly Lazy<YesNoOptions> _yesNo =
            new Lazy<YesNoOptions>(() => new YesNoOptions());

        private static readonly Lazy<Numbers> _numbers =
            new Lazy<Numbers>(() => new Numbers());

        private static readonly Lazy<Years> _years =
            new Lazy<Years>(() => new Years());

        private static readonly Lazy<TimeZones> _timeZones =
            new Lazy<TimeZones>(() => new TimeZones());

        /// <summary>
        /// Gets US state, DC, and territory data.
        /// </summary>
        /// <example>
        /// <code>
        /// // Get all 50 states
        /// var states = DropdownData.UsStates.States50;
        /// 
        /// // Get states and DC
        /// var statesAndDC = DropdownData.UsStates.StatesAndDC;
        /// 
        /// // Look up a state
        /// var texas = DropdownData.UsStates.ByAbbreviation("TX");
        /// 
        /// // Use the builder for custom lists
        /// var customList = DropdownData.UsStates.Builder()
        ///     .IncludeDC()
        ///     .Exclude("AS", "GU")
        ///     .OrderByName()
        ///     .Build();
        /// </code>
        /// </example>
        public static UsStates UsStates => _usStates.Value;

        /// <summary>
        /// Gets Canadian province and territory data.
        /// </summary>
        /// <example>
        /// <code>
        /// // Get all 10 provinces
        /// var provinces = DropdownData.CanadianProvinces.Provinces;
        /// 
        /// // Get all provinces and territories
        /// var all = DropdownData.CanadianProvinces.All;
        /// 
        /// // Look up a province
        /// var ontario = DropdownData.CanadianProvinces.ByAbbreviation("ON");
        /// 
        /// // Use the builder for custom lists
        /// var customList = DropdownData.CanadianProvinces.Builder()
        ///     .IncludeTerritories()
        ///     .Exclude("NU", "NT")
        ///     .OrderByName()
        ///     .Build();
        /// </code>
        /// </example>
        public static CanadianProvinces CanadianProvinces => _canadianProvinces.Value;

        /// <summary>
        /// Gets Mexican state and federal district data.
        /// </summary>
        /// <example>
        /// <code>
        /// // Get all 31 states (excludes Ciudad de México)
        /// var states = DropdownData.MexicanStates.States;
        /// 
        /// // Get states and federal district
        /// var all = DropdownData.MexicanStates.StatesAndCDMX;
        /// 
        /// // Look up a state
        /// var jalisco = DropdownData.MexicanStates.ByAbbreviation("JAL");
        /// 
        /// // Use the builder for custom lists
        /// var customList = DropdownData.MexicanStates.Builder()
        ///     .IncludeCDMX()
        ///     .Exclude("OAX", "GRO")
        ///     .OrderByName()
        ///     .Build();
        /// </code>
        /// </example>
        public static MexicanStates MexicanStates => _mexicanStates.Value;

        /// <summary>
        /// Gets country data based on ISO 3166-1 standard.
        /// </summary>
        /// <example>
        /// <code>
        /// // Get all countries
        /// var countries = DropdownData.Countries.All;
        /// 
        /// // Get countries by continent
        /// var european = DropdownData.Countries.ByContinent("Europe");
        /// 
        /// // Look up a country
        /// var usa = DropdownData.Countries.ByAlpha2Code("US");
        /// var germany = DropdownData.Countries.ByAlpha3Code("DEU");
        /// 
        /// // Use the builder for custom lists
        /// var customList = DropdownData.Countries.Builder()
        ///     .InContinent("Europe", "North America")
        ///     .Exclude("RU")
        ///     .OrderByName()
        ///     .Build();
        /// </code>
        /// </example>
        public static Countries Countries => _countries.Value;

        /// <summary>
        /// Gets Months data. 
        /// </summary>
        /// <example></example>
        /// <code>
        /// // Get all months
        /// var months = DropdownData.Months.All;
        /// 
        /// // Look up a month
        /// var january = DropdownData.Months.ByName("January");
        /// 
        /// </code>
        /// </example>
        public static Months Months => _months.Value;

        /// <summary>
        /// Gets the collection of days of the week.
        /// </summary>
        /// <example>
        /// <code>
        /// var days = DropdownData.Days.All;
        /// 
        /// //Look up a day 
        /// 
        /// var Monday = DropdownData.Days.ByName("Monday");
        /// </code></example>
        public static Days Days => _daysOfWeek.Value;

        /// <summary>
        /// Gets the collection of supported prefix titles, such as "Mr.", "Dr.", or "Ms.".
        /// </summary>
        /// <example>
        /// <code>
        /// var titles = DropdownData.PrefixTitles.All;
        /// 
        /// // Look up a title
        /// var mr = DropdownData.PrefixTitles.ByName("Mr.");
        /// </code></example>
        public static PrefixTitle PrefixTitles => _prefixTitles.Value;

        /// <summary>
        /// Gets name suffix data (Jr., PhD, Esq., etc.).
        /// </summary>
        public static Suffixes Suffixes => _suffixes.Value;

        /// <summary>
        /// Gets gender options.
        /// </summary>
        public static Genders Genders => _genders.Value;

        /// <summary>
        /// Gets marital status options.
        /// </summary>
        public static MaritalStatuses MaritalStatuses => _maritalStatuses.Value;

        /// <summary>
        /// Gets yes/no options with various presets (Basic, WithNA, WithUnknown).
        /// </summary>
        public static YesNoOptions YesNo => _yesNo.Value;

        /// <summary>
        /// Provides numeric ranges for dropdown lists.
        /// </summary>
        /// <example>
        /// <code>
        /// // Basic range: 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
        /// var numbers = DropdownData.Numbers.Range(1, 10);
        /// 
        /// // With step: 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100
        /// var tens = DropdownData.Numbers.Range(0, 100, step: 10);
        /// 
        /// // Descending: 10, 9, 8, 7, 6, 5, 4, 3, 2, 1
        /// var countdown = DropdownData.Numbers.Range(1, 10, descending: true);
        /// </code>
        /// </example>
        public static Numbers Numbers => _numbers.Value;

        /// <summary>
        /// Provides year ranges for dropdown lists with rolling window support.
        /// </summary>
        /// <example>
        /// <code>
        /// // Birth year dropdown - last 100 years, descending (most recent first)
        /// var birthYears = DropdownData.Years.Last(100);
        /// 
        /// // Credit card expiration - next 10 years, ascending
        /// var expirationYears = DropdownData.Years.Next(10);
        /// 
        /// // Explicit range
        /// var years = DropdownData.Years.Range(1990, 2030);
        /// </code>
        /// </example>
        public static Years Years => _years.Value;

        /// <summary>
        /// Gets a curated list of common time zones for dropdown selection.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is a curated list of ~40 commonly used time zones, not a comprehensive
        /// IANA time zone database. It covers approximately 90% of real-world form use cases.
        /// </para>
        /// <para>
        /// The <c>UtcOffset</c> property represents the standard time (non-DST) offset.
        /// For DST-aware applications, use <see cref="System.TimeZoneInfo"/> with the
        /// <c>WindowsId</c> property, or NodaTime with the <c>IanaId</c> property.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Get all curated time zones
        /// var zones = DropdownData.TimeZones.All;
        /// 
        /// // Filter by region
        /// var americas = DropdownData.TimeZones.ByRegion("Americas");
        /// var europe = DropdownData.TimeZones.ByRegion("Europe");
        /// 
        /// // Lookups
        /// var eastern = DropdownData.TimeZones.ByIanaId("America/New_York");
        /// var pst = DropdownData.TimeZones.ByAbbreviation("PST");
        /// 
        /// // Use the builder for customization
        /// var customList = DropdownData.TimeZones.Builder()
        ///     .InRegion("Americas", "Europe")
        ///     .Exclude("America/New_York")
        ///     .OrderByOffset()
        ///     .Build();
        /// </code>
        /// </example>
        public static TimeZones TimeZones => _timeZones.Value;

        // Future data providers will be added here:

    }
}