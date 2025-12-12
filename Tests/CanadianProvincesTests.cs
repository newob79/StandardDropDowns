using FluentAssertions;
using StandardDropdowns;
using StandardDropdowns.Interfaces;
using StandardDropdowns.Models;

namespace StandardDropdowns.Tests;

public class CanadianProvincesTests
{
    #region Data Accuracy Tests

    [Fact]
    public void All_ShouldContain13Entries()
    {
        // 10 provinces + 3 territories = 13
        var all = DropdownData.CanadianProvinces.All;

        all.Should().HaveCount(13);
    }

    [Fact]
    public void Provinces_ShouldContainExactly10Provinces()
    {
        var provinces = DropdownData.CanadianProvinces.Provinces;

        provinces.Should().HaveCount(10);
        provinces.Should().OnlyContain(p => !p.IsTerritory);
    }

    [Fact]
    public void Territories_ShouldContain3Entries()
    {
        var territories = DropdownData.CanadianProvinces.Territories;

        territories.Should().HaveCount(3);
        territories.Should().OnlyContain(p => p.IsTerritory);
    }

    [Theory]
    [InlineData("ON", "Ontario")]
    [InlineData("QC", "Quebec")]
    [InlineData("BC", "British Columbia")]
    [InlineData("AB", "Alberta")]
    [InlineData("MB", "Manitoba")]
    [InlineData("SK", "Saskatchewan")]
    [InlineData("NS", "Nova Scotia")]
    [InlineData("NB", "New Brunswick")]
    [InlineData("PE", "Prince Edward Island")]
    [InlineData("NL", "Newfoundland and Labrador")]
    public void All_ShouldContainExpectedProvinces(string abbreviation, string name)
    {
        var province = DropdownData.CanadianProvinces.All.FirstOrDefault(p => p.Abbreviation == abbreviation);

        province.Should().NotBeNull();
        province!.Name.Should().Be(name);
        province.IsTerritory.Should().BeFalse();
    }

    [Theory]
    [InlineData("NT", "Northwest Territories")]
    [InlineData("NU", "Nunavut")]
    [InlineData("YT", "Yukon")]
    public void Territories_ShouldContainExpectedTerritories(string abbreviation, string name)
    {
        var territory = DropdownData.CanadianProvinces.Territories.FirstOrDefault(p => p.Abbreviation == abbreviation);

        territory.Should().NotBeNull();
        territory!.Name.Should().Be(name);
        territory.IsTerritory.Should().BeTrue();
    }

    #endregion

    #region ISelectOption Interface Tests

    [Fact]
    public void ProvinceInfo_ShouldImplementISelectOption()
    {
        var province = DropdownData.CanadianProvinces.ByAbbreviation("ON");

        province.Should().BeAssignableTo<ISelectOption>();
    }

    [Fact]
    public void ProvinceInfo_Value_ShouldReturnAbbreviation()
    {
        var province = DropdownData.CanadianProvinces.ByAbbreviation("ON");

        province!.Value.Should().Be("ON");
    }

    [Fact]
    public void ProvinceInfo_Text_ShouldReturnName()
    {
        var province = DropdownData.CanadianProvinces.ByAbbreviation("ON");

        province!.Text.Should().Be("Ontario");
    }

    [Fact]
    public void AllProvinces_ShouldBeUsableAsISelectOption()
    {
        IEnumerable<ISelectOption> options = DropdownData.CanadianProvinces.Provinces;

        options.Should().HaveCount(10);
        options.All(o => !string.IsNullOrEmpty(o.Value) && !string.IsNullOrEmpty(o.Text)).Should().BeTrue();
    }

    #endregion

    #region Lookup Tests

