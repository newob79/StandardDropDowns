using StandardDropdowns.Interfaces;

namespace StandardDropdowns.Models
{
    /// <summary>
    /// Represents a yes/no option.
    /// </summary>
    public class YesNoInfo : ISelectOption
    {
        /// <summary>
        /// The display name (e.g., "Yes", "No", "N/A").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The short code (e.g., "Y", "N", "NA").
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The numeric identifier.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The preset group this option belongs to (e.g., "Basic", "WithNA", "WithUnknown").
        /// </summary>
        public string Preset { get; set; }

        /// <summary>
        /// Gets the code as the value for dropdown lists.
        /// </summary>
        public string Value => Code;

        /// <summary>
        /// Gets the name as the display text for dropdown lists.
        /// </summary>
        public string Text => Name;

        /// <summary>
        /// Returns the yes/no name and code.
        /// </summary>
        public override string ToString() => $"{Name} ({Code})";
    }
}