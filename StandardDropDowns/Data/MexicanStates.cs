using System;
using System.Collections.Generic;
using System.Linq;
using StandardDropdowns.Builders;
using StandardDropdowns.Models;

namespace StandardDropdowns.Data
{
    /// <summary>
    /// Provides Mexican state and federal district data for dropdown lists.
    /// </summary>
    public class MexicanStates
    {
        private static readonly Lazy<IReadOnlyList<MexicanStateInfo>> _all =
            new Lazy<IReadOnlyList<MexicanStateInfo>>(LoadAllStates);

        /// <summary>
        /// Gets all Mexican states and the federal district (Ciudad de México).
        /// </summary>
        public IReadOnlyList<MexicanStateInfo> All => _all.Value;

        /// <summary>
        /// Gets only the 31 Mexican states (excludes Ciudad de México).
        /// </summary>
        public IReadOnlyList<MexicanStateInfo> States =>
            _all.Value.Where(s => !s.IsFederalDistrict).ToList().AsReadOnly();

        /// <summary>
        /// Gets the 31 Mexican states plus Ciudad de México.
        /// </summary>
        public IReadOnlyList<MexicanStateInfo> StatesAndCDMX => All;

        /// <summary>
        /// Finds a state by its ISO 3166-2:MX abbreviation.
        /// </summary>
        /// <param name="abbreviation">The abbreviation (case-insensitive).</param>
        /// <returns>The matching MexicanStateInfo, or null if not found.</returns>
        public MexicanStateInfo ByAbbreviation(string abbreviation)
        {
            if (string.IsNullOrWhiteSpace(abbreviation))
                return null;

            return _all.Value.FirstOrDefault(s =>
                s.Abbreviation.Equals(abbreviation.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds a state by its full name.
        /// </summary>
        /// <param name="name">The full state name (case-insensitive).</param>
        /// <returns>The matching MexicanStateInfo, or null if not found.</returns>
        public MexicanStateInfo ByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return _all.Value.FirstOrDefault(s =>
                s.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Creates a builder for customizing the state list.
        /// </summary>
        /// <returns>A new MexicanStatesBuilder instance.</returns>
        public MexicanStatesBuilder Builder() => new MexicanStatesBuilder();

        private static IReadOnlyList<MexicanStateInfo> LoadAllStates()
        {
            return new List<MexicanStateInfo>
            {
                // 31 Mexican States (alphabetically by name)
                new MexicanStateInfo { Name = "Aguascalientes", Abbreviation = "AGU" },
                new MexicanStateInfo { Name = "Baja California", Abbreviation = "BCN" },
                new MexicanStateInfo { Name = "Baja California Sur", Abbreviation = "BCS" },
                new MexicanStateInfo { Name = "Campeche", Abbreviation = "CAM" },
                new MexicanStateInfo { Name = "Chiapas", Abbreviation = "CHP" },
                new MexicanStateInfo { Name = "Chihuahua", Abbreviation = "CHH" },
                new MexicanStateInfo { Name = "Coahuila", Abbreviation = "COA" },
                new MexicanStateInfo { Name = "Colima", Abbreviation = "COL" },
                new MexicanStateInfo { Name = "Durango", Abbreviation = "DUR" },
                new MexicanStateInfo { Name = "Guanajuato", Abbreviation = "GUA" },
                new MexicanStateInfo { Name = "Guerrero", Abbreviation = "GRO" },
                new MexicanStateInfo { Name = "Hidalgo", Abbreviation = "HID" },
                new MexicanStateInfo { Name = "Jalisco", Abbreviation = "JAL" },
                new MexicanStateInfo { Name = "México", Abbreviation = "MEX" },
                new MexicanStateInfo { Name = "Michoacán", Abbreviation = "MIC" },
                new MexicanStateInfo { Name = "Morelos", Abbreviation = "MOR" },
                new MexicanStateInfo { Name = "Nayarit", Abbreviation = "NAY" },
                new MexicanStateInfo { Name = "Nuevo León", Abbreviation = "NLE" },
                new MexicanStateInfo { Name = "Oaxaca", Abbreviation = "OAX" },
                new MexicanStateInfo { Name = "Puebla", Abbreviation = "PUE" },
                new MexicanStateInfo { Name = "Querétaro", Abbreviation = "QUE" },
                new MexicanStateInfo { Name = "Quintana Roo", Abbreviation = "ROO" },
                new MexicanStateInfo { Name = "San Luis Potosí", Abbreviation = "SLP" },
                new MexicanStateInfo { Name = "Sinaloa", Abbreviation = "SIN" },
                new MexicanStateInfo { Name = "Sonora", Abbreviation = "SON" },
                new MexicanStateInfo { Name = "Tabasco", Abbreviation = "TAB" },
                new MexicanStateInfo { Name = "Tamaulipas", Abbreviation = "TAM" },
                new MexicanStateInfo { Name = "Tlaxcala", Abbreviation = "TLA" },
                new MexicanStateInfo { Name = "Veracruz", Abbreviation = "VER" },
                new MexicanStateInfo { Name = "Yucatán", Abbreviation = "YUC" },
                new MexicanStateInfo { Name = "Zacatecas", Abbreviation = "ZAC" },

                // Federal District
                new MexicanStateInfo { Name = "Ciudad de México", Abbreviation = "CMX", IsFederalDistrict = true }
            }.AsReadOnly();
        }
    }
}