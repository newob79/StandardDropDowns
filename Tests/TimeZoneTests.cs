using System;
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

public class TimeZonesTests
{
    #region Data Accuracy Tests

    [Fact]
    public void All_ShouldContainExpectedNumberOfTimeZones()
    {
        var all = DropdownData.TimeZones.All;

        // Curated list should have approximately 40 zones
        all.Should().HaveCountGreaterThanOrEqualTo(35);
        all.Should().HaveCountLessThanOrEqualTo(50);
    }

    [Fact]
    public void All_ShouldContainAllExpectedRegions()
    {
        var regions = DropdownData.TimeZones.All
            .Select(tz => tz.Region)
            .Distinct()
            .ToList();

        regions.Should().Contain("Americas");
        regions.Should().Contain("Europe");
        regions.Should().Contain("Asia");
        regions.Should().Contain("Pacific");
        regions.Should().Contain("Africa");
        regions.Should().Contain("Atlantic");
        regions.Should().Contain("Universal");
    }

    [Fact]
    public void All_ShouldContainUTC()
    {
        var utc = DropdownData.TimeZones.ByIanaId("Etc/UTC");

        utc.Should().NotBeNull();
        utc!.DisplayName.Should().Be("Coordinated Universal Time");
        utc.Abbreviation.Should().Be("UTC");
        utc.UtcOffset.Should().Be(TimeSpan.Zero);
        utc.Region.Should().Be("Universal");
        utc.ObservesDst.Should().BeFalse();
    }

    [Theory]
    [InlineData("America/New_York", "Eastern Standard Time", "Eastern Time (US & Canada)", "EST", "EDT", -5, true)]
    [InlineData("America/Los_Angeles", "Pacific Standard Time", "Pacific Time (US & Canada)", "PST", "PDT", -8, true)]
    [InlineData("Europe/London", "GMT Standard Time", "London, Dublin, Edinburgh", "GMT", "BST", 0, true)]
    [InlineData("Asia/Tokyo", "Tokyo Standard Time", "Tokyo, Osaka, Sapporo", "JST", null, 9, false)]
    [InlineData("Australia/Sydney", "AUS Eastern Standard Time", "Sydney, Melbourne, Canberra", "AEST", "AEDT", 10, true)]
    public void All_ShouldContainExpectedTimeZones(
        string ianaId,
        string windowsId,
        string displayName,
        string abbreviation,
        string abbreviationDst,
        int offsetHours,
        bool observesDst)
    {
        var tz = DropdownData.TimeZones.ByIanaId(ianaId);

        tz.Should().NotBeNull();
        tz!.WindowsId.Should().Be(windowsId);
        tz.DisplayName.Should().Be(displayName);
        tz.Abbreviation.Should().Be(abbreviation);
        tz.AbbreviationDst.Should().Be(abbreviationDst);
        tz.UtcOffset.Should().Be(TimeSpan.FromHours(offsetHours));
        tz.ObservesDst.Should().Be(observesDst);
    }

    [Fact]
    public void All_ShouldContainIndiaWithHalfHourOffset()
    {
        var india = DropdownData.TimeZones.ByIanaId("Asia/Kolkata");

        india.Should().NotBeNull();
        india!.UtcOffset.Should().Be(new TimeSpan(5, 30, 0));
    }

    [Fact]
    public void All_ShouldContainAdelaideWithHalfHourOffset()
    {
        var adelaide = DropdownData.TimeZones.ByIanaId("Australia/Adelaide");

        adelaide.Should().NotBeNull();
        adelaide!.UtcOffset.Should().Be(new TimeSpan(9, 30, 0));
    }

    [Theory]
    [InlineData("Americas")]
    [InlineData("Europe")]
    [InlineData("Asia")]
    [InlineData("Pacific")]
    [InlineData("Africa")]
    public void ByRegion_ShouldReturnTimeZonesForValidRegions(string region)
    {
        var zones = DropdownData.TimeZones.ByRegion(region);

        zones.Should().NotBeEmpty();
        zones.Should().OnlyContain(tz => tz.Region == region);
    }

