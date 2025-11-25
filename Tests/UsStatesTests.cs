using FluentAssertions;
using StandardDropdowns;
using StandardDropdowns.Interfaces;
using StandardDropdowns.Models;

namespace StandardDropdowns.Tests;

public class UsStatesTests
{
    #region Data Accuracy Tests

    [Fact]
    public void All_ShouldContain56Entries()
    {
        // 50 states + DC + 5 territories = 56
        var all = DropdownData.UsStates.All;

        all.Should().HaveCount(56);
    }

    [Fact]
    public void States50_ShouldContainExactly50States()
    {
        var states = DropdownData.UsStates.States50;

        states.Should().HaveCount(50);
        states.Should().OnlyContain(s => !s.IsDC && !s.IsTerritory);
    }

    [Fact]
    public void StatesAndDC_ShouldContain51Entries()
    {
        var statesAndDC = DropdownData.UsStates.StatesAndDC;

        statesAndDC.Should().HaveCount(51);
        statesAndDC.Should().OnlyContain(s => !s.IsTerritory);
    }

    [Fact]
    public void Territories_ShouldContain5Entries()
    {
        var territories = DropdownData.UsStates.Territories;

        territories.Should().HaveCount(5);
        territories.Should().OnlyContain(s => s.IsTerritory);
    }

    [Fact]
    public void All_ShouldContainDC()
    {
        var dc = DropdownData.UsStates.All.FirstOrDefault(s => s.IsDC);

        dc.Should().NotBeNull();
        dc!.Abbreviation.Should().Be("DC");
        dc.Name.Should().Be("District of Columbia");
    }

    [Theory]
    [InlineData("TX", "Texas")]
    [InlineData("CA", "California")]
    [InlineData("NY", "New York")]
    [InlineData("FL", "Florida")]
    [InlineData("AK", "Alaska")]
    [InlineData("HI", "Hawaii")]
    public void All_ShouldContainExpectedStates(string abbreviation, string name)
    {
        var state = DropdownData.UsStates.All.FirstOrDefault(s => s.Abbreviation == abbreviation);

        state.Should().NotBeNull();
        state!.Name.Should().Be(name);
    }

    [Theory]
    [InlineData("PR", "Puerto Rico")]
    [InlineData("GU", "Guam")]
    [InlineData("VI", "U.S. Virgin Islands")]
    [InlineData("AS", "American Samoa")]
    [InlineData("MP", "Northern Mariana Islands")]
    public void Territories_ShouldContainExpectedTerritories(string abbreviation, string name)
    {
        var territory = DropdownData.UsStates.Territories.FirstOrDefault(s => s.Abbreviation == abbreviation);

        territory.Should().NotBeNull();
        territory!.Name.Should().Be(name);
        territory.IsTerritory.Should().BeTrue();
    }

    #endregion

    #region ISelectOption Interface Tests

    [Fact]
    public void StateInfo_ShouldImplementISelectOption()
    {
        var state = DropdownData.UsStates.ByAbbreviation("TX");

        state.Should().BeAssignableTo<ISelectOption>();
    }

    [Fact]
    public void StateInfo_Value_ShouldReturnAbbreviation()
    {
        var state = DropdownData.UsStates.ByAbbreviation("TX");

        state!.Value.Should().Be("TX");
    }

    [Fact]
    public void StateInfo_Text_ShouldReturnName()
    {
        var state = DropdownData.UsStates.ByAbbreviation("TX");

        state!.Text.Should().Be("Texas");
    }

    [Fact]
    public void AllStates_ShouldBeUsableAsISelectOption()
    {
        IEnumerable<ISelectOption> options = DropdownData.UsStates.States50;

        options.Should().HaveCount(50);
        options.All(o => !string.IsNullOrEmpty(o.Value) && !string.IsNullOrEmpty(o.Text)).Should().BeTrue();
    }

    #endregion

    #region Lookup Tests

