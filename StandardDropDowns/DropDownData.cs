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

        // Future data providers will be added here:
        // public static Countries Countries => _countries.Value;
        // public static Months Months => _months.Value;
        // public static DaysOfWeek DaysOfWeek => _daysOfWeek.Value;
        // etc.
    }
}
