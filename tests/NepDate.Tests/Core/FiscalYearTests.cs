namespace NepDate.Tests.Core;

public class FiscalYearTests
{
    [Fact]
    public void GetFiscalYearStartAndEndDate_ValidYear_ReturnsCorrectDates()
    {
        // Arrange
        int fiscalYear = 2080;

        // Act
        var (startDate, endDate) = NepaliDate.GetFiscalYearStartAndEndDate(fiscalYear);

        // Assert
        Assert.Equal(new NepaliDate(2080, 4, 1), startDate);
        Assert.Equal(new NepaliDate(2081, 3, new NepaliDate(2081, 3, 1).MonthEndDay), endDate);
    }

    [Theory]
    [InlineData(1843)] // Below minimum
    [InlineData(2200)] // Above maximum
    public void GetFiscalYearStartAndEndDate_InvalidYear_ThrowsException(int year)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => NepaliDate.GetFiscalYearStartAndEndDate(year));
    }

    [Fact]
    public void GetFiscalYearStartDate_ValidYear_ReturnsCorrectDate()
    {
        // Arrange
        int fiscalYear = 2080;

        // Act
        var startDate = NepaliDate.GetFiscalYearStartDate(fiscalYear);

        // Assert
        Assert.Equal(new NepaliDate(2080, 4, 1), startDate);
    }

    [Fact]
    public void GetFiscalYearEndDate_ValidYear_ReturnsCorrectDate()
    {
        // Arrange
        int fiscalYear = 2080;

        // Act
        var endDate = NepaliDate.GetFiscalYearEndDate(fiscalYear);

        // Assert
        Assert.Equal(new NepaliDate(2081, 3, new NepaliDate(2081, 3, 1).MonthEndDay), endDate);
    }

    [Fact]
    public void FiscalYearStartAndEndDate_OnNepaliDateInstance_ReturnsCorrectDates()
    {
        // Arrange
        var date = new NepaliDate(2080, 6, 15);

        // Act
        var (startDate, endDate) = date.FiscalYearStartAndEndDate();

        // Assert
        Assert.Equal(new NepaliDate(2080, 4, 1), startDate);
        Assert.Equal(new NepaliDate(2081, 3, new NepaliDate(2081, 3, 1).MonthEndDay), endDate);
    }

    [Fact]
    public void FiscalYearStartDate_OnNepaliDateInstance_ReturnsCorrectDate()
    {
        // Arrange
        var date = new NepaliDate(2080, 6, 15);

        // Act
        var startDate = date.FiscalYearStartDate();

        // Assert
        Assert.Equal(new NepaliDate(2080, 4, 1), startDate);
    }

    [Fact]
    public void FiscalYearEndDate_OnNepaliDateInstance_ReturnsCorrectDate()
    {
        // Arrange
        var date = new NepaliDate(2080, 6, 15);

        // Act
        var endDate = date.FiscalYearEndDate();

        // Assert
        Assert.Equal(new NepaliDate(2081, 3, new NepaliDate(2081, 3, 1).MonthEndDay), endDate);
    }

    [Fact]
    public void GetFiscalYearQuarterStartAndEndDate_ValidYearAndMonth_ReturnsCorrectDates()
    {
        // Arrange
        int fiscalYear = 2080;
        int month = 5; // Bhadra, first quarter

        // Act
        var (startDate, endDate) = NepaliDate.GetFiscalYearQuarterStartAndEndDate(fiscalYear, month);

        // Assert
        Assert.Equal(new NepaliDate(2080, 4, 1), startDate); // Quarter starts with Shrawan (month 4)
        Assert.Equal(new NepaliDate(2080, 6, new NepaliDate(2080, 6, 1).MonthEndDay), endDate); // Ends with Ashwin (month 6)
    }

    [Fact]
    public void GetFiscalYearQuarterStartDate_ValidYearAndMonth_ReturnsCorrectDate()
    {
        // Arrange
        int fiscalYear = 2080;
        int month = 8; // Mangsir, second quarter

        // Act
        var startDate = NepaliDate.GetFiscalYearQuarterStartDate(fiscalYear, month);

        // Assert
        Assert.Equal(new NepaliDate(2080, 7, 1), startDate); // Quarter starts with Kartik (month 7)
    }

    [Fact]
    public void GetFiscalYearQuarterEndDate_ValidYearAndMonth_ReturnsCorrectDate()
    {
        // Arrange
        int fiscalYear = 2080;
        int month = 11; // Falgun, third quarter

        // Act
        var endDate = NepaliDate.GetFiscalYearQuarterEndDate(fiscalYear, month);

        // Assert
        Assert.Equal(new NepaliDate(2080, 12, new NepaliDate(2080, 12, 1).MonthEndDay), endDate); // Ends with Chaitra (month 12)
    }
}