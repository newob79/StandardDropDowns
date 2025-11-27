using StandardDropdowns.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StandardDropDowns.Models
{
    public class MonthInfo : ISelectOption
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int Number { get; set; }

        public string Value => Number.ToString("D2");
        public string Text => Name;
        public override string ToString() => $"{Name} ({Abbreviation})";
    }
}