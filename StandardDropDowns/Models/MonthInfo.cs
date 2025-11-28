using StandardDropdowns.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StandardDropdowns.Models
{
    public class MonthInfo : ISelectOption
    {
        /// <summary>
        /// The full name of the month (e.g., "January", "February").
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The 3 character abbreviation of the month (e.g., "Jan", "Feb").
        /// </summary>
        public string Abbreviation { get; set; }
        /// <summary>
        /// Gets or sets the numeric value to be used for the month (1-12).
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// The two-digit string representation of the month number (e.g., "01" for January).
        /// </summary>
        public string Value => Number.ToString("D2");
        /// <summary>
        /// Gets the text representation of the month.
        /// </summary>
        public string Text => Name;
        /// <summary>
        /// Returns a string that represents the current object, including its name and abbreviation.   
        /// </summary>
        /// <returns>A string in the format "Name (Abbreviation)", where Name and Abbreviation are the values of the
        /// corresponding properties.</returns>
        public override string ToString() => $"{Name} ({Abbreviation})";
    }
}