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

public class NumbersTests
{
    #region Basic Range Tests

    [Fact]
    public void Range_BasicRange_ShouldReturn10Numbers()
    {
        var numbers = new Numbers().Range(1, 10);

        numbers.Should().HaveCount(10);
        numbers.Select(n => n.Number).Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
    }

    [Fact]
    public void Range_BasicRange_ShouldBeAscendingByDefault()
    {
        var numbers = new Numbers().Range(1, 10);

        numbers.Should().BeInAscendingOrder(n => n.Number);
    }

    [Fact]
    public void Range_SingleNumber_ShouldReturnOneItem()
    {
        var numbers = new Numbers().Range(5, 5);

        numbers.Should().HaveCount(1);
        numbers.First().Number.Should().Be(5);
    }

    [Fact]
    public void Range_NegativeNumbers_ShouldWork()
    {
        var numbers = new Numbers().Range(-5, 5);

        numbers.Should().HaveCount(11);
        numbers.First().Number.Should().Be(-5);
        numbers.Last().Number.Should().Be(5);
    }

    [Fact]
    public void Range_StartGreaterThanEnd_ShouldNormalizeAndReturnAscending()
    {
        var numbers = new Numbers().Range(10, 1);

        numbers.Should().HaveCount(10);
        numbers.Should().BeInAscendingOrder(n => n.Number);
        numbers.First().Number.Should().Be(1);
        numbers.Last().Number.Should().Be(10);
    }

    #endregion

    #region Descending Tests

    [Fact]
    public void Range_Descending_ShouldReturnDescendingOrder()
    {
        var numbers = new Numbers().Range(1, 10, descending: true);

        numbers.Should().HaveCount(10);
        numbers.Should().BeInDescendingOrder(n => n.Number);
        numbers.First().Number.Should().Be(10);
        numbers.Last().Number.Should().Be(1);
    }

    [Fact]
    public void Range_StartGreaterThanEnd_Descending_ShouldReturnDescending()
    {
        var numbers = new Numbers().Range(10, 1, descending: true);

        numbers.Should().BeInDescendingOrder(n => n.Number);
    }

    #endregion

    #region Step Tests

    [Fact]
    public void Range_WithStep_ShouldReturnCorrectIncrements()
    {
        var numbers = new Numbers().Range(0, 100, step: 10);

        numbers.Should().HaveCount(11);
        numbers.Select(n => n.Number).Should().BeEquivalentTo(new[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 });
    }

    [Fact]
    public void Range_WithStep_Descending_ShouldWork()
    {
        var numbers = new Numbers().Range(0, 100, step: 10, descending: true);

        numbers.Should().HaveCount(11);
        numbers.First().Number.Should().Be(100);
        numbers.Last().Number.Should().Be(0);
    }

    [Fact]
    public void Range_StepLargerThanRange_ShouldReturnOnlyStart()
    {
        var numbers = new Numbers().Range(1, 5, step: 10);

        numbers.Should().HaveCount(1);
        numbers.First().Number.Should().Be(1);
    }

    [Fact]
    public void Range_StepDoesNotDivideEvenly_ShouldStopBeforeExceedingEnd()
    {
        var numbers = new Numbers().Range(1, 10, step: 3);

        // 1, 4, 7, 10
        numbers.Should().HaveCount(4);
        numbers.Select(n => n.Number).Should().BeEquivalentTo(new[] { 1, 4, 7, 10 });
    }

    [Fact]
    public void Range_StepOf5_ShouldWork()
    {
        var numbers = new Numbers().Range(0, 25, step: 5);

        numbers.Select(n => n.Number).Should().BeEquivalentTo(new[] { 0, 5, 10, 15, 20, 25 });
    }

    #endregion

    #region Validation Tests

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Range_InvalidStep_ShouldThrowArgumentException(int step)
    {
        var numbers = new Numbers();

        Action act = () => numbers.Range(1, 10, step: step);

        act.Should().Throw<ArgumentException>()
            .WithParameterName("step")
            .WithMessage("*greater than zero*");
    }

    #endregion

    #region ISelectOption Interface Tests

    [Fact]
    public void NumberInfo_ShouldImplementISelectOption()
    {
        var numbers = new Numbers().Range(1, 5);

        numbers.First().Should().BeAssignableTo<ISelectOption>();
    }

    [Fact]
    public void NumberInfo_Value_ShouldReturnNumberAsString()
    {
        var numbers = new Numbers().Range(42, 42);

        numbers.First().Value.Should().Be("42");
    }

    [Fact]
    public void NumberInfo_Text_ShouldReturnNumberAsString()
    {
        var numbers = new Numbers().Range(42, 42);

        numbers.First().Text.Should().Be("42");
    }

    [Fact]
    public void AllNumbers_ShouldBeUsableAsISelectOption()
    {
        IEnumerable<ISelectOption> options = new Numbers().Range(1, 10);

        options.Should().HaveCount(10);
        options.All(o => !string.IsNullOrEmpty(o.Value) && !string.IsNullOrEmpty(o.Text)).Should().BeTrue();
    }

    #endregion

    #region Immutability Tests

    [Fact]
    public void Range_ShouldReturnReadOnlyList()
    {
        var numbers = new Numbers().Range(1, 10);

        numbers.Should().BeAssignableTo<IReadOnlyList<NumberInfo>>();
    }

    [Fact]
    public void Range_MultipleCalls_ShouldReturnIndependentLists()
    {
        var provider = new Numbers();

        var first = provider.Range(1, 10);
        var second = provider.Range(1, 10);

        first.Should().NotBeSameAs(second);
        first.Should().BeEquivalentTo(second);
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void NumberInfo_ToString_ShouldReturnNumber()
    {
        var numbers = new Numbers().Range(42, 42);

        numbers.First().ToString().Should().Be("42");
    }

    [Fact]
    public void NumberInfo_ToString_NegativeNumber_ShouldWork()
    {
        var numbers = new Numbers().Range(-5, -5);

        numbers.First().ToString().Should().Be("-5");
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void Range_LargeRange_ShouldWork()
    {
        var numbers = new Numbers().Range(1, 1000);

        numbers.Should().HaveCount(1000);
        numbers.First().Number.Should().Be(1);
        numbers.Last().Number.Should().Be(1000);
    }

    [Fact]
    public void Range_ZeroInRange_ShouldWork()
    {
        var numbers = new Numbers().Range(-2, 2);

        numbers.Should().HaveCount(5);
        numbers.Should().Contain(n => n.Number == 0);
    }

    #endregion
}