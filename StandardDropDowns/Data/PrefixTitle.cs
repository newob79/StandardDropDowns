using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Models;

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
        private static IReadOnlyList<PrefixTitleInfo> LoadAllTitles()
        {
            return new List<PrefixTitleInfo>
            {
                new PrefixTitleInfo { Name = "Mr.", Abbreviation = "Mister",  Category = "Civilian", Number = 1 },
                new PrefixTitleInfo { Name = "Ms.", Abbreviation = "Miss", Category = "Civilian", Number = 2 },
                new PrefixTitleInfo { Name = "Mrs.", Abbreviation = "Mistress", Category = "Civilian", Number = 3 },
                new PrefixTitleInfo { Name = "Dr.", Abbreviation = "Doctor", Category = "Professional", Number = 4 },
                new PrefixTitleInfo { Name = "Prof.", Abbreviation = "Professor", Category = "Professional", Number = 5 }
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
