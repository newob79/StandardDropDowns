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

public class PrefixTitleTests
{
    #region Data Accuracy

    [Fact]
    public void All_ShouldContain9Entries()
    {
        var provider = new PrefixTitle();

        var all = provider.All;

        all.Should().HaveCount(9);
    }

    [Fact]
    public void All_ShouldContainExpectedTitles()
    {
        var provider = new PrefixTitle();

        var abbreviations = provider.All.Select(t => t.Abbreviation).ToList();

        abbreviations.Should().BeEquivalentTo(new[]
        {
        "Mr.", "Miss", "Ms.", "Mrs.",
        "Dr.", "Prof.",
        "Rev.", "Fr.",
        "Hon."
    });
    }

    [Fact]
    public void All_ShouldContainExpectedCategories()
    {
        var provider = new PrefixTitle();

        var categories = provider.All.Select(t => t.Category).Distinct().ToList();

        categories.Should().BeEquivalentTo(new[] { "Civilian", "Professional", "Religious", "Honorific" });
    }

    #endregion

    #region Lookup Tests

    [Theory]
    [InlineData(1, "Mr.")]
    [InlineData(2, "Miss")]
    [InlineData(3, "Ms.")]
    [InlineData(4, "Mrs.")]
    [InlineData(5, "Dr.")]
    [InlineData(6, "Prof.")]
    [InlineData(7, "Rev.")]
    [InlineData(8, "Fr.")]
    [InlineData(9, "Hon.")]
    public void ByNumber_ShouldReturnExpectedTitle(int number, string expectedAbbreviation)
    {
        var provider = new PrefixTitle();

        var title = provider.ByNumber(number);

        title.Should().NotBeNull();
        title!.Abbreviation.Should().Be(expectedAbbreviation);
        title.Number.Should().Be(number);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(11)]
    [InlineData(-1)]
    public void ByNumber_ShouldReturnNullForInvalidNumbers(int number)
    {
        var provider = new PrefixTitle();

        var title = provider.ByNumber(number);

        title.Should().BeNull();
    }