    [Theory]
    [InlineData("ON", "Ontario")]
    [InlineData("on", "Ontario")]
    [InlineData("On", "Ontario")]
    [InlineData(" ON ", "Ontario")]
    public void ByAbbreviation_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedName)
    {
        var province = DropdownData.CanadianProvinces.ByAbbreviation(input);

        province.Should().NotBeNull();
        province!.Name.Should().Be(expectedName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByAbbreviation_ShouldReturnNullForInvalidInput(string? input)
    {
        var province = DropdownData.CanadianProvinces.ByAbbreviation(input!);

        province.Should().BeNull();
    }

    [Fact]
    public void ByAbbreviation_ShouldReturnNullForNonExistent()
    {
        var province = DropdownData.CanadianProvinces.ByAbbreviation("XX");

        province.Should().BeNull();
    }

    [Theory]
    [InlineData("Ontario", "ON")]
    [InlineData("ontario", "ON")]
    [InlineData("ONTARIO", "ON")]
    [InlineData(" Ontario ", "ON")]
    public void ByName_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedAbbr)
    {
        var province = DropdownData.CanadianProvinces.ByName(input);

        province.Should().NotBeNull();
        province!.Abbreviation.Should().Be(expectedAbbr);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByName_ShouldReturnNullForInvalidInput(string? input)
    {
        var province = DropdownData.CanadianProvinces.ByName(input!);

        province.Should().BeNull();
    }

    [Fact]
    public void ByName_ShouldReturnNullForNonExistent()
    {
        var province = DropdownData.CanadianProvinces.ByName("Nonexistent Province");

        province.Should().BeNull();
    }

    #endregion

    #region Builder Tests - Basic Filtering

    [Fact]
    public void Builder_Default_ShouldReturnOnly10Provinces()
    {
        var provinces = DropdownData.CanadianProvinces.Builder().Build();

        provinces.Should().HaveCount(10);
        provinces.Should().OnlyContain(p => !p.IsTerritory);
    }

    [Fact]
    public void Builder_IncludeTerritories_ShouldReturn13Entries()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .IncludeTerritories()
            .Build();

        provinces.Should().HaveCount(13);
        provinces.Should().Contain(p => p.IsTerritory);
    }

    [Fact]
    public void Builder_IncludeAll_ShouldReturn13Entries()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .IncludeAll()
            .Build();

        provinces.Should().HaveCount(13);
    }

    [Fact]
    public void Builder_ExcludeProvinces_WithTerritories_ShouldReturnOnlyTerritories()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .ExcludeProvinces()
            .IncludeTerritories()
            .Build();

        provinces.Should().HaveCount(3);
        provinces.Should().OnlyContain(p => p.IsTerritory);
    }

    #endregion

    #region Builder Tests - Exclusions

    [Fact]
    public void Builder_Exclude_ShouldRemoveSpecifiedProvinces()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .Exclude("ON", "QC", "BC")
            .Build();

        provinces.Should().HaveCount(7);
        provinces.Should().NotContain(p => p.Abbreviation == "ON");
        provinces.Should().NotContain(p => p.Abbreviation == "QC");
        provinces.Should().NotContain(p => p.Abbreviation == "BC");
    }

    [Fact]
    public void Builder_Exclude_ShouldBeCaseInsensitive()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .Exclude("on", "qc")
            .Build();

        provinces.Should().NotContain(p => p.Abbreviation == "ON");
        provinces.Should().NotContain(p => p.Abbreviation == "QC");
    }

    [Fact]
    public void Builder_Exclude_ShouldHandleNullAndEmpty()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .Exclude(null!)
            .Exclude("", "  ")
            .Build();

        provinces.Should().HaveCount(10);
    }

    [Fact]
    public void Builder_Exclude_ShouldWorkWithTerritoriesIncluded()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .IncludeTerritories()
            .Exclude("NU", "NT")
            .Build();

        provinces.Should().HaveCount(11);
        provinces.Should().NotContain(p => p.Abbreviation == "NU");
        provinces.Should().NotContain(p => p.Abbreviation == "NT");
    }

    #endregion

    #region Builder Tests - Only (Include Specific)

    [Fact]
    public void Builder_Only_ShouldReturnOnlySpecifiedProvinces()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .Only("ON", "QC", "BC")
            .Build();

        provinces.Should().HaveCount(3);
        provinces.Select(p => p.Abbreviation).Should().BeEquivalentTo(new[] { "ON", "QC", "BC" });
    }

    [Fact]
    public void Builder_Only_ShouldBeCaseInsensitive()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .Only("on", "qc")
            .Build();

        provinces.Should().HaveCount(2);
    }

    [Fact]
    public void Builder_Only_CanIncludeTerritories()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .Only("ON", "YT", "NU")
            .Build();

        provinces.Should().HaveCount(3);
        provinces.Should().Contain(p => p.Abbreviation == "YT");
        provinces.Should().Contain(p => p.Abbreviation == "NU");
    }

    [Fact]
    public void Builder_Only_WithExclude_ShouldApplyBoth()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .Only("ON", "QC", "BC", "AB")
            .Exclude("AB")
            .Build();

        provinces.Should().HaveCount(3);
        provinces.Should().NotContain(p => p.Abbreviation == "AB");
    }

    #endregion

    #region Builder Tests - Ordering

    [Fact]
    public void Builder_OrderByName_ShouldSortAlphabetically()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .OrderByName()
            .Build();

        provinces.First().Name.Should().Be("Alberta");
        provinces.Last().Name.Should().Be("Saskatchewan");
        provinces.Should().BeInAscendingOrder(p => p.Name);
    }

    [Fact]
    public void Builder_OrderByNameDescending_ShouldSortReverseAlphabetically()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .OrderByNameDescending()
            .Build();

        provinces.First().Name.Should().Be("Saskatchewan");
        provinces.Last().Name.Should().Be("Alberta");
        provinces.Should().BeInDescendingOrder(p => p.Name);
    }

    [Fact]
    public void Builder_OrderByAbbreviation_ShouldSortByAbbreviation()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .OrderByAbbreviation()
            .Build();

        provinces.First().Abbreviation.Should().Be("AB");
        provinces.Should().BeInAscendingOrder(p => p.Abbreviation);
    }

    [Fact]
    public void Builder_OrderByAbbreviationDescending_ShouldSortByAbbreviationReverse()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .OrderByAbbreviationDescending()
            .Build();

        provinces.First().Abbreviation.Should().Be("SK");
        provinces.Should().BeInDescendingOrder(p => p.Abbreviation);
    }

    [Fact]
    public void Builder_OrderByName_WithIncludeAll_ShouldSortAllEntries()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .IncludeAll()
            .OrderByName()
            .Build();

        provinces.Should().HaveCount(13);
        provinces.First().Name.Should().Be("Alberta");
        provinces.Should().BeInAscendingOrder(p => p.Name);
    }

    #endregion

    #region Builder Tests - Complex Combinations

    [Fact]
    public void Builder_ComplexCombination_ShouldWorkCorrectly()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .IncludeTerritories()
            .Exclude("NU", "NT")
            .OrderByName()
            .Build();

        provinces.Should().HaveCount(11); // 10 provinces + YT territory - 2 excluded
        provinces.Should().NotContain(p => p.Abbreviation == "NU");
        provinces.Should().NotContain(p => p.Abbreviation == "NT");
        provinces.Should().Contain(p => p.Abbreviation == "YT");
        provinces.Should().BeInAscendingOrder(p => p.Name);
    }

    [Fact]
    public void Builder_ChainedCalls_ShouldAccumulate()
    {
        var provinces = DropdownData.CanadianProvinces.Builder()
            .Exclude("ON")
            .Exclude("QC")
            .Exclude("BC")
            .Build();

        provinces.Should().HaveCount(7);
    }

    #endregion

    #region Thread Safety Tests

    [Fact]
    public void CanadianProvinces_ShouldBeSafeForConcurrentAccess()
    {
        var results = new List<int>();

        Parallel.For(0, 100, _ =>
        {
            var count = DropdownData.CanadianProvinces.All.Count;
            lock (results)
            {
                results.Add(count);
            }
        });

        results.Should().AllBeEquivalentTo(13);
    }

    #endregion

    #region Immutability Tests

    [Fact]
    public void All_ShouldReturnSameInstance()
    {
        var first = DropdownData.CanadianProvinces.All;
        var second = DropdownData.CanadianProvinces.All;

        first.Should().BeSameAs(second);
    }

    [Fact]
    public void All_ShouldBeReadOnly()
    {
        var provinces = DropdownData.CanadianProvinces.All;

        provinces.Should().BeAssignableTo<IReadOnlyList<ProvinceInfo>>();
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ProvinceInfo_ToString_ShouldReturnFormattedString()
    {
        var province = DropdownData.CanadianProvinces.ByAbbreviation("ON");

        province!.ToString().Should().Be("Ontario (ON)");
    }

    #endregion
}