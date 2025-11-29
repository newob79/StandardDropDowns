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

public class GendersTests
{
    #region Data Accuracy

    [Fact]
    public void All_ShouldContain3Entries()
    {
        var all = DropdownData.Genders.All;

        all.Should().HaveCount(3);
    }

    [Theory]
    [InlineData(1, "Male", "M")]
    [InlineData(2, "Female", "F")]
    [InlineData(3, "Prefer Not to Say", "U")]
    public void All_ShouldContainExpectedGenders(int number, string name, string code)
    {
        var gender = DropdownData.Genders.ByNumber(number);

        gender.Should().NotBeNull();
        gender!.Name.Should().Be(name);
        gender.Code.Should().Be(code);
    }

    #endregion

    #region ISelectOption Interface Tests

    [Fact]
    public void GenderInfo_ShouldImplementISelectOption()
    {
        var gender = DropdownData.Genders.ByNumber(1);

        gender.Should().BeAssignableTo<ISelectOption>();
    }

    [Fact]
    public void GenderInfo_Value_ShouldReturnCode()
    {
        var gender = DropdownData.Genders.ByNumber(1);

        gender!.Value.Should().Be("M");
    }

    [Fact]
    public void GenderInfo_Text_ShouldReturnName()
    {
        var gender = DropdownData.Genders.ByNumber(1);

        gender!.Text.Should().Be("Male");
    }

    [Fact]
    public void AllGenders_ShouldBeUsableAsISelectOption()
    {
        IEnumerable<ISelectOption> options = DropdownData.Genders.All;

        options.Should().HaveCount(3);
        options.All(o => !string.IsNullOrEmpty(o.Value) && !string.IsNullOrEmpty(o.Text)).Should().BeTrue();
    }

    #endregion

    #region Lookup Tests

    [Theory]
    [InlineData("M", "Male")]
    [InlineData("m", "Male")]
    [InlineData(" M ", "Male")]
    public void ByCode_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedName)
    {
        var gender = DropdownData.Genders.ByCode(input);

        gender.Should().NotBeNull();
        gender!.Name.Should().Be(expectedName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByCode_ShouldReturnNullForInvalidInput(string? input)
    {
        var gender = DropdownData.Genders.ByCode(input!);

        gender.Should().BeNull();
    }

    [Fact]
    public void ByCode_ShouldReturnNullForNonExistent()
    {
        var gender = DropdownData.Genders.ByCode("X");

        gender.Should().BeNull();
    }

    [Theory]
    [InlineData("Male", "M")]
    [InlineData("male", "M")]
    [InlineData(" Male ", "M")]
    public void ByName_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedCode)
    {
        var gender = DropdownData.Genders.ByName(input);

        gender.Should().NotBeNull();
        gender!.Code.Should().Be(expectedCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByName_ShouldReturnNullForInvalidInput(string? input)
    {
        var gender = DropdownData.Genders.ByName(input!);

        gender.Should().BeNull();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(4)]
    [InlineData(-1)]
    public void ByNumber_ShouldReturnNullForInvalidNumbers(int number)
    {
        var gender = DropdownData.Genders.ByNumber(number);

        gender.Should().BeNull();
    }

    #endregion

    #region Thread Safety Tests

    [Fact]
    public void Genders_ShouldBeSafeForConcurrentAccess()
    {
        var results = new List<int>();
        var expectedCount = DropdownData.Genders.All.Count;

        Parallel.For(0, 100, _ =>
        {
            var count = DropdownData.Genders.All.Count;
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
        var first = DropdownData.Genders.All;
        var second = DropdownData.Genders.All;

        first.Should().BeSameAs(second);
    }

    [Fact]
    public void All_ShouldBeReadOnly()
    {
        var genders = DropdownData.Genders.All;

        genders.Should().BeAssignableTo<IReadOnlyList<GenderInfo>>();
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void GenderInfo_ToString_ShouldReturnFormattedString()
    {
        var gender = DropdownData.Genders.ByNumber(1);

        gender!.ToString().Should().Be("Male (M)");
    }

    #endregion
}