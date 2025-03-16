using NepDate.Extensions;

namespace NepDate.Tests.Extensions;

public class DateTimeExtensionsTests
{
    [Fact]
    public void ToNepaliDate_ValidDateTime_ReturnsCorrectNepaliDate()
    {
        // Arrange
        var englishDate = new DateTime(2023, 8, 30);

        // Act
        var nepaliDate = englishDate.ToNepaliDate();

        // Assert
        Assert.Equal(2080, nepaliDate.Year);
        Assert.Equal(5, nepaliDate.Month);
        Assert.Equal(13, nepaliDate.Day);
    }

    [Fact]
    public void ToNepaliDate_MinValue_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var minDate = DateTime.MinValue;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => minDate.ToNepaliDate());
    }

    [Fact]
    public void ToNepaliDate_MaxValue_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var maxDate = DateTime.MaxValue;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => maxDate.ToNepaliDate());
    }

    [Fact]
    public void ToNepaliDate_DateBeforeMinSupported_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var tooEarlyDate = new DateTime(1843, 1, 1); // Before NepaliDate.MinValue

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => tooEarlyDate.ToNepaliDate());
    }

    [Fact]
    public void ToNepaliDate_DateAfterMaxSupported_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var tooLateDate = new DateTime(2200, 1, 1); // After NepaliDate.MaxValue

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => tooLateDate.ToNepaliDate());
    }
}