using FluentAssertions;
using StandardDropdowns;
using StandardDropdowns.Interfaces;
using StandardDropdowns.Models;

namespace StandardDropdowns.Tests;

public class CountriesTests
{
    #region Data Accuracy Tests

    [Fact]
    public void All_ShouldContainExpectedNumberOfCountries()
    {
        var all = DropdownData.Countries.All;
        
        // We have 195 UN member states + some territories/special regions
        // Current data set has countries organized by continent
        all.Should().HaveCountGreaterThan(190);
    }

    [Fact]
    public void All_ShouldContainAllContinents()
    {
        var continents = DropdownData.Countries.All
            .Select(c => c.Continent)
            .Distinct()
            .ToList();
        
        continents.Should().Contain("Africa");
        continents.Should().Contain("Antarctica");
        continents.Should().Contain("Asia");
        continents.Should().Contain("Europe");
        continents.Should().Contain("North America");
        continents.Should().Contain("Oceania");
        continents.Should().Contain("South America");
    }

    [Theory]
    [InlineData("US", "USA", "840", "United States")]
    [InlineData("CA", "CAN", "124", "Canada")]
    [InlineData("GB", "GBR", "826", "United Kingdom")]
    [InlineData("DE", "DEU", "276", "Germany")]
    [InlineData("JP", "JPN", "392", "Japan")]
    [InlineData("AU", "AUS", "036", "Australia")]
    [InlineData("BR", "BRA", "076", "Brazil")]
    [InlineData("ZA", "ZAF", "710", "South Africa")]
    public void All_ShouldContainExpectedCountries(string alpha2, string alpha3, string numeric, string name)
    {
        var country = DropdownData.Countries.All.FirstOrDefault(c => c.Alpha2Code == alpha2);
        
        country.Should().NotBeNull();
        country!.Alpha3Code.Should().Be(alpha3);
        country.NumericCode.Should().Be(numeric);
        country.Name.Should().Be(name);
    }

    [Theory]
    [InlineData("Africa", 54)]
    [InlineData("Europe", 45)]
    [InlineData("Asia", 51)]
    [InlineData("North America", 23)]
    [InlineData("South America", 12)]
    [InlineData("Oceania", 14)]
    [InlineData("Antarctica", 1)]
    public void ByContinent_ShouldReturnExpectedCount(string continent, int expectedMinCount)
    {
        var countries = DropdownData.Countries.ByContinent(continent);
        
        countries.Should().HaveCountGreaterThanOrEqualTo(expectedMinCount);
        countries.Should().OnlyContain(c => c.Continent == continent);
    }

    #endregion

    #region ISelectOption Interface Tests

    [Fact]
    public void CountryInfo_ShouldImplementISelectOption()
    {
        var country = DropdownData.Countries.ByAlpha2Code("US");
        
        country.Should().BeAssignableTo<ISelectOption>();
    }

    [Fact]
    public void CountryInfo_Value_ShouldReturnAlpha2Code()
    {
        var country = DropdownData.Countries.ByAlpha2Code("US");
        
        country!.Value.Should().Be("US");
    }

    [Fact]
    public void CountryInfo_Text_ShouldReturnName()
    {
        var country = DropdownData.Countries.ByAlpha2Code("US");
        
        country!.Text.Should().Be("United States");
    }

    [Fact]
    public void AllCountries_ShouldBeUsableAsISelectOption()
    {
        IEnumerable<ISelectOption> options = DropdownData.Countries.All;
        
        options.Should().HaveCountGreaterThan(190);
        options.All(o => !string.IsNullOrEmpty(o.Value) && !string.IsNullOrEmpty(o.Text)).Should().BeTrue();
    }

    #endregion

    #region Lookup Tests

