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

        // Future data providers will be added here:
        // public static Months Months => _months.Value;
        // public static DaysOfWeek DaysOfWeek => _daysOfWeek.Value;
        // etc.
    }
}