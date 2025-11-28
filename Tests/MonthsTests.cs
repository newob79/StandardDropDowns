using FluentAssertions;
using StandardDropdowns;
using StandardDropdowns.Data;
using StandardDropdowns.Interfaces;
using StandardDropdowns.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StandardDropdowns.Tests;

public class MonthsTests
{
    [Fact]
    public void All_ShouldContain12Entries()
    {
        var provider = new Months();

        var all = provider.All;

        all.Should().HaveCount(12);
    }

    [Theory]
    [InlineData(1, "January", "Jan", "01")]
    [InlineData(6, "June", "Jun", "06")]
    [InlineData(12, "December", "Dec", "12")]
    public void ByNumber_ShouldReturnExpectedMonth(int number, string expectedName, string expectedAbbr, string expectedValue)
    {
        var provider = new Months();

        var month = provider.ByNumber(number);

        month.Should().NotBeNull();
        month!.Name.Should().Be(expectedName);
        month.Abbreviation.Should().Be(expectedAbbr);
        month.Value.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(13)]
    [InlineData(-1)]
    public void ByNumber_ShouldReturnNullForInvalidNumbers(int number)
    {
        var provider = new Months();

        var month = provider.ByNumber(number);

        month.Should().BeNull();
    }

    [Theory]
    [InlineData("January", "January")]
    [InlineData("january", "January")]
    [InlineData(" January ", "January")]
    public void ByName_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedName)
    {
        var provider = new Months();

        var month = provider.ByName(input);

        month.Should().NotBeNull();
        month!.Name.Should().Be(expectedName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByName_ShouldReturnNullForInvalidInput(string? input)
    {
        var provider = new Months();

        var month = provider.ByName(input!);

        month.Should().BeNull();
    }

    [Theory]
    [InlineData("Jan", "January")]
    [InlineData("jan", "January")]
    [InlineData(" Jan ", "January")]
    public void ByAbbreviation_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedName)
    {
        var provider = new Months();

        var month = provider.ByAbbreviation(input);

        month.Should().NotBeNull();
        month!.Name.Should().Be(expectedName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByAbbreviation_ShouldReturnNullForInvalidInput(string? input)
    {
        var provider = new Months();

        var month = provider.ByAbbreviation(input!);

        month.Should().BeNull();
    }

    [Fact]
    public void MonthInfo_ShouldImplementISelectOption()
    {
        var provider = new Months();

        var month = provider.ByNumber(1);

        month.Should().BeAssignableTo<ISelectOption>();
    }

    [Fact]
    public void MonthInfo_Value_ShouldReturnTwoDigitMonth()
    {
        var provider = new Months();

        var january = provider.ByNumber(1);
        var december = provider.ByNumber(12);

        january!.Value.Should().Be("01");
        december!.Value.Should().Be("12");
    }

    [Fact]
    public void MonthInfo_Text_ShouldReturnName()
    {
        var provider = new Months();

        var month = provider.ByNumber(4);

        month!.Text.Should().Be("April");
    }

    [Fact]
    public void All_ShouldReturnSameInstance()
    {
        var provider = new Months();

        var first = provider.All;
        var second = provider.All;

        first.Should().BeSameAs(second);
    }

    [Fact]
    public void All_ShouldBeReadOnly()
    {
        var provider = new Months();

        var all = provider.All;

        all.Should().BeAssignableTo<IReadOnlyList<MonthInfo>>();
    }

    [Fact]
    public void Months_ShouldBeSafeForConcurrentAccess()
    {
        var provider = new Months();
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

    [Fact]
    public void MonthInfo_ToString_ShouldReturnFormattedString()
    {
        var provider = new Months();

        var month = provider.ByNumber(1);

        month!.ToString().Should().Be("January (Jan)");
    }
}