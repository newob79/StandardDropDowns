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
    public void All_ShouldContain5Entries()
    {
        var provider = new PrefixTitle();

        var all = provider.All;

        all.Should().HaveCount(5);
    }

    [Fact]
    public void All_ShouldContainExpectedTitles()
    {
        var provider = new PrefixTitle();

        var names = provider.All.Select(t => t.Name).ToList();

        names.Should().ContainInOrder(new[] { "Mr.", "Ms.", "Mrs.", "Dr.", "Prof." });
    }

    #endregion

    #region Lookup Tests

    [Theory]
    [InlineData(1, "Mr.")]
    [InlineData(2, "Ms.")]
    [InlineData(3, "Mrs.")]
    [InlineData(4, "Dr.")]
    [InlineData(5, "Prof.")]
    public void ByNumber_ShouldReturnExpectedTitle(int number, string expectedName)
    {
        var provider = new PrefixTitle();

        var title = provider.ByNumber(number);

        title.Should().NotBeNull();
        title!.Name.Should().Be(expectedName);
        title.Number.Should().Be(number);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(6)]
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
    public void ByName_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedName)
    {
        var provider = new PrefixTitle();

        var title = provider.ByName(input);

        title.Should().NotBeNull();
        title!.Name.Should().Be(expectedName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByName_ShouldReturnNullForInvalidInput(string? input)
    {
        var provider = new PrefixTitle();

        var title = provider.ByName(input!);

        title.Should().BeNull();
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

        title!.Text.Should().Be("Ms.");
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

        title!.ToString().Should().Be("Mr. (1)");
    }

    #endregion
}