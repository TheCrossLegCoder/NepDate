using System;
using BenchmarkDotNet.Attributes;

namespace NepDate.DotNetFrameworkBench
{
    [RankColumn]
    [MemoryDiagnoser(true)]
    public class Benchmarks
    {
        [Benchmark]
        public void GetEngDate_NepaliCalendar() => _ = NepaliCalender.Convert.ToEnglish("2070-01-12");

        [Benchmark]
        public void GetNepDate_NepaliCalendar() => _ = NepaliCalender.Convert.ToNepali(DateTime.Now);

        [Benchmark]
        public void GetEngDate_NepaliDateConverter() => _ = new NepaliDateConverter.DateConverter().GetAdDateFromBsDate(2070, 12, 12);

        [Benchmark]
        public void GetNepDate_NepaliDateConverter() => _ = new NepaliDateConverter.DateConverter().GetBsDateFromAdDate(2023, 04, 03);

        [Benchmark]
        public DateTime GetEngDate_NepDate() => new NepaliDate(2079, 12, 12).EnglishDate;

        [Benchmark]
        public string GetNepDate_NepDate() => new NepaliDate(DateTime.Now).Value;
    }
}