    [Theory]
    [InlineData("Mr.", "Mr.")]
    [InlineData("mr.", "Mr.")]
    [InlineData(" MR. ", "Mr.")]
    [InlineData("Rev.", "Rev.")]
    [InlineData("rev.", "Rev.")]
    public void ByAbbreviation_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedAbbreviation)
    {
        var provider = new PrefixTitle();

        var title = provider.ByAbbreviation(input);

        title.Should().NotBeNull();
        title!.Abbreviation.Should().Be(expectedAbbreviation);
    }

    #endregion

    #region ISelectOption Tests

    [Fact]
    public void TitleInfo_ShouldImplementISelectOption()
    {
        var provider = new PrefixTitle();

        var title = provider.ByNumber(1);

        title.Should().BeAssignableTo<ISelectOption>();
    }

    [Fact]
    public void TitleInfo_Value_ShouldReturnNumberString()
    {
        var provider = new PrefixTitle();

        var title = provider.ByNumber(4);

        title!.Value.Should().Be("4");
    }

    [Fact]
    public void TitleInfo_Text_ShouldReturnName()
    {
        var provider = new PrefixTitle();

        var title = provider.ByNumber(2);

        title!.Text.Should().Be("Miss");
    }

    #endregion

    #region Immutability & Thread Safety

    [Fact]
    public void All_ShouldReturnSameInstance()
    {
        var provider = new PrefixTitle();

        var first = provider.All;
        var second = provider.All;

        first.Should().BeSameAs(second);
    }

    [Fact]
    public void All_ShouldBeReadOnly()
    {
        var provider = new PrefixTitle();

        var all = provider.All;

        all.Should().BeAssignableTo<IReadOnlyList<PrefixTitleInfo>>();
    }

    [Fact]
    public void PrefixTitles_ShouldBeSafeForConcurrentAccess()
    {
        var provider = new PrefixTitle();
        var results = new List<int>();
        var expected = provider.All.Count;

        Parallel.For(0, 100, _ =>
        {
            var count = provider.All.Count;
            lock (results)
            {
                results.Add(count);
            }
        });

        results.Should().AllBeEquivalentTo(expected);
    }

    #endregion

    #region ToString

    [Fact]
    public void TitleInfo_ToString_ShouldReturnFormattedString()
    {
        var provider = new PrefixTitle();

        var title = provider.ByNumber(1);

        title!.ToString().Should().Be("Mister (1)");
    }

    #endregion

    #region Builder Tests - Category Filtering

    [Fact]
    public void Builder_InCategory_Civilian_ShouldReturn4Titles()
    {
        var titles = DropdownData.PrefixTitles.Builder()
            .InCategory("Civilian")
            .Build();

        titles.Should().HaveCount(4);
        titles.Should().OnlyContain(t => t.Category == "Civilian");
    }

    [Fact]
    public void Builder_InCategory_Religious_ShouldReturn2Titles()
    {
        var titles = DropdownData.PrefixTitles.Builder()
            .InCategory("Religious")
            .Build();

        titles.Should().HaveCount(2);
        titles.Should().OnlyContain(t => t.Category == "Religious");
    }

    [Fact]
    public void Builder_Default_ShouldReturnAllTitles()
    {
        var titles = DropdownData.PrefixTitles.Builder().Build();

        titles.Should().HaveCount(DropdownData.PrefixTitles.All.Count);
    }

    [Fact]
    public void Builder_InCategory_ShouldFilterByCategory()
    {
        var titles = DropdownData.PrefixTitles.Builder()
            .InCategory("Civilian")
            .Build();

        titles.Should().OnlyContain(t => t.Category == "Civilian");
    }

    [Fact]
    public void Builder_InCategory_MultipleCategoriesShouldIncludeAll()
    {
        var titles = DropdownData.PrefixTitles.Builder()
            .InCategory("Civilian", "Professional")
            .Build();

        titles.Should().OnlyContain(t => t.Category == "Civilian" || t.Category == "Professional");
    }

    [Fact]
    public void Builder_InCategory_ShouldBeCaseInsensitive()
    {
        var titles = DropdownData.PrefixTitles.Builder()
            .InCategory("civilian")
            .Build();

        titles.Should().OnlyContain(t => t.Category == "Civilian");
    }

    [Fact]
    public void Builder_ExcludeCategory_ShouldRemoveCategory()
    {
        var allCount = DropdownData.PrefixTitles.All.Count;
        var professionalCount = DropdownData.PrefixTitles.All.Count(t => t.Category == "Professional");

        var titles = DropdownData.PrefixTitles.Builder()
            .ExcludeCategory("Professional")
            .Build();

        titles.Should().HaveCount(allCount - professionalCount);
        titles.Should().NotContain(t => t.Category == "Professional");
    }

    [Fact]
    public void Builder_ExcludeCategory_ShouldBeCaseInsensitive()
    {
        var titles = DropdownData.PrefixTitles.Builder()
            .ExcludeCategory("professional")
            .Build();

        titles.Should().NotContain(t => t.Category == "Professional");
    }

    #endregion

    #region Builder Tests - Exclusions (from BaseBuilder)

    [Fact]
    public void Builder_Exclude_ShouldRemoveSpecifiedTitles()
    {
        var titles = DropdownData.PrefixTitles.Builder()
            .Exclude("1", "2")  // By Value (Number as string)
            .Build();

        titles.Should().NotContain(t => t.Value == "1");
        titles.Should().NotContain(t => t.Value == "2");
    }

    [Fact]
    public void Builder_Only_ShouldReturnOnlySpecifiedTitles()
    {
        var titles = DropdownData.PrefixTitles.Builder()
            .Only("1", "4")  // Mr. and Dr.
            .Build();

        titles.Should().HaveCount(2);
        titles.Select(t => t.Value).Should().BeEquivalentTo(new[] { "1", "4" });
    }

    [Fact]
    public void Builder_Only_WithExclude_ShouldApplyBoth()
    {
        var titles = DropdownData.PrefixTitles.Builder()
            .Only("1", "2", "3")
            .Exclude("3")
            .Build();

        titles.Should().HaveCount(2);
        titles.Should().NotContain(t => t.Value == "3");
    }

    #endregion

    #region Builder Tests - Ordering

    [Fact]
    public void Builder_OrderByName_ShouldSortAlphabetically()
    {
        var titles = DropdownData.PrefixTitles.Builder()
            .OrderByName()
            .Build();

        titles.Should().BeInAscendingOrder(t => t.Name, StringComparer.InvariantCulture);
    }

    [Fact]
    public void Builder_OrderByNameDescending_ShouldSortReverseAlphabetically()
    {
        var titles = DropdownData.PrefixTitles.Builder()
            .OrderByNameDescending()
            .Build();

        titles.Should().BeInDescendingOrder(t => t.Name, StringComparer.InvariantCulture);
    }

    #endregion

    #region Builder Tests - Complex Combinations

    [Fact]
    public void Builder_ComplexCombination_ShouldWorkCorrectly()
    {
        var titles = DropdownData.PrefixTitles.Builder()
            .InCategory("Civilian", "Professional")
            .Exclude("1")  // Exclude Mr.
            .OrderByName()
            .Build();

        titles.Should().NotContain(t => t.Value == "1");
        titles.Should().OnlyContain(t => t.Category == "Civilian" || t.Category == "Professional");
        titles.Should().BeInAscendingOrder(t => t.Name, StringComparer.InvariantCulture);
    }

    #endregion
}