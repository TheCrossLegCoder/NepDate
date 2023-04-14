using BenchmarkDotNet.Attributes;

namespace NepDate.Benchmarks;

[RankColumn]
[MemoryDiagnoser(true)]
public class Benchmarks
{
    [Benchmark]
    public DateTime GetEngDate_NepDate() => new NepaliDate(2079, 12, 12).EnglishDate;

    [Benchmark]
    public string GetNepDate_NepDate() => new NepaliDate(DateTime.Now).ToString();

    [Benchmark]
    public void GetEngDate_NepaliDateConverter_NETCORE() => NepaliDateConverter.DateConverter.ConvertToEnglish(2079, 12, 12);

    [Benchmark]
    public void GetNepDate_NepaliDateConverter_NETCORE()
    {
        var today = DateTime.Now;
        NepaliDateConverter.DateConverter.ConvertToNepali(today.Year, today.Month, today.Day);
    }

    [Benchmark]
    public void GetEngDate_NepaliCalendarBS() => NepaliCalendarBS.NepaliCalendar.Convert_BS2AD("2079/12/12");

    [Benchmark]
    public void GetNepDate_NepaliCalendarBS()
    {
        var today = DateTime.Now;
        NepaliCalendarBS.NepaliCalendar.Convert_AD2BS(today);
    }

    [Benchmark]
    public void GetEngDate_NepaliDateConverter_Net() => NepaliDateConverter.Net.DateConverter.ConvertToEnglish(2079, 12, 12);

    [Benchmark]
    public void GetNepDate_NepaliDateConverter_Net()
    {
        var today = DateTime.Now;
        NepaliDateConverter.Net.DateConverter.ConvertToNepali(today.Year, today.Month, today.Day);
    }
}