    [Theory]
    [InlineData("TX", "Texas")]
    [InlineData("tx", "Texas")]
    [InlineData("Tx", "Texas")]
    [InlineData(" TX ", "Texas")]
    public void ByAbbreviation_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedName)
    {
        var state = DropdownData.UsStates.ByAbbreviation(input);

        state.Should().NotBeNull();
        state!.Name.Should().Be(expectedName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByAbbreviation_ShouldReturnNullForInvalidInput(string? input)
    {
        var state = DropdownData.UsStates.ByAbbreviation(input!);

        state.Should().BeNull();
    }

    [Fact]
    public void ByAbbreviation_ShouldReturnNullForNonExistent()
    {
        var state = DropdownData.UsStates.ByAbbreviation("XX");

        state.Should().BeNull();
    }

    [Theory]
    [InlineData("Texas", "TX")]
    [InlineData("texas", "TX")]
    [InlineData("TEXAS", "TX")]
    [InlineData(" Texas ", "TX")]
    public void ByName_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedAbbr)
    {
        var state = DropdownData.UsStates.ByName(input);

        state.Should().NotBeNull();
        state!.Abbreviation.Should().Be(expectedAbbr);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByName_ShouldReturnNullForInvalidInput(string? input)
    {
        var state = DropdownData.UsStates.ByName(input!);

        state.Should().BeNull();
    }

    [Fact]
    public void ByName_ShouldReturnNullForNonExistent()
    {
        var state = DropdownData.UsStates.ByName("Nonexistent State");

        state.Should().BeNull();
    }

    #endregion

    #region Builder Tests - Basic Filtering

    [Fact]
    public void Builder_Default_ShouldReturnOnly50States()
    {
        var states = DropdownData.UsStates.Builder().Build();

        states.Should().HaveCount(50);
        states.Should().OnlyContain(s => !s.IsDC && !s.IsTerritory);
    }

    [Fact]
    public void Builder_IncludeDC_ShouldReturn51Entries()
    {
        var states = DropdownData.UsStates.Builder()
            .IncludeDC()
            .Build();

        states.Should().HaveCount(51);
        states.Should().Contain(s => s.IsDC);
    }

    [Fact]
    public void Builder_IncludeTerritories_ShouldReturn55Entries()
    {
        var states = DropdownData.UsStates.Builder()
            .IncludeTerritories()
            .Build();

        states.Should().HaveCount(55);
        states.Should().Contain(s => s.IsTerritory);
    }

    [Fact]
    public void Builder_IncludeAll_ShouldReturn56Entries()
    {
        var states = DropdownData.UsStates.Builder()
            .IncludeAll()
            .Build();

        states.Should().HaveCount(56);
    }

    [Fact]
    public void Builder_ExcludeStates_WithDC_ShouldReturnOnlyDC()
    {
        var states = DropdownData.UsStates.Builder()
            .ExcludeStates()
            .IncludeDC()
            .Build();

        states.Should().HaveCount(1);
        states.First().IsDC.Should().BeTrue();
    }

    [Fact]
    public void Builder_ExcludeStates_WithTerritories_ShouldReturnOnlyTerritories()
    {
        var states = DropdownData.UsStates.Builder()
            .ExcludeStates()
            .IncludeTerritories()
            .Build();

        states.Should().HaveCount(5);
        states.Should().OnlyContain(s => s.IsTerritory);
    }

    #endregion

    #region Builder Tests - Exclusions

    [Fact]
    public void Builder_Exclude_ShouldRemoveSpecifiedStates()
    {
        var states = DropdownData.UsStates.Builder()
            .Exclude("TX", "CA", "NY")
            .Build();

        states.Should().HaveCount(47);
        states.Should().NotContain(s => s.Abbreviation == "TX");
        states.Should().NotContain(s => s.Abbreviation == "CA");
        states.Should().NotContain(s => s.Abbreviation == "NY");
    }

    [Fact]
    public void Builder_Exclude_ShouldBeCaseInsensitive()
    {
        var states = DropdownData.UsStates.Builder()
            .Exclude("tx", "ca")
            .Build();

        states.Should().NotContain(s => s.Abbreviation == "TX");
        states.Should().NotContain(s => s.Abbreviation == "CA");
    }

    [Fact]
    public void Builder_Exclude_ShouldHandleNullAndEmpty()
    {
        var states = DropdownData.UsStates.Builder()
            .Exclude(null!)
            .Exclude("", "  ")
            .Build();

        states.Should().HaveCount(50);
    }

    [Fact]
    public void Builder_Exclude_ShouldWorkWithTerritoriesIncluded()
    {
        var states = DropdownData.UsStates.Builder()
            .IncludeTerritories()
            .Exclude("PR", "GU")
            .Build();

        states.Should().HaveCount(53);
        states.Should().NotContain(s => s.Abbreviation == "PR");
        states.Should().NotContain(s => s.Abbreviation == "GU");
    }

    #endregion

    #region Builder Tests - Only (Include Specific)

    [Fact]
    public void Builder_Only_ShouldReturnOnlySpecifiedStates()
    {
        var states = DropdownData.UsStates.Builder()
            .Only("TX", "CA", "NY")
            .Build();

        states.Should().HaveCount(3);
        states.Select(s => s.Abbreviation).Should().BeEquivalentTo(new[] { "TX", "CA", "NY" });
    }

    [Fact]
    public void Builder_Only_ShouldBeCaseInsensitive()
    {
        var states = DropdownData.UsStates.Builder()
            .Only("tx", "ca")
            .Build();

        states.Should().HaveCount(2);
    }

    [Fact]
    public void Builder_Only_CanIncludeDCAndTerritories()
    {
        var states = DropdownData.UsStates.Builder()
            .Only("TX", "DC", "PR")
            .Build();

        states.Should().HaveCount(3);
        states.Should().Contain(s => s.Abbreviation == "DC");
        states.Should().Contain(s => s.Abbreviation == "PR");
    }

    [Fact]
    public void Builder_Only_WithExclude_ShouldApplyBoth()
    {
        var states = DropdownData.UsStates.Builder()
            .Only("TX", "CA", "NY", "FL")
            .Exclude("FL")
            .Build();

        states.Should().HaveCount(3);
        states.Should().NotContain(s => s.Abbreviation == "FL");
    }

    #endregion

    #region Builder Tests - Ordering

    [Fact]
    public void Builder_OrderByName_ShouldSortAlphabetically()
    {
        var states = DropdownData.UsStates.Builder()
            .OrderByName()
            .Build();

        states.First().Name.Should().Be("Alabama");
        states.Last().Name.Should().Be("Wyoming");
        states.Should().BeInAscendingOrder(s => s.Name);
    }

    [Fact]
    public void Builder_OrderByNameDescending_ShouldSortReverseAlphabetically()
    {
        var states = DropdownData.UsStates.Builder()
            .OrderByNameDescending()
            .Build();

        states.First().Name.Should().Be("Wyoming");
        states.Last().Name.Should().Be("Alabama");
        states.Should().BeInDescendingOrder(s => s.Name);
    }

    [Fact]
    public void Builder_OrderByAbbreviation_ShouldSortByAbbreviation()
    {
        var states = DropdownData.UsStates.Builder()
            .OrderByAbbreviation()
            .Build();

        states.First().Abbreviation.Should().Be("AK");
        states.Should().BeInAscendingOrder(s => s.Abbreviation);
    }

    [Fact]
    public void Builder_OrderByAbbreviationDescending_ShouldSortByAbbreviationReverse()
    {
        var states = DropdownData.UsStates.Builder()
            .OrderByAbbreviationDescending()
            .Build();

        states.First().Abbreviation.Should().Be("WY");
        states.Should().BeInDescendingOrder(s => s.Abbreviation);
    }

    [Fact]
    public void Builder_OrderByName_WithIncludeAll_ShouldSortAllEntries()
    {
        var states = DropdownData.UsStates.Builder()
            .IncludeAll()
            .OrderByName()
            .Build();

        states.Should().HaveCount(56);
        states.First().Name.Should().Be("Alabama");
        states.Should().BeInAscendingOrder(s => s.Name);
    }

    #endregion

    #region Builder Tests - Complex Combinations

    [Fact]
    public void Builder_ComplexCombination_ShouldWorkCorrectly()
    {
        var states = DropdownData.UsStates.Builder()
            .IncludeDC()
            .IncludeTerritories()
            .Exclude("AS", "GU", "MP")
            .OrderByName()
            .Build();

        states.Should().HaveCount(53); // 56 - 3 excluded
        states.Should().NotContain(s => s.Abbreviation == "AS");
        states.Should().NotContain(s => s.Abbreviation == "GU");
        states.Should().NotContain(s => s.Abbreviation == "MP");
        states.Should().BeInAscendingOrder(s => s.Name);
    }

    [Fact]
    public void Builder_ChainedCalls_ShouldAccumulate()
    {
        var states = DropdownData.UsStates.Builder()
            .Exclude("TX")
            .Exclude("CA")
            .Exclude("NY")
            .Build();

        states.Should().HaveCount(47);
    }

    #endregion

    #region Thread Safety Tests

    [Fact]
    public void UsStates_ShouldBeSafeForConcurrentAccess()
    {
        var results = new List<int>();

        Parallel.For(0, 100, _ =>
        {
            var count = DropdownData.UsStates.All.Count;
            lock (results)
            {
                results.Add(count);
            }
        });

        results.Should().AllBeEquivalentTo(56);
    }

    #endregion

    #region Immutability Tests

    [Fact]
    public void All_ShouldReturnSameInstance()
    {
        var first = DropdownData.UsStates.All;
        var second = DropdownData.UsStates.All;

        first.Should().BeSameAs(second);
    }

    [Fact]
    public void All_ShouldBeReadOnly()
    {
        var states = DropdownData.UsStates.All;

        states.Should().BeAssignableTo<IReadOnlyList<StateInfo>>();
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void StateInfo_ToString_ShouldReturnFormattedString()
    {
        var state = DropdownData.UsStates.ByAbbreviation("TX");

        state!.ToString().Should().Be("Texas (TX)");
    }

    #endregion
}