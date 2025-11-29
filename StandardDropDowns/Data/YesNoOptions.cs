using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Builders;
using StandardDropdowns.Models;

namespace StandardDropdowns.Data
{
    /// <summary>
    /// Provides yes/no options for dropdown lists with various presets.
    /// </summary>
    public class YesNoOptions
    {
        private static readonly Lazy<IReadOnlyList<YesNoInfo>> _all =
            new Lazy<IReadOnlyList<YesNoInfo>>(LoadAllOptions);

        /// <summary>
        /// Gets all yes/no options across all presets.
        /// </summary>
        public IReadOnlyList<YesNoInfo> All => _all.Value;

        /// <summary>
        /// Gets basic Yes/No options only.
        /// </summary>
        public IReadOnlyList<YesNoInfo> Basic =>
            _all.Value.Where(o => o.Preset == "Basic").ToList().AsReadOnly();

        /// <summary>
        /// Gets Yes/No/N/A options.
        /// </summary>
        public IReadOnlyList<YesNoInfo> WithNA =>
            _all.Value.Where(o => o.Preset == "Basic" || o.Code == "NA").ToList().AsReadOnly();

        /// <summary>
        /// Gets Yes/No/Unknown options.
        /// </summary>
        public IReadOnlyList<YesNoInfo> WithUnknown =>
            _all.Value.Where(o => o.Preset == "Basic" || o.Code == "U").ToList().AsReadOnly();

        /// <summary>
        /// Finds an option by its code.
        /// </summary>
        /// <param name="code">The option code (case-insensitive).</param>
        /// <returns>The matching YesNoInfo, or null if not found.</returns>
        public YesNoInfo ByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;

            return _all.Value.FirstOrDefault(o =>
                o.Code.Equals(code.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds an option by its name.
        /// </summary>
        /// <param name="name">The option name (case-insensitive).</param>
        /// <returns>The matching YesNoInfo, or null if not found.</returns>
        public YesNoInfo ByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return _all.Value.FirstOrDefault(o =>
                o.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds an option by its number.
        /// </summary>
        /// <param name="number">The option number.</param>
        /// <returns>The matching YesNoInfo, or null if not found.</returns>
        public YesNoInfo ByNumber(int number)
        {
            return _all.Value.FirstOrDefault(o => o.Number == number);
        }

        /// <summary>
        /// Creates a builder for customizing the yes/no options list.
        /// </summary>
        /// <returns>A new YesNoOptionsBuilder instance.</returns>
        public YesNoOptionsBuilder Builder() => new YesNoOptionsBuilder();

        private static IReadOnlyList<YesNoInfo> LoadAllOptions()
        {
            return new List<YesNoInfo>
            {
                // Basic (Yes/No)
                new YesNoInfo { Name = "Yes", Code = "Y", Number = 1, Preset = "Basic" },
                new YesNoInfo { Name = "No", Code = "N", Number = 2, Preset = "Basic" },

                // Extended options
                new YesNoInfo { Name = "N/A", Code = "NA", Number = 3, Preset = "Extended" },
                new YesNoInfo { Name = "Unknown", Code = "U", Number = 4, Preset = "Extended" }
            }.AsReadOnly();
        }
    }
}