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

public class SuffixesTests
{
    #region Data Accuracy

    [Fact]
    public void All_ShouldContain22Entries()
    {
        var all = DropdownData.Suffixes.All;

        all.Should().HaveCount(22);
    }

    [Fact]
    public void All_ShouldContainExpectedCategories()
    {
        var categories = DropdownData.Suffixes.All
            .Select(s => s.Category)
            .Distinct()
            .ToList();

        categories.Should().BeEquivalentTo(new[] { "Generational", "Academic", "Professional" });
    }

    [Fact]
    public void ByCategory_Generational_ShouldReturn6Entries()
    {
        var suffixes = DropdownData.Suffixes.ByCategory("Generational");

        suffixes.Should().HaveCount(6);
        suffixes.Should().OnlyContain(s => s.Category == "Generational");
    }

    [Fact]
    public void ByCategory_Academic_ShouldReturn10Entries()
    {
        var suffixes = DropdownData.Suffixes.ByCategory("Academic");

        suffixes.Should().HaveCount(10);
        suffixes.Should().OnlyContain(s => s.Category == "Academic");
    }

    [Fact]
    public void ByCategory_Professional_ShouldReturn6Entries()
    {
        var suffixes = DropdownData.Suffixes.ByCategory("Professional");

        suffixes.Should().HaveCount(6);
        suffixes.Should().OnlyContain(s => s.Category == "Professional");
    }

    [Theory]
    [InlineData("Jr.", "Junior", "Generational")]
    [InlineData("Sr.", "Senior", "Generational")]
    [InlineData("II", "The Second", "Generational")]
    [InlineData("PhD", "Doctor of Philosophy", "Academic")]
    [InlineData("MD", "Doctor of Medicine", "Academic")]
    [InlineData("Esq.", "Esquire", "Professional")]
    [InlineData("CPA", "Certified Public Accountant", "Professional")]
    public void All_ShouldContainExpectedSuffixes(string abbreviation, string name, string category)
    {
        var suffix = DropdownData.Suffixes.All.FirstOrDefault(s => s.Abbreviation == abbreviation);

        suffix.Should().NotBeNull();
        suffix!.Name.Should().Be(name);
        suffix.Category.Should().Be(category);
    }

    #endregion

    #region ISelectOption Interface Tests

    [Fact]
    public void SuffixInfo_ShouldImplementISelectOption()
    {
        var suffix = DropdownData.Suffixes.ByNumber(1);

        suffix.Should().BeAssignableTo<ISelectOption>();
    }

    [Fact]
    public void SuffixInfo_Value_ShouldReturnNumberAsString()
    {
        var suffix = DropdownData.Suffixes.ByNumber(7);

        suffix!.Value.Should().Be("7");
    }

    [Fact]
    public void SuffixInfo_Text_ShouldReturnAbbreviation()
    {
        var suffix = DropdownData.Suffixes.ByNumber(7);

        suffix!.Text.Should().Be("PhD");
    }

    [Fact]
    public void AllSuffixes_ShouldBeUsableAsISelectOption()
    {
        IEnumerable<ISelectOption> options = DropdownData.Suffixes.All;

        options.Should().HaveCount(22);
        options.All(o => !string.IsNullOrEmpty(o.Value) && !string.IsNullOrEmpty(o.Text)).Should().BeTrue();
    }

    #endregion

    #region Lookup Tests

    [Theory]
    [InlineData(1, "Jr.")]
    [InlineData(7, "PhD")]
    [InlineData(17, "Esq.")]
    [InlineData(22, "CFP")]
    public void ByNumber_ShouldReturnExpectedSuffix(int number, string expectedAbbreviation)
    {
        var suffix = DropdownData.Suffixes.ByNumber(number);

        suffix.Should().NotBeNull();
        suffix!.Abbreviation.Should().Be(expectedAbbreviation);
        suffix.Number.Should().Be(number);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(23)]
    [InlineData(-1)]
    public void ByNumber_ShouldReturnNullForInvalidNumbers(int number)
    {
        var suffix = DropdownData.Suffixes.ByNumber(number);

        suffix.Should().BeNull();
    }

