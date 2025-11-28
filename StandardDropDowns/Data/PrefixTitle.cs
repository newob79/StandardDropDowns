using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                new PrefixTitleInfo { Name = "Mister", Abbreviation = "Mr.",  Category = "Civilian", Number = 1 },
                new PrefixTitleInfo { Name = "Miss", Abbreviation = "Ms.", Category = "Civilian", Number = 2 },
                new PrefixTitleInfo { Name = "Mistress", Abbreviation = "Mrs.", Category = "Civilian", Number = 3 },
                new PrefixTitleInfo { Name = "Doctor", Abbreviation = "Dr.", Category = "Professional", Number = 4 },
                new PrefixTitleInfo { Name = "Professor", Abbreviation = "Prof.", Category = "Professional", Number = 5 }
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
