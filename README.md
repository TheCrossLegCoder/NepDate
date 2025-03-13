![NepDate Logo](https://user-images.githubusercontent.com/37014558/231635618-bf6599e3-554e-4b02-93df-019e7b8aecc3.png)

<div align="center">

# NepDate - Fast & Efficient Nepali Date Library for .NET

### Modern, High-Performance Bikram Sambat Date Operations for .NET Applications

[![GitHub Stars](https://img.shields.io/github/stars/TheCrossLegCoder/NepDate)](https://github.com/TheCrossLegCoder/NepDate)
[![NuGet Version](https://img.shields.io/nuget/v/NepDate.svg)](https://www.nuget.org/packages/NepDate/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/NepDate)](https://www.nuget.org/packages/NepDate/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

</div>

---

NepDate is a **super-fast** and **memory-efficient** `struct` built on `.NET Standard 2.0` that offers comprehensive Nepali date functionality in a lightweight package. Designed to closely resemble the `DateOnly` struct in `.NET`, NepDate provides a familiar, intuitive API for working with Nepali dates (Bikram Sambat).

## 📋 Table of Contents


| Section | Description |
|---------|-------------|
| 📦 [Installation](#-installation) | How to add NepDate to your project |
| ✨ [Key Features](#-key-features) | What makes NepDate special |
| 🚀 [Getting Started](#-getting-started) | Begin using NepDate quickly |
| 📅 [Date Operations](#-date-operations) | Core date manipulation functions |
| 🔄 [Date Range Operations](#-date-range-operations) | Operations related to date ranges |
| 🖨️ [Formatting & Display](#-formatting--display) | Control how dates appear |
| 💼 [Fiscal Year Operations](#-fiscal-year-operations) | Business date calculations |
| 🔍 [Advanced Features](#-advanced-features) | For power users |
| ⚡ [Performance](#-performance) | Why NepDate is faster |
| 👥 [Contributions](#-contributions) | How to help improve NepDate |
| 📝 [Change Log](#-change-log) | Recent updates |



## 📦 Installation


### Using Package Manager Console

```bash
Install-Package NepDate
```

### Using .NET CLI

```bash
dotnet add package NepDate
```


## ✨ Key Features

- 🔄 **Date Conversion:** Seamlessly convert between Bikram Sambat (B.S.) and Gregorian (A.D.) dates
- ✅ **Robust Validation:** Full support for Nepali date validation with correct day limits for each month
- 🧮 **Date Arithmetic:** Add or subtract days, months, and years with precision
- 🔍 **Smart Parsing:** Intelligent date parsing with support for multiple formats and dialects
- 🖋️ **Date Formatting:** Localized date formatting with multiple Nepali dialects and formal/informal styles
- 📊 **Fiscal Year Support:** Built-in functions for fiscal year calculations and quarter periods
- 💾 **Serialization Support:** Complete integration with System.Text.Json, Newtonsoft.Json, and XML
- ⚡ **Performance:** Benchmarked to be significantly faster than other Nepali date libraries
- 📦 **Memory Efficient:** Implemented as a struct to minimize memory footprint

## 🚀 Getting Started

### Initialize NepaliDate

#### From Nepali year, month, and day values

```csharp
using NepDate;

var nepDate = new NepaliDate(2079, 12, 16);
```

#### From a string

```csharp
using NepDate;

var nepDate = new NepaliDate("2079/12/16");
// or
var nepDate = NepaliDate.Parse("2079/12/16");
```

#### From English DateTime

```csharp
using NepDate;
using NepDate.Extensions;

var nepDate = new NepaliDate(DateTime.Now);
// or
var nepDate = DateTime.Now.ToNepaliDate();
```

#### Get current Nepali date

```csharp
using NepDate;

var today = NepaliDate.Now; // Initializes with current Nepali date
```

### Accessing Properties

```csharp
using NepDate;

var nepDate = new NepaliDate("2079/12/16");

// Basic properties
int year = nepDate.Year;        // 2079
int month = nepDate.Month;      // 12
int day = nepDate.Day;          // 16
DateTime engDate = nepDate.EnglishDate;  // 2023/03/30
DayOfWeek weekDay = nepDate.DayOfWeek;   // Thursday
int dayOfYear = nepDate.DayOfYear;       // 245

// Month details
int lastDay = nepDate.MonthEndDay;       // 30
NepaliDate monthEnd = nepDate.MonthEndDate();  // 2079/12/30
NepaliMonths monthName = nepDate.MonthName;  // Chaitra
```

## 📅 Date Operations

### Adding and Subtracting Time

```csharp
using NepDate;

var nepDate = new NepaliDate("2081/04/32");

// Add or subtract days
var fiveDaysLater = nepDate.AddDays(5);       // 2081/05/05
var fiveDaysEarlier = nepDate.AddDays(-5);    // 2081/04/27

// Add or subtract months
var twoMonthsLater = nepDate.AddMonths(2);    // 2081/06/30
var twoMonthsEarlier = nepDate.AddMonths(-2); // 2081/02/32

// Fractional months
var twoAndHalfMonths = nepDate.AddMonths(2.5); // 2081/07/15

// Adjust away from month end when needed
var adjustedDate = nepDate.AddMonths(2, awayFromMonthEnd: true); // 2081/07/02
```

### Date Comparison

```csharp
using NepDate;

var date1 = NepaliDate.Parse("2079/12/16");
var date2 = DateTime.Now.ToNepaliDate();

// Equality comparison
if (date1 == date2) { /* dates are equal */ }

// Less than / greater than
if (date1 < date2) { /* date1 is earlier than date2 */ }
if (date1 > date2) { /* date1 is after date2 */ }

// Get time span between dates
var timeSpan = date2 - date1;  // TimeSpan object
// or
timeSpan = date2.Subtract(date1);
```

### Simple Conversion

```csharp
using NepDate;

// Convert DateTime to Nepali date string
string nepaliDate = DateTime.Now.ToNepaliDate().ToString();

// Convert Nepali date string to DateTime
DateTime englishDate = NepaliDate.Parse("2079/12/16").EnglishDate;
```

### Bulk Conversion

```csharp
var engDates = new List<DateTime>();
var nepDatesAsString = new List<string>();

// Convert a collection of English dates to Nepali dates
var nepDates = NepaliDate.BulkConvert.ToNepaliDates(engDates);

// Convert a collection of Nepali dates to English dates
var newEngDates = NepaliDate.BulkConvert.ToEnglishDates(nepDates);

// Convert a collection of Nepali date strings to English dates
var parsedEngDates = NepaliDate.BulkConvert.ToEnglishDates(nepDatesAsString);
```

## 🔄 Date Range Operations

### Creating Date Ranges

```csharp
using NepDate;

// Create a range between two dates
var start = new NepaliDate(2080, 1, 1);
var end = new NepaliDate(2080, 3, 15);
var range = new NepaliDateRange(start, end);

// Create a range for a single day
var singleDay = NepaliDateRange.SingleDay(start);

// Create a range with a specific number of days
var tenDays = NepaliDateRange.FromDayCount(start, 10);

// Create ranges for specific periods
var monthRange = NepaliDateRange.ForMonth(2080, 1);      // Full month
var fiscalYear = NepaliDateRange.ForFiscalYear(2080);    // Full fiscal year
var calendarYear = NepaliDateRange.ForCalendarYear(2080); // Full calendar year

// Get current period ranges
var currentMonth = NepaliDateRange.CurrentMonth();
var currentFiscalYear = NepaliDateRange.CurrentFiscalYear();
var currentCalendarYear = NepaliDateRange.CurrentCalendarYear();
```

### Range Properties and Operations

```csharp
var range = new NepaliDateRange(start, end);

// Basic properties
bool isEmpty = range.IsEmpty;     // Check if range is empty
int length = range.Length;        // Get number of days in range
var startDate = range.Start;      // Get start date
var endDate = range.End;         // Get end date

// Range operations
bool contains = range.Contains(someDate);           // Check if date is in range
bool containsRange = range.Contains(otherRange);    // Check if range contains another range
bool overlaps = range.Overlaps(otherRange);        // Check if ranges overlap
bool adjacent = range.IsAdjacentTo(otherRange);    // Check if ranges are adjacent

// Range manipulation
var intersection = range.Intersect(otherRange);    // Get intersection of two ranges
var excluded = range.Except(otherRange);           // Get range with another range excluded
```

### Splitting and Iterating Ranges

```csharp
var range = new NepaliDateRange(start, end);

// Split range by periods
var monthRanges = range.SplitByMonth();    // Split into month ranges
var quarterRanges = range.SplitByQuarter(); // Split into quarter ranges

// Iterate through dates
foreach (var date in range)  // Iterate all dates
{
    // Process each date
}

// Get specific date collections
var workingDays = range.WorkingDays();          // Get working days (excluding Saturdays)
var workingDaysNoSunday = range.WorkingDays(excludeSunday: true); // Exclude Sundays too
var weekendDays = range.WeekendDays();          // Get weekend days
var intervalDates = range.DatesWithInterval(7);  // Get dates with 7-day interval
```

### Range Formatting

```csharp
var range = new NepaliDateRange(start, end);

// Basic string representation
string basic = range.ToString();  // "2080/01/01 - 2080/03/15"

// Formatted string
string formatted = range.ToString(DateFormats.DayMonthYear, Separators.Dash);  // "01-01-2080 - 15-03-2080"
```

## 🖨️ Formatting & Display


### Formatting Options

🗓️ **Default** `2079/02/06`  <br>  📆 **Custom** `6-2-2079`  <br>  📝 **Long** `Friday, Jestha 6`  
📊 **Unicode** `०६.०२.२०७९`  <br>  🌟 **Nepali** `शुक्रबार, जेठ ६`


### Basic Formatting

```csharp
using NepDate;

var nepDate = new NepaliDate("2079/02/06");

// Default format
string defaultFormat = nepDate.ToString();  // "2079/02/06"

// Custom format
string customFormat = nepDate.ToString(DateFormats.DayMonthYear, Separators.Dash, leadingZeros: false);  // "6-2-2079"

// Long date format
string longDate = nepDate.ToLongDateString(leadingZeros: false, displayDayName: true, displayYear: false);  // "Friday, Jestha 6"

// Nepali unicode digits
string nepaliDigits = nepDate.ToUnicodeString(DateFormats.DayMonthYear, Separators.Dot, leadingZeros: true);  // "०६.०२.२०७९"

// Long date in Nepali
string nepaliLongDate = nepDate.ToLongDateUnicodeString(leadingZeros: false, displayDayName: true, displayYear: false);  // "शुक्रबार, जेठ ६"
```

### Smart Date Parsing

```csharp
using NepDate;
using NepDate.Extensions;

// Parse with auto adjustment
var date1 = NepaliDate.Parse("2077_05_25", autoAdjust: true);  // 2077/05/25
var date2 = NepaliDate.Parse("25-05-077", autoAdjust: true);   // 2077/05/25
var date3 = NepaliDate.Parse("05.25.2077", autoAdjust: true);  // 2077/05/25

// Control month position
var date4 = NepaliDate.Parse("05/06/2077", autoAdjust: true);  // 2077/06/05
var date5 = NepaliDate.Parse("05/06/2077", autoAdjust: true, monthInMiddle: false);  // 2077/05/06

// Smart parser supports various formats
var date6 = SmartDateParser.Parse("15 Shrawan 2080");  // DD Month YYYY
var date7 = SmartDateParser.Parse("Shrawan 15, 2080");  // Month DD, YYYY
var date8 = SmartDateParser.Parse("15 Saun 2080");  // With alternate spelling
var date9 = SmartDateParser.Parse("२०८०/०४/१५");  // Nepali digits
var date10 = SmartDateParser.Parse("१५ श्रावण २०८०");  // Full Nepali format

// Extension method
var date11 = "15 Shrawan 2080".ToNepaliDate();

// Safe parsing
if ("15 Shrawan 2080".TryToNepaliDate(out var date12)) {
    // Use date12
}
```

## 💼 Fiscal Year Operations


### Fiscal Year Support

| Operation | Result | Time Period |
|-----------|--------|-------------|
| Fiscal Year Start | 2081/04/01 | Beginning of fiscal year |
| Fiscal Year End | 2082/03/31 | End of fiscal year |
| Quarter Start | 2081/04/01 | Beginning of quarter |
| Quarter End | 2081/06/30 | End of quarter |


```csharp
using NepDate;

var nepDate = new NepaliDate("2081/04/15");

// For current date
var fyStartDate = nepDate.FiscalYearStartDate();  // 2081/04/01
var fyEndDate = nepDate.FiscalYearEndDate();  // 2082/03/31
var (start, end) = nepDate.FiscalYearStartAndEndDate();  // (2081/04/01, 2082/03/31)

// Quarter information
var qStartDate = nepDate.FiscalYearQuarterStartDate();  // 2081/04/01
var qEndDate = nepDate.FiscalYearQuarterEndDate();  // 2081/06/30
var (qStart, qEnd) = nepDate.FiscalYearQuarterStartAndEndDate();  // (2081/04/01, 2081/06/30)

// Static methods using fiscal year number
var fy2080Start = NepaliDate.GetFiscalYearStartDate(2080);  // 2080/04/01
var fy2080End = NepaliDate.GetFiscalYearEndDate(2080);  // 2081/03/31
var fy2080Quarter = NepaliDate.GetFiscalYearQuarterStartAndEndDate(2080, 1);  // (2081/01/01, 2081/03/31)
```

## 🔍 Advanced Features

### Additional Helper Methods

```csharp
using NepDate;

var nepDate = new NepaliDate("2079/12/16");

// Check if year is leap year
bool isLeap = nepDate.IsLeapYear();  // False/True

// Date comparison helpers
bool isToday = nepDate.IsToday();
bool isYesterday = nepDate.IsYesterday();
bool isTomorrow = nepDate.IsTomorrow();

// Safe parsing
if (NepaliDate.TryParse("2079/13/16", out var result)) {
    // use the result NepaliDate object
}
```

### Serialization Support


#### Supporting Multiple Serialization Formats

JSON (String): `"2080-04-15"`  <br>  JSON (Object): `{"Year":2080,"Month":4,"Day":15}`  <br>  XML: Custom wrapper

#### System.Text.Json

```csharp
using System.Text.Json;
using NepDate;
using NepDate.Serialization;

// Configure for string format (default)
var options = new JsonSerializerOptions()
    .ConfigureForNepaliDate();

// Or for object format
var objectOptions = new JsonSerializerOptions()
    .ConfigureForNepaliDate(useObjectFormat: true);

// Serialize
var date = new NepaliDate(2080, 4, 15);
string jsonString = JsonSerializer.Serialize(date, options);  // "2080-04-15"
string jsonObject = JsonSerializer.Serialize(date, objectOptions);  // {"Year":2080,"Month":4,"Day":15}

// Deserialize
var deserializedDate = JsonSerializer.Deserialize<NepaliDate>(jsonString, options);
```

#### Newtonsoft.Json

```csharp
using Newtonsoft.Json;
using NepDate;
using NepDate.Serialization;

// Configure for string format (default)
var settings = new JsonSerializerSettings()
    .ConfigureForNepaliDate();

// Or for object format
var objectSettings = new JsonSerializerSettings()
    .ConfigureForNepaliDate(useObjectFormat: true);

// Serialize
var date = new NepaliDate(2080, 4, 15);
string jsonString = JsonConvert.SerializeObject(date, settings);  // "2080-04-15"
string jsonObject = JsonConvert.SerializeObject(date, objectSettings);  // {"Year":2080,"Month":4,"Day":15}

// Deserialize
var deserializedDate = JsonConvert.DeserializeObject<NepaliDate>(jsonString, settings);
```

#### XML Serialization

```csharp
using System.Xml.Serialization;
using NepDate;
using NepDate.Serialization;

// Create a wrapper class for XML serialization
public class PersonWithDate
{
    public string Name { get; set; }
    
    // Use the XML serializer wrapper
    public NepaliDateXmlSerializer BirthDate { get; set; }
    
    // Helper property for convenience
    [XmlIgnore]
    public NepaliDate ActualBirthDate
    {
        get => BirthDate?.Value ?? default;
        set => BirthDate = new NepaliDateXmlSerializer(value);
    }
}

// Usage
var person = new PersonWithDate
{
    Name = "Ram Sharma",
    ActualBirthDate = new NepaliDate(2040, 2, 15)
};

var serializer = new XmlSerializer(typeof(PersonWithDate));
// Serialize to XML...
```

## ⚡ Performance


### NepDate is up to 1000x faster than other libraries


NepDate is designed for exceptional performance, significantly outperforming other Nepali date libraries while using minimal memory.


| Package `Method`                       |  Mean (ns) | Error (ns) | StdDev (ns) | Rank | Allocated (B) |
| -------------------------------------- | ---------: | ---------: | ----------: | ---: | ------------: |
| NepDate `BS -> AD`                     |      62.59 |      0.295 |       0.261 |   1️⃣ |             - |
| NepDate `AD -> BS`                     |     276.83 |      0.593 |       0.526 |   2️⃣ |           120 |
| NepaliDateConverter.NETCORE `BS -> AD` |  63,460.38 |     54.052 |      42.201 |   3️⃣ |         20176 |
| NepaliDateConverter.NETCORE `AD -> BS` | 186,610.23 |    420.217 |     350.901 |   7️⃣ |         20160 |
| NepaliCalendarBS `BS -> AD`            |  99,511.43 |    247.038 |     231.080 |   5️⃣ |        159328 |
| NepaliCalendarBS `AD -> BS`            | 113,258.50 |    364.280 |     340.748 |   6️⃣ |        158760 |
| NepaliDateConverter.Net `BS -> AD`     |  75,327.75 |    269.244 |     251.851 |   4️⃣ |         20176 |
| NepaliDateConverter.Net `AD -> BS`     | 212,478.96 |  4,192.698 |   5,877.576 |   8️⃣ |         20160 |

<br>

<div align="center">

## 👥 Contributions



### We welcome contributions from the community!


Contributions are welcome! Please check out the [CONTRIBUTING](https://github.com/TheCrossLegCoder/NepDate/blob/main/CONTRIBUTING.md) guide for more information on how you can help improve NepDate.

## 📝 Change Log

For a detailed list of changes in each release, visit the [releases page](https://github.com/TheCrossLegCoder/NepDate/releases).

---


Made with ❤️ by [TheCrossLegCoder](https://github.com/TheCrossLegCoder)

</div>
