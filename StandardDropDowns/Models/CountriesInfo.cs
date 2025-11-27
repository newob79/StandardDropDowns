using StandardDropdowns.Interfaces;

namespace StandardDropdowns.Models
{
    /// <summary>
    /// Represents a country or territory based on ISO 3166-1 standard.
    /// </summary>
    public class CountryInfo : ISelectOption
    {
        /// <summary>
        /// The common name of the country (e.g., "United States", "Canada").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The ISO 3166-1 alpha-2 code (e.g., "US", "CA").
        /// </summary>
        public string Alpha2Code { get; set; }

        /// <summary>
        /// The ISO 3166-1 alpha-3 code (e.g., "USA", "CAN").
        /// </summary>
        public string Alpha3Code { get; set; }

        /// <summary>
        /// The ISO 3166-1 numeric code (e.g., "840", "124").
        /// </summary>
        public string NumericCode { get; set; }

        /// <summary>
        /// The continent this country belongs to.
        /// </summary>
        public string Continent { get; set; }

        /// <summary>
        /// Gets the alpha-2 code as the value for dropdown lists.
        /// </summary>
        public string Value => Alpha2Code;

        /// <summary>
        /// Gets the common name as the display text for dropdown lists.
        /// </summary>
        public string Text => Name;

        /// <summary>
        /// Returns the country name and alpha-2 code.
        /// </summary>
        public override string ToString() => $"{Name} ({Alpha2Code})";
    }
}