    [Theory]
    [InlineData("Jr.", "Jr.")]
    [InlineData("jr.", "Jr.")]
    [InlineData(" JR. ", "Jr.")]
    [InlineData("PhD", "PhD")]
    [InlineData("phd", "PhD")]
    public void ByAbbreviation_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedAbbreviation)
    {
        var suffix = DropdownData.Suffixes.ByAbbreviation(input);

        suffix.Should().NotBeNull();
        suffix!.Abbreviation.Should().Be(expectedAbbreviation);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByAbbreviation_ShouldReturnNullForInvalidInput(string? input)
    {
        var suffix = DropdownData.Suffixes.ByAbbreviation(input!);

        suffix.Should().BeNull();
    }

    [Fact]
    public void ByAbbreviation_ShouldReturnNullForNonExistent()
    {
        var suffix = DropdownData.Suffixes.ByAbbreviation("XYZ");

        suffix.Should().BeNull();
    }

    [Theory]
    [InlineData("Junior", "Jr.")]
    [InlineData("junior", "Jr.")]
    [InlineData(" Junior ", "Jr.")]
    [InlineData("Doctor of Philosophy", "PhD")]
    public void ByName_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedAbbreviation)
    {
        var suffix = DropdownData.Suffixes.ByName(input);

        suffix.Should().NotBeNull();
        suffix!.Abbreviation.Should().Be(expectedAbbreviation);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByName_ShouldReturnNullForInvalidInput(string? input)
    {
        var suffix = DropdownData.Suffixes.ByName(input!);

        suffix.Should().BeNull();
    }

    [Theory]
    [InlineData("Generational", 6)]
    [InlineData("generational", 6)]
    [InlineData(" Generational ", 6)]
    public void ByCategory_ShouldBeCaseInsensitiveAndTrimmed(string input, int expectedCount)
    {
        var suffixes = DropdownData.Suffixes.ByCategory(input);

        suffixes.Should().HaveCount(expectedCount);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByCategory_ShouldReturnEmptyForInvalidInput(string? input)
    {
        var suffixes = DropdownData.Suffixes.ByCategory(input!);

        suffixes.Should().BeEmpty();
    }

    [Fact]
    public void ByCategory_ShouldReturnEmptyForNonExistent()
    {
        var suffixes = DropdownData.Suffixes.ByCategory("NotACategory");

        suffixes.Should().BeEmpty();
    }

    #endregion

    #region Builder Tests - Category Filtering

    [Fact]
    public void Builder_Default_ShouldReturnAllSuffixes()
    {
        var suffixes = DropdownData.Suffixes.Builder().Build();

        suffixes.Should().HaveCount(22);
    }

    [Fact]
    public void Builder_InCategory_ShouldFilterByCategory()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .InCategory("Generational")
            .Build();

        suffixes.Should().HaveCount(6);
        suffixes.Should().OnlyContain(s => s.Category == "Generational");
    }

    [Fact]
    public void Builder_InCategory_MultipleCategories_ShouldIncludeAll()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .InCategory("Generational", "Professional")
            .Build();

        suffixes.Should().HaveCount(12);
        suffixes.Should().OnlyContain(s => s.Category == "Generational" || s.Category == "Professional");
    }

    [Fact]
    public void Builder_InCategory_ShouldBeCaseInsensitive()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .InCategory("generational")
            .Build();

        suffixes.Should().HaveCount(6);
        suffixes.Should().OnlyContain(s => s.Category == "Generational");
    }

    [Fact]
    public void Builder_ExcludeCategory_ShouldRemoveCategory()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .ExcludeCategory("Academic")
            .Build();

        suffixes.Should().HaveCount(12);
        suffixes.Should().NotContain(s => s.Category == "Academic");
    }

    [Fact]
    public void Builder_ExcludeCategory_MultipleCategories_ShouldRemoveAll()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .ExcludeCategory("Academic", "Professional")
            .Build();

        suffixes.Should().HaveCount(6);
        suffixes.Should().OnlyContain(s => s.Category == "Generational");
    }

    [Fact]
    public void Builder_ExcludeCategory_ShouldBeCaseInsensitive()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .ExcludeCategory("academic")
            .Build();

        suffixes.Should().NotContain(s => s.Category == "Academic");
    }

    #endregion

    #region Builder Tests - Exclusions (from BaseBuilder)

    [Fact]
    public void Builder_Exclude_ShouldRemoveSpecifiedSuffixes()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .Exclude("1", "2", "3")
            .Build();

        suffixes.Should().HaveCount(19);
        suffixes.Should().NotContain(s => s.Value == "1");
        suffixes.Should().NotContain(s => s.Value == "2");
        suffixes.Should().NotContain(s => s.Value == "3");
    }

    [Fact]
    public void Builder_Exclude_ShouldBeCaseInsensitive()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .Exclude("1")
            .Build();

        suffixes.Should().NotContain(s => s.Value == "1");
    }

    [Fact]
    public void Builder_Exclude_ShouldHandleNullAndEmpty()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .Exclude(null!)
            .Exclude("", "  ")
            .Build();

        suffixes.Should().HaveCount(22);
    }

    #endregion

    #region Builder Tests - Only (Include Specific)

    [Fact]
    public void Builder_Only_ShouldReturnOnlySpecifiedSuffixes()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .Only("1", "7", "17")
            .Build();

        suffixes.Should().HaveCount(3);
        suffixes.Select(s => s.Value).Should().BeEquivalentTo(new[] { "1", "7", "17" });
    }

    [Fact]
    public void Builder_Only_WithExclude_ShouldApplyBoth()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .Only("1", "2", "3", "4")
            .Exclude("4")
            .Build();

        suffixes.Should().HaveCount(3);
        suffixes.Should().NotContain(s => s.Value == "4");
    }

    #endregion

    #region Builder Tests - Ordering

    [Fact]
    public void Builder_OrderByName_ShouldSortAlphabetically()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .OrderByName()
            .Build();

        suffixes.Should().BeInAscendingOrder(s => s.Text, StringComparer.Ordinal);
    }

    [Fact]
    public void Builder_OrderByNameDescending_ShouldSortReverseAlphabetically()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .OrderByNameDescending()
            .Build();

        suffixes.Should().BeInDescendingOrder(s => s.Text, StringComparer.InvariantCulture);
    }

    [Fact]
    public void Builder_OrderByAbbreviation_ShouldSortByValue()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .OrderByAbbreviation()
            .Build();

        suffixes.Should().BeInAscendingOrder(s => s.Value, StringComparer.Ordinal);
    }

    [Fact]
    public void Builder_OrderByAbbreviationDescending_ShouldSortByValueReverse()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .OrderByAbbreviationDescending()
            .Build();

        suffixes.Should().BeInDescendingOrder(s => s.Value, StringComparer.Ordinal);
    }

    #endregion

    #region Builder Tests - Complex Combinations

    [Fact]
    public void Builder_ComplexCombination_ShouldWorkCorrectly()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .InCategory("Generational", "Academic")
            .Exclude("1")
            .OrderByName()
            .Build();

        suffixes.Should().HaveCount(15);
        suffixes.Should().NotContain(s => s.Value == "1");
        suffixes.Should().OnlyContain(s => s.Category == "Generational" || s.Category == "Academic");
        suffixes.Should().BeInAscendingOrder(s => s.Text, StringComparer.Ordinal);
    }

    [Fact]
    public void Builder_ChainedExcludeCategory_ShouldAccumulate()
    {
        var suffixes = DropdownData.Suffixes.Builder()
            .ExcludeCategory("Academic")
            .ExcludeCategory("Professional")
            .Build();

        suffixes.Should().HaveCount(6);
        suffixes.Should().OnlyContain(s => s.Category == "Generational");
    }

    #endregion

    #region Thread Safety Tests

    [Fact]
    public void Suffixes_ShouldBeSafeForConcurrentAccess()
    {
        var results = new List<int>();
        var expectedCount = DropdownData.Suffixes.All.Count;

        Parallel.For(0, 100, _ =>
        {
            var count = DropdownData.Suffixes.All.Count;
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
        var first = DropdownData.Suffixes.All;
        var second = DropdownData.Suffixes.All;

        first.Should().BeSameAs(second);
    }

    [Fact]
    public void All_ShouldBeReadOnly()
    {
        var suffixes = DropdownData.Suffixes.All;

        suffixes.Should().BeAssignableTo<IReadOnlyList<SuffixInfo>>();
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void SuffixInfo_ToString_ShouldReturnFormattedString()
    {
        var suffix = DropdownData.Suffixes.ByNumber(1);

        suffix!.ToString().Should().Be("Junior (Jr.)");
    }

    #endregion
}