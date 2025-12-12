using FluentAssertions;
using StandardDropdowns;
using StandardDropdowns.Interfaces;
using StandardDropdowns.Models;

namespace StandardDropdowns.Tests;

public class MexicanStatesTests
{
    #region Data Accuracy Tests

    [Fact]
    public void All_ShouldContain32Entries()
    {
        // 31 states + Ciudad de México = 32
        var all = DropdownData.MexicanStates.All;

        all.Should().HaveCount(32);
    }

    [Fact]
    public void States_ShouldContainExactly31States()
    {
        var states = DropdownData.MexicanStates.States;

        states.Should().HaveCount(31);
        states.Should().OnlyContain(s => !s.IsFederalDistrict);
    }

    [Fact]
    public void StatesAndCDMX_ShouldContain32Entries()
    {
        var statesAndCDMX = DropdownData.MexicanStates.StatesAndCDMX;

        statesAndCDMX.Should().HaveCount(32);
    }

    [Fact]
    public void All_ShouldContainCDMX()
    {
        var cdmx = DropdownData.MexicanStates.All.FirstOrDefault(s => s.IsFederalDistrict);

        cdmx.Should().NotBeNull();
        cdmx!.Abbreviation.Should().Be("CMX");
        cdmx.Name.Should().Be("Ciudad de México");
    }

    [Theory]
    [InlineData("JAL", "Jalisco")]
    [InlineData("MEX", "México")]
    [InlineData("NLE", "Nuevo León")]
    [InlineData("YUC", "Yucatán")]
    [InlineData("BCN", "Baja California")]
    [InlineData("BCS", "Baja California Sur")]
    public void All_ShouldContainExpectedStates(string abbreviation, string name)
    {
        var state = DropdownData.MexicanStates.All.FirstOrDefault(s => s.Abbreviation == abbreviation);

        state.Should().NotBeNull();
        state!.Name.Should().Be(name);
    }

    [Fact]
    public void All_ShouldContainAllExpectedAbbreviations()
    {
        var expectedAbbreviations = new[]
        {
            "AGU", "BCN", "BCS", "CAM", "CHP", "CHH", "COA", "COL", "DUR", "GUA",
            "GRO", "HID", "JAL", "MEX", "MIC", "MOR", "NAY", "NLE", "OAX", "PUE",
            "QUE", "ROO", "SLP", "SIN", "SON", "TAB", "TAM", "TLA", "VER", "YUC",
            "ZAC", "CMX"
        };

        var actualAbbreviations = DropdownData.MexicanStates.All.Select(s => s.Abbreviation).ToList();

        actualAbbreviations.Should().BeEquivalentTo(expectedAbbreviations);
    }

    #endregion

    #region ISelectOption Interface Tests

    [Fact]
    public void MexicanStateInfo_ShouldImplementISelectOption()
    {
        var state = DropdownData.MexicanStates.ByAbbreviation("JAL");

        state.Should().BeAssignableTo<ISelectOption>();
    }

    [Fact]
    public void MexicanStateInfo_Value_ShouldReturnAbbreviation()
    {
        var state = DropdownData.MexicanStates.ByAbbreviation("JAL");

        state!.Value.Should().Be("JAL");
    }

    [Fact]
    public void MexicanStateInfo_Text_ShouldReturnName()
    {
        var state = DropdownData.MexicanStates.ByAbbreviation("JAL");

        state!.Text.Should().Be("Jalisco");
    }

    [Fact]
    public void AllStates_ShouldBeUsableAsISelectOption()
    {
        IEnumerable<ISelectOption> options = DropdownData.MexicanStates.States;

        options.Should().HaveCount(31);
        options.All(o => !string.IsNullOrEmpty(o.Value) && !string.IsNullOrEmpty(o.Text)).Should().BeTrue();
    }

    #endregion

    #region Lookup Tests

