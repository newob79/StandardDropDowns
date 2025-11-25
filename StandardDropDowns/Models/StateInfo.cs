using StandardDropdowns.Interfaces;

namespace StandardDropdowns.Models
{
    /// <summary>
    /// Represents a US state, territory, or the District of Columbia.
    /// </summary>
    public class StateInfo : ISelectOption
    {
        /// <summary>
        /// The full name of the state (e.g., "Texas", "California").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The two-letter USPS abbreviation (e.g., "TX", "CA").
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Indicates whether this is a US territory (e.g., Puerto Rico, Guam).
        /// </summary>
        public bool IsTerritory { get; set; }

        /// <summary>
        /// Indicates whether this is the District of Columbia.
        /// </summary>
        public bool IsDC { get; set; }

        /// <summary>
        /// Gets the abbreviation as the value for dropdown lists.
        /// </summary>
        public string Value => Abbreviation;

        /// <summary>
        /// Gets the full name as the display text for dropdown lists.
        /// </summary>
        public string Text => Name;

        /// <summary>
        /// Returns the state name and abbreviation.
        /// </summary>
        public override string ToString() => $"{Name} ({Abbreviation})";
    }
}
