using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Builders;
using StandardDropdowns.Models;

namespace StandardDropdowns.Data
{
    /// <summary>
    /// Provides US state, territory, and DC data for dropdown lists.
    /// </summary>
    public class UsStates
    {
        private static readonly Lazy<IReadOnlyList<StateInfo>> _all =
            new Lazy<IReadOnlyList<StateInfo>>(LoadAllStates);

        /// <summary>
        /// Gets all US states, DC, and territories.
        /// </summary>
        public IReadOnlyList<StateInfo> All => _all.Value;

        /// <summary>
        /// Gets only the 50 US states (excludes DC and territories).
        /// </summary>
        public IReadOnlyList<StateInfo> States50 =>
            _all.Value.Where(s => !s.IsTerritory && !s.IsDC).ToList().AsReadOnly();

        /// <summary>
        /// Gets the 50 US states plus DC.
        /// </summary>
        public IReadOnlyList<StateInfo> StatesAndDC =>
            _all.Value.Where(s => !s.IsTerritory).ToList().AsReadOnly();

        /// <summary>
        /// Gets only US territories.
        /// </summary>
        public IReadOnlyList<StateInfo> Territories =>
            _all.Value.Where(s => s.IsTerritory).ToList().AsReadOnly();

        /// <summary>
        /// Finds a state by its two-letter abbreviation.
        /// </summary>
        /// <param name="abbreviation">The two-letter USPS abbreviation (case-insensitive).</param>
        /// <returns>The matching StateInfo, or null if not found.</returns>
        public StateInfo ByAbbreviation(string abbreviation)
        {
            if (string.IsNullOrWhiteSpace(abbreviation))
                return null;

            return _all.Value.FirstOrDefault(s =>
                s.Abbreviation.Equals(abbreviation.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds a state by its full name.
        /// </summary>
        /// <param name="name">The full state name (case-insensitive).</param>
        /// <returns>The matching StateInfo, or null if not found.</returns>
        public StateInfo ByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return _all.Value.FirstOrDefault(s =>
                s.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Creates a builder for customizing the state list.
        /// </summary>
        /// <returns>A new UsStatesBuilder instance.</returns>
        public UsStatesBuilder Builder() => new UsStatesBuilder();

        private static IReadOnlyList<StateInfo> LoadAllStates()
        {
            return new List<StateInfo>
            {
                // 50 US States
                new StateInfo { Name = "Alabama", Abbreviation = "AL" },
                new StateInfo { Name = "Alaska", Abbreviation = "AK" },
                new StateInfo { Name = "Arizona", Abbreviation = "AZ" },
                new StateInfo { Name = "Arkansas", Abbreviation = "AR" },
                new StateInfo { Name = "California", Abbreviation = "CA" },
                new StateInfo { Name = "Colorado", Abbreviation = "CO" },
                new StateInfo { Name = "Connecticut", Abbreviation = "CT" },
                new StateInfo { Name = "Delaware", Abbreviation = "DE" },
                new StateInfo { Name = "Florida", Abbreviation = "FL" },
                new StateInfo { Name = "Georgia", Abbreviation = "GA" },
                new StateInfo { Name = "Hawaii", Abbreviation = "HI" },
                new StateInfo { Name = "Idaho", Abbreviation = "ID" },
                new StateInfo { Name = "Illinois", Abbreviation = "IL" },
                new StateInfo { Name = "Indiana", Abbreviation = "IN" },
                new StateInfo { Name = "Iowa", Abbreviation = "IA" },
                new StateInfo { Name = "Kansas", Abbreviation = "KS" },
                new StateInfo { Name = "Kentucky", Abbreviation = "KY" },
                new StateInfo { Name = "Louisiana", Abbreviation = "LA" },
                new StateInfo { Name = "Maine", Abbreviation = "ME" },
                new StateInfo { Name = "Maryland", Abbreviation = "MD" },
                new StateInfo { Name = "Massachusetts", Abbreviation = "MA" },
                new StateInfo { Name = "Michigan", Abbreviation = "MI" },
                new StateInfo { Name = "Minnesota", Abbreviation = "MN" },
                new StateInfo { Name = "Mississippi", Abbreviation = "MS" },
                new StateInfo { Name = "Missouri", Abbreviation = "MO" },
                new StateInfo { Name = "Montana", Abbreviation = "MT" },
                new StateInfo { Name = "Nebraska", Abbreviation = "NE" },
                new StateInfo { Name = "Nevada", Abbreviation = "NV" },
                new StateInfo { Name = "New Hampshire", Abbreviation = "NH" },
                new StateInfo { Name = "New Jersey", Abbreviation = "NJ" },
                new StateInfo { Name = "New Mexico", Abbreviation = "NM" },
                new StateInfo { Name = "New York", Abbreviation = "NY" },
                new StateInfo { Name = "North Carolina", Abbreviation = "NC" },
                new StateInfo { Name = "North Dakota", Abbreviation = "ND" },
                new StateInfo { Name = "Ohio", Abbreviation = "OH" },
                new StateInfo { Name = "Oklahoma", Abbreviation = "OK" },
                new StateInfo { Name = "Oregon", Abbreviation = "OR" },
                new StateInfo { Name = "Pennsylvania", Abbreviation = "PA" },
                new StateInfo { Name = "Rhode Island", Abbreviation = "RI" },
                new StateInfo { Name = "South Carolina", Abbreviation = "SC" },
                new StateInfo { Name = "South Dakota", Abbreviation = "SD" },
                new StateInfo { Name = "Tennessee", Abbreviation = "TN" },
                new StateInfo { Name = "Texas", Abbreviation = "TX" },
                new StateInfo { Name = "Utah", Abbreviation = "UT" },
                new StateInfo { Name = "Vermont", Abbreviation = "VT" },
                new StateInfo { Name = "Virginia", Abbreviation = "VA" },
                new StateInfo { Name = "Washington", Abbreviation = "WA" },
                new StateInfo { Name = "West Virginia", Abbreviation = "WV" },
                new StateInfo { Name = "Wisconsin", Abbreviation = "WI" },
                new StateInfo { Name = "Wyoming", Abbreviation = "WY" },

                // District of Columbia
                new StateInfo { Name = "District of Columbia", Abbreviation = "DC", IsDC = true },

                // US Territories
                new StateInfo { Name = "American Samoa", Abbreviation = "AS", IsTerritory = true },
                new StateInfo { Name = "Guam", Abbreviation = "GU", IsTerritory = true },
                new StateInfo { Name = "Northern Mariana Islands", Abbreviation = "MP", IsTerritory = true },
                new StateInfo { Name = "Puerto Rico", Abbreviation = "PR", IsTerritory = true },
                new StateInfo { Name = "U.S. Virgin Islands", Abbreviation = "VI", IsTerritory = true }
            }.AsReadOnly();
        }
    }
}