    [Fact]
    public void ByRegion_Americas_ShouldHaveReasonableCount()
    {
        var americas = DropdownData.TimeZones.ByRegion("Americas");

        americas.Should().HaveCountGreaterThanOrEqualTo(10);
    }

    #endregion

    #region ISelectOption Interface Tests

    [Fact]
    public void TimeZoneData_ShouldImplementISelectOption()
    {
        var tz = DropdownData.TimeZones.ByIanaId("America/New_York");

        tz.Should().BeAssignableTo<ISelectOption>();
    }

    [Fact]
    public void TimeZoneData_Value_ShouldReturnIanaId()
    {
        var tz = DropdownData.TimeZones.ByIanaId("America/New_York");

        tz!.Value.Should().Be("America/New_York");
    }

    [Fact]
    public void TimeZoneData_Text_ShouldReturnFormattedDisplayWithOffset()
    {
        var tz = DropdownData.TimeZones.ByIanaId("America/New_York");

        tz!.Text.Should().Be("(UTC-05:00) Eastern Time (US & Canada)");
    }

    [Fact]
    public void TimeZoneData_Text_ShouldShowPositiveOffsetWithPlusSign()
    {
        var tz = DropdownData.TimeZones.ByIanaId("Asia/Tokyo");

        tz!.Text.Should().Be("(UTC+09:00) Tokyo, Osaka, Sapporo");
    }

    [Fact]
    public void TimeZoneData_Text_ShouldShowUTCWithPlusZero()
    {
        var tz = DropdownData.TimeZones.ByIanaId("Etc/UTC");

        tz!.Text.Should().Be("(UTC+00:00) Coordinated Universal Time");
    }

    [Fact]
    public void TimeZoneData_Text_ShouldShowHalfHourOffsets()
    {
        var tz = DropdownData.TimeZones.ByIanaId("Asia/Kolkata");

        tz!.Text.Should().Be("(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi");
    }

    [Fact]
    public void AllTimeZones_ShouldBeUsableAsISelectOption()
    {
        IEnumerable<ISelectOption> options = DropdownData.TimeZones.All;

        options.Should().NotBeEmpty();
        options.All(o => !string.IsNullOrEmpty(o.Value) && !string.IsNullOrEmpty(o.Text)).Should().BeTrue();
    }

    #endregion

    #region Lookup Tests

