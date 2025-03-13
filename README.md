![LogoWhiteCropped](https://user-images.githubusercontent.com/37014558/231635618-bf6599e3-554e-4b02-93df-019e7b8aecc3.png)

![GitHub Repo Stars](https://img.shields.io/github/stars/TheCrossLegCoder/NepDate)

## Package

| Package                                            | NuGet Stable                                                                                      | Downloads                                                                                      |
| -------------------------------------------------- | ------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| [NepDate](https://www.nuget.org/packages/NepDate/) | [![NepDate](https://img.shields.io/nuget/v/NepDate.svg)](https://www.nuget.org/packages/NepDate/) | [![NepDate](https://img.shields.io/nuget/dt/NepDate)](https://www.nuget.org/packages/NepDate/) |

## Installation

### Install NepDate with the package manager console

```bash
Install-Package NepDate
```

### Install NepDate with the .NET CLI

```bash
dotnet add package NepDate
```

## Features

NepDate is a super-fast and memory-efficient `struct` based on `.NET Standard 2.0` that closely resembles the `DateOnly` `struct` in `.NET` with built in powerful features related to Nepali date functionality.

- Convert between Bikram Sambat (B.S.) and Gregorian (A.D.) dates
- Full support for Nepali date validation
- Calculate day of week for any Nepali date
- Perform date arithmetic (add/subtract days, months, years)
- Generate monthly calendars in Nepali dates
- Create and work with date ranges
- Intelligent date parsing with support for multiple formats and dialects
- Localized date formatting with support for multiple Nepali dialects and formal/informal styles
- Serialization support for System.Text.Json, Newtonsoft.Json, and XML

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

// Can also provide month as decimal
nepDate.AddMonths(2.5); // 2081/07/15

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
// Here, We take the first year as Fiscal Year. Eg: 2080 means Fy 2080/2081
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
var newNepDates = NepaliDate.BulkConvert.ToNepaliDates(engDates);

// Converts a collection of Nepali date instances to English dates
var newEngDates = NepaliDate.BulkConvert.ToEnglishDates(newNepDates);

// Converts a collection of Nepali dates represented as strings to English dates
var newEngDates = NepaliDate.BulkConvert.ToEnglishDates(nepDatesAsString);
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

### Localized Date Formatting

### Smart Date Parser

NepDate includes a powerful Smart Date Parser that can intelligently parse a wide variety of Nepali date formats:

```csharp
using NepDate;
using NepDate.Extensions;

// Parse dates in different standard formats
var date1 = SmartDateParser.Parse("2080/04/15");     // YYYY/MM/DD
var date2 = SmartDateParser.Parse("15-04-2080");     // DD-MM-YYYY
var date3 = SmartDateParser.Parse("04.15.2080");     // MM.DD.YYYY

// Parse dates with month names (supports multiple spelling variations)
var date4 = SmartDateParser.Parse("15 Shrawan 2080"); // DD Month YYYY
var date5 = SmartDateParser.Parse("Shrawan 15, 2080"); // Month DD, YYYY
var date6 = SmartDateParser.Parse("15 Saun 2080");    // Alternate spelling

// Parse dates with Nepali unicode digits and/or month names
var date7 = SmartDateParser.Parse("२०८०/०४/१५");      // YYYY/MM/DD in Nepali digits
var date8 = SmartDateParser.Parse("१५ श्रावण २०८०");   // DD Month YYYY in Nepali

// Parse mixed formats (combining Nepali and English elements)
var date9 = SmartDateParser.Parse("15 साउन 2080");    // DD Nepali_Month English_Year

// Handle dates with common suffixes and formats
var date10 = SmartDateParser.Parse("15 Shrawan 2080 B.S."); // With B.S. suffix
var date11 = SmartDateParser.Parse("15 साउन, 2080 गते");    // With 'gate' suffix

// Use the extension method for easier access
var date12 = "15 Shrawan 2080".ToNepaliDate();

// Try parsing with error handling
if (SmartDateParser.TryParse("15 Shrawan 2080", out var result))
{
    // Use the successful result
    Console.WriteLine(result);
}

// Extension method version for error handling
if ("15 Shrawan 2080".TryToNepaliDate(out var date))
{
    // Use the successfully parsed date
    Console.WriteLine(date);
}
```

The Smart Date Parser features:

- Support for multiple date formats (YYYY/MM/DD, DD/MM/YYYY, MM/DD/YYYY)
- Multiple separator support (/, -, ., space, etc.)
- Month name recognition in both English and Nepali with multiple spelling variations
- Nepali unicode digit support
- Mixed format handling (combining English and Nepali elements)
- Fuzzy matching for month names to handle typos
- Support for common date suffixes (B.S., V.S., गते, etc.)
- Intelligent handling of 2-digit and 3-digit years

### Serialization Support

NepDate provides comprehensive serialization support for various .NET serialization frameworks:

#### System.Text.Json Serialization

```csharp
using System.Text.Json;
using NepDate;
using NepDate.Serialization;

// Configure serialization options (string format by default)
var options = new JsonSerializerOptions()
    .ConfigureForNepaliDate();

// For object format serialization (with Year, Month, Day properties)
var objectOptions = new JsonSerializerOptions()
    .ConfigureForNepaliDate(useObjectFormat: true);

// Serialize a NepaliDate
var date = new NepaliDate(2080, 4, 15);
string jsonString = JsonSerializer.Serialize(date, options);
// Result: "2080-04-15"

// Serialize with object format
string jsonObject = JsonSerializer.Serialize(date, objectOptions);
// Result: {"Year":2080,"Month":4,"Day":15}

// Deserialize back to NepaliDate
var deserializedDate = JsonSerializer.Deserialize<NepaliDate>(jsonString, options);
```

#### Newtonsoft.Json Serialization

```csharp
using Newtonsoft.Json;
using NepDate;
using NepDate.Serialization;

// Configure serialization settings (string format by default)
var settings = new JsonSerializerSettings()
    .ConfigureForNepaliDate();

// For object format serialization (with Year, Month, Day properties)
var objectSettings = new JsonSerializerSettings()
    .ConfigureForNepaliDate(useObjectFormat: true);

// Serialize a NepaliDate
var date = new NepaliDate(2080, 4, 15);
string jsonString = JsonConvert.SerializeObject(date, settings);
// Result: "2080-04-15"

// Serialize with object format
string jsonObject = JsonConvert.SerializeObject(date, objectSettings);
// Result: {"Year":2080,"Month":4,"Day":15}

// Deserialize back to NepaliDate
var deserializedDate = JsonConvert.DeserializeObject<NepaliDate>(jsonString, settings);
```

#### XML Serialization

XML serialization requires using the `NepaliDateXmlSerializer` wrapper class:

```csharp
using System.Xml.Serialization;
using NepDate;
using NepDate.Serialization;

// Create a wrapper for XML serialization
public class PersonWithDate
{
    public string Name { get; set; }
    
    // Use the XML serializer wrapper
    public NepaliDateXmlSerializer BirthDate { get; set; }
    
    // Non-serialized property for convenience
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
