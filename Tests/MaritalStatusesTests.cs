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

public class MaritalStatusesTests
{
    #region Data Accuracy

    [Fact]
    public void All_ShouldContain6Entries()
    {
        var all = DropdownData.MaritalStatuses.All;

        all.Should().HaveCount(6);
    }

    [Theory]
    [InlineData(1, "Single", "S")]
    [InlineData(2, "Married", "M")]
    [InlineData(3, "Divorced", "D")]
    [InlineData(4, "Widowed", "W")]
    [InlineData(5, "Separated", "SP")]
    [InlineData(6, "Domestic Partnership", "DP")]
    public void All_ShouldContainExpectedStatuses(int number, string name, string code)
    {
        var status = DropdownData.MaritalStatuses.ByNumber(number);

        status.Should().NotBeNull();
        status!.Name.Should().Be(name);
        status.Code.Should().Be(code);
    }

    #endregion

    #region ISelectOption Interface Tests

    [Fact]
    public void MaritalStatusInfo_ShouldImplementISelectOption()
    {
        var status = DropdownData.MaritalStatuses.ByNumber(1);

        status.Should().BeAssignableTo<ISelectOption>();
    }

    [Fact]
    public void MaritalStatusInfo_Value_ShouldReturnCode()
    {
        var status = DropdownData.MaritalStatuses.ByNumber(1);

        status!.Value.Should().Be("S");
    }

    [Fact]
    public void MaritalStatusInfo_Text_ShouldReturnName()
    {
        var status = DropdownData.MaritalStatuses.ByNumber(1);

        status!.Text.Should().Be("Single");
    }

    [Fact]
    public void AllStatuses_ShouldBeUsableAsISelectOption()
    {
        IEnumerable<ISelectOption> options = DropdownData.MaritalStatuses.All;

        options.Should().HaveCount(6);
        options.All(o => !string.IsNullOrEmpty(o.Value) && !string.IsNullOrEmpty(o.Text)).Should().BeTrue();
    }

    #endregion

    #region Lookup Tests

    [Theory]
    [InlineData("S", "Single")]
    [InlineData("s", "Single")]
    [InlineData(" S ", "Single")]
    [InlineData("DP", "Domestic Partnership")]
    [InlineData("dp", "Domestic Partnership")]
    public void ByCode_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedName)
    {
        var status = DropdownData.MaritalStatuses.ByCode(input);

        status.Should().NotBeNull();
        status!.Name.Should().Be(expectedName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByCode_ShouldReturnNullForInvalidInput(string? input)
    {
        var status = DropdownData.MaritalStatuses.ByCode(input!);

        status.Should().BeNull();
    }

    [Fact]
    public void ByCode_ShouldReturnNullForNonExistent()
    {
        var status = DropdownData.MaritalStatuses.ByCode("X");

        status.Should().BeNull();
    }

    [Theory]
    [InlineData("Single", "S")]
    [InlineData("single", "S")]
    [InlineData(" Single ", "S")]
    [InlineData("Domestic Partnership", "DP")]
    public void ByName_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedCode)
    {
        var status = DropdownData.MaritalStatuses.ByName(input);

        status.Should().NotBeNull();
        status!.Code.Should().Be(expectedCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByName_ShouldReturnNullForInvalidInput(string? input)
    {
        var status = DropdownData.MaritalStatuses.ByName(input!);

        status.Should().BeNull();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(7)]
    [InlineData(-1)]
    public void ByNumber_ShouldReturnNullForInvalidNumbers(int number)
    {
        var status = DropdownData.MaritalStatuses.ByNumber(number);

        status.Should().BeNull();
    }

    #endregion

    #region Thread Safety Tests

    [Fact]
    public void MaritalStatuses_ShouldBeSafeForConcurrentAccess()
    {
        var results = new List<int>();
        var expectedCount = DropdownData.MaritalStatuses.All.Count;

        Parallel.For(0, 100, _ =>
        {
            var count = DropdownData.MaritalStatuses.All.Count;
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
        var first = DropdownData.MaritalStatuses.All;
        var second = DropdownData.MaritalStatuses.All;

        first.Should().BeSameAs(second);
    }

    [Fact]
    public void All_ShouldBeReadOnly()
    {
        var statuses = DropdownData.MaritalStatuses.All;

        statuses.Should().BeAssignableTo<IReadOnlyList<MaritalStatusInfo>>();
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void MaritalStatusInfo_ToString_ShouldReturnFormattedString()
    {
        var status = DropdownData.MaritalStatuses.ByNumber(1);

        status!.ToString().Should().Be("Single (S)");
    }

    #endregion
}