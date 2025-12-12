# StandardDropdowns

[![NuGet](https://img.shields.io/nuget/v/StandardDropdowns.svg)](https://www.nuget.org/packages/StandardDropdowns/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A comprehensive .NET library providing standardized reference data for dropdown/select lists. Stop recreating the same dropdown data across projects—use StandardDropdowns for consistent, well-tested reference data.

## Why StandardDropdowns?

How many times have you implemented a US States dropdown? Or a countries list? Or months of the year? StandardDropdowns eliminates this repetitive work by providing:

✅ **Accurate, standardized data** - ISO codes, official names, proper abbreviations  
✅ **Framework agnostic** - Works with ASP.NET Core, Blazor, WinForms, WPF, MAUI, and more  
✅ **Zero dependencies** - Pure .NET Standard 2.0 implementation  
✅ **Flexible API** - Simple static access for common cases, builder pattern for customization  
✅ **Thread-safe & immutable** - Safe for concurrent access  
✅ **Comprehensive test coverage** - Battle-tested with extensive unit tests

## Installation

Install via NuGet Package Manager:

```bash
dotnet add package StandardDropdowns
```

Or via Package Manager Console:

```powershell
Install-Package StandardDropdowns
```

## Quick Start

```csharp
using StandardDropdowns;

// Get all 50 US states
var states = DropdownData.UsStates.States50;

// Get all countries
var countries = DropdownData.Countries.All;

// Get all months
var months = DropdownData.Months.All;

// Generate a range of numbers
var quantities = DropdownData.Numbers.Range(1, 10);

// Get last 100 years for birth date dropdown
var birthYears = DropdownData.Years.Last(100);

// Lookup specific items
var texas = DropdownData.UsStates.ByAbbreviation("TX");
var usa = DropdownData.Countries.ByAlpha2Code("US");
var january = DropdownData.Months.ByNumber(1);
```

## Available Data Sets

StandardDropdowns provides the following reference data:

### Geographic Data

- **US States** - 50 states, District of Columbia, and 5 territories
- **Canadian Provinces** - 10 provinces and 3 territories with Canada Post abbreviations
- **Mexican States** - 31 states and CDMX with ISO 3166-2:MX codes
- **Countries** - 200+ countries with ISO 3166-1 codes, organized by continent
- **Time Zones** - Curated list of common time zones with IANA/Windows IDs

### Temporal Data

- **Months** - 12 months with names, abbreviations, and numbers
- **Days of Week** - 7 days with multiple abbreviation formats
- **Years** - Dynamic year ranges with rolling windows (Last, Next, Range)
- **Numbers** - Configurable numeric ranges with optional step

### Personal/Demographic Data

- **Prefixes/Titles** - Mr., Mrs., Ms., Dr., Prof., Rev., etc. (9 options)
- **Suffixes** - Jr., Sr., PhD, MD, Esq., etc. (22 options across 3 categories)
- **Gender** - Configurable gender options
- **Marital Status** - Single, Married, Divorced, Widowed, Separated, Domestic Partnership

### General Purpose Data

- **Yes/No Options** - Multiple presets (Basic, With N/A, With Unknown)

## Usage Examples

### US States

```csharp
using StandardDropdowns;

// Get different state groupings
var states50 = DropdownData.UsStates.States50;           // Just the 50 states
var statesAndDC = DropdownData.UsStates.StatesAndDC;     // 50 states + DC
var territories = DropdownData.UsStates.Territories;      // US territories only
var all = DropdownData.UsStates.All;                     // Everything

// Lookup by abbreviation or name
var texas = DropdownData.UsStates.ByAbbreviation("TX");
var california = DropdownData.UsStates.ByName("California");

// Use with ASP.NET Core MVC
@Html.DropDownListFor(m => m.StateCode, 
    new SelectList(DropdownData.UsStates.States50, "Value", "Text"))

// Use with Blazor
<select @bind="selectedState">
    @foreach (var state in DropdownData.UsStates.States50)
    {
        <option value="@state.Value">@state.Text</option>
    }
</select>

// Custom builder
var customStates = DropdownData.UsStates.Builder()
    .IncludeDC()
    .IncludeTerritories()
    .Exclude("AS", "GU", "MP")  // Exclude specific territories
    .OrderByName()
    .Build();
```

### Canadian Provinces

```csharp
using StandardDropdowns;

// Get different groupings
var provinces = DropdownData.CanadianProvinces.Provinces;     // Just the 10 provinces
var territories = DropdownData.CanadianProvinces.Territories; // 3 territories only
var all = DropdownData.CanadianProvinces.All;                 // Everything

// Lookup by abbreviation or name
var ontario = DropdownData.CanadianProvinces.ByAbbreviation("ON");
var quebec = DropdownData.CanadianProvinces.ByName("Quebec");

// Custom builder
var customProvinces = DropdownData.CanadianProvinces.Builder()
    .IncludeTerritories()
    .Exclude("NU", "NT")
    .OrderByName()
    .Build();
```

### Mexican States

```csharp
using StandardDropdowns;

// Get different groupings
var states = DropdownData.MexicanStates.States;         // Just the 31 states
var statesAndCDMX = DropdownData.MexicanStates.StatesAndCDMX; // 31 states + CDMX
var all = DropdownData.MexicanStates.All;               // Everything

// Lookup by abbreviation or name
var jalisco = DropdownData.MexicanStates.ByAbbreviation("JAL");
var cdmx = DropdownData.MexicanStates.ByName("Ciudad de México");

// Custom builder
var customStates = DropdownData.MexicanStates.Builder()
    .IncludeCDMX()
    .OrderByName()
    .Build();
```

### Countries

```csharp
using StandardDropdowns;

// Get all countries
var allCountries = DropdownData.Countries.All;

// Filter by continent
var europeanCountries = DropdownData.Countries.ByContinent("Europe");

// Lookup by different codes
var usa = DropdownData.Countries.ByAlpha2Code("US");        // By 2-letter code
var canada = DropdownData.Countries.ByAlpha3Code("CAN");    // By 3-letter code
var germany = DropdownData.Countries.ByNumericCode("276");  // By numeric code
var france = DropdownData.Countries.ByName("France");       // By name

// Custom builder - North American countries only
var northAmerica = DropdownData.Countries.Builder()
    .InContinent("North America")
    .OrderByName()
    .Build();

// European Union subset (example)
var euCountries = DropdownData.Countries.Builder()
    .Only("AT", "BE", "BG", "HR", "CY", "CZ", "DK", "EE", "FI", 
          "FR", "DE", "GR", "HU", "IE", "IT", "LV", "LT", "LU",
          "MT", "NL", "PL", "PT", "RO", "SK", "SI", "ES", "SE")
    .OrderByName()
    .Build();
```

### Time Zones

```csharp
using StandardDropdowns;

// Get all curated time zones
var allZones = DropdownData.TimeZones.All;

// Filter by region
var americasZones = DropdownData.TimeZones.ByRegion("Americas");
var europeZones = DropdownData.TimeZones.ByRegion("Europe");

// Lookup by different identifiers
var eastern = DropdownData.TimeZones.ByIanaId("America/New_York");
var pst = DropdownData.TimeZones.ByAbbreviation("PST");

// Custom builder
var customZones = DropdownData.TimeZones.Builder()
    .InRegion("Americas", "Europe")
    .OrderByOffset()
    .Build();
```

### Years (Dynamic Rolling Windows)

```csharp
using StandardDropdowns;

// Last 100 years - perfect for birth dates (descending order)
var birthYears = DropdownData.Years.Last(100);

// Next 10 years - perfect for expiration dates (ascending order)
var expirationYears = DropdownData.Years.Next(10);

// Explicit range
var customYears = DropdownData.Years.Range(1990, 2030);
```

### Numbers

```csharp
using StandardDropdowns;

// Simple range
var oneToTen = DropdownData.Numbers.Range(1, 10);

// With step parameter
var evens = DropdownData.Numbers.Range(2, 20, step: 2);

// Descending order
var countdown = DropdownData.Numbers.Range(10, 1);
```

### Months and Days

```csharp
using StandardDropdowns;

// Get all months
var months = DropdownData.Months.All;

// Lookup specific month
var january = DropdownData.Months.ByNumber(1);
var december = DropdownData.Months.ByName("December");
var feb = DropdownData.Months.ByAbbreviation("Feb");

// Get all days of the week
var days = DropdownData.Days.All;

// Lookup specific day
var monday = DropdownData.Days.ByNumber(1);  // 0 = Sunday, 1 = Monday, etc.
var friday = DropdownData.Days.ByName("Friday");
var sat = DropdownData.Days.ByAbbreviation("Sat");
var su = DropdownData.Days.ByShortAbbreviation("Su");
```

### Prefixes and Suffixes

```csharp
using StandardDropdowns;

// Get all prefix titles
var prefixes = DropdownData.PrefixTitles.All;

// Lookup specific prefix
var doctor = DropdownData.PrefixTitles.ByNumber(5);
var mr = DropdownData.PrefixTitles.ByAbbreviation("Mr.");

// Filter by category
var civilianOnly = DropdownData.PrefixTitles.Builder()
    .InCategory("Civilian")
    .Build();

// Get all suffixes
var suffixes = DropdownData.Suffixes.All;

// Filter by category
var academicDegrees = DropdownData.Suffixes.ByCategory("Academic");
var generational = DropdownData.Suffixes.ByCategory("Generational");

// Custom suffix list
var doctorates = DropdownData.Suffixes.Builder()
    .InCategory("Academic")
    .Only("7", "8", "9", "10")  // PhD, MD, DDS, EdD
    .OrderByName()
    .Build();
```

### Gender and Marital Status

```csharp
using StandardDropdowns;

// Get gender options
var genders = DropdownData.Genders.All;
var male = DropdownData.Genders.ByCode("M");

// Get marital statuses
var statuses = DropdownData.MaritalStatuses.All;
var married = DropdownData.MaritalStatuses.ByCode("M");
var single = DropdownData.MaritalStatuses.ByName("Single");
```

### Yes/No Options

```csharp
using StandardDropdowns;

// Get different yes/no presets
var basic = DropdownData.YesNo.Basic;              // Yes, No
var withNA = DropdownData.YesNo.WithNA;            // Yes, No, N/A
var withUnknown = DropdownData.YesNo.WithUnknown;  // Yes, No, Unknown

// Lookup specific option
var yes = DropdownData.YesNo.ByCode("Y");

// Custom builder
var customOptions = DropdownData.YesNo.Builder()
    .IncludeNA()
    .OrderByName()
    .Build();
```

## The ISelectOption Interface

All models implement the `ISelectOption` interface, making them easy to use with any UI framework:

```csharp
public interface ISelectOption
{
    string Value { get; }  // The value to submit (e.g., "TX", "US", "1")
    string Text { get; }   // The display text (e.g., "Texas", "United States", "January")
}
```

This means you can use any dropdown data with generic rendering code:

```csharp
// Generic method that works with ANY dropdown data
public SelectList CreateSelectList<T>(IEnumerable<T> items) 
    where T : ISelectOption
{
    return new SelectList(items, "Value", "Text");
}

// Use it with any data type
var statesList = CreateSelectList(DropdownData.UsStates.States50);
var countriesList = CreateSelectList(DropdownData.Countries.All);
var monthsList = CreateSelectList(DropdownData.Months.All);
```

## Builder Pattern

StandardDropdowns uses a powerful builder pattern for customization:

```csharp
// Example: Custom state list for a registration form
var registrationStates = DropdownData.UsStates.Builder()
    .IncludeDC()                    // Include Washington DC
    .Exclude("AS", "GU", "MP", "VI") // Exclude some territories
    .OrderByName()                   // Sort alphabetically
    .Build();

// Example: Major countries for shipping
var shippingCountries = DropdownData.Countries.Builder()
    .Only("US", "CA", "MX", "GB", "FR", "DE", "AU", "JP")
    .OrderByName()
    .Build();

// Example: Academic suffixes only
var academicSuffixes = DropdownData.Suffixes.Builder()
    .InCategory("Academic")
    .ExcludeCategory("Professional")
    .OrderByName()
    .Build();

// Example: Time zones sorted by offset
var sortedZones = DropdownData.TimeZones.Builder()
    .InRegion("Americas")
    .OrderByOffset()
    .Build();
```

### Common Builder Methods

All builders support these methods:

- `Exclude(params string[] values)` - Exclude specific items by value
- `Only(params string[] values)` - Include only specific items
- `OrderByText()` - Sort by display text (ascending)
- `OrderByTextDescending()` - Sort by display text (descending)
- `OrderByValue()` - Sort by value (ascending)
- `OrderByValueDescending()` - Sort by value (descending)
- `Build()` - Build the final read-only list

Category-specific builders have additional methods:

**US States:**
- `IncludeDC()` - Include District of Columbia
- `IncludeTerritories()` - Include US territories
- `IncludeAll()` - Include everything
- `ExcludeStates()` - Exclude the 50 states
- `OrderByName()`, `OrderByAbbreviation()` - Convenience methods

**Canadian Provinces:**
- `IncludeTerritories()` - Include territories
- `ExcludeTerritories()` - Exclude territories
- `OrderByName()`, `OrderByAbbreviation()` - Convenience methods

**Mexican States:**
- `IncludeCDMX()` - Include Ciudad de México (federal district)
- `OrderByName()`, `OrderByAbbreviation()` - Convenience methods

**Countries:**
- `InContinent(params string[] continents)` - Include specific continents
- `ExcludeContinent(params string[] continents)` - Exclude continents
- `OrderByName()`, `OrderByCode()` - Convenience methods

**Time Zones:**
- `InRegion(params string[] regions)` - Include specific regions
- `ExcludeRegion(params string[] regions)` - Exclude regions
- `OrderByOffset()` - Sort by UTC offset (west to east)
- `OrderByOffsetDescending()` - Sort by UTC offset (east to west)

**Prefixes/Suffixes:**
- `InCategory(params string[] categories)` - Include specific categories
- `ExcludeCategory(params string[] categories)` - Exclude categories

**Yes/No:**
- `InPreset(params string[] presets)` - Include specific presets
- `ExcludePreset(params string[] presets)` - Exclude presets
- `IncludeNA()` - Include Yes, No, N/A
- `IncludeUnknown()` - Include Yes, No, Unknown

## Framework Examples

### ASP.NET Core MVC

```csharp
// In your controller
public IActionResult Create()
{
    ViewBag.States = new SelectList(
        DropdownData.UsStates.States50, 
        "Value", 
        "Text"
    );
    ViewBag.Years = new SelectList(
        DropdownData.Years.Last(100),
        "Value",
        "Text"
    );
    return View();
}

// In your view
@Html.DropDownListFor(
    m => m.StateCode, 
    (SelectList)ViewBag.States,
    "Select a state...",
    new { @class = "form-control" }
)
```

### Blazor

```razor
<InputSelect @bind-Value="model.CountryCode" class="form-control">
    <option value="">Select a country...</option>
    @foreach (var country in DropdownData.Countries.All)
    {
        <option value="@country.Value">@country.Text</option>
    }
</InputSelect>

<InputSelect @bind-Value="model.BirthYear" class="form-control">
    <option value="">Select year...</option>
    @foreach (var year in DropdownData.Years.Last(100))
    {
        <option value="@year.Value">@year.Text</option>
    }
</InputSelect>
```

### WinForms

```csharp
// Bind to a ComboBox
stateComboBox.DataSource = DropdownData.UsStates.States50.ToList();
stateComboBox.DisplayMember = "Text";
stateComboBox.ValueMember = "Value";
```

### WPF

```xml
<ComboBox ItemsSource="{Binding States}" 
          DisplayMemberPath="Text" 
          SelectedValuePath="Value" 
          SelectedValue="{Binding SelectedState}" />
```

```csharp
// In your ViewModel
public IEnumerable<StateInfo> States => DropdownData.UsStates.States50;
```

### MAUI

```xml
<Picker Title="Select State" 
        ItemsSource="{Binding States}" 
        ItemDisplayBinding="{Binding Text}" 
        SelectedItem="{Binding SelectedState}" />
```

## Data Accuracy

StandardDropdowns uses authoritative sources for all data:

- **US States**: Official USPS abbreviations and names
- **Canadian Provinces**: Canada Post official abbreviations
- **Mexican States**: ISO 3166-2:MX codes
- **Countries**: ISO 3166-1 standard (alpha-2, alpha-3, and numeric codes)
- **Time Zones**: IANA Time Zone Database with Windows mappings
- **Months/Days**: Standard calendar conventions
- **Prefixes/Suffixes**: Common professional and social conventions

All data is thoroughly tested with comprehensive unit test coverage.

## Thread Safety

All data providers use lazy initialization and return immutable read-only collections, making them safe for concurrent access:

```csharp
// Safe to call from multiple threads
Parallel.For(0, 100, i =>
{
    var states = DropdownData.UsStates.All;  // Thread-safe
    var texas = DropdownData.UsStates.ByAbbreviation("TX");  // Thread-safe
});
```

## Requirements

**.NET Standard 2.0** - Compatible with:

- .NET Framework 4.6.1+
- .NET Core 2.0+
- .NET 5, 6, 7, 8+
- Xamarin
- MAUI

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

Areas for contribution:
- Additional data sets (languages, currencies, etc.)
- Localization support
- Additional builder methods
- Bug fixes and improvements

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

## Changelog

### v1.1.0
- Added Canadian Provinces (10 provinces + 3 territories)
- Added Mexican States (31 states + CDMX)
- Added Time Zones (curated list with IANA/Windows IDs)
- Added Years with rolling windows (Last, Next, Range)
- Added Numbers utility for numeric ranges

### v1.0.0
- Initial release
- US States, Countries, Months, Days
- Prefix Titles, Suffixes, Genders, Marital Status, Yes/No

## Roadmap

Future versions may include:

### Additional Data Sets
- Languages (ISO 639)
- Currencies (ISO 4217)
- Industry classifications (NAICS)
- Education levels
- Phone/address types
- Employment statuses
- Company sizes
- Payment methods

### Localization Support
- Translated country/month names
- Culture-specific formats

### Conversion Methods
- Direct conversion to framework-specific types
- Custom mapping support

## Support

If you encounter any issues or have questions:

- 📝 [Open an issue](https://github.com/newob79/StandardDropdowns/issues)
- 💬 [Start a discussion](https://github.com/newob79/StandardDropdowns/discussions)

## Acknowledgments

Built with care to save developers time and ensure data consistency across .NET applications.

---

Made with ❤️ for the .NET community