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
var year = nepDate.Year; // 2079
var month = nepDate.Month; // 12
var day = nepDate.Day; // 16

// get the equivalent English date as a DateTime
var englishDate = nepDate.EnglishDate; // 2023/03/30

// get the day of the week as a DayOfWeek enum value
var dayOfWeek = nepDate.DayOfWeek; // Thursday

// get the last day of the month as an integer value
var monthEndDay = nepDate.MonthEndDay; // 30

// get the last day of the month as a NepaliDate object
var monthEndDate = nepDate.MonthEndDate; // 2079/12/30

// get the name of the month as a NepaliMonths enum
var monthName = nepDate.MonthName; // Chaitra
```

### Additional Functions

```csharp
using NepDate;

var nepDate = new NepaliDate("2079/12/16");

// get the equivalent Nepali date as string
var nepDateAsString = nepDate.ToString(); // 2079/12/16

// determine if the Nepali year is a leap year
var isLeapYear = nepDate.IsLeapYear(); // False

// add or subtract days from a Nepali date
var newDate = nepDate.AddDays(30); // 2080/01/16

// get next month as a NepaliDate object
var nextMonth = nepDate.NextMonth(); // NepaliDate obj with value 2080/01/01
var nextMonth = nepDate.NextMonth(returnFirstDay: false); // NepaliDate obj with value 2080/01/16

// get previous month as a NepaliDate object
var prevMonth = nepDate.PreviousMonth(); // NepaliDate obj with value 2079/11/01
var prevMonth = nepDate.PreviousMonth(returnFirstDay: false); // NepaliDate obj with value 2079/11/16

// subtract two Nepali dates to get a TimeSpan object
var nepDate2 = new NepaliDate("2080/12/16");
var timeSpan = nepDate2 - nepDate; // Timespan object with value 365.00:00:00
// or
var timeSpan = nepDate2.Subtract(nepDate); // Timespan object with value 365.00:00:00

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
| NepDate `BS -> AD`                     |      62.59 |      0.295 |       0.261 |   1️⃣ |             - |
| NepDate `AD -> BS`                     |     276.83 |      0.593 |       0.526 |   2️⃣ |           120 |
| NepaliDateConverter.NETCORE `BS -> AD` |  63,460.38 |     54.052 |      42.201 |   3️⃣ |         20176 |
| NepaliDateConverter.NETCORE `AD -> BS` | 186,610.23 |    420.217 |     350.901 |   7️⃣ |         20160 |
| NepaliCalendarBS `BS -> AD`            |  99,511.43 |    247.038 |     231.080 |   5️⃣ |        159328 |
| NepaliCalendarBS `AD -> BS`            | 113,258.50 |    364.280 |     340.748 |   6️⃣ |        158760 |
| NepaliDateConverter.Net `BS -> AD`     |  75,327.75 |    269.244 |     251.851 |   4️⃣ |         20176 |
| NepaliDateConverter.Net `AD -> BS`     | 212,478.96 |  4,192.698 |   5,877.576 |   8️⃣ |         20160 |

```ini
BenchmarkDotNet=v0.13.5, OS=Windows 11 (10.0.22621.1413/22H2/2022Update/SunValley2)
Intel Core i5-10400 CPU 2.90GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8.1 (4.8.9139.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8.1 (4.8.9139.0), X86 LegacyJIT
```

| Package `Method`               | Mean (ns) | Error (ns) | StdDev (ns) | Rank | Allocated (B) |
| ------------------------------ | --------: | ---------: | ----------: | ---: | ------------: |
| NepDate `BS -> AD`             |     121.8 |       0.15 |        0.14 |   1️⃣ |             - |
| NepDate `AD -> BS`             |     896.6 |       3.16 |        2.80 |   2️⃣ |           413 |
| NepaliDateConverter `BS -> AD` |   2,086.9 |       4.13 |        3.86 |   3️⃣ |          2948 |
| NepaliDateConverter `AD -> BS` |   3,925.9 |      32.39 |       30.30 |   4️⃣ |          3041 |
| NepaliCalendar `BS -> AD`      | 169,622.5 |     377.10 |      334.29 |   5️⃣ |           230 |
| NepaliCalendar `AD -> BS`      | 488,003.8 |   1,433.94 |    1,271.15 |   6️⃣ |           312 |


## Change logs
https://github.com/TheCrossLegCoder/NepDate/releases


## Contributions

Please view the [CONTRIBUTING](https://github.com/TheCrossLegCoder/NepDate/blob/main/CONTRIBUTING.md) guide for more information.
