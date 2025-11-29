using StandardDropdowns.Interfaces;

namespace StandardDropdowns.Models
{
    /// <summary>
    /// Represents a name suffix (e.g., Jr., PhD, Esq.).
    /// </summary>
    public class SuffixInfo : ISelectOption
    {
        /// <summary>
        /// The full name of the suffix (e.g., "Junior", "Doctor of Philosophy").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The abbreviated form of the suffix (e.g., "Jr.", "PhD").
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// The category of suffix (e.g., "Generational", "Academic", "Professional").
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The numeric identifier for this suffix.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets the number as the value for dropdown lists.
        /// </summary>
        public string Value => Number.ToString();

        /// <summary>
        /// Gets the abbreviation as the display text for dropdown lists.
        /// </summary>
        public string Text => Abbreviation;

        /// <summary>
        /// Returns the suffix name and abbreviation.
        /// </summary>
        public override string ToString() => $"{Name} ({Abbreviation})";
    }
}
