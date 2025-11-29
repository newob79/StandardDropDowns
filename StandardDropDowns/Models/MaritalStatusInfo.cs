using StandardDropdowns.Interfaces;

namespace StandardDropdowns.Models
{
    /// <summary>
    /// Represents a marital status option.
    /// </summary>
    public class MaritalStatusInfo : ISelectOption
    {
        /// <summary>
        /// The display name (e.g., "Single", "Married").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The short code (e.g., "S", "M", "D").
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The numeric identifier.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets the code as the value for dropdown lists.
        /// </summary>
        public string Value => Code;

        /// <summary>
        /// Gets the name as the display text for dropdown lists.
        /// </summary>
        public string Text => Name;

        /// <summary>
        /// Returns the marital status name and code.
        /// </summary>
        public override string ToString() => $"{Name} ({Code})";
    }
}