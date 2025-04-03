namespace NepDate.Tests.Core;

public class NepaliDatePropertiesTests
{
    [Fact]
    public void EnglishDate_Conversion_ReturnsCorrectDate()
    {
        // Arrange
        var nepaliDate = new NepaliDate(2080, 5, 15);

        // Act
        var englishDate = nepaliDate.EnglishDate;

        // Assert
        Assert.Equal(2023, englishDate.Year);
        Assert.Equal(9, englishDate.Month);
        Assert.Equal(1, englishDate.Day);
    }

    [Fact]
    public void DayOfWeek_ReturnsCorrectValue()
    {
        // Arrange
        var nepaliDate = new NepaliDate(2080, 5, 15);

        // Act
        var dayOfWeek = nepaliDate.DayOfWeek;

        // Assert
        Assert.Equal(DayOfWeek.Friday, dayOfWeek);
    }

    [Fact]
    public void MonthEndDay_ReturnsCorrectValue()
    {
        // Arrange
        var nepaliDate = new NepaliDate(2080, 5, 15);

        // Act
        var monthEndDay = nepaliDate.MonthEndDay;

        // Assert
        Assert.Equal(31, monthEndDay);
    }

    [Fact]
    public void MonthName_ReturnsCorrectEnum()
    {
        // Arrange
        var nepaliDate = new NepaliDate(2080, 5, 15);

        // Act
        var monthName = nepaliDate.MonthName;

        // Assert
        Assert.Equal(NepaliMonths.Bhadra, monthName);
    }

    [Fact]
    public void Now_ReturnsCurrentNepaliDate()
    {
        // Arrange
        var today = DateTime.Today;
        var expectedNepaliDate = new NepaliDate(today);

        // Act
        var nowNepaliDate = NepaliDate.Now;

        // Assert
        Assert.Equal(expectedNepaliDate, nowNepaliDate);
    }
} 