    [Theory]
    [InlineData("America/New_York", "Eastern Time (US & Canada)")]
    [InlineData("america/new_york", "Eastern Time (US & Canada)")]
    [InlineData("AMERICA/NEW_YORK", "Eastern Time (US & Canada)")]
    [InlineData(" America/New_York ", "Eastern Time (US & Canada)")]
    public void ByIanaId_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedDisplayName)
    {
        var tz = DropdownData.TimeZones.ByIanaId(input);

        tz.Should().NotBeNull();
        tz!.DisplayName.Should().Be(expectedDisplayName);
    }

    [Theory]
    [InlineData("Eastern Standard Time", "America/New_York")]
    [InlineData("eastern standard time", "America/New_York")]
    [InlineData(" Eastern Standard Time ", "America/New_York")]
    public void ByWindowsId_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedIanaId)
    {
        var tz = DropdownData.TimeZones.ByWindowsId(input);

        tz.Should().NotBeNull();
        tz!.IanaId.Should().Be(expectedIanaId);
    }

    [Theory]
    [InlineData("EST", "America/New_York")]
    [InlineData("est", "America/New_York")]
    [InlineData(" EST ", "America/New_York")]
    [InlineData("PST", "America/Los_Angeles")]
    [InlineData("JST", "Asia/Tokyo")]
    public void ByAbbreviation_ShouldBeCaseInsensitiveAndTrimmed(string input, string expectedIanaId)
    {
        var tz = DropdownData.TimeZones.ByAbbreviation(input);

        tz.Should().NotBeNull();
        tz!.IanaId.Should().Be(expectedIanaId);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByIanaId_ShouldReturnNullForInvalidInput(string? input)
    {
        var tz = DropdownData.TimeZones.ByIanaId(input!);

        tz.Should().BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByWindowsId_ShouldReturnNullForInvalidInput(string? input)
    {
        var tz = DropdownData.TimeZones.ByWindowsId(input!);

        tz.Should().BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByAbbreviation_ShouldReturnNullForInvalidInput(string? input)
    {
        var tz = DropdownData.TimeZones.ByAbbreviation(input!);

        tz.Should().BeNull();
    }

    [Fact]
    public void ByIanaId_ShouldReturnNullForNonExistent()
    {
        var tz = DropdownData.TimeZones.ByIanaId("Invalid/TimeZone");

        tz.Should().BeNull();
    }

    [Fact]
    public void ByRegion_ShouldReturnEmptyForInvalidRegion()
    {
        var zones = DropdownData.TimeZones.ByRegion("NotARegion");

        zones.Should().BeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ByRegion_ShouldReturnEmptyForInvalidInput(string? input)
    {
        var zones = DropdownData.TimeZones.ByRegion(input!);

        zones.Should().BeEmpty();
    }

    #endregion

    #region DST Filter Tests

    [Fact]
    public void WithDst_ShouldReturnOnlyDstZones()
    {
        var dstZones = DropdownData.TimeZones.WithDst;

        dstZones.Should().NotBeEmpty();
        dstZones.Should().OnlyContain(tz => tz.ObservesDst);
        dstZones.Should().OnlyContain(tz => tz.AbbreviationDst != null);
    }

    [Fact]
    public void WithoutDst_ShouldReturnOnlyNonDstZones()
    {
        var nonDstZones = DropdownData.TimeZones.WithoutDst;

        nonDstZones.Should().NotBeEmpty();
        nonDstZones.Should().OnlyContain(tz => !tz.ObservesDst);
        nonDstZones.Should().OnlyContain(tz => tz.AbbreviationDst == null);
    }

    [Fact]
    public void DstAndNonDst_ShouldCoverAllZones()
    {
        var dstCount = DropdownData.TimeZones.WithDst.Count;
        var nonDstCount = DropdownData.TimeZones.WithoutDst.Count;
        var totalCount = DropdownData.TimeZones.All.Count;

        (dstCount + nonDstCount).Should().Be(totalCount);
    }

    #endregion

    #region Builder Tests - Basic Filtering

    [Fact]
    public void Builder_Default_ShouldReturnAllTimeZones()
    {
        var zones = DropdownData.TimeZones.Builder().Build();

        zones.Should().HaveCount(DropdownData.TimeZones.All.Count);
    }

    [Fact]
    public void Builder_InRegion_ShouldFilterByRegion()
    {
        var zones = DropdownData.TimeZones.Builder()
            .InRegion("Americas")
            .Build();

        zones.Should().NotBeEmpty();
        zones.Should().OnlyContain(tz => tz.Region == "Americas");
    }

    [Fact]
    public void Builder_InRegion_MultipleRegions_ShouldIncludeAll()
    {
        var zones = DropdownData.TimeZones.Builder()
            .InRegion("Americas", "Europe")
            .Build();

        zones.Should().OnlyContain(tz => tz.Region == "Americas" || tz.Region == "Europe");
    }

    [Fact]
    public void Builder_InRegion_ShouldBeCaseInsensitive()
    {
        var zones = DropdownData.TimeZones.Builder()
            .InRegion("americas")
            .Build();

        zones.Should().OnlyContain(tz => tz.Region == "Americas");
    }

    [Fact]
    public void Builder_ExcludeRegion_ShouldRemoveRegion()
    {
        var allCount = DropdownData.TimeZones.All.Count;
        var americasCount = DropdownData.TimeZones.ByRegion("Americas").Count;

        var zones = DropdownData.TimeZones.Builder()
            .ExcludeRegion("Americas")
            .Build();

        zones.Should().HaveCount(allCount - americasCount);
        zones.Should().NotContain(tz => tz.Region == "Americas");
    }

    [Fact]
    public void Builder_ExcludeRegion_MultipleRegions_ShouldRemoveAll()
    {
        var zones = DropdownData.TimeZones.Builder()
            .ExcludeRegion("Americas", "Europe")
            .Build();

        zones.Should().NotContain(tz => tz.Region == "Americas");
        zones.Should().NotContain(tz => tz.Region == "Europe");
    }

    [Fact]
    public void Builder_WithDstOnly_ShouldReturnOnlyDstZones()
    {
        var zones = DropdownData.TimeZones.Builder()
            .WithDstOnly()
            .Build();

        zones.Should().OnlyContain(tz => tz.ObservesDst);
    }

    [Fact]
    public void Builder_WithoutDst_ShouldReturnOnlyNonDstZones()
    {
        var zones = DropdownData.TimeZones.Builder()
            .WithoutDst()
            .Build();

        zones.Should().OnlyContain(tz => !tz.ObservesDst);
    }

    #endregion

    #region Builder Tests - Exclusions (from BaseBuilder)

    [Fact]
    public void Builder_Exclude_ShouldRemoveSpecifiedTimeZones()
    {
        var zones = DropdownData.TimeZones.Builder()
            .Exclude("America/New_York", "America/Los_Angeles")
            .Build();

        zones.Should().NotContain(tz => tz.IanaId == "America/New_York");
        zones.Should().NotContain(tz => tz.IanaId == "America/Los_Angeles");
    }

    [Fact]
    public void Builder_Exclude_ShouldBeCaseInsensitive()
    {
        var zones = DropdownData.TimeZones.Builder()
            .Exclude("america/new_york")
            .Build();

        zones.Should().NotContain(tz => tz.IanaId == "America/New_York");
    }

    [Fact]
    public void Builder_Exclude_ShouldHandleNullAndEmpty()
    {
        var allCount = DropdownData.TimeZones.All.Count;

        var zones = DropdownData.TimeZones.Builder()
            .Exclude(null!)
            .Exclude("", "  ")
            .Build();

        zones.Should().HaveCount(allCount);
    }

    #endregion

    #region Builder Tests - Only (Include Specific)

    [Fact]
    public void Builder_Only_ShouldReturnOnlySpecifiedTimeZones()
    {
        var zones = DropdownData.TimeZones.Builder()
            .Only("America/New_York", "America/Los_Angeles", "Europe/London")
            .Build();

        zones.Should().HaveCount(3);
        zones.Select(tz => tz.IanaId).Should().BeEquivalentTo(
            new[] { "America/New_York", "America/Los_Angeles", "Europe/London" });
    }

    [Fact]
    public void Builder_Only_ShouldBeCaseInsensitive()
    {
        var zones = DropdownData.TimeZones.Builder()
            .Only("america/new_york", "europe/london")
            .Build();

        zones.Should().HaveCount(2);
    }

    [Fact]
    public void Builder_Only_WithExclude_ShouldApplyBoth()
    {
        var zones = DropdownData.TimeZones.Builder()
            .Only("America/New_York", "America/Los_Angeles", "Europe/London", "Asia/Tokyo")
            .Exclude("Asia/Tokyo")
            .Build();

        zones.Should().HaveCount(3);
        zones.Should().NotContain(tz => tz.IanaId == "Asia/Tokyo");
    }

    #endregion

    #region Builder Tests - Ordering

    [Fact]
    public void Builder_OrderByOffset_ShouldSortWestToEast()
    {
        var zones = DropdownData.TimeZones.Builder()
            .OrderByOffset()
            .Build();

        zones.Should().BeInAscendingOrder(tz => tz.UtcOffset);
        // Hawaii should be near the start (UTC-10)
        zones.First().UtcOffset.Should().BeLessThan(TimeSpan.Zero);
    }

    [Fact]
    public void Builder_OrderByOffsetDescending_ShouldSortEastToWest()
    {
        var zones = DropdownData.TimeZones.Builder()
            .OrderByOffsetDescending()
            .Build();

        zones.Should().BeInDescendingOrder(tz => tz.UtcOffset);
        // Pacific zones should be near the start (UTC+12 or so)
        zones.First().UtcOffset.Should().BeGreaterThan(TimeSpan.Zero);
    }

    [Fact]
    public void Builder_OrderByName_ShouldSortAlphabetically()
    {
        var zones = DropdownData.TimeZones.Builder()
            .OrderByName()
            .Build();

        zones.Should().BeInAscendingOrder(tz => tz.Text, StringComparer.InvariantCulture);
    }

    [Fact]
    public void Builder_OrderByNameDescending_ShouldSortReverseAlphabetically()
    {
        var zones = DropdownData.TimeZones.Builder()
            .OrderByNameDescending()
            .Build();

        zones.Should().BeInDescendingOrder(tz => tz.Text, StringComparer.InvariantCulture);
    }

    [Fact]
    public void Builder_OrderByIanaId_ShouldSortByIanaId()
    {
        var zones = DropdownData.TimeZones.Builder()
            .OrderByIanaId()
            .Build();

        zones.Should().BeInAscendingOrder(tz => tz.IanaId, StringComparer.InvariantCulture);
    }

    [Fact]
    public void Builder_OrderByOffset_WithInRegion_ShouldSortFilteredEntries()
    {
        var zones = DropdownData.TimeZones.Builder()
            .InRegion("Americas")
            .OrderByOffset()
            .Build();

        zones.Should().OnlyContain(tz => tz.Region == "Americas");
        zones.Should().BeInAscendingOrder(tz => tz.UtcOffset);
    }

    #endregion

    #region Builder Tests - Complex Combinations

    [Fact]
    public void Builder_ComplexCombination_ShouldWorkCorrectly()
    {
        var zones = DropdownData.TimeZones.Builder()
            .InRegion("Americas", "Europe")
            .WithDstOnly()
            .Exclude("America/Toronto")
            .OrderByOffset()
            .Build();

        zones.Should().OnlyContain(tz => tz.Region == "Americas" || tz.Region == "Europe");
        zones.Should().OnlyContain(tz => tz.ObservesDst);
        zones.Should().NotContain(tz => tz.IanaId == "America/Toronto");
        zones.Should().BeInAscendingOrder(tz => tz.UtcOffset);
    }

    [Fact]
    public void Builder_ChainedCalls_ShouldAccumulate()
    {
        var zones = DropdownData.TimeZones.Builder()
            .InRegion("Americas")
            .Exclude("America/New_York")
            .Exclude("America/Los_Angeles")
            .Exclude("America/Chicago")
            .Build();

        zones.Should().NotContain(tz => tz.IanaId == "America/New_York");
        zones.Should().NotContain(tz => tz.IanaId == "America/Los_Angeles");
        zones.Should().NotContain(tz => tz.IanaId == "America/Chicago");
    }

    #endregion

    #region Thread Safety Tests

    [Fact]
    public void TimeZones_ShouldBeSafeForConcurrentAccess()
    {
        var results = new List<int>();
        var expectedCount = DropdownData.TimeZones.All.Count;

        Parallel.For(0, 100, _ =>
        {
            var count = DropdownData.TimeZones.All.Count;
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
        var first = DropdownData.TimeZones.All;
        var second = DropdownData.TimeZones.All;

        first.Should().BeSameAs(second);
    }

    [Fact]
    public void All_ShouldBeReadOnly()
    {
        var zones = DropdownData.TimeZones.All;

        zones.Should().BeAssignableTo<IReadOnlyList<TimeZoneData>>();
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void TimeZoneData_ToString_ShouldReturnFormattedString()
    {
        var tz = DropdownData.TimeZones.ByIanaId("America/New_York");

        tz!.ToString().Should().Be("Eastern Time (US & Canada) (America/New_York)");
    }

    #endregion
}