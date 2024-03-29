using BenchmarkDotNet.Attributes;

namespace NepDate.Benchmarks;

[RankColumn]
[MemoryDiagnoser(true)]
[BenchmarkDotNet.Attributes.AllStatisticsColumn]
public class Benchmarks
{
    [Benchmark]
    public void GetEngDate_NepDate()
    {
        _ = new NepaliDate(2079, 12, 12).EnglishDate;

    }

    [Benchmark]
    public void GetNepDate_NepDate()
    {
        _ = new NepaliDate(DateTime.Now).ToString();
    }


    [Benchmark]
    public void GetEngDate_NepaliDateConverter_NETCORE()
    {
        var date = NepaliDateConverter.DateConverter.ConvertToEnglish(2079, 12, 12);
        _ = new DateTime(date.Year, date.Month, date.Day);
    }

    [Benchmark]
    public void GetNepDate_NepaliDateConverter_NETCORE()
    {
        var today = DateTime.Now;
        var date = NepaliDateConverter.DateConverter.ConvertToNepali(today.Year, today.Month, today.Day);
        _ = $"{date.Year:D4}/{date.Month:D2}/{date.Day:D2}";
    }

    [Benchmark]
    public void GetEngDate_NepaliCalendarBS()
    {
        _ = NepaliCalendarBS.NepaliCalendar.Convert_BS2AD("2079/12/12");
    }

    [Benchmark]
    public void GetNepDate_NepaliCalendarBS()
    {
        var date = NepaliCalendarBS.NepaliCalendar.Convert_AD2BS(DateTime.Now);
        _ = $"{date.Year:D4}/{date.Month:D2}/{date.Day:D2}";
    }

    [Benchmark]
    public void GetEngDate_NepaliDateConverter_Net()
    {
        var date = NepaliDateConverter.Net.DateConverter.ConvertToEnglish(2079, 12, 12);
        _ = new DateTime(date.Year, date.Month, date.Day);
    }

    [Benchmark]
    public void GetNepDate_NepaliDateConverter_Net()
    {
        var today = DateTime.Now;
        var date = NepaliDateConverter.Net.DateConverter.ConvertToNepali(today.Year, today.Month, today.Day);
        _ = $"{date.Year:D4}/{date.Month:D2}/{date.Day:D2}";
    }
}
