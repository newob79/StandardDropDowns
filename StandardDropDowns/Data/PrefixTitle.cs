using StandardDropdowns.Builders;
using StandardDropdowns.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StandardDropdowns.Data
{
    public class PrefixTitle
    {
        private static readonly Lazy<IReadOnlyList<PrefixTitleInfo>> _all =
            new Lazy<IReadOnlyList<PrefixTitleInfo>>(LoadAllTitles);
        /// <summary>
        /// Gets all prefix titles.
        /// </summary>
        public IReadOnlyList<PrefixTitleInfo> All => _all.Value;
        /// <summary>
        /// Creates a builder for customizing the prefix title list.
        /// </summary>
        /// <returns>A new PrefixTitleBuilder instance.</returns>
        public PrefixTitleBuilder Builder() => new PrefixTitleBuilder();
        private static IReadOnlyList<PrefixTitleInfo> LoadAllTitles()
        {
            return new List<PrefixTitleInfo>
            {
                // Civilian
                new PrefixTitleInfo { Name = "Mister", Abbreviation = "Mr.", Category = "Civilian", Number = 1 },
                new PrefixTitleInfo { Name = "Miss", Abbreviation = "Miss", Category = "Civilian", Number = 2 },
                new PrefixTitleInfo { Name = "Ms", Abbreviation = "Ms.", Category = "Civilian", Number = 3 },
                new PrefixTitleInfo { Name = "Mistress", Abbreviation = "Mrs.", Category = "Civilian", Number = 4 },
        
                // Professional
                new PrefixTitleInfo { Name = "Doctor", Abbreviation = "Dr.", Category = "Professional", Number = 5 },
                new PrefixTitleInfo { Name = "Professor", Abbreviation = "Prof.", Category = "Professional", Number = 6 },
        
                // Religious
                new PrefixTitleInfo { Name = "Reverend", Abbreviation = "Rev.", Category = "Religious", Number = 7 },
                new PrefixTitleInfo { Name = "Father", Abbreviation = "Fr.", Category = "Religious", Number = 8 },
        
                // Honorific
                new PrefixTitleInfo { Name = "Honorable", Abbreviation = "Hon.", Category = "Honorific", Number = 9 }
            }.AsReadOnly();
        }

        public PrefixTitleInfo ByNumber(int number)
        {
            return _all.Value.FirstOrDefault(t => t.Number == number);
        }

        public PrefixTitleInfo ByAbbreviation(string abbreviation)
        {
            if (string.IsNullOrWhiteSpace(abbreviation))
                return null;
            return _all.Value.FirstOrDefault(t =>
                t.Abbreviation.Equals(abbreviation.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public PrefixTitleInfo ByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;
            return _all.Value.FirstOrDefault(t =>
                t.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }
}
