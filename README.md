![LogoWhiteCropped](https://user-images.githubusercontent.com/37014558/231635618-bf6599e3-554e-4b02-93df-019e7b8aecc3.png)

![GitHub Repo Stars](https://img.shields.io/github/stars/TheCrossLegCoder/NepDate)

## Package

| Package                                            | NuGet Stable                                                                                      | Downloads                                                                                      |
| -------------------------------------------------- | ------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| [NepDate](https://www.nuget.org/packages/NepDate/) | [![NepDate](https://img.shields.io/nuget/v/NepDate.svg)](https://www.nuget.org/packages/NepDate/) | [![NepDate](https://img.shields.io/nuget/dt/NepDate)](https://www.nuget.org/packages/NepDate/) |

## Installation

### Install NepDate with the package manager console

```bash
NuGet\Install-Package NepDate
```

### Install NepDate with the .NET CLI

```bash
dotnet add package NepDate
```

## Features

NepDate is a `struct` based on `.NET Standard 2.0` that closely resembles the `DateOnly` `struct` in `.NET`. It stands out due to its `easy integration`, `super-fast` and `memory-efficient` conversions, and built in `powerful features` related to Nepali date functionality.

### Initialization

#### By nepali year, month, and day values

```csharp
using NepDate;

var nepDate = new NepaliDate(2079, 12, 16);
```

#### By Nepali date as a string

```csharp
using NepDate;

var nepDate = new NepaliDate("2079/12/16");
// or
var nepDate = NepaliDate.Parse("2079/12/16");
```

#### By English DateTime

```csharp
using NepDate;
using NepDate.Extensions;

var nepDate = new NepaliDate(DateTime.Now);
// or
var nepDate = DateTime.Now.ToNepaliDate();
```

#### By Nothing

```csharp
using NepDate;

var nepDate = NepaliDate.Now; // Initializes NepaliDate object with current Nepali date
```

### Properties

```csharp
using NepDate;

var nepDate = new NepaliDate("2079/12/16");

// get Nepali year, month, and day values
nepDate.Year; // 2079
nepDate.Month; // 12
nepDate.Day; // 16

// get the equivalent English date as a DateTime
nepDate.EnglishDate; // 2023/03/30

// get the day of the week as a DayOfWeek enum value
nepDate.DayOfWeek; // Thursday

//Gets the day of the year, expressed as a value between 1 and 365
nepDate.DayOfYear; //245

// get the last day of the month as an integer value
nepDate.MonthEndDay; // 30

// get the last day of the month as a NepaliDate object
nepDate.MonthEndDate; // 2079/12/30

// get the name of the month as a NepaliMonths enum
nepDate.MonthName; // Chaitra
```

### Formatting

```csharp
using NepDate;

var nepDate = new NepaliDate("2079/02/06");

// Obtain the Nepali date represented as a string in its default format.
nepDate.ToString(); // 2079/02/06

// Customize the output by specifying different date formats, separators, and the use of leading zeros if needed.
nepDate.ToString(DateFormats.DayMonthYear, Separators.Dash, leadingZeros: false); // 6-2-2079

// Retrieve a representation of the date with options to omit leading zeros, display the day name, and exclude the year.
nepDate.ToLongDateString(leadingZeros: false, displayDayName: true, displayYear: false); // Friday, Jestha 6

// Obtain the date in Nepali numerical format, with options to specify the format, separators, and the use of leading zeros.
nepDate.ToUnicodeString(DateFormats.DayMonthYear, Separators.Dot, leadingZeros: true); // ०६.०२.२०७९

// Retrieve the Nepali representation of the date, with options to exclude leading zeros, display the day name, and exclude the year.
nepDate.ToLongDateUnicodeString(leadingZeros: false, displayDayName: true, displayYear: false); // शुक्रबार, जेठ ६

```

### Add & Subtract Nepali Months & Days

```csharp
using NepDate;

var nepDate = new NepaliDate("2081/04/32");

// Increment or decrement the Nepali date by a specified number of days.
nepDate.AddDays(05); // 2081/05/05
nepDate.AddDays(-5); // 2081/04/27

// Adjust the Nepali date by adding or subtracting a specified number of months.
nepDate.AddMonths(2); // 2081/06/30
nepDate.AddMonths(-2); // 2081/02/32

// When adding months, if the resulting month doesn't have the same end date as the input month,
// 'awayFromMonthEnd' option ensures adjustment away from the end of the resulting month.
nepDate.AddMonths(2, awayFromMonthEnd: true); // 2081/07/02

```

### Fiscal Year

```csharp
// Can obtain various fiscal year details using the NepaliDate Instance
var nepDate = new NepaliDate("2081/04/15");
nepDate.FiscalYearStartDate(); // 2081/04/01
nepDate.FiscalYearEndDate(); // 2082/03/31
nepDate.FiscalYearStartAndEndDate(); // (2081/04/01, 2082/03/31)
nepDate.FiscalYearQuarterStartDate(); // 2081/04/01
nepDate.FiscalYearQuarterEndDate(); // 2081/06/30
nepDate.FiscalYearQuarterStartAndEndDate(); // (2081/04/01, 2081/06/30)

// Also can achieve the same details through parameters
NepaliDate.GetFiscalYearStartDate(2080); // 2080/04/01
NepaliDate.GetFiscalYearEndDate(2080); // 2081/03/31
NepaliDate.GetFiscalYearStartAndEndDate(2080); // (2080/04/01, 2081/03/31)
NepaliDate.GetFiscalYearQuarterStartDate(2080, 1); // 2081/01/01
NepaliDate.GetFiscalYearQuarterEndDate(2080, 1); // 2081/03/31
NepaliDate.GetFiscalYearQuarterStartAndEndDate(2080, 1); // (2081/01/01, 2081/03/31)
```

### Bulk Conversion

```csharp
var engDates = new List<DateTime>();
var nepDatesAsString = new List<string>();

// Converts a collection of English dates to Nepali dates
var newNepDates = NepaliDate.BulkConvert.ToNepaliDate(engDates);

// Converts a collection of Nepali date instances to English dates
var newEngDates = NepaliDate.BulkConvert.ToEnglishDate(newNepDates);

// Converts a collection of Nepali dates represented as strings to English dates
var newEngDates = NepaliDate.BulkConvert.ToEnglishDate(nepDatesAsString);
```

### Additional Functions

```csharp
using NepDate;

var nepDate = new NepaliDate("2079/12/16");

// determine if the Nepali year is a leap year
nepDate.IsLeapYear(); // False/True

nepDate.IsToday(); // False/True

nepDate.IsYesterday(); // False/True

nepDate.IsTomorrow(); // False/True

// subtract two Nepali dates to get a TimeSpan object
var nepDate2 = new NepaliDate("2080/12/16");
nepDate2 - nepDate; // Timespan object with value 365.00:00:00
// or
nepDate2.Subtract(nepDate); // Timespan object with value 365.00:00:00

// check if a string is a valid Nepali date and convert it to a NepaliDate object
if (NepaliDate.TryParse("2079/13/16", out var result))
{
    // use the result NepaliDate object
}
```

### Comparing NepaliDate objects

```csharp
using NepDate;

var nepDate = NepaliDate.Parse("2079/12/16");
var nepDate2 = DateTime.Now.ToNepaliDate();

if (nepDate == nepDate2)
{
    // the two NepaliDate objects are equal
}

if (nepDate < nepDate2)
{
    // nepDate is earlier than nepaliDate2
}

if (nepDate > nepDate2)
{
    // nepDate is after nepaliDate2
}

// etc
```

### You just need the conversion?

```csharp
using NepDate;

// Convert a DateTime directly to a NepaliDate string
var convertedToBS = DateTime.Now.ToNepaliDate().ToString();

// Convert a NepaliDate string directly to a DateTime
var convertedToAD = NepaliDate.Parse("2079/12/16").EnglishDate;
```

### Parsing Nepali Date With `AutoAdjust`

```csharp
// Parsing will try it's best to accurately identify the year, month and day
// And returns the date in the standard format of "yyyy/MM/dd"
// Below exmaples will demonstrate the probabilities.


// Replaces "_" To "/", Returns without adjusting if is already adjusted
var nepDate = NepaliDate.Parse("2077_05_25", autoAdjust: true); // 2077/05/25

// Replaces "-" To "/", Identifies '25' as day, '05' as month and '077' as year '2077'
var nepDate = NepaliDate.Parse("25-05-077", autoAdjust: true); // 2077/05/25

// Replaces "." To "/", Identifies '05' as month and '25' as day
var nepDate = NepaliDate.Parse("05.25.2077", autoAdjust: true); // 2077/05/25

// As '06' is on middle, Identifies it as month and '05' as day
var nepDate = NepaliDate.Parse("05/06/2077", autoAdjust: true); // 2077/06/05

// Identifies '05' as month due to parm 'monthInMiddle = false' and '06' as day
var nepDate = NepaliDate.Parse("05/06/2077", autoAdjust: true, monthInMiddle: false); // 2077/05/06
```

## Performance

NepDate is distinguished by its capacity to perform with exceptional speed while utilizing minimal runtime memory resources. The metrics presented below exemplify NepDate's remarkable efficiency and proficiency, while remaining mindful of resource consumption.

The benchmarks can be found in [NepDate.Benchmarks](https://github.com/TheCrossLegCoder/NepDate/tree/main/benchmarks/NepDate.Benchmarks) & [NepDate.DotNetFrameworkBench](https://github.com/TheCrossLegCoder/NepDate/tree/main/benchmarks/NepDate.DotNetFrameworkBench)

### Output from latest run is

```ini
BenchmarkDotNet=v0.13.5, OS=Windows 11 (10.0.22621.1413/22H2/2022Update/SunValley2)
Intel Core i5-10400 CPU 2.90GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.200
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
```

| Package `Method`                       |  Mean (ns) | Error (ns) | StdDev (ns) | Rank | Allocated (B) |
| -------------------------------------- | ---------: | ---------: | ----------: | ---: | ------------: |
| NepDate `BS -> AD`                     |      62.59 |      0.295 |       0.261 |   1️ |             - |
| NepDate `AD -> BS`                     |     276.83 |      0.593 |       0.526 |   2️ |           120 |
| NepaliDateConverter.NETCORE `BS -> AD` |  63,460.38 |     54.052 |      42.201 |   3️ |         20176 |
| NepaliDateConverter.NETCORE `AD -> BS` | 186,610.23 |    420.217 |     350.901 |   7️ |         20160 |
| NepaliCalendarBS `BS -> AD`            |  99,511.43 |    247.038 |     231.080 |   5️ |        159328 |
| NepaliCalendarBS `AD -> BS`            | 113,258.50 |    364.280 |     340.748 |   6️ |        158760 |
| NepaliDateConverter.Net `BS -> AD`     |  75,327.75 |    269.244 |     251.851 |   4️ |         20176 |
| NepaliDateConverter.Net `AD -> BS`     | 212,478.96 |  4,192.698 |   5,877.576 |   8️ |         20160 |

## Change logs

https://github.com/TheCrossLegCoder/NepDate/releases

## Contributions

Please view the [CONTRIBUTING](https://github.com/TheCrossLegCoder/NepDate/blob/main/CONTRIBUTING.md) guide for more information.
