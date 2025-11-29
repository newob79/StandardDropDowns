using StandardDropdowns.Interfaces;

namespace StandardDropdowns.Models
{
    /// <summary>
    /// Represents a gender option.
    /// </summary>
    public class GenderInfo : ISelectOption
    {
        /// <summary>
        /// The display name (e.g., "Male", "Female").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The short code (e.g., "M", "F", "U").
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
        /// Returns the gender name and code.
        /// </summary>
        public override string ToString() => $"{Name} ({Code})";
    }
}