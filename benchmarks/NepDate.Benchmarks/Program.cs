using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using NepDate.Benchmarks;

internal class Program
{
    private static void Main(string[] args)
    {
        //BenchmarkRunner.Run<Benchmarks>(ManualConfig.Create(DefaultConfig.Instance).WithOptions(ConfigOptions.DisableOptimizationsValidator));
        var today = NepDate.NepaliDate.Now;
        var remainingDaysOnThisMonth = today.MonthEndDay - today.Day;
        Console.WriteLine(remainingDaysOnThisMonth.ToString());
    }
}
