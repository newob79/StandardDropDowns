using StandardDropdowns.Interfaces;

namespace StandardDropdowns.Models
{
    /// <summary>
    /// Represents a Canadian province or territory.
    /// </summary>
    public class ProvinceInfo : ISelectOption
    {
        /// <summary>
        /// The full name of the province or territory (e.g., "Ontario", "British Columbia").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The two-letter Canada Post abbreviation (e.g., "ON", "BC").
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Indicates whether this is a territory (e.g., Yukon, Northwest Territories, Nunavut).
        /// </summary>
        public bool IsTerritory { get; set; }

        /// <summary>
        /// Gets the abbreviation as the value for dropdown lists.
        /// </summary>
        public string Value => Abbreviation;

        /// <summary>
        /// Gets the full name as the display text for dropdown lists.
        /// </summary>
        public string Text => Name;

        /// <summary>
        /// Returns the province/territory name and abbreviation.
        /// </summary>
        public override string ToString() => $"{Name} ({Abbreviation})";
    }
}