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

        // Future data providers will be added here:
        // public static Months Months => _months.Value;
        // public static DaysOfWeek DaysOfWeek => _daysOfWeek.Value;
        // etc.
    }
}