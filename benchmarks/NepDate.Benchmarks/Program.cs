using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using NepDate.Benchmarks;
using NepDate.Core.Enums;
using System.Globalization;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        BenchmarkRunner.Run<Benchmarks>(ManualConfig.Create(DefaultConfig.Instance).WithOptions(ConfigOptions.DisableOptimizationsValidator));

        //var today = NepDate.NepaliDate.Now;
        //var remainingDaysOnThisMonth = today.MonthEndDay - today.Day;
        //Console.WriteLine(remainingDaysOnThisMonth.ToString());

        //var nepDateAddMonthTest1 = NepDate.NepaliDate.Parse("2080/01/01").ToLongDateUnicodeString(true, true, true);
        //var nepDateAddMonthTest3 = NepDate.NepaliDate.Parse("2080/01/01").ToString("yyyy/mm/dd", CultureInfo.InvariantCulture);



        //Console.WriteLine(nepDateAddMonthTest1);
        //Console.WriteLine(nepDateAddMonthTest3);

    }
}
