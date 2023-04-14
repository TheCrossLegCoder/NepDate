using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace NepDate.DotNetFrameworkBench
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var services = new ServiceCollection();

            //services.AddSingleton<NepaliDateConverter.IDateConverter, NepaliDateConverter.DateConverter>();

            //var serviceProvider = services.BuildServiceProvider();
            //var service = serviceProvider.GetService<IDateConverter>();

            //Console.WriteLine(NepaliCalender.Convert.ToEnglish("2079-12-12"));
            //Console.WriteLine(new DateConverter().GetAdDateFromBsDate(2079, 12, 12));

            _ = BenchmarkRunner.Run<Benchmarks>(ManualConfig.Create(DefaultConfig.Instance).WithOptions(ConfigOptions.DisableOptimizationsValidator));
        }
    }
}
