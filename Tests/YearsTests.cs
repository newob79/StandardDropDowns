using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using StandardDropdowns;
using StandardDropdowns.Data;
using StandardDropdowns.Interfaces;
using StandardDropdowns.Models;
using Xunit;

namespace StandardDropdowns.Tests;

public class YearsTests
{
    private readonly int _currentYear = DateTime.Now.Year;

    #region Last() Tests

    [Fact]
    public void Last_ShouldReturnCorrectCount()
    {
        var years = new Years().Last(100);

        years.Should().HaveCount(100);
    }

    [Fact]
    public void Last_ShouldIncludeCurrentYear()
    {
        var years = new Years().Last(10);

        years.Should().Contain(y => y.Year == _currentYear);
    }

    [Fact]
    public void Last_ShouldStartFromCorrectYear()
    {
        var years = new Years().Last(100);

        int expectedOldestYear = _currentYear - 99;
        years.Should().Contain(y => y.Year == expectedOldestYear);
    }

    [Fact]
    public void Last_ShouldBeDescendingByDefault()
    {
        var years = new Years().Last(10);

        years.Should().BeInDescendingOrder(y => y.Year);
        years.First().Year.Should().Be(_currentYear);
    }

    [Fact]
    public void Last_Ascending_ShouldReturnAscendingOrder()
    {
        var years = new Years().Last(10, descending: false);

        years.Should().BeInAscendingOrder(y => y.Year);
        years.First().Year.Should().Be(_currentYear - 9);
        years.Last().Year.Should().Be(_currentYear);
    }

    [Fact]
    public void Last_SingleYear_ShouldReturnOnlyCurrentYear()
    {
        var years = new Years().Last(1);

        years.Should().HaveCount(1);
        years.First().Year.Should().Be(_currentYear);
    }

    #endregion

    #region Next() Tests

    [Fact]
    public void Next_ShouldReturnCorrectCount()
    {
        var years = new Years().Next(10);

        years.Should().HaveCount(10);
    }

    [Fact]
    public void Next_ShouldIncludeCurrentYear()
    {
        var years = new Years().Next(10);

        years.Should().Contain(y => y.Year == _currentYear);
    }

    [Fact]
    public void Next_ShouldEndAtCorrectYear()
    {
        var years = new Years().Next(10);

        int expectedFutureYear = _currentYear + 9;
        years.Should().Contain(y => y.Year == expectedFutureYear);
    }

    [Fact]
    public void Next_ShouldBeAscendingByDefault()
    {
        var years = new Years().Next(10);

        years.Should().BeInAscendingOrder(y => y.Year);
        years.First().Year.Should().Be(_currentYear);
    }

    [Fact]
    public void Next_Descending_ShouldReturnDescendingOrder()
    {
        var years = new Years().Next(10, descending: true);

        years.Should().BeInDescendingOrder(y => y.Year);
        years.First().Year.Should().Be(_currentYear + 9);
        years.Last().Year.Should().Be(_currentYear);
    }

    [Fact]
    public void Next_SingleYear_ShouldReturnOnlyCurrentYear()
    {
        var years = new Years().Next(1);

        years.Should().HaveCount(1);
        years.First().Year.Should().Be(_currentYear);
    }

    #endregion

    #region Range() Tests

    [Fact]
    public void Range_ShouldReturnCorrectCount()
    {
        var years = new Years().Range(1990, 2030);

        years.Should().HaveCount(41); // 2030 - 1990 + 1 = 41
    }

    [Fact]
    public void Range_ShouldBeAscendingByDefault()
    {
        var years = new Years().Range(1990, 2000);

        years.Should().BeInAscendingOrder(y => y.Year);
        years.First().Year.Should().Be(1990);
        years.Last().Year.Should().Be(2000);
    }

    [Fact]
    public void Range_Descending_ShouldReturnDescendingOrder()
    {
        var years = new Years().Range(1990, 2000, descending: true);

        years.Should().BeInDescendingOrder(y => y.Year);
        years.First().Year.Should().Be(2000);
        years.Last().Year.Should().Be(1990);
    }

    [Fact]
    public void Range_StartGreaterThanEnd_ShouldNormalizeAndReturnAscending()
    {
        var years = new Years().Range(2000, 1990);

        years.Should().HaveCount(11);
        years.Should().BeInAscendingOrder(y => y.Year);
        years.First().Year.Should().Be(1990);
        years.Last().Year.Should().Be(2000);
    }

