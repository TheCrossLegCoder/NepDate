using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace NepDate.DotNetFrameworkBench
{
    internal class Program
    {
        private static void Main(string[] args)
            => BenchmarkRunner.Run<Benchmarks>(ManualConfig.Create(DefaultConfig.Instance).WithOptions(ConfigOptions.DisableOptimizationsValidator));
    }
}
