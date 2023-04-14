using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using NepDate;
using NepDate.Benchmarks;

internal class Program
{
    private static void Main(string[] args)
    {
        BenchmarkRunner.Run<Benchmarks>(ManualConfig.Create(DefaultConfig.Instance).WithOptions(ConfigOptions.DisableOptimizationsValidator));
        //var memoryStart = System.GC.GetTotalMemory(true);

        //Console.WriteLine(new NepaliDate(DateTime.Now).AddDays(10).ToString());
        //Console.WriteLine(new NepaliDate("2079/08/05").AddDays(29).EnglishDate);
        //var benches = new NepDate.Benchmarks.Benchmarks();
        //Console.WriteLine(benches.GetEngDate_NepDate());
        //Console.WriteLine(benches.GetNepDate_NepDate());
    }
}