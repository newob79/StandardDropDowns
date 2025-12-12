using StandardDropdowns.Interfaces;

namespace StandardDropdowns.Models
{
    /// <summary>
    /// Represents a year option for dropdown lists.
    /// </summary>
    public class YearInfo : ISelectOption
    {
        /// <summary>
        /// The year value.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets the year as a string value for dropdown lists.
        /// </summary>
        public string Value => Year.ToString();

        /// <summary>
        /// Gets the year as display text for dropdown lists.
        /// </summary>
        public string Text => Year.ToString();

        /// <summary>
        /// Returns the year as a string.
        /// </summary>
        public override string ToString() => Year.ToString();
    }
}