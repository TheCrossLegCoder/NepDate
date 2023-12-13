using NepDate;

internal class Program
{
    private static void Main(string[] args)
    {
        //BenchmarkRunner.Run<Benchmarks>(ManualConfig.Create(DefaultConfig.Instance).WithOptions(ConfigOptions.DisableOptimizationsValidator));
        //var today = NepDate.NepaliDate.Now;
        //var remainingDaysOnThisMonth = today.MonthEndDay - today.Day;
        //Console.WriteLine(remainingDaysOnThisMonth.ToString());

        var nepDateAddMonthTest1 = NepDate.NepaliDate.Parse("2080/04/32").AddMonths(1, true);
        var nepDateAddMonthTest3 = NepDate.NepaliDate.Parse("2080/04/32").AddMonths(1, false);
      
        

        Console.WriteLine(nepDateAddMonthTest1.ToString());
        Console.WriteLine(nepDateAddMonthTest3.ToString());

    }
}
