# NepaliDateRange - Date Range Operations for NepDate

The `NepaliDateRange` feature adds powerful date range capabilities to the NepDate library, allowing you to work with ranges of Nepali dates efficiently and perform various operations on them.

## Basic Usage

### Creating Date Ranges

```csharp
// Create a range between two specific dates
var startDate = new NepaliDate(2080, 1, 1);  // 1 Baisakh 2080
var endDate = new NepaliDate(2080, 1, 15);   // 15 Baisakh 2080
var dateRange = new NepaliDateRange(startDate, endDate);

// Create a range for a single day
var singleDay = NepaliDateRange.SingleDay(new NepaliDate(2080, 1, 1));

// Create a range with a specific number of days
var tenDayRange = NepaliDateRange.FromDayCount(startDate, 10);

// Create a range for a complete month
var monthRange = NepaliDateRange.ForMonth(2080, 1);  // Baisakh 2080

// Create a range for a complete fiscal year
var fiscalYearRange = NepaliDateRange.ForFiscalYear(2080);  // 2080/81 fiscal year

// Create a range for a complete calendar year
var calendarYearRange = NepaliDateRange.ForCalendarYear(2080);  // 2080 BS

// Get ranges for current periods
var currentMonth = NepaliDateRange.CurrentMonth();
var currentFiscalYear = NepaliDateRange.CurrentFiscalYear();
var currentCalendarYear = NepaliDateRange.CurrentCalendarYear();
```

### Basic Properties

```csharp
var range = new NepaliDateRange(
    new NepaliDate(2080, 1, 1),
    new NepaliDate(2080, 1, 15)
);

// Access start and end dates
Console.WriteLine($"Start: {range.Start}");  // Start: 2080/01/01
Console.WriteLine($"End: {range.End}");      // End: 2080/01/15

// Get the length in days
Console.WriteLine($"Length: {range.Length} days");  // Length: 15 days

// Check if a range is empty
Console.WriteLine($"Is empty: {range.IsEmpty}");  // Is empty: False
```

## Range Operations

### Checking if a Date is Within a Range

```csharp
var range = new NepaliDateRange(
    new NepaliDate(2080, 1, 1),
    new NepaliDate(2080, 1, 15)
);

var date = new NepaliDate(2080, 1, 10);
bool isInRange = range.Contains(date);  // true
```

### Checking if a Range Contains Another Range

```csharp
var outerRange = new NepaliDateRange(
    new NepaliDate(2080, 1, 1),
    new NepaliDate(2080, 1, 30)
);

var innerRange = new NepaliDateRange(
    new NepaliDate(2080, 1, 10),
    new NepaliDate(2080, 1, 20)
);

bool contains = outerRange.Contains(innerRange);  // true
```

### Checking if Ranges Overlap

```csharp
var range1 = new NepaliDateRange(
    new NepaliDate(2080, 1, 1),
    new NepaliDate(2080, 1, 15)
);

var range2 = new NepaliDateRange(
    new NepaliDate(2080, 1, 10),
    new NepaliDate(2080, 1, 25)
);

bool overlaps = range1.Overlaps(range2);  // true
```

### Finding the Intersection of Two Ranges

```csharp
var range1 = new NepaliDateRange(
    new NepaliDate(2080, 1, 1),
    new NepaliDate(2080, 1, 15)
);

var range2 = new NepaliDateRange(
    new NepaliDate(2080, 1, 10),
    new NepaliDate(2080, 1, 25)
);

var intersection = range1.Intersect(range2);
// intersection is 2080/01/10 - 2080/01/15
```

### Finding the Union of Two Ranges

```csharp
var range1 = new NepaliDateRange(
    new NepaliDate(2080, 1, 1),
    new NepaliDate(2080, 1, 15)
);

var range2 = new NepaliDateRange(
    new NepaliDate(2080, 1, 10),
    new NepaliDate(2080, 1, 25)
);

var union = range1.Union(range2);
// union is 2080/01/01 - 2080/01/25
```

### Excluding a Range

```csharp
var fullRange = new NepaliDateRange(
    new NepaliDate(2080, 1, 1),
    new NepaliDate(2080, 1, 30)
);

var middleRange = new NepaliDateRange(
    new NepaliDate(2080, 1, 10),
    new NepaliDate(2080, 1, 20)
);

var result = fullRange.Except(middleRange);
// result is an array of two ranges:
// [0]: 2080/01/01 - 2080/01/09
// [1]: 2080/01/21 - 2080/01/30
```

## Splitting Ranges

### Splitting by Month

```csharp
var range = new NepaliDateRange(
    new NepaliDate(2080, 1, 15),  // 15 Baisakh
    new NepaliDate(2080, 3, 10)   // 10 Ashadh
);

var monthRanges = range.SplitByMonth();
// monthRanges contains 3 ranges:
// [0]: 2080/01/15 - 2080/01/31 (rest of Baisakh)
// [1]: 2080/02/01 - 2080/02/32 (all of Jestha)
// [2]: 2080/03/01 - 2080/03/10 (first 10 days of Ashadh)
```

### Splitting by Fiscal Quarter

```csharp
var fiscalYear = NepaliDateRange.ForFiscalYear(2080);
var quarters = fiscalYear.SplitByFiscalQuarter();
// quarters contains 4 ranges:
// [0]: 2080/04/01 - 2080/06/32 (First Quarter)
// [1]: 2080/07/01 - 2080/09/30 (Second Quarter)
// [2]: 2080/10/01 - 2080/12/30 (Third Quarter)
// [3]: 2081/01/01 - 2081/03/32 (Fourth Quarter)
```

