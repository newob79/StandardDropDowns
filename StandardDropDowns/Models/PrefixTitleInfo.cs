using StandardDropdowns.Interfaces;
using System;
using System.Collections.Generic;

namespace StandardDropdowns.Models
{
    /// <summary>
    /// Represents a selectable option consisting of a prefix title and its associated number for use in UI selection
    /// controls.
    /// </summary>
    /// <remarks>This class implements <see cref="ISelectOption"/> to provide standardized text and value
    /// representations for selection lists. It is commonly used to display and select prefix titles, such as honorifics
    /// or titles, in user interfaces.</remarks>
    public class PrefixTitleInfo : ISelectOption
    {
        /// <summary>
        /// Gets or sets the name of the prefix title (e.g., "Mr.", "Ms.", "Dr.").
        /// </summary>
        public string Name { get; set; }  

        /// <summary>
        /// Gets or sets the abbreviated form of the name or identifier.
        /// </summary>
        public string Abbreviation { get; set; }
        
        /// <summary>
        /// Gets or sets the numeric value associated with this instance.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the category associated with the item.
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Provides the value used in dropdowns. Matches other models' pattern by exposing a simple scalar.
        /// </summary>
        public string Value => Number.ToString();

        /// <summary>
        /// The display text for dropdowns.
        /// </summary>
        public string Text => Name;

        /// <summary>
        /// Returns a string that represents the current object, including its name and number.
        /// </summary>
        /// <returns>A string in the format "Name (Number)", where Name and Number are the values of the corresponding
        /// properties.</returns>
        public override string ToString() => $"{Name} ({Number})";
    }
}
