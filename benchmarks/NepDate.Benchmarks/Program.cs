using NepDate;
using NepDate.Extensions;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        //BenchmarkRunner.Run<Benchmarks>(ManualConfig.Create(DefaultConfig.Instance).WithOptions(ConfigOptions.DisableOptimizationsValidator));
        //var today = NepDate.NepaliDate.Now;
        //var remainingDaysOnThisMonth = today.MonthEndDay - today.Day;
        //Console.WriteLine(remainingDaysOnThisMonth.ToString());

        var nepDateAddMonthTest1 = NepDate.NepaliDate.Parse("2080/08/30").SubtractMonths(36, true);
        var nepDateAddMonthTest3 = NepDate.NepaliDate.Parse("2080/09/29").AddMonths(1, false);


        
        Console.WriteLine(nepDateAddMonthTest1.ToString(NepDate.Core.Enums.DateFormats.DayMonthYear, NepDate.Core.Enums.Separators.Dot, false, true));
        Console.WriteLine(nepDateAddMonthTest3.ToUnicodeString(NepDate.Core.Enums.DateFormats.MonthDayYear, NepDate.Core.Enums.Separators.Space, true, true));

    }
}