## Filtering Dates

### Working Days

```csharp
var range = new NepaliDateRange(
    new NepaliDate(2080, 1, 1),
    new NepaliDate(2080, 1, 15)
);

// Get all working days (excluding Saturdays)
var workingDays = range.WorkingDays().ToList();

// Get all working days (excluding both Saturday and Sunday)
var strictWorkingDays = range.WorkingDays(excludeSunday: true).ToList();
```

### Weekend Days

```csharp
var range = new NepaliDateRange(
    new NepaliDate(2080, 1, 1),
    new NepaliDate(2080, 1, 30)
);

// Get all weekend days (Saturdays and Sundays)
var weekendDays = range.WeekendDays().ToList();

// Get only Saturdays
var saturdaysOnly = range.WeekendDays(includeSunday: false).ToList();
```

### Dates at Regular Intervals

```csharp
var range = new NepaliDateRange(
    new NepaliDate(2080, 1, 1),
    new NepaliDate(2080, 3, 30)
);

// Get dates at weekly intervals
var weeklyDates = range.DatesWithInterval(7).ToList();

// Get dates at 10-day intervals
var tenDayDates = range.DatesWithInterval(10).ToList();
```

## Iterating Through Ranges

```csharp
var range = new NepaliDateRange(
    new NepaliDate(2080, 1, 1),
    new NepaliDate(2080, 1, 5)
);

// Use foreach to iterate through all dates in the range
foreach (var date in range)
{
    Console.WriteLine(date);
}
// Output:
// 2080/01/01
// 2080/01/02
// 2080/01/03
// 2080/01/04
// 2080/01/05

// Or use LINQ methods
var allDates = range.ToList();
var formattedDates = range.Select(d => d.ToString(DateFormats.YearMonthDay));
```

## Real-World Examples

### Project Timeline Management

```csharp
// Define a project timeline
var projectRange = new NepaliDateRange(
    new NepaliDate(2080, 4, 15), // 15 Shrawan 2080
    new NepaliDate(2080, 8, 30)  // 30 Mangsir 2080
);

// Define festival periods that are non-working days
var dashainRange = new NepaliDateRange(
    new NepaliDate(2080, 6, 25), // approximate start
    new NepaliDate(2080, 7, 5)   // approximate end
);

var tiharRange = new NepaliDateRange(
    new NepaliDate(2080, 7, 20), // approximate start
    new NepaliDate(2080, 7, 25)  // approximate end
);

// Check if project overlaps with festivals
bool overlapsDashain = projectRange.Overlaps(dashainRange); // true
bool overlapsTihar = projectRange.Overlaps(tiharRange);     // true

// Calculate actual working days excluding festivals and weekends
var nonWorkingRanges = new[] { dashainRange, tiharRange };
var workingRanges = new List<NepaliDateRange> { projectRange };

// Remove festival periods
foreach (var festivalRange in nonWorkingRanges)
{
    workingRanges = workingRanges
        .SelectMany(r => r.Except(festivalRange))
        .ToList();
}

// Count working days
int totalWorkingDays = workingRanges
    .SelectMany(r => r.WorkingDays())
    .Count();

Console.WriteLine($"Project duration: {projectRange.Length} days");
Console.WriteLine($"Actual working days: {totalWorkingDays} days");
```

### Fiscal Year Reporting

```csharp
// Get the current fiscal year
var fiscalYear = NepaliDateRange.CurrentFiscalYear();

// Split by quarters for quarterly reporting
var quarters = fiscalYear.SplitByFiscalQuarter();

// Split by months for monthly reporting
var months = fiscalYear.SplitByMonth();

// Calculate working days in each quarter
var workingDaysByQuarter = quarters
    .Select((q, i) => new {
        Quarter = i + 1,
        WorkingDays = q.WorkingDays().Count()
    })
    .ToList();

// Print quarterly info
foreach (var q in workingDaysByQuarter)
{
    Console.WriteLine($"Quarter {q.Quarter}: {q.WorkingDays} working days");
}
```

### Date Range Validation

```csharp
// Function to validate if a date range is valid for a vacation request
bool IsValidVacationPeriod(NepaliDateRange vacationRange)
{
    // Check if range is in the future
    var today = new NepaliDate(DateTime.Today);
    if (vacationRange.Start < today)
    {
        return false;
    }
    
    // Check if length is within policy (e.g., maximum 15 days)
    if (vacationRange.Length > 15)
    {
        return false;
    }
    
    // Check if it conflicts with blackout periods
    var dashainRange = new NepaliDateRange(
        new NepaliDate(2080, 6, 25),
        new NepaliDate(2080, 7, 5)
    );
    
    if (vacationRange.Overlaps(dashainRange))
    {
        return false; // No vacations during Dashain
    }
    
    // Check if only requesting weekdays
    var requestedDays = vacationRange.ToList();
    return requestedDays.All(d => d.DayOfWeek != DayOfWeek.Saturday 
                               && d.DayOfWeek != DayOfWeek.Sunday);
}

// Test the function
var vacationRequest = new NepaliDateRange(
    new NepaliDate(2080, 5, 1),
    new NepaliDate(2080, 5, 5)
);

bool isValid = IsValidVacationPeriod(vacationRequest);
Console.WriteLine($"Vacation request is valid: {isValid}");
``` 