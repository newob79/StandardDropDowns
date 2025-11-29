using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using StandardDropdowns;
using StandardDropdowns.Data;
using StandardDropdowns.Interfaces;
using StandardDropdowns.Models;
using Xunit;

namespace StandardDropdowns.Tests;

public class YesNoOptionsTests
{
    #region Data Accuracy

    [Fact]
    public void All_ShouldContain4Entries()
    {
        var all = DropdownData.YesNo.All;

        all.Should().HaveCount(4);
    }

    [Theory]
    [InlineData(1, "Yes", "Y", "Basic")]
    [InlineData(2, "No", "N", "Basic")]
    [InlineData(3, "N/A", "NA", "Extended")]
    [InlineData(4, "Unknown", "U", "Extended")]
    public void All_ShouldContainExpectedOptions(int number, string name, string code, string preset)
    {
        var option = DropdownData.YesNo.ByNumber(number);

        option.Should().NotBeNull();
        option!.Name.Should().Be(name);
        option.Code.Should().Be(code);
        option.Preset.Should().Be(preset);
    }

    [Fact]
    public void Basic_ShouldContain2Entries()
    {
        var basic = DropdownData.YesNo.Basic;

        basic.Should().HaveCount(2);
        basic.Select(o => o.Code).Should().BeEquivalentTo(new[] { "Y", "N" });
    }

    [Fact]
    public void WithNA_ShouldContain3Entries()
    {
        var withNA = DropdownData.YesNo.WithNA;

        withNA.Should().HaveCount(3);
        withNA.Select(o => o.Code).Should().BeEquivalentTo(new[] { "Y", "N", "NA" });
    }

    [Fact]
    public void WithUnknown_ShouldContain3Entries()
    {
        var withUnknown = DropdownData.YesNo.WithUnknown;

        withUnknown.Should().HaveCount(3);
        withUnknown.Select(o => o.Code).Should().BeEquivalentTo(new[] { "Y", "N", "U" });
    }

    #endregion

    #region ISelectOption Interface Tests

    [Fact]
    public void YesNoInfo_ShouldImplementISelectOption()
    {
        var option = DropdownData.YesNo.ByNumber(1);

        option.Should().BeAssignableTo<ISelectOption>();
    }

    [Fact]
    public void YesNoInfo_Value_ShouldReturnCode()
    {
        var option = DropdownData.YesNo.ByNumber(1);

        option!.Value.Should().Be("Y");
    }

    [Fact]
    public void YesNoInfo_Text_ShouldReturnName()
    {
        var option = DropdownData.YesNo.ByNumber(1);

        option!.Text.Should().Be("Yes");
    }

    [Fact]
    public void AllOptions_ShouldBeUsableAsISelectOption()
    {
        IEnumerable<ISelectOption> options = DropdownData.YesNo.All;

        options.Should().HaveCount(4);
        options.All(o => !string.IsNullOrEmpty(o.Value) && !string.IsNullOrEmpty(o.Text)).Should().BeTrue();
    }

    #endregion

    #region Lookup Tests

