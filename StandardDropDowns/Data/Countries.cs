using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Builders;
using StandardDropdowns.Models;

namespace StandardDropdowns.Data
{
    /// <summary>
    /// Provides country data based on ISO 3166-1 standard for dropdown lists.
    /// </summary>
    public class Countries
    {
        private static readonly Lazy<IReadOnlyList<CountryInfo>> _all =
            new Lazy<IReadOnlyList<CountryInfo>>(LoadAllCountries);

        /// <summary>
        /// Gets all countries and territories.
        /// </summary>
        public IReadOnlyList<CountryInfo> All => _all.Value;

        /// <summary>
        /// Gets countries in a specific continent.
        /// </summary>
        /// <param name="continent">The continent name (e.g., "Europe", "Asia").</param>
        public IReadOnlyList<CountryInfo> ByContinent(string continent)
        {
            if (string.IsNullOrWhiteSpace(continent))
                return new List<CountryInfo>().AsReadOnly();

            return _all.Value
                .Where(c => c.Continent.Equals(continent.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList()
                .AsReadOnly();
        }

        /// <summary>
        /// Finds a country by its ISO 3166-1 alpha-2 code.
        /// </summary>
        /// <param name="alpha2Code">The two-letter code (case-insensitive).</param>
        /// <returns>The matching CountryInfo, or null if not found.</returns>
        public CountryInfo ByAlpha2Code(string alpha2Code)
        {
            if (string.IsNullOrWhiteSpace(alpha2Code))
                return null;

            return _all.Value.FirstOrDefault(c =>
                c.Alpha2Code.Equals(alpha2Code.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds a country by its ISO 3166-1 alpha-3 code.
        /// </summary>
        /// <param name="alpha3Code">The three-letter code (case-insensitive).</param>
        /// <returns>The matching CountryInfo, or null if not found.</returns>
        public CountryInfo ByAlpha3Code(string alpha3Code)
        {
            if (string.IsNullOrWhiteSpace(alpha3Code))
                return null;

            return _all.Value.FirstOrDefault(c =>
                c.Alpha3Code.Equals(alpha3Code.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds a country by its ISO 3166-1 numeric code.
        /// </summary>
        /// <param name="numericCode">The numeric code.</param>
        /// <returns>The matching CountryInfo, or null if not found.</returns>
        public CountryInfo ByNumericCode(string numericCode)
        {
            if (string.IsNullOrWhiteSpace(numericCode))
                return null;

            return _all.Value.FirstOrDefault(c =>
                c.NumericCode.Equals(numericCode.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds a country by its name.
        /// </summary>
        /// <param name="name">The country name (case-insensitive).</param>
        /// <returns>The matching CountryInfo, or null if not found.</returns>
        public CountryInfo ByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return _all.Value.FirstOrDefault(c =>
                c.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Creates a builder for customizing the country list.
        /// </summary>
        /// <returns>A new CountriesBuilder instance.</returns>
        public CountriesBuilder Builder() => new CountriesBuilder();

        private static IReadOnlyList<CountryInfo> LoadAllCountries()
        {
            return new List<CountryInfo>
            {
                // Africa
                new CountryInfo { Name = "Algeria", Alpha2Code = "DZ", Alpha3Code = "DZA", NumericCode = "012", Continent = "Africa" },
                new CountryInfo { Name = "Angola", Alpha2Code = "AO", Alpha3Code = "AGO", NumericCode = "024", Continent = "Africa" },
                new CountryInfo { Name = "Benin", Alpha2Code = "BJ", Alpha3Code = "BEN", NumericCode = "204", Continent = "Africa" },
                new CountryInfo { Name = "Botswana", Alpha2Code = "BW", Alpha3Code = "BWA", NumericCode = "072", Continent = "Africa" },
                new CountryInfo { Name = "Burkina Faso", Alpha2Code = "BF", Alpha3Code = "BFA", NumericCode = "854", Continent = "Africa" },
                new CountryInfo { Name = "Burundi", Alpha2Code = "BI", Alpha3Code = "BDI", NumericCode = "108", Continent = "Africa" },
                new CountryInfo { Name = "Cabo Verde", Alpha2Code = "CV", Alpha3Code = "CPV", NumericCode = "132", Continent = "Africa" },
                new CountryInfo { Name = "Cameroon", Alpha2Code = "CM", Alpha3Code = "CMR", NumericCode = "120", Continent = "Africa" },
                new CountryInfo { Name = "Central African Republic", Alpha2Code = "CF", Alpha3Code = "CAF", NumericCode = "140", Continent = "Africa" },
                new CountryInfo { Name = "Chad", Alpha2Code = "TD", Alpha3Code = "TCD", NumericCode = "148", Continent = "Africa" },
                new CountryInfo { Name = "Comoros", Alpha2Code = "KM", Alpha3Code = "COM", NumericCode = "174", Continent = "Africa" },
                new CountryInfo { Name = "Congo (Democratic Republic)", Alpha2Code = "CD", Alpha3Code = "COD", NumericCode = "180", Continent = "Africa" },
                new CountryInfo { Name = "Congo (Republic)", Alpha2Code = "CG", Alpha3Code = "COG", NumericCode = "178", Continent = "Africa" },
                new CountryInfo { Name = "Côte d'Ivoire", Alpha2Code = "CI", Alpha3Code = "CIV", NumericCode = "384", Continent = "Africa" },
                new CountryInfo { Name = "Djibouti", Alpha2Code = "DJ", Alpha3Code = "DJI", NumericCode = "262", Continent = "Africa" },
                new CountryInfo { Name = "Egypt", Alpha2Code = "EG", Alpha3Code = "EGY", NumericCode = "818", Continent = "Africa" },
                new CountryInfo { Name = "Equatorial Guinea", Alpha2Code = "GQ", Alpha3Code = "GNQ", NumericCode = "226", Continent = "Africa" },
                new CountryInfo { Name = "Eritrea", Alpha2Code = "ER", Alpha3Code = "ERI", NumericCode = "232", Continent = "Africa" },
                new CountryInfo { Name = "Eswatini", Alpha2Code = "SZ", Alpha3Code = "SWZ", NumericCode = "748", Continent = "Africa" },
                new CountryInfo { Name = "Ethiopia", Alpha2Code = "ET", Alpha3Code = "ETH", NumericCode = "231", Continent = "Africa" },
                new CountryInfo { Name = "Gabon", Alpha2Code = "GA", Alpha3Code = "GAB", NumericCode = "266", Continent = "Africa" },
                new CountryInfo { Name = "Gambia", Alpha2Code = "GM", Alpha3Code = "GMB", NumericCode = "270", Continent = "Africa" },
                new CountryInfo { Name = "Ghana", Alpha2Code = "GH", Alpha3Code = "GHA", NumericCode = "288", Continent = "Africa" },
                new CountryInfo { Name = "Guinea", Alpha2Code = "GN", Alpha3Code = "GIN", NumericCode = "324", Continent = "Africa" },
                new CountryInfo { Name = "Guinea-Bissau", Alpha2Code = "GW", Alpha3Code = "GNB", NumericCode = "624", Continent = "Africa" },
                new CountryInfo { Name = "Kenya", Alpha2Code = "KE", Alpha3Code = "KEN", NumericCode = "404", Continent = "Africa" },
                new CountryInfo { Name = "Lesotho", Alpha2Code = "LS", Alpha3Code = "LSO", NumericCode = "426", Continent = "Africa" },
                new CountryInfo { Name = "Liberia", Alpha2Code = "LR", Alpha3Code = "LBR", NumericCode = "430", Continent = "Africa" },
                new CountryInfo { Name = "Libya", Alpha2Code = "LY", Alpha3Code = "LBY", NumericCode = "434", Continent = "Africa" },
                new CountryInfo { Name = "Madagascar", Alpha2Code = "MG", Alpha3Code = "MDG", NumericCode = "450", Continent = "Africa" },
                new CountryInfo { Name = "Malawi", Alpha2Code = "MW", Alpha3Code = "MWI", NumericCode = "454", Continent = "Africa" },
                new CountryInfo { Name = "Mali", Alpha2Code = "ML", Alpha3Code = "MLI", NumericCode = "466", Continent = "Africa" },
                new CountryInfo { Name = "Mauritania", Alpha2Code = "MR", Alpha3Code = "MRT", NumericCode = "478", Continent = "Africa" },
                new CountryInfo { Name = "Mauritius", Alpha2Code = "MU", Alpha3Code = "MUS", NumericCode = "480", Continent = "Africa" },
                new CountryInfo { Name = "Morocco", Alpha2Code = "MA", Alpha3Code = "MAR", NumericCode = "504", Continent = "Africa" },
                new CountryInfo { Name = "Mozambique", Alpha2Code = "MZ", Alpha3Code = "MOZ", NumericCode = "508", Continent = "Africa" },
                new CountryInfo { Name = "Namibia", Alpha2Code = "NA", Alpha3Code = "NAM", NumericCode = "516", Continent = "Africa" },
                new CountryInfo { Name = "Niger", Alpha2Code = "NE", Alpha3Code = "NER", NumericCode = "562", Continent = "Africa" },
                new CountryInfo { Name = "Nigeria", Alpha2Code = "NG", Alpha3Code = "NGA", NumericCode = "566", Continent = "Africa" },
                new CountryInfo { Name = "Rwanda", Alpha2Code = "RW", Alpha3Code = "RWA", NumericCode = "646", Continent = "Africa" },
                new CountryInfo { Name = "São Tomé and Príncipe", Alpha2Code = "ST", Alpha3Code = "STP", NumericCode = "678", Continent = "Africa" },
                new CountryInfo { Name = "Senegal", Alpha2Code = "SN", Alpha3Code = "SEN", NumericCode = "686", Continent = "Africa" },
                new CountryInfo { Name = "Seychelles", Alpha2Code = "SC", Alpha3Code = "SYC", NumericCode = "690", Continent = "Africa" },
                new CountryInfo { Name = "Sierra Leone", Alpha2Code = "SL", Alpha3Code = "SLE", NumericCode = "694", Continent = "Africa" },
                new CountryInfo { Name = "Somalia", Alpha2Code = "SO", Alpha3Code = "SOM", NumericCode = "706", Continent = "Africa" },
                new CountryInfo { Name = "South Africa", Alpha2Code = "ZA", Alpha3Code = "ZAF", NumericCode = "710", Continent = "Africa" },
                new CountryInfo { Name = "South Sudan", Alpha2Code = "SS", Alpha3Code = "SSD", NumericCode = "728", Continent = "Africa" },
                new CountryInfo { Name = "Sudan", Alpha2Code = "SD", Alpha3Code = "SDN", NumericCode = "729", Continent = "Africa" },
                new CountryInfo { Name = "Tanzania", Alpha2Code = "TZ", Alpha3Code = "TZA", NumericCode = "834", Continent = "Africa" },
                new CountryInfo { Name = "Togo", Alpha2Code = "TG", Alpha3Code = "TGO", NumericCode = "768", Continent = "Africa" },
                new CountryInfo { Name = "Tunisia", Alpha2Code = "TN", Alpha3Code = "TUN", NumericCode = "788", Continent = "Africa" },
                new CountryInfo { Name = "Uganda", Alpha2Code = "UG", Alpha3Code = "UGA", NumericCode = "800", Continent = "Africa" },
                new CountryInfo { Name = "Zambia", Alpha2Code = "ZM", Alpha3Code = "ZMB", NumericCode = "894", Continent = "Africa" },
                new CountryInfo { Name = "Zimbabwe", Alpha2Code = "ZW", Alpha3Code = "ZWE", NumericCode = "716", Continent = "Africa" },

                // Antarctica
                new CountryInfo { Name = "Antarctica", Alpha2Code = "AQ", Alpha3Code = "ATA", NumericCode = "010", Continent = "Antarctica" },

                // Asia
                new CountryInfo { Name = "Afghanistan", Alpha2Code = "AF", Alpha3Code = "AFG", NumericCode = "004", Continent = "Asia" },
                new CountryInfo { Name = "Armenia", Alpha2Code = "AM", Alpha3Code = "ARM", NumericCode = "051", Continent = "Asia" },
                new CountryInfo { Name = "Azerbaijan", Alpha2Code = "AZ", Alpha3Code = "AZE", NumericCode = "031", Continent = "Asia" },
                new CountryInfo { Name = "Bahrain", Alpha2Code = "BH", Alpha3Code = "BHR", NumericCode = "048", Continent = "Asia" },
                new CountryInfo { Name = "Bangladesh", Alpha2Code = "BD", Alpha3Code = "BGD", NumericCode = "050", Continent = "Asia" },
                new CountryInfo { Name = "Bhutan", Alpha2Code = "BT", Alpha3Code = "BTN", NumericCode = "064", Continent = "Asia" },
                new CountryInfo { Name = "Brunei", Alpha2Code = "BN", Alpha3Code = "BRN", NumericCode = "096", Continent = "Asia" },
                new CountryInfo { Name = "Cambodia", Alpha2Code = "KH", Alpha3Code = "KHM", NumericCode = "116", Continent = "Asia" },
                new CountryInfo { Name = "China", Alpha2Code = "CN", Alpha3Code = "CHN", NumericCode = "156", Continent = "Asia" },
                new CountryInfo { Name = "Cyprus", Alpha2Code = "CY", Alpha3Code = "CYP", NumericCode = "196", Continent = "Asia" },
                new CountryInfo { Name = "Georgia", Alpha2Code = "GE", Alpha3Code = "GEO", NumericCode = "268", Continent = "Asia" },
                new CountryInfo { Name = "Hong Kong", Alpha2Code = "HK", Alpha3Code = "HKG", NumericCode = "344", Continent = "Asia" },
                new CountryInfo { Name = "India", Alpha2Code = "IN", Alpha3Code = "IND", NumericCode = "356", Continent = "Asia" },
                new CountryInfo { Name = "Indonesia", Alpha2Code = "ID", Alpha3Code = "IDN", NumericCode = "360", Continent = "Asia" },
                new CountryInfo { Name = "Iran", Alpha2Code = "IR", Alpha3Code = "IRN", NumericCode = "364", Continent = "Asia" },
                new CountryInfo { Name = "Iraq", Alpha2Code = "IQ", Alpha3Code = "IRQ", NumericCode = "368", Continent = "Asia" },
                new CountryInfo { Name = "Israel", Alpha2Code = "IL", Alpha3Code = "ISR", NumericCode = "376", Continent = "Asia" },
                new CountryInfo { Name = "Japan", Alpha2Code = "JP", Alpha3Code = "JPN", NumericCode = "392", Continent = "Asia" },
                new CountryInfo { Name = "Jordan", Alpha2Code = "JO", Alpha3Code = "JOR", NumericCode = "400", Continent = "Asia" },
                new CountryInfo { Name = "Kazakhstan", Alpha2Code = "KZ", Alpha3Code = "KAZ", NumericCode = "398", Continent = "Asia" },
                new CountryInfo { Name = "Kuwait", Alpha2Code = "KW", Alpha3Code = "KWT", NumericCode = "414", Continent = "Asia" },
                new CountryInfo { Name = "Kyrgyzstan", Alpha2Code = "KG", Alpha3Code = "KGZ", NumericCode = "417", Continent = "Asia" },
                new CountryInfo { Name = "Laos", Alpha2Code = "LA", Alpha3Code = "LAO", NumericCode = "418", Continent = "Asia" },
                new CountryInfo { Name = "Lebanon", Alpha2Code = "LB", Alpha3Code = "LBN", NumericCode = "422", Continent = "Asia" },
                new CountryInfo { Name = "Macau", Alpha2Code = "MO", Alpha3Code = "MAC", NumericCode = "446", Continent = "Asia" },
                new CountryInfo { Name = "Malaysia", Alpha2Code = "MY", Alpha3Code = "MYS", NumericCode = "458", Continent = "Asia" },
                new CountryInfo { Name = "Maldives", Alpha2Code = "MV", Alpha3Code = "MDV", NumericCode = "462", Continent = "Asia" },
                new CountryInfo { Name = "Mongolia", Alpha2Code = "MN", Alpha3Code = "MNG", NumericCode = "496", Continent = "Asia" },
                new CountryInfo { Name = "Myanmar", Alpha2Code = "MM", Alpha3Code = "MMR", NumericCode = "104", Continent = "Asia" },
                new CountryInfo { Name = "Nepal", Alpha2Code = "NP", Alpha3Code = "NPL", NumericCode = "524", Continent = "Asia" },
                new CountryInfo { Name = "North Korea", Alpha2Code = "KP", Alpha3Code = "PRK", NumericCode = "408", Continent = "Asia" },
                new CountryInfo { Name = "Oman", Alpha2Code = "OM", Alpha3Code = "OMN", NumericCode = "512", Continent = "Asia" },
                new CountryInfo { Name = "Pakistan", Alpha2Code = "PK", Alpha3Code = "PAK", NumericCode = "586", Continent = "Asia" },
                new CountryInfo { Name = "Palestine", Alpha2Code = "PS", Alpha3Code = "PSE", NumericCode = "275", Continent = "Asia" },
                new CountryInfo { Name = "Philippines", Alpha2Code = "PH", Alpha3Code = "PHL", NumericCode = "608", Continent = "Asia" },
                new CountryInfo { Name = "Qatar", Alpha2Code = "QA", Alpha3Code = "QAT", NumericCode = "634", Continent = "Asia" },
                new CountryInfo { Name = "Saudi Arabia", Alpha2Code = "SA", Alpha3Code = "SAU", NumericCode = "682", Continent = "Asia" },
                new CountryInfo { Name = "Singapore", Alpha2Code = "SG", Alpha3Code = "SGP", NumericCode = "702", Continent = "Asia" },
                new CountryInfo { Name = "South Korea", Alpha2Code = "KR", Alpha3Code = "KOR", NumericCode = "410", Continent = "Asia" },
                new CountryInfo { Name = "Sri Lanka", Alpha2Code = "LK", Alpha3Code = "LKA", NumericCode = "144", Continent = "Asia" },
                new CountryInfo { Name = "Syria", Alpha2Code = "SY", Alpha3Code = "SYR", NumericCode = "760", Continent = "Asia" },
                new CountryInfo { Name = "Taiwan", Alpha2Code = "TW", Alpha3Code = "TWN", NumericCode = "158", Continent = "Asia" },
                new CountryInfo { Name = "Tajikistan", Alpha2Code = "TJ", Alpha3Code = "TJK", NumericCode = "762", Continent = "Asia" },
                new CountryInfo { Name = "Thailand", Alpha2Code = "TH", Alpha3Code = "THA", NumericCode = "764", Continent = "Asia" },
                new CountryInfo { Name = "Timor-Leste", Alpha2Code = "TL", Alpha3Code = "TLS", NumericCode = "626", Continent = "Asia" },
                new CountryInfo { Name = "Turkey", Alpha2Code = "TR", Alpha3Code = "TUR", NumericCode = "792", Continent = "Asia" },
                new CountryInfo { Name = "Turkmenistan", Alpha2Code = "TM", Alpha3Code = "TKM", NumericCode = "795", Continent = "Asia" },
                new CountryInfo { Name = "United Arab Emirates", Alpha2Code = "AE", Alpha3Code = "ARE", NumericCode = "784", Continent = "Asia" },
                new CountryInfo { Name = "Uzbekistan", Alpha2Code = "UZ", Alpha3Code = "UZB", NumericCode = "860", Continent = "Asia" },
                new CountryInfo { Name = "Vietnam", Alpha2Code = "VN", Alpha3Code = "VNM", NumericCode = "704", Continent = "Asia" },
                new CountryInfo { Name = "Yemen", Alpha2Code = "YE", Alpha3Code = "YEM", NumericCode = "887", Continent = "Asia" },

                // Europe
                new CountryInfo { Name = "Albania", Alpha2Code = "AL", Alpha3Code = "ALB", NumericCode = "008", Continent = "Europe" },
                new CountryInfo { Name = "Andorra", Alpha2Code = "AD", Alpha3Code = "AND", NumericCode = "020", Continent = "Europe" },
                new CountryInfo { Name = "Austria", Alpha2Code = "AT", Alpha3Code = "AUT", NumericCode = "040", Continent = "Europe" },
                new CountryInfo { Name = "Belarus", Alpha2Code = "BY", Alpha3Code = "BLR", NumericCode = "112", Continent = "Europe" },
                new CountryInfo { Name = "Belgium", Alpha2Code = "BE", Alpha3Code = "BEL", NumericCode = "056", Continent = "Europe" },
                new CountryInfo { Name = "Bosnia and Herzegovina", Alpha2Code = "BA", Alpha3Code = "BIH", NumericCode = "070", Continent = "Europe" },
                new CountryInfo { Name = "Bulgaria", Alpha2Code = "BG", Alpha3Code = "BGR", NumericCode = "100", Continent = "Europe" },
                new CountryInfo { Name = "Croatia", Alpha2Code = "HR", Alpha3Code = "HRV", NumericCode = "191", Continent = "Europe" },
                new CountryInfo { Name = "Czech Republic", Alpha2Code = "CZ", Alpha3Code = "CZE", NumericCode = "203", Continent = "Europe" },
                new CountryInfo { Name = "Denmark", Alpha2Code = "DK", Alpha3Code = "DNK", NumericCode = "208", Continent = "Europe" },
                new CountryInfo { Name = "Estonia", Alpha2Code = "EE", Alpha3Code = "EST", NumericCode = "233", Continent = "Europe" },
                new CountryInfo { Name = "Finland", Alpha2Code = "FI", Alpha3Code = "FIN", NumericCode = "246", Continent = "Europe" },
                new CountryInfo { Name = "France", Alpha2Code = "FR", Alpha3Code = "FRA", NumericCode = "250", Continent = "Europe" },
                new CountryInfo { Name = "Germany", Alpha2Code = "DE", Alpha3Code = "DEU", NumericCode = "276", Continent = "Europe" },
                new CountryInfo { Name = "Greece", Alpha2Code = "GR", Alpha3Code = "GRC", NumericCode = "300", Continent = "Europe" },
                new CountryInfo { Name = "Hungary", Alpha2Code = "HU", Alpha3Code = "HUN", NumericCode = "348", Continent = "Europe" },
                new CountryInfo { Name = "Iceland", Alpha2Code = "IS", Alpha3Code = "ISL", NumericCode = "352", Continent = "Europe" },
                new CountryInfo { Name = "Ireland", Alpha2Code = "IE", Alpha3Code = "IRL", NumericCode = "372", Continent = "Europe" },
                new CountryInfo { Name = "Italy", Alpha2Code = "IT", Alpha3Code = "ITA", NumericCode = "380", Continent = "Europe" },
                new CountryInfo { Name = "Kosovo", Alpha2Code = "XK", Alpha3Code = "XKX", NumericCode = "383", Continent = "Europe" },
                new CountryInfo { Name = "Latvia", Alpha2Code = "LV", Alpha3Code = "LVA", NumericCode = "428", Continent = "Europe" },
                new CountryInfo { Name = "Liechtenstein", Alpha2Code = "LI", Alpha3Code = "LIE", NumericCode = "438", Continent = "Europe" },
                new CountryInfo { Name = "Lithuania", Alpha2Code = "LT", Alpha3Code = "LTU", NumericCode = "440", Continent = "Europe" },
                new CountryInfo { Name = "Luxembourg", Alpha2Code = "LU", Alpha3Code = "LUX", NumericCode = "442", Continent = "Europe" },
                new CountryInfo { Name = "Malta", Alpha2Code = "MT", Alpha3Code = "MLT", NumericCode = "470", Continent = "Europe" },
                new CountryInfo { Name = "Moldova", Alpha2Code = "MD", Alpha3Code = "MDA", NumericCode = "498", Continent = "Europe" },
                new CountryInfo { Name = "Monaco", Alpha2Code = "MC", Alpha3Code = "MCO", NumericCode = "492", Continent = "Europe" },
                new CountryInfo { Name = "Montenegro", Alpha2Code = "ME", Alpha3Code = "MNE", NumericCode = "499", Continent = "Europe" },
                new CountryInfo { Name = "Netherlands", Alpha2Code = "NL", Alpha3Code = "NLD", NumericCode = "528", Continent = "Europe" },
                new CountryInfo { Name = "North Macedonia", Alpha2Code = "MK", Alpha3Code = "MKD", NumericCode = "807", Continent = "Europe" },
                new CountryInfo { Name = "Norway", Alpha2Code = "NO", Alpha3Code = "NOR", NumericCode = "578", Continent = "Europe" },
                new CountryInfo { Name = "Poland", Alpha2Code = "PL", Alpha3Code = "POL", NumericCode = "616", Continent = "Europe" },
                new CountryInfo { Name = "Portugal", Alpha2Code = "PT", Alpha3Code = "PRT", NumericCode = "620", Continent = "Europe" },
                new CountryInfo { Name = "Romania", Alpha2Code = "RO", Alpha3Code = "ROU", NumericCode = "642", Continent = "Europe" },
                new CountryInfo { Name = "Russia", Alpha2Code = "RU", Alpha3Code = "RUS", NumericCode = "643", Continent = "Europe" },
                new CountryInfo { Name = "San Marino", Alpha2Code = "SM", Alpha3Code = "SMR", NumericCode = "674", Continent = "Europe" },
                new CountryInfo { Name = "Serbia", Alpha2Code = "RS", Alpha3Code = "SRB", NumericCode = "688", Continent = "Europe" },
                new CountryInfo { Name = "Slovakia", Alpha2Code = "SK", Alpha3Code = "SVK", NumericCode = "703", Continent = "Europe" },
                new CountryInfo { Name = "Slovenia", Alpha2Code = "SI", Alpha3Code = "SVN", NumericCode = "705", Continent = "Europe" },
                new CountryInfo { Name = "Spain", Alpha2Code = "ES", Alpha3Code = "ESP", NumericCode = "724", Continent = "Europe" },
                new CountryInfo { Name = "Sweden", Alpha2Code = "SE", Alpha3Code = "SWE", NumericCode = "752", Continent = "Europe" },
                new CountryInfo { Name = "Switzerland", Alpha2Code = "CH", Alpha3Code = "CHE", NumericCode = "756", Continent = "Europe" },
                new CountryInfo { Name = "Ukraine", Alpha2Code = "UA", Alpha3Code = "UKR", NumericCode = "804", Continent = "Europe" },
                new CountryInfo { Name = "United Kingdom", Alpha2Code = "GB", Alpha3Code = "GBR", NumericCode = "826", Continent = "Europe" },
                new CountryInfo { Name = "Vatican City", Alpha2Code = "VA", Alpha3Code = "VAT", NumericCode = "336", Continent = "Europe" },

                // North America
                new CountryInfo { Name = "Antigua and Barbuda", Alpha2Code = "AG", Alpha3Code = "ATG", NumericCode = "028", Continent = "North America" },
                new CountryInfo { Name = "Bahamas", Alpha2Code = "BS", Alpha3Code = "BHS", NumericCode = "044", Continent = "North America" },
                new CountryInfo { Name = "Barbados", Alpha2Code = "BB", Alpha3Code = "BRB", NumericCode = "052", Continent = "North America" },
                new CountryInfo { Name = "Belize", Alpha2Code = "BZ", Alpha3Code = "BLZ", NumericCode = "084", Continent = "North America" },
                new CountryInfo { Name = "Canada", Alpha2Code = "CA", Alpha3Code = "CAN", NumericCode = "124", Continent = "North America" },
                new CountryInfo { Name = "Costa Rica", Alpha2Code = "CR", Alpha3Code = "CRI", NumericCode = "188", Continent = "North America" },
                new CountryInfo { Name = "Cuba", Alpha2Code = "CU", Alpha3Code = "CUB", NumericCode = "192", Continent = "North America" },
                new CountryInfo { Name = "Dominica", Alpha2Code = "DM", Alpha3Code = "DMA", NumericCode = "212", Continent = "North America" },
                new CountryInfo { Name = "Dominican Republic", Alpha2Code = "DO", Alpha3Code = "DOM", NumericCode = "214", Continent = "North America" },
                new CountryInfo { Name = "El Salvador", Alpha2Code = "SV", Alpha3Code = "SLV", NumericCode = "222", Continent = "North America" },
                new CountryInfo { Name = "Grenada", Alpha2Code = "GD", Alpha3Code = "GRD", NumericCode = "308", Continent = "North America" },
                new CountryInfo { Name = "Guatemala", Alpha2Code = "GT", Alpha3Code = "GTM", NumericCode = "320", Continent = "North America" },
                new CountryInfo { Name = "Haiti", Alpha2Code = "HT", Alpha3Code = "HTI", NumericCode = "332", Continent = "North America" },
                new CountryInfo { Name = "Honduras", Alpha2Code = "HN", Alpha3Code = "HND", NumericCode = "340", Continent = "North America" },
                new CountryInfo { Name = "Jamaica", Alpha2Code = "JM", Alpha3Code = "JAM", NumericCode = "388", Continent = "North America" },
                new CountryInfo { Name = "Mexico", Alpha2Code = "MX", Alpha3Code = "MEX", NumericCode = "484", Continent = "North America" },
                new CountryInfo { Name = "Nicaragua", Alpha2Code = "NI", Alpha3Code = "NIC", NumericCode = "558", Continent = "North America" },
                new CountryInfo { Name = "Panama", Alpha2Code = "PA", Alpha3Code = "PAN", NumericCode = "591", Continent = "North America" },
                new CountryInfo { Name = "Saint Kitts and Nevis", Alpha2Code = "KN", Alpha3Code = "KNA", NumericCode = "659", Continent = "North America" },
                new CountryInfo { Name = "Saint Lucia", Alpha2Code = "LC", Alpha3Code = "LCA", NumericCode = "662", Continent = "North America" },
                new CountryInfo { Name = "Saint Vincent and the Grenadines", Alpha2Code = "VC", Alpha3Code = "VCT", NumericCode = "670", Continent = "North America" },
                new CountryInfo { Name = "Trinidad and Tobago", Alpha2Code = "TT", Alpha3Code = "TTO", NumericCode = "780", Continent = "North America" },
                new CountryInfo { Name = "United States", Alpha2Code = "US", Alpha3Code = "USA", NumericCode = "840", Continent = "North America" },

                // Oceania
                new CountryInfo { Name = "Australia", Alpha2Code = "AU", Alpha3Code = "AUS", NumericCode = "036", Continent = "Oceania" },
                new CountryInfo { Name = "Fiji", Alpha2Code = "FJ", Alpha3Code = "FJI", NumericCode = "242", Continent = "Oceania" },
                new CountryInfo { Name = "Kiribati", Alpha2Code = "KI", Alpha3Code = "KIR", NumericCode = "296", Continent = "Oceania" },
                new CountryInfo { Name = "Marshall Islands", Alpha2Code = "MH", Alpha3Code = "MHL", NumericCode = "584", Continent = "Oceania" },
                new CountryInfo { Name = "Micronesia", Alpha2Code = "FM", Alpha3Code = "FSM", NumericCode = "583", Continent = "Oceania" },
                new CountryInfo { Name = "Nauru", Alpha2Code = "NR", Alpha3Code = "NRU", NumericCode = "520", Continent = "Oceania" },
                new CountryInfo { Name = "New Zealand", Alpha2Code = "NZ", Alpha3Code = "NZL", NumericCode = "554", Continent = "Oceania" },
                new CountryInfo { Name = "Palau", Alpha2Code = "PW", Alpha3Code = "PLW", NumericCode = "585", Continent = "Oceania" },
                new CountryInfo { Name = "Papua New Guinea", Alpha2Code = "PG", Alpha3Code = "PNG", NumericCode = "598", Continent = "Oceania" },
                new CountryInfo { Name = "Samoa", Alpha2Code = "WS", Alpha3Code = "WSM", NumericCode = "882", Continent = "Oceania" },
                new CountryInfo { Name = "Solomon Islands", Alpha2Code = "SB", Alpha3Code = "SLB", NumericCode = "090", Continent = "Oceania" },
                new CountryInfo { Name = "Tonga", Alpha2Code = "TO", Alpha3Code = "TON", NumericCode = "776", Continent = "Oceania" },
                new CountryInfo { Name = "Tuvalu", Alpha2Code = "TV", Alpha3Code = "TUV", NumericCode = "798", Continent = "Oceania" },
                new CountryInfo { Name = "Vanuatu", Alpha2Code = "VU", Alpha3Code = "VUT", NumericCode = "548", Continent = "Oceania" },

                // South America
                new CountryInfo { Name = "Argentina", Alpha2Code = "AR", Alpha3Code = "ARG", NumericCode = "032", Continent = "South America" },
                new CountryInfo { Name = "Bolivia", Alpha2Code = "BO", Alpha3Code = "BOL", NumericCode = "068", Continent = "South America" },
                new CountryInfo { Name = "Brazil", Alpha2Code = "BR", Alpha3Code = "BRA", NumericCode = "076", Continent = "South America" },
                new CountryInfo { Name = "Chile", Alpha2Code = "CL", Alpha3Code = "CHL", NumericCode = "152", Continent = "South America" },
                new CountryInfo { Name = "Colombia", Alpha2Code = "CO", Alpha3Code = "COL", NumericCode = "170", Continent = "South America" },
                new CountryInfo { Name = "Ecuador", Alpha2Code = "EC", Alpha3Code = "ECU", NumericCode = "218", Continent = "South America" },
                new CountryInfo { Name = "Guyana", Alpha2Code = "GY", Alpha3Code = "GUY", NumericCode = "328", Continent = "South America" },
                new CountryInfo { Name = "Paraguay", Alpha2Code = "PY", Alpha3Code = "PRY", NumericCode = "600", Continent = "South America" },
                new CountryInfo { Name = "Peru", Alpha2Code = "PE", Alpha3Code = "PER", NumericCode = "604", Continent = "South America" },
                new CountryInfo { Name = "Suriname", Alpha2Code = "SR", Alpha3Code = "SUR", NumericCode = "740", Continent = "South America" },
                new CountryInfo { Name = "Uruguay", Alpha2Code = "UY", Alpha3Code = "URY", NumericCode = "858", Continent = "South America" },
                new CountryInfo { Name = "Venezuela", Alpha2Code = "VE", Alpha3Code = "VEN", NumericCode = "862", Continent = "South America" }
            }.AsReadOnly();
        }
    }
}