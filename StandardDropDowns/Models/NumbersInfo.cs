using StandardDropdowns.Interfaces;

namespace StandardDropdowns.Models
{
    /// <summary>
    /// Represents a numeric option for dropdown lists.
    /// </summary>
    public class NumberInfo : ISelectOption
    {
        /// <summary>
        /// The numeric value.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets the number as a string value for dropdown lists.
        /// </summary>
        public string Value => Number.ToString();

        /// <summary>
        /// Gets the number as display text for dropdown lists.
        /// </summary>
        public string Text => Number.ToString();

        /// <summary>
        /// Returns the number as a string.
        /// </summary>
        public override string ToString() => Number.ToString();
    }
}