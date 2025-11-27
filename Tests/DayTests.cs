using FluentAssertions;
using StandardDropdowns.Interfaces;
using StandardDropDowns.Data;
using StandardDropDowns.Models;
using StandardDropdowns.Data;
using StandardDropdowns.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StandardDropdowns.Tests;

public class DaysTests
{
    [Fact]
    public void All_ShouldContain7Entries()
    {
        var provider = new Days();

        var all = provider.All;

        all.Should().HaveCount(7);
    }

    [Theory]
    [InlineData(0, "Sunday", "Sun", "Su", "0")]
    [InlineData(1, "Monday", "Mon", "M", "1")]
    [InlineData(6, "Saturday", "Sat", "Sa", "6")]
    public void ByNumber_ShouldReturnExpectedDay(int number, string expectedName, string expectedAbbr, string expectedShort, string expectedValue)
    {
        var provider = new Days();

        var day = provider.ByNumber(number);

        day.Should().NotBeNull();
        day!.Name.Should().Be(expectedName);
        day.Abbreviation.Should().Be(expectedAbbr);
        day.ShortAbbreviation.Should().Be(expectedShort);
        day.Value.Should().Be(expectedValue);
        day.Text.Should().Be(expectedName);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(7)]
    public void ByNumber_ShouldReturnNullForInvalidNumber(int number)
    {
        var provider = new Days();

        var day = provider.ByNumber(number);

        day.Should().BeNull();
    }

    [Theory]
    [InlineData("Sunday", "Sunday")]
    [InlineData("sunday", "Sunday")]
    [InlineData(" Sunday ", "Sunday")]
    public void ByName_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedName)
    {
        var provider = new Days();

        var day = provider.ByName(input);

        day.Should().NotBeNull();
        day!.Name.Should().Be(expectedName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByName_ShouldReturnNullForInvalidInput(string? input)
    {
        var provider = new Days();

        var day = provider.ByName(input!);

        day.Should().BeNull();
    }

    [Theory]
    [InlineData("Sun", "Sunday")]
    [InlineData("sun", "Sunday")]
    [InlineData(" Sun ", "Sunday")]
    public void ByAbbreviation_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedName)
    {
        var provider = new Days();

        var day = provider.ByAbbreviation(input);

        day.Should().NotBeNull();
        day!.Name.Should().Be(expectedName);
    }

    [Theory]
    [InlineData("Su", "Sunday")]
    [InlineData("su", "Sunday")]
    [InlineData(" Su ", "Sunday")]
    public void ByShortAbbreviation_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedName)
    {
        var provider = new Days();

        var day = provider.ByShortAbbreviation(input);

        day.Should().NotBeNull();
        day!.Name.Should().Be(expectedName);
    }

    [Fact]
    public void DaysOfWeekInfo_ShouldImplementISelectOption()
    {
        var provider = new Days();

        var day = provider.ByNumber(0);

        day.Should().BeAssignableTo<ISelectOption>();
    }

    [Fact]
    public void ToString_ShouldReturnFormattedString()
    {
        var provider = new Days();

        var day = provider.ByNumber(0);

        day!.ToString().Should().Be("Sunday (Sun)");
    }

    [Fact]
    public void All_ShouldBeReadOnlyAndSameInstance()
    {
        var provider = new Days();

        var first = provider.All;
        var second = provider.All;

        first.Should().BeSameAs(second);
        first.Should().BeAssignableTo<IReadOnlyList<DaysOfWeekInfo>>();
    }

    [Fact]
    public void Days_ShouldBeSafeForConcurrentAccess()
    {
        var provider = new Days();
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
}