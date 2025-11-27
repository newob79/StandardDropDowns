using StandardDropdowns.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StandardDropDowns.Models
{
    /// <summary>
    /// Represents a day of the week.
    /// </summary>
    public class DaysOfWeekInfo : ISelectOption
    {
        /// <summary>
        /// The full name of the day (e.g., "Monday", "Tuesday").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The 3 character abbreviation of the day (e.g., "Mon", "Tue").
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// The short abbreviation associated with this instance (e.g., "Su" for Sunday, "M" for Monday).
        /// </summary>
        public string ShortAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the numeric value to be used for the day.
        /// Uses 0-6 where 0 = Sunday, 1 = Monday, ..., 6 = Saturday.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Provides the value used in dropdowns. Matches other models' pattern by exposing a simple scalar.
        /// </summary>
        public string Value => Number.ToString();

        /// <summary>
        /// The display text for dropdowns.
        /// </summary>
        public string Text => Name;

        /// <summary>
        /// Returns the day name and abbreviation.
        /// </summary>
        public override string ToString() => $"{Name} ({Abbreviation})";
    }
}
