using StandardDropdowns.Interfaces;

namespace StandardDropdowns.Models
{
    /// <summary>
    /// Represents a Mexican state or the federal district (Ciudad de México).
    /// </summary>
    public class MexicanStateInfo : ISelectOption
    {
        /// <summary>
        /// The full name of the state (e.g., "Jalisco", "Ciudad de México").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The ISO 3166-2:MX abbreviation (e.g., "JAL", "CMX").
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Indicates whether this is the federal district (Ciudad de México).
        /// </summary>
        public bool IsFederalDistrict { get; set; }

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