    [Fact]
    public void Range_SingleYear_ShouldReturnOneItem()
    {
        var years = new Years().Range(2020, 2020);

        years.Should().HaveCount(1);
        years.First().Year.Should().Be(2020);
    }

    #endregion

    #region Validation Tests

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Last_InvalidCount_ShouldThrowArgumentException(int count)
    {
        var years = new Years();

        Action act = () => years.Last(count);

        act.Should().Throw<ArgumentException>()
            .WithParameterName("count")
            .WithMessage("*greater than zero*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Next_InvalidCount_ShouldThrowArgumentException(int count)
    {
        var years = new Years();

        Action act = () => years.Next(count);

        act.Should().Throw<ArgumentException>()
            .WithParameterName("count")
            .WithMessage("*greater than zero*");
    }

    #endregion

    #region ISelectOption Interface Tests

    [Fact]
    public void YearInfo_ShouldImplementISelectOption()
    {
        var years = new Years().Last(5);

        years.First().Should().BeAssignableTo<ISelectOption>();
    }

    [Fact]
    public void YearInfo_Value_ShouldReturnYearAsString()
    {
        var years = new Years().Range(2020, 2020);

        years.First().Value.Should().Be("2020");
    }

    [Fact]
    public void YearInfo_Text_ShouldReturnYearAsString()
    {
        var years = new Years().Range(2020, 2020);

        years.First().Text.Should().Be("2020");
    }

    [Fact]
    public void AllYears_ShouldBeUsableAsISelectOption()
    {
        IEnumerable<ISelectOption> options = new Years().Last(10);

        options.Should().HaveCount(10);
        options.All(o => !string.IsNullOrEmpty(o.Value) && !string.IsNullOrEmpty(o.Text)).Should().BeTrue();
    }

    #endregion

    #region Immutability Tests

    [Fact]
    public void Years_ShouldReturnReadOnlyList()
    {
        var years = new Years().Last(10);

        years.Should().BeAssignableTo<IReadOnlyList<YearInfo>>();
    }

    [Fact]
    public void Years_MultipleCalls_ShouldReturnIndependentLists()
    {
        var provider = new Years();

        var first = provider.Last(10);
        var second = provider.Last(10);

        first.Should().NotBeSameAs(second);
        first.Should().BeEquivalentTo(second);
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void YearInfo_ToString_ShouldReturnYear()
    {
        var years = new Years().Range(2020, 2020);

        years.First().ToString().Should().Be("2020");
    }

    #endregion

    #region Use Case Tests

    [Fact]
    public void Last100Years_ShouldWorkForBirthYearDropdown()
    {
        // Birth year dropdown use case
        var birthYears = new Years().Last(100);

        birthYears.Should().HaveCount(100);
        birthYears.Should().BeInDescendingOrder(y => y.Year);
        birthYears.First().Year.Should().Be(_currentYear);
        birthYears.Last().Year.Should().Be(_currentYear - 99);
    }

    [Fact]
    public void Next10Years_ShouldWorkForExpirationYearDropdown()
    {
        // Credit card expiration year use case
        var expirationYears = new Years().Next(10);

        expirationYears.Should().HaveCount(10);
        expirationYears.Should().BeInAscendingOrder(y => y.Year);
        expirationYears.First().Year.Should().Be(_currentYear);
        expirationYears.Last().Year.Should().Be(_currentYear + 9);
    }

    [Fact]
    public void ExplicitRange_ShouldWorkForHistoricalDropdown()
    {
        // Historical events dropdown use case
        var historicalYears = new Years().Range(1900, 2000);

        historicalYears.Should().HaveCount(101);
        historicalYears.First().Year.Should().Be(1900);
        historicalYears.Last().Year.Should().Be(2000);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void Last_LargeCount_ShouldWork()
    {
        var years = new Years().Last(500);

        years.Should().HaveCount(500);
    }

    [Fact]
    public void Next_LargeCount_ShouldWork()
    {
        var years = new Years().Next(500);

        years.Should().HaveCount(500);
    }

    [Fact]
    public void Range_HistoricalYears_ShouldWork()
    {
        var years = new Years().Range(1800, 1850);

        years.Should().HaveCount(51);
        years.First().Year.Should().Be(1800);
    }

    [Fact]
    public void Range_FutureYears_ShouldWork()
    {
        var years = new Years().Range(2050, 2100);

        years.Should().HaveCount(51);
        years.First().Year.Should().Be(2050);
    }

    #endregion
}