    [Theory]
    [InlineData("Y", "Yes")]
    [InlineData("y", "Yes")]
    [InlineData(" Y ", "Yes")]
    [InlineData("NA", "N/A")]
    [InlineData("na", "N/A")]
    public void ByCode_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedName)
    {
        var option = DropdownData.YesNo.ByCode(input);

        option.Should().NotBeNull();
        option!.Name.Should().Be(expectedName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByCode_ShouldReturnNullForInvalidInput(string? input)
    {
        var option = DropdownData.YesNo.ByCode(input!);

        option.Should().BeNull();
    }

    [Fact]
    public void ByCode_ShouldReturnNullForNonExistent()
    {
        var option = DropdownData.YesNo.ByCode("X");

        option.Should().BeNull();
    }

    [Theory]
    [InlineData("Yes", "Y")]
    [InlineData("yes", "Y")]
    [InlineData(" Yes ", "Y")]
    [InlineData("N/A", "NA")]
    public void ByName_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedCode)
    {
        var option = DropdownData.YesNo.ByName(input);

        option.Should().NotBeNull();
        option!.Code.Should().Be(expectedCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByName_ShouldReturnNullForInvalidInput(string? input)
    {
        var option = DropdownData.YesNo.ByName(input!);

        option.Should().BeNull();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(-1)]
    public void ByNumber_ShouldReturnNullForInvalidNumbers(int number)
    {
        var option = DropdownData.YesNo.ByNumber(number);

        option.Should().BeNull();
    }

    #endregion

    #region Builder Tests - Preset Filtering

    [Fact]
    public void Builder_Default_ShouldReturnAllOptions()
    {
        var options = DropdownData.YesNo.Builder().Build();

        options.Should().HaveCount(4);
    }

    [Fact]
    public void Builder_InPreset_Basic_ShouldReturn2Options()
    {
        var options = DropdownData.YesNo.Builder()
            .InPreset("Basic")
            .Build();

        options.Should().HaveCount(2);
        options.Should().OnlyContain(o => o.Preset == "Basic");
    }

    [Fact]
    public void Builder_InPreset_Extended_ShouldReturn2Options()
    {
        var options = DropdownData.YesNo.Builder()
            .InPreset("Extended")
            .Build();

        options.Should().HaveCount(2);
        options.Should().OnlyContain(o => o.Preset == "Extended");
    }

    [Fact]
    public void Builder_InPreset_ShouldBeCaseInsensitive()
    {
        var options = DropdownData.YesNo.Builder()
            .InPreset("basic")
            .Build();

        options.Should().HaveCount(2);
    }

    [Fact]
    public void Builder_ExcludePreset_ShouldRemovePreset()
    {
        var options = DropdownData.YesNo.Builder()
            .ExcludePreset("Extended")
            .Build();

        options.Should().HaveCount(2);
        options.Should().NotContain(o => o.Preset == "Extended");
    }

    [Fact]
    public void Builder_IncludeNA_ShouldReturn3Options()
    {
        var options = DropdownData.YesNo.Builder()
            .IncludeNA()
            .Build();

        options.Should().HaveCount(3);
        options.Select(o => o.Code).Should().BeEquivalentTo(new[] { "Y", "N", "NA" });
    }

    [Fact]
    public void Builder_IncludeUnknown_ShouldReturn3Options()
    {
        var options = DropdownData.YesNo.Builder()
            .IncludeUnknown()
            .Build();

        options.Should().HaveCount(3);
        options.Select(o => o.Code).Should().BeEquivalentTo(new[] { "Y", "N", "U" });
    }

    #endregion

    #region Builder Tests - Exclusions (from BaseBuilder)

    [Fact]
    public void Builder_Exclude_ShouldRemoveSpecifiedOptions()
    {
        var options = DropdownData.YesNo.Builder()
            .Exclude("Y")
            .Build();

        options.Should().HaveCount(3);
        options.Should().NotContain(o => o.Code == "Y");
    }

    [Fact]
    public void Builder_Only_ShouldReturnOnlySpecifiedOptions()
    {
        var options = DropdownData.YesNo.Builder()
            .Only("Y", "N")
            .Build();

        options.Should().HaveCount(2);
        options.Select(o => o.Code).Should().BeEquivalentTo(new[] { "Y", "N" });
    }

    #endregion

    #region Builder Tests - Ordering

    [Fact]
    public void Builder_OrderByName_ShouldSortAlphabetically()
    {
        var options = DropdownData.YesNo.Builder()
            .OrderByName()
            .Build();

        options.Should().BeInAscendingOrder(o => o.Text, StringComparer.Ordinal);
    }

    [Fact]
    public void Builder_OrderByNameDescending_ShouldSortReverseAlphabetically()
    {
        var options = DropdownData.YesNo.Builder()
            .OrderByNameDescending()
            .Build();

        options.Should().BeInDescendingOrder(o => o.Text, StringComparer.Ordinal);
    }

    #endregion

    #region Thread Safety Tests

    [Fact]
    public void YesNoOptions_ShouldBeSafeForConcurrentAccess()
    {
        var results = new List<int>();
        var expectedCount = DropdownData.YesNo.All.Count;

        Parallel.For(0, 100, _ =>
        {
            var count = DropdownData.YesNo.All.Count;
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
        var first = DropdownData.YesNo.All;
        var second = DropdownData.YesNo.All;

        first.Should().BeSameAs(second);
    }

    [Fact]
    public void All_ShouldBeReadOnly()
    {
        var options = DropdownData.YesNo.All;

        options.Should().BeAssignableTo<IReadOnlyList<YesNoInfo>>();
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void YesNoInfo_ToString_ShouldReturnFormattedString()
    {
        var option = DropdownData.YesNo.ByNumber(1);

        option!.ToString().Should().Be("Yes (Y)");
    }

    #endregion
}