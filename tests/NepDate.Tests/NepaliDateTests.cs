using NepDate.Core.Enums;
using NepDate.Exceptions;

namespace NepDate.Tests;

public class NepaliDateTests
{
    [Fact]
    public void Constructor_InvalidNepaliDateFormat_ThrowsException()
    {
        // Arrange
        int year = 2079;
        int month = 14;
        int day = 32;

        // Act and Assert
        _ = Assert.Throws<NepDateException.InvalidNepaliDateFormatException>(() => new NepaliDate(year, month, day));
    }

    [Theory]
    [InlineData("2079/04/16", DayOfWeek.Monday)]
    [InlineData("2078/12/25", DayOfWeek.Friday)]
    [InlineData("2081/01/01", DayOfWeek.Saturday)]
    public void DayOfWeek_ValidNepaliDate_ReturnsCorrectDayOfWeek(string nepaliDateStr, DayOfWeek expected)
    {
        // Arrange
        NepaliDate nepaliDate = NepaliDate.Parse(nepaliDateStr);

        // Act
        DayOfWeek actual = nepaliDate.DayOfWeek;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(2079, false)]
    [InlineData(2080, true)]
    [InlineData(2081, false)]
    [InlineData(2082, false)]
    public void IsLeapYear_ValidNepaliYear_ReturnsCorrectValue(int nepaliYear, bool expected)
    {
        bool actual = new NepaliDate(nepaliYear, 1, 1).IsLeapYear();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("2079/04/15", 5, "2079/04/20")]
    [InlineData("2078/12/25", 10, "2079/01/05")]
    [InlineData("2081/01/01", -5, "2080/12/26")]
    public void AddDays_ValidNepaliDate_ReturnsCorrectValue(string nepaliDateStr, int daysToAdd, string expectedNepaliDateStr)
    {
        // Arrange
        NepaliDate nepaliDate = NepaliDate.Parse(nepaliDateStr);
        NepaliDate expected = NepaliDate.Parse(expectedNepaliDateStr);

        // Act
        NepaliDate actual = nepaliDate.AddDays(daysToAdd);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_Validate_ReturnsCorrectValue()
    {
        NepaliDate nepaliDate = new NepaliDate(2079, 4, 15);

        // Test cases for DateFormats.YearMonthDay
        Assert.Equal("2079/04/15", nepaliDate.ToString(DateFormats.YearMonthDay, Separators.ForwardSlash, true));
        Assert.Equal("2079\\4\\15", nepaliDate.ToString(DateFormats.YearMonthDay, Separators.BackwardSlash, false));
        Assert.Equal("2079-04-15", nepaliDate.ToString(DateFormats.YearMonthDay, Separators.Dash, true));
        Assert.Equal("2079.4.15", nepaliDate.ToString(DateFormats.YearMonthDay, Separators.Dot, false));
        Assert.Equal("2079_04_15", nepaliDate.ToString(DateFormats.YearMonthDay, Separators.Underscore, true));
        Assert.Equal("2079 4 15", nepaliDate.ToString(DateFormats.YearMonthDay, Separators.Space, false));

        // Test cases for DateFormats.YearDayMonth
        Assert.Equal("2079/15/04", nepaliDate.ToString(DateFormats.YearDayMonth, Separators.ForwardSlash, true));
        
        // Add more test cases for DateFormats.YearDayMonth with different separators

        // Test cases for DateFormats.MonthDayYear
        Assert.Equal("04/15/2079", nepaliDate.ToString(DateFormats.MonthDayYear, Separators.ForwardSlash, true));
        // Add more test cases for DateFormats.MonthDayYear with different separators

        // Test cases for DateFormats.MonthYearDay
        // Add test cases for DateFormats.MonthYearDay with different separators

        // Test cases for DateFormats.DayMonthYear
        // Add test cases for DateFormats.DayMonthYear with different separators

        // Test cases for DateFormats.DayYearMonth
        // Add test cases for DateFormats.DayYearMonth with different separators

    }
}