    [Theory]
    [InlineData("JAL", "Jalisco")]
    [InlineData("jal", "Jalisco")]
    [InlineData("Jal", "Jalisco")]
    [InlineData(" JAL ", "Jalisco")]
    public void ByAbbreviation_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedName)
    {
        var state = DropdownData.MexicanStates.ByAbbreviation(input);

        state.Should().NotBeNull();
        state!.Name.Should().Be(expectedName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByAbbreviation_ShouldReturnNullForInvalidInput(string? input)
    {
        var state = DropdownData.MexicanStates.ByAbbreviation(input!);

        state.Should().BeNull();
    }

    [Fact]
    public void ByAbbreviation_ShouldReturnNullForNonExistent()
    {
        var state = DropdownData.MexicanStates.ByAbbreviation("XX");

        state.Should().BeNull();
    }

    [Theory]
    [InlineData("Jalisco", "JAL")]
    [InlineData("jalisco", "JAL")]
    [InlineData("JALISCO", "JAL")]
    [InlineData(" Jalisco ", "JAL")]
    [InlineData("Ciudad de México", "CMX")]
    [InlineData("ciudad de méxico", "CMX")]
    public void ByName_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedAbbr)
    {
        var state = DropdownData.MexicanStates.ByName(input);

        state.Should().NotBeNull();
        state!.Abbreviation.Should().Be(expectedAbbr);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByName_ShouldReturnNullForInvalidInput(string? input)
    {
        var state = DropdownData.MexicanStates.ByName(input!);

        state.Should().BeNull();
    }

    [Fact]
    public void ByName_ShouldReturnNullForNonExistent()
    {
        var state = DropdownData.MexicanStates.ByName("Nonexistent State");

        state.Should().BeNull();
    }

    #endregion

    #region Builder Tests - Basic Filtering

    [Fact]
    public void Builder_Default_ShouldReturnOnly31States()
    {
        var states = DropdownData.MexicanStates.Builder().Build();

        states.Should().HaveCount(31);
        states.Should().OnlyContain(s => !s.IsFederalDistrict);
    }

    [Fact]
    public void Builder_IncludeCDMX_ShouldReturn32Entries()
    {
        var states = DropdownData.MexicanStates.Builder()
            .IncludeCDMX()
            .Build();

        states.Should().HaveCount(32);
        states.Should().Contain(s => s.IsFederalDistrict);
    }

    [Fact]
    public void Builder_ExcludeStates_WithCDMX_ShouldReturnOnlyCDMX()
    {
        var states = DropdownData.MexicanStates.Builder()
            .ExcludeStates()
            .IncludeCDMX()
            .Build();

        states.Should().HaveCount(1);
        states.First().IsFederalDistrict.Should().BeTrue();
        states.First().Abbreviation.Should().Be("CMX");
    }

    #endregion

    #region Builder Tests - Exclusions

    [Fact]
    public void Builder_Exclude_ShouldRemoveSpecifiedStates()
    {
        var states = DropdownData.MexicanStates.Builder()
            .Exclude("JAL", "MEX", "NLE")
            .Build();

        states.Should().HaveCount(28);
        states.Should().NotContain(s => s.Abbreviation == "JAL");
        states.Should().NotContain(s => s.Abbreviation == "MEX");
        states.Should().NotContain(s => s.Abbreviation == "NLE");
    }

    [Fact]
    public void Builder_Exclude_ShouldBeCaseInsensitive()
    {
        var states = DropdownData.MexicanStates.Builder()
            .Exclude("jal", "mex")
            .Build();

        states.Should().NotContain(s => s.Abbreviation == "JAL");
        states.Should().NotContain(s => s.Abbreviation == "MEX");
    }

    [Fact]
    public void Builder_Exclude_ShouldHandleNullAndEmpty()
    {
        var states = DropdownData.MexicanStates.Builder()
            .Exclude(null!)
            .Exclude("", "  ")
            .Build();

        states.Should().HaveCount(31);
    }

    [Fact]
    public void Builder_Exclude_ShouldWorkWithCDMXIncluded()
    {
        var states = DropdownData.MexicanStates.Builder()
            .IncludeCDMX()
            .Exclude("OAX", "GRO")
            .Build();

        states.Should().HaveCount(30);
        states.Should().NotContain(s => s.Abbreviation == "OAX");
        states.Should().NotContain(s => s.Abbreviation == "GRO");
        states.Should().Contain(s => s.Abbreviation == "CMX");
    }

    #endregion

    #region Builder Tests - Only (Include Specific)

    [Fact]
    public void Builder_Only_ShouldReturnOnlySpecifiedStates()
    {
        var states = DropdownData.MexicanStates.Builder()
            .Only("JAL", "MEX", "NLE")
            .Build();

        states.Should().HaveCount(3);
        states.Select(s => s.Abbreviation).Should().BeEquivalentTo(new[] { "JAL", "MEX", "NLE" });
    }

    [Fact]
    public void Builder_Only_ShouldBeCaseInsensitive()
    {
        var states = DropdownData.MexicanStates.Builder()
            .Only("jal", "mex")
            .Build();

        states.Should().HaveCount(2);
    }

    [Fact]
    public void Builder_Only_CanIncludeCDMX()
    {
        var states = DropdownData.MexicanStates.Builder()
            .Only("JAL", "CMX")
            .Build();

        states.Should().HaveCount(2);
        states.Should().Contain(s => s.Abbreviation == "CMX");
    }

    [Fact]
    public void Builder_Only_WithExclude_ShouldApplyBoth()
    {
        var states = DropdownData.MexicanStates.Builder()
            .Only("JAL", "MEX", "NLE", "YUC")
            .Exclude("YUC")
            .Build();

        states.Should().HaveCount(3);
        states.Should().NotContain(s => s.Abbreviation == "YUC");
    }

    #endregion

    #region Builder Tests - Ordering

    [Fact]
    public void Builder_OrderByName_ShouldSortAlphabetically()
    {
        var states = DropdownData.MexicanStates.Builder()
            .OrderByName()
            .Build();

        states.First().Name.Should().Be("Aguascalientes");
        states.Last().Name.Should().Be("Zacatecas");
        states.Should().BeInAscendingOrder(s => s.Name, StringComparer.InvariantCulture);
    }

    [Fact]
    public void Builder_OrderByNameDescending_ShouldSortReverseAlphabetically()
    {
        var states = DropdownData.MexicanStates.Builder()
            .OrderByNameDescending()
            .Build();

        states.First().Name.Should().Be("Zacatecas");
        states.Last().Name.Should().Be("Aguascalientes");
        states.Should().BeInDescendingOrder(s => s.Name, StringComparer.InvariantCulture);
    }

    [Fact]
    public void Builder_OrderByAbbreviation_ShouldSortByAbbreviation()
    {
        var states = DropdownData.MexicanStates.Builder()
            .OrderByAbbreviation()
            .Build();

        states.First().Abbreviation.Should().Be("AGU");
        states.Should().BeInAscendingOrder(s => s.Abbreviation);
    }

    [Fact]
    public void Builder_OrderByAbbreviationDescending_ShouldSortByAbbreviationReverse()
    {
        var states = DropdownData.MexicanStates.Builder()
            .OrderByAbbreviationDescending()
            .Build();

        states.First().Abbreviation.Should().Be("ZAC");
        states.Should().BeInDescendingOrder(s => s.Abbreviation);
    }

    [Fact]
    public void Builder_OrderByName_WithIncludeCDMX_ShouldSortAllEntries()
    {
        var states = DropdownData.MexicanStates.Builder()
            .IncludeCDMX()
            .OrderByName()
            .Build();

        states.Should().HaveCount(32);
        states.First().Name.Should().Be("Aguascalientes");
        states.Should().BeInAscendingOrder(s => s.Name, StringComparer.InvariantCulture);
    }

    [Fact]
    public void Builder_OrderByName_ShouldHandleSpanishCharactersCorrectly()
    {
        var states = DropdownData.MexicanStates.Builder()
            .Only("MEX", "MIC", "QUE", "YUC")
            .OrderByName()
            .Build();

        // Should order: México, Michoacán, Querétaro, Yucatán
        var names = states.Select(s => s.Name).ToList();
        names.Should().BeInAscendingOrder(StringComparer.InvariantCulture);
    }

    #endregion

    #region Builder Tests - Complex Combinations

    [Fact]
    public void Builder_ComplexCombination_ShouldWorkCorrectly()
    {
        var states = DropdownData.MexicanStates.Builder()
            .IncludeCDMX()
            .Exclude("OAX", "GRO", "CHP")
            .OrderByName()
            .Build();

        states.Should().HaveCount(29); // 32 - 3 excluded
        states.Should().NotContain(s => s.Abbreviation == "OAX");
        states.Should().NotContain(s => s.Abbreviation == "GRO");
        states.Should().NotContain(s => s.Abbreviation == "CHP");
        states.Should().BeInAscendingOrder(s => s.Name, StringComparer.InvariantCulture);
    }

    [Fact]
    public void Builder_ChainedCalls_ShouldAccumulate()
    {
        var states = DropdownData.MexicanStates.Builder()
            .Exclude("JAL")
            .Exclude("MEX")
            .Exclude("NLE")
            .Build();

        states.Should().HaveCount(28);
    }

    #endregion

    #region Thread Safety Tests

    [Fact]
    public void MexicanStates_ShouldBeSafeForConcurrentAccess()
    {
        var results = new List<int>();

        Parallel.For(0, 100, _ =>
        {
            var count = DropdownData.MexicanStates.All.Count;
            lock (results)
            {
                results.Add(count);
            }
        });

        results.Should().AllBeEquivalentTo(32);
    }

    #endregion

    #region Immutability Tests

    [Fact]
    public void All_ShouldReturnSameInstance()
    {
        var first = DropdownData.MexicanStates.All;
        var second = DropdownData.MexicanStates.All;

        first.Should().BeSameAs(second);
    }

    [Fact]
    public void All_ShouldBeReadOnly()
    {
        var states = DropdownData.MexicanStates.All;

        states.Should().BeAssignableTo<IReadOnlyList<MexicanStateInfo>>();
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void MexicanStateInfo_ToString_ShouldReturnFormattedString()
    {
        var state = DropdownData.MexicanStates.ByAbbreviation("JAL");

        state!.ToString().Should().Be("Jalisco (JAL)");
    }

    [Fact]
    public void MexicanStateInfo_ToString_ShouldHandleSpanishCharacters()
    {
        var state = DropdownData.MexicanStates.ByAbbreviation("YUC");

        state!.ToString().Should().Be("Yucatán (YUC)");
    }

    #endregion
}