    [Theory]
    [InlineData("US", "United States")]
    [InlineData("us", "United States")]
    [InlineData("Us", "United States")]
    [InlineData(" US ", "United States")]
    public void ByAlpha2Code_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedName)
    {
        var country = DropdownData.Countries.ByAlpha2Code(input);
        
        country.Should().NotBeNull();
        country!.Name.Should().Be(expectedName);
    }

    [Theory]
    [InlineData("USA", "United States")]
    [InlineData("usa", "United States")]
    [InlineData("Usa", "United States")]
    [InlineData(" USA ", "United States")]
    public void ByAlpha3Code_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedName)
    {
        var country = DropdownData.Countries.ByAlpha3Code(input);
        
        country.Should().NotBeNull();
        country!.Name.Should().Be(expectedName);
    }

    [Theory]
    [InlineData("840", "United States")]
    [InlineData(" 840 ", "United States")]
    public void ByNumericCode_ShouldBeTrimmed(string input, string expectedName)
    {
        var country = DropdownData.Countries.ByNumericCode(input);
        
        country.Should().NotBeNull();
        country!.Name.Should().Be(expectedName);
    }

    [Theory]
    [InlineData("United States", "US")]
    [InlineData("united states", "US")]
    [InlineData("UNITED STATES", "US")]
    [InlineData(" United States ", "US")]
    public void ByName_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedCode)
    {
        var country = DropdownData.Countries.ByName(input);
        
        country.Should().NotBeNull();
        country!.Alpha2Code.Should().Be(expectedCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByAlpha2Code_ShouldReturnNullForInvalidInput(string? input)
    {
        var country = DropdownData.Countries.ByAlpha2Code(input!);
        
        country.Should().BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByAlpha3Code_ShouldReturnNullForInvalidInput(string? input)
    {
        var country = DropdownData.Countries.ByAlpha3Code(input!);
        
        country.Should().BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByNumericCode_ShouldReturnNullForInvalidInput(string? input)
    {
        var country = DropdownData.Countries.ByNumericCode(input!);
        
        country.Should().BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByName_ShouldReturnNullForInvalidInput(string? input)
    {
        var country = DropdownData.Countries.ByName(input!);
        
        country.Should().BeNull();
    }

    [Fact]
    public void ByAlpha2Code_ShouldReturnNullForNonExistent()
    {
        var country = DropdownData.Countries.ByAlpha2Code("XX");
        
        country.Should().BeNull();
    }

    [Fact]
    public void ByContinent_ShouldReturnEmptyForInvalidContinent()
    {
        var countries = DropdownData.Countries.ByContinent("NotAContinent");
        
        countries.Should().BeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByContinent_ShouldReturnEmptyForInvalidInput(string? input)
    {
        var countries = DropdownData.Countries.ByContinent(input!);
        
        countries.Should().BeEmpty();
    }

    #endregion

    #region Builder Tests - Basic Filtering

    [Fact]
    public void Builder_Default_ShouldReturnAllCountries()
    {
        var countries = DropdownData.Countries.Builder().Build();
        
        countries.Should().HaveCountGreaterThan(190);
    }

    [Fact]
    public void Builder_InContinent_ShouldFilterByContinent()
    {
        var countries = DropdownData.Countries.Builder()
            .InContinent("Europe")
            .Build();
        
        countries.Should().HaveCountGreaterThanOrEqualTo(40);
        countries.Should().OnlyContain(c => c.Continent == "Europe");
    }

    [Fact]
    public void Builder_InContinent_MultipleContinents_ShouldIncludeAll()
    {
        var countries = DropdownData.Countries.Builder()
            .InContinent("Europe", "North America")
            .Build();
        
        countries.Should().OnlyContain(c => c.Continent == "Europe" || c.Continent == "North America");
    }

    [Fact]
    public void Builder_InContinent_ShouldBeCaseInsensitive()
    {
        var countries = DropdownData.Countries.Builder()
            .InContinent("europe")
            .Build();
        
        countries.Should().OnlyContain(c => c.Continent == "Europe");
    }

    [Fact]
    public void Builder_ExcludeContinent_ShouldRemoveContinent()
    {
        var allCount = DropdownData.Countries.All.Count;
        var europeCount = DropdownData.Countries.ByContinent("Europe").Count;
        
        var countries = DropdownData.Countries.Builder()
            .ExcludeContinent("Europe")
            .Build();
        
        countries.Should().HaveCount(allCount - europeCount);
        countries.Should().NotContain(c => c.Continent == "Europe");
    }

    [Fact]
    public void Builder_ExcludeContinent_MultipleContinents_ShouldRemoveAll()
    {
        var countries = DropdownData.Countries.Builder()
            .ExcludeContinent("Antarctica", "Oceania")
            .Build();
        
        countries.Should().NotContain(c => c.Continent == "Antarctica");
        countries.Should().NotContain(c => c.Continent == "Oceania");
    }

    #endregion

    #region Builder Tests - Exclusions (from BaseBuilder)

    [Fact]
    public void Builder_Exclude_ShouldRemoveSpecifiedCountries()
    {
        var countries = DropdownData.Countries.Builder()
            .Exclude("US", "CA", "MX")
            .Build();
        
        countries.Should().NotContain(c => c.Alpha2Code == "US");
        countries.Should().NotContain(c => c.Alpha2Code == "CA");
        countries.Should().NotContain(c => c.Alpha2Code == "MX");
    }

    [Fact]
    public void Builder_Exclude_ShouldBeCaseInsensitive()
    {
        var countries = DropdownData.Countries.Builder()
            .Exclude("us", "ca")
            .Build();
        
        countries.Should().NotContain(c => c.Alpha2Code == "US");
        countries.Should().NotContain(c => c.Alpha2Code == "CA");
    }

    [Fact]
    public void Builder_Exclude_ShouldHandleNullAndEmpty()
    {
        var allCount = DropdownData.Countries.All.Count;
        
        var countries = DropdownData.Countries.Builder()
            .Exclude(null!)
            .Exclude("", "  ")
            .Build();
        
        countries.Should().HaveCount(allCount);
    }

    #endregion

    #region Builder Tests - Only (Include Specific)

    [Fact]
    public void Builder_Only_ShouldReturnOnlySpecifiedCountries()
    {
        var countries = DropdownData.Countries.Builder()
            .Only("US", "CA", "MX")
            .Build();
        
        countries.Should().HaveCount(3);
        countries.Select(c => c.Alpha2Code).Should().BeEquivalentTo(new[] { "US", "CA", "MX" });
    }

    [Fact]
    public void Builder_Only_ShouldBeCaseInsensitive()
    {
        var countries = DropdownData.Countries.Builder()
            .Only("us", "ca")
            .Build();
        
        countries.Should().HaveCount(2);
    }

    [Fact]
    public void Builder_Only_WithExclude_ShouldApplyBoth()
    {
        var countries = DropdownData.Countries.Builder()
            .Only("US", "CA", "MX", "GB")
            .Exclude("GB")
            .Build();
        
        countries.Should().HaveCount(3);
        countries.Should().NotContain(c => c.Alpha2Code == "GB");
    }

    #endregion

    #region Builder Tests - Ordering

    [Fact]
    public void Builder_OrderByName_ShouldSortAlphabetically()
    {
        var countries = DropdownData.Countries.Builder()
            .OrderByName()
            .Build();
        
        countries.Should().BeInAscendingOrder(c => c.Name, StringComparer.InvariantCulture);
    }

    [Fact]
    public void Builder_OrderByNameDescending_ShouldSortReverseAlphabetically()
    {
        var countries = DropdownData.Countries.Builder()
            .OrderByNameDescending()
            .Build();
        
        countries.Should().BeInDescendingOrder(c => c.Name, StringComparer.InvariantCulture);
    }

    [Fact]
    public void Builder_OrderByCode_ShouldSortByAlpha2Code()
    {
        var countries = DropdownData.Countries.Builder()
            .OrderByCode()
            .Build();
        
        countries.Should().BeInAscendingOrder(c => c.Alpha2Code);
    }

    [Fact]
    public void Builder_OrderByCodeDescending_ShouldSortByAlpha2CodeReverse()
    {
        var countries = DropdownData.Countries.Builder()
            .OrderByCodeDescending()
            .Build();
        
        countries.Should().BeInDescendingOrder(c => c.Alpha2Code);
    }

    [Fact]
    public void Builder_OrderByName_WithInContinent_ShouldSortFilteredEntries()
    {
        var countries = DropdownData.Countries.Builder()
            .InContinent("Europe")
            .OrderByName()
            .Build();
        
        countries.Should().OnlyContain(c => c.Continent == "Europe");
        countries.Should().BeInAscendingOrder(c => c.Name);
    }

    #endregion

    #region Builder Tests - Complex Combinations

    [Fact]
    public void Builder_ComplexCombination_ShouldWorkCorrectly()
    {
        var countries = DropdownData.Countries.Builder()
            .InContinent("Europe", "North America")
            .Exclude("RU", "CU")
            .OrderByName()
            .Build();
        
        countries.Should().OnlyContain(c => c.Continent == "Europe" || c.Continent == "North America");
        countries.Should().NotContain(c => c.Alpha2Code == "RU");
        countries.Should().NotContain(c => c.Alpha2Code == "CU");
        countries.Should().BeInAscendingOrder(c => c.Name);
    }

    [Fact]
    public void Builder_ChainedCalls_ShouldAccumulate()
    {
        var countries = DropdownData.Countries.Builder()
            .InContinent("Europe")
            .Exclude("DE")
            .Exclude("FR")
            .Exclude("GB")
            .Build();
        
        countries.Should().NotContain(c => c.Alpha2Code == "DE");
        countries.Should().NotContain(c => c.Alpha2Code == "FR");
        countries.Should().NotContain(c => c.Alpha2Code == "GB");
    }

    #endregion

    #region Thread Safety Tests

    [Fact]
    public void Countries_ShouldBeSafeForConcurrentAccess()
    {
        var results = new List<int>();
        var expectedCount = DropdownData.Countries.All.Count;
        
        Parallel.For(0, 100, _ =>
        {
            var count = DropdownData.Countries.All.Count;
            lock (results)
            {
                results.Add(count);
            }
        });
        
        results.Should().AllBeEquivalentTo(expectedCount);
    }

    #endregion

    #region Immutability Tests

    [Fact]
    public void All_ShouldReturnSameInstance()
    {
        var first = DropdownData.Countries.All;
        var second = DropdownData.Countries.All;
        
        first.Should().BeSameAs(second);
    }

    [Fact]
    public void All_ShouldBeReadOnly()
    {
        var countries = DropdownData.Countries.All;
        
        countries.Should().BeAssignableTo<IReadOnlyList<CountryInfo>>();
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void CountryInfo_ToString_ShouldReturnFormattedString()
    {
        var country = DropdownData.Countries.ByAlpha2Code("US");
        
        country!.ToString().Should().Be("United States (US)");
    }

    #endregion
}