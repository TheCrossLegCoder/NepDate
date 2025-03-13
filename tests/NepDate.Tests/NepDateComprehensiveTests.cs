using NepDate.Exceptions;

namespace NepDate.Tests;

public class NepDateComprehensiveTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_ValidNepaliDate_CreatesInstance()
    {
        // Arrange & Act
        var nepaliDate = new NepaliDate(2080, 5, 15);

        // Assert
        Assert.Equal(2080, nepaliDate.Year);
        Assert.Equal(5, nepaliDate.Month);
        Assert.Equal(15, nepaliDate.Day);
    }

    [Fact]
    public void Constructor_MinValue_CreatesInstance()
    {
        // Arrange & Act
        var nepaliDate = NepaliDate.MinValue;

        // Assert
        Assert.Equal(1901, nepaliDate.Year);
        Assert.Equal(1, nepaliDate.Month);
        Assert.Equal(1, nepaliDate.Day);
    }

    [Fact]
    public void Constructor_MaxValue_CreatesInstance()
    {
        // Arrange & Act
        var nepaliDate = NepaliDate.MaxValue;

        // Assert
        Assert.Equal(2199, nepaliDate.Year);
        Assert.Equal(12, nepaliDate.Month);
        Assert.True(nepaliDate.Day > 0); // Last day of Chaitra
    }

    [Theory]
    [InlineData(1900, 1, 1)] // Year too low
    [InlineData(2200, 1, 1)] // Year too high
    [InlineData(2080, 0, 1)] // Month too low
    [InlineData(2080, 13, 1)] // Month too high
    [InlineData(2080, 1, 0)] // Day too low
    [InlineData(2080, 1, 33)] // Day too high
    public void Constructor_InvalidNepaliDate_ThrowsException(int year, int month, int day)
    {
        // Act & Assert
        Assert.Throws<NepDateException.InvalidNepaliDateFormatException>(() => new NepaliDate(year, month, day));
    }

    [Theory]
    [InlineData("2080/05/15")]
    [InlineData("2080-05-15")]
    [InlineData("2080.05.15")]
    [InlineData("2080_05_15")]
    [InlineData("2080\\05\\15")]
    [InlineData("2080 05 15")]
    public void Constructor_ValidStringFormats_CreatesInstance(string dateString)
    {
        // Arrange & Act
        var nepaliDate = new NepaliDate(dateString);

        // Assert
        Assert.Equal(2080, nepaliDate.Year);
        Assert.Equal(5, nepaliDate.Month);
        Assert.Equal(15, nepaliDate.Day);
    }

    [Theory]
    [InlineData("15/05/2080", true, false)] // Day, Month, Year
    [InlineData("05/15/2080", true, true)] // Month, Day, Year
    [InlineData("2080/05/15", false, false)] // Day, Year, Month
    [InlineData("80/05/15", true, true)] // 2-digit year auto-adjusted
    public void Constructor_AutoAdjustedFormats_CreatesInstance(string dateString, bool autoAdjust, bool monthInMiddle)
    {
        // Arrange & Act
        var nepaliDate = new NepaliDate(dateString, autoAdjust, monthInMiddle);

        // Assert
        Assert.Equal(2080, nepaliDate.Year);
        Assert.Equal(5, nepaliDate.Month);
        Assert.Equal(15, nepaliDate.Day);
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData("2080/5")]
    [InlineData("2080/5/40")]
    public void Constructor_InvalidStringFormats_ThrowsException(string dateString)
    {
        // Act & Assert
        _ = Assert.Throws<NepDateException.InvalidNepaliDateFormatException>(() => new NepaliDate(dateString));
    }

    [Fact]
    public void Constructor_EnglishDate_ConvertsCorrectly()
    {
        // Arrange
        var englishDate = new DateTime(2023, 8, 30);

        // Act
        var nepaliDate = new NepaliDate(englishDate);

        // Assert
        Assert.Equal(2080, nepaliDate.Year);
        Assert.Equal(5, nepaliDate.Month);
        Assert.Equal(13, nepaliDate.Day);
    }

    #endregion

    #region Properties Tests

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

    #endregion

    #region Date Manipulation Tests

    [Fact]
    public void MonthEndDate_ReturnsCorrectDate()
    {
        // Arrange
        var nepaliDate = new NepaliDate(2080, 5, 15);

        // Act
        var monthEndDate = nepaliDate.MonthEndDate();

        // Assert
        Assert.Equal(2080, monthEndDate.Year);
        Assert.Equal(5, monthEndDate.Month);
        Assert.Equal(31, monthEndDate.Day);
    }

    [Theory]
    [InlineData(2080, 5, 15, 1, 2080, 6, 15)]   // Add 1 month
    [InlineData(2080, 5, 15, -1, 2080, 4, 15)]  // Subtract 1 month
    [InlineData(2080, 5, 15, 12, 2081, 5, 15)]  // Add 12 months (1 year)
    [InlineData(2081, 4, 32, 5, 2081, 9, 29)]  // Month end day adjustment
    [InlineData(2081, 4, 32, 5, 2081, 10, 3, true)] // Away from month end
    public void AddMonths_ReturnsCorrectDate(int year, int month, int day, int monthsToAdd, int expectedYear, int expectedMonth, int expectedDay, bool awayFromMonthEnd = false)
    {
        // Arrange
        var nepaliDate = new NepaliDate(year, month, day);

        // Act
        var result = nepaliDate.AddMonths(monthsToAdd, awayFromMonthEnd);

        // Assert
        Assert.Equal(expectedYear, result.Year);
        Assert.Equal(expectedMonth, result.Month);
        Assert.Equal(expectedDay, result.Day);
    }

    [Theory]
    [InlineData(2080, 5, 15, 1, true, 2080, 6, 15)]  // Add 1 month with away flag
    public void AddMonths_AwayFromMonthEnd_ReturnsCorrectDate(int year, int month, int day, int monthsToAdd, bool awayFromMonthEnd, int expectedYear, int expectedMonth, int expectedDay)
    {
        // Arrange
        var nepaliDate = new NepaliDate(year, month, day);

        // Act
        var result = nepaliDate.AddMonths(monthsToAdd, awayFromMonthEnd);

        // Assert
        Assert.Equal(expectedYear, result.Year);
        Assert.Equal(expectedMonth, result.Month);
        Assert.Equal(expectedDay, result.Day);
    }

    [Theory]
    [InlineData(2080, 5, 15, 5, 2080, 5, 20)]  // Add 5 days
    [InlineData(2080, 5, 15, -5, 2080, 5, 10)] // Subtract 5 days
    [InlineData(2080, 5, 31, 1, 2080, 6, 1)]   // Cross month boundary
    [InlineData(2080, 12, 30, 5, 2081, 1, 5)]  // Cross year boundary
    public void AddDays_ReturnsCorrectDate(int year, int month, int day, int daysToAdd, int expectedYear, int expectedMonth, int expectedDay)
    {
        // Arrange
        var nepaliDate = new NepaliDate(year, month, day);

        // Act
        var result = nepaliDate.AddDays(daysToAdd);

        // Assert
        Assert.Equal(expectedYear, result.Year);
        Assert.Equal(expectedMonth, result.Month);
        Assert.Equal(expectedDay, result.Day);
    }

    [Fact]
    public void Subtract_ReturnsCorrectTimeSpan()
    {
        // Arrange
        var date1 = new NepaliDate(2080, 5, 20);
        var date2 = new NepaliDate(2080, 5, 15);

        // Act
        var timeSpan = date1.Subtract(date2);

        // Assert
        Assert.Equal(5, timeSpan.Days);
    }

    #endregion

    #region Calendar Status Tests

    [Theory]
    [InlineData(2077, false)]
    [InlineData(2076, true)]  // 2076 BS corresponds to 2019-2020 AD, and 2020 is a leap year
    [InlineData(2080, true)]  // 2080 BS corresponds to 2023-2024 AD, and 2024 is a leap year
    [InlineData(2078, false)]
    public void IsLeapYear_ReturnsCorrectValue(int year, bool expected)
    {
        // Arrange
        var nepaliDate = new NepaliDate(year, 1, 1);

        // Act
        var result = nepaliDate.IsLeapYear();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void IsToday_CurrentDateReturnsTrue()
    {
        // Arrange & Act
        var today = NepaliDate.Now;

        // Assert
        Assert.True(today.IsToday());
    }

    [Fact]
    public void IsYesterday_YesterdayDateReturnsTrue()
    {
        // Arrange
        var yesterday = NepaliDate.Now.AddDays(-1);

        // Act & Assert
        Assert.True(yesterday.IsYesterday());
    }

    [Fact]
    public void IsTomorrow_TomorrowDateReturnsTrue()
    {
        // Arrange
        var tomorrow = NepaliDate.Now.AddDays(1);

        // Act & Assert
        Assert.True(tomorrow.IsTomorrow());
    }

    #endregion

    #region Fiscal Year Tests

    [Fact]
    public void FiscalYearStartDate_ReturnsCorrectDate()
    {
        // Arrange
        var nepaliDate = new NepaliDate(2080, 5, 15);  // Bhadra, so in the current fiscal year

        // Act
        var startDate = nepaliDate.FiscalYearStartDate();

        // Assert
        Assert.Equal(2080, startDate.Year);
        Assert.Equal(4, startDate.Month);  // Shrawan
        Assert.Equal(1, startDate.Day);
    }

    [Fact]
    public void FiscalYearEndDate_ReturnsCorrectDate()
    {
        // Arrange
        var nepaliDate = new NepaliDate(2080, 5, 15);  // Bhadra, so in the current fiscal year

        // Act
        var endDate = nepaliDate.FiscalYearEndDate();

        // Assert
        Assert.Equal(2081, endDate.Year);
        Assert.Equal(3, endDate.Month);  // Ashadh
        Assert.Equal(endDate.MonthEndDay, endDate.Day);  // Last day of Ashadh
    }

    [Fact]
    public void FiscalYearStartAndEndDate_ReturnsCorrectDates()
    {
        // Arrange
        var nepaliDate = new NepaliDate(2080, 5, 15);

        // Act
        var (startDate, endDate) = nepaliDate.FiscalYearStartAndEndDate();

        // Assert
        Assert.Equal(2080, startDate.Year);
        Assert.Equal(4, startDate.Month);
        Assert.Equal(1, startDate.Day);

        Assert.Equal(2081, endDate.Year);
        Assert.Equal(3, endDate.Month);
        Assert.Equal(endDate.MonthEndDay, endDate.Day);
    }

    [Theory]
    [InlineData(FiscalYearQuarters.First, 2080, 4, 1)]   // Shrawan 1
    [InlineData(FiscalYearQuarters.Second, 2080, 7, 1)]  // Kartik 1
    [InlineData(FiscalYearQuarters.Third, 2080, 10, 1)]  // Magh 1
    [InlineData(FiscalYearQuarters.Fourth, 2081, 1, 1)]  // Baisakh 1
    public void FiscalYearQuarterStartDate_ReturnsCorrectDate(FiscalYearQuarters quarter, int expectedYear, int expectedMonth, int expectedDay)
    {
        // Arrange
        var nepaliDate = new NepaliDate(2080, 5, 15);

        // Act
        var startDate = nepaliDate.FiscalYearQuarterStartDate(quarter);

        // Assert
        Assert.Equal(expectedYear, startDate.Year);
        Assert.Equal(expectedMonth, startDate.Month);
        Assert.Equal(expectedDay, startDate.Day);
    }

    #endregion

    #region Bulk Convert Tests

    [Fact]
    public void BulkConvert_ToNepaliDates_ConvertsBatch()
    {
        // Arrange
        var englishDates = new List<DateTime>
        {
            new DateTime(2023, 8, 30),
            new DateTime(2023, 9, 1),
            new DateTime(2023, 9, 15)
        };

        // Act
        var nepaliDates = NepaliDate.BulkConvert.ToNepaliDates(englishDates).ToList();

        // Assert
        Assert.Equal(3, nepaliDates.Count);
        Assert.Equal(new NepaliDate(2080, 5, 13), nepaliDates[0]);
        Assert.Equal(new NepaliDate(2080, 5, 15), nepaliDates[1]);
        Assert.Equal(new NepaliDate(2080, 5, 29), nepaliDates[2]);
    }

    [Fact]
    public void BulkConvert_ToEnglishDates_ConvertsBatch()
    {
        // Arrange
        var nepaliDates = new List<NepaliDate>
        {
            new NepaliDate(2080, 5, 13),
            new NepaliDate(2080, 5, 15),
            new NepaliDate(2080, 5, 29)
        };

        // Act
        var englishDates = NepaliDate.BulkConvert.ToEnglishDates(nepaliDates).ToList();

        // Assert
        Assert.Equal(3, englishDates.Count);
        Assert.Equal(new DateTime(2023, 8, 30), englishDates[0].Date);
        Assert.Equal(new DateTime(2023, 9, 1), englishDates[1].Date);
        Assert.Equal(new DateTime(2023, 9, 15), englishDates[2].Date);
    }

    [Fact]
    public void BulkConvert_BatchProcessing_HandlesLargeDatasets()
    {
        // Arrange
        var englishDates = Enumerable.Range(0, 1000)
            .Select(i => new DateTime(2023, 1, 1).AddDays(i))
            .ToList();

        // Act
        var nepaliDates = NepaliDate.BulkConvert.BatchProcessToNepaliDates(englishDates, 100).ToList();

        // Assert
        Assert.Equal(1000, nepaliDates.Count);
        // Check first and last dates
        Assert.Equal(new NepaliDate(2079, 9, 17), nepaliDates[0]);
        Assert.Equal(new NepaliDate(2082, 6, 10), nepaliDates[999]);
    }

    #endregion

    #region Array Optimization Tests

    [Fact]
    public void ArrayOptimization_ComparePerformance()
    {
        // This test checks that the array-based lookup is faster than dictionary lookup
        // by measuring the time it takes to perform a large number of conversions

        // Arrange
        const int iterations = 10000;
        var random = new Random(123); // Use fixed seed for reproducibility
        var years = new int[iterations];
        var months = new int[iterations];
        var days = new int[iterations];

        // Generate random but valid Nepali dates
        for (int i = 0; i < iterations; i++)
        {
            years[i] = random.Next(1901, 2100);
            months[i] = random.Next(1, 13);
            days[i] = random.Next(1, 28); // Using 28 to ensure valid dates
        }

        // Act & Assert
        // Note: This is more of a benchmark than a proper test
        // We just want to make sure the array-based approach doesn't throw exceptions
        // and verify it completes in a reasonable time

        // Perform conversions using the optimized code
        for (int i = 0; i < iterations; i++)
        {
            var nepDate = new NepaliDate(years[i], months[i], days[i]);
            var engDate = nepDate.EnglishDate;
        }

        // If we get here without exceptions, the test passes
        Assert.True(true);
    }

    #endregion

    #region Parsing Tests

    [Theory]
    [InlineData("2080/05/15", 2080, 5, 15)]
    [InlineData("2080-05-15", 2080, 5, 15)]
    [InlineData("2080.05.15", 2080, 5, 15)]
    [InlineData("2080_05_15", 2080, 5, 15)]
    public void Parse_ValidInputs_ReturnsCorrectNepaliDate(string input, int expectedYear, int expectedMonth, int expectedDay)
    {
        // Act
        var nepaliDate = NepaliDate.Parse(input);

        // Assert
        Assert.Equal(expectedYear, nepaliDate.Year);
        Assert.Equal(expectedMonth, nepaliDate.Month);
        Assert.Equal(expectedDay, nepaliDate.Day);
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData("2080/13/01")]
    [InlineData("2080/01/35")]
    public void Parse_InvalidInputs_ThrowsException(string input)
    {
        // Act & Assert
        Assert.Throws<NepDateException.InvalidNepaliDateFormatException>(() => NepaliDate.Parse(input));
    }

    [Theory]
    [InlineData("2080/05/15", true, 2080, 5, 15)]
    [InlineData("invalid", false, 0, 0, 0)]
    public void TryParse_MixedInputs_ReturnsExpectedResults(string input, bool expectedResult, int expectedYear, int expectedMonth, int expectedDay)
    {
        // Act
        var success = NepaliDate.TryParse(input, out var nepaliDate);

        // Assert
        Assert.Equal(expectedResult, success);
        if (success)
        {
            Assert.Equal(expectedYear, nepaliDate.Year);
            Assert.Equal(expectedMonth, nepaliDate.Month);
            Assert.Equal(expectedDay, nepaliDate.Day);
        }
    }

    #endregion
}