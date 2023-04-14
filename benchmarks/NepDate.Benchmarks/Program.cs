using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using NepDate.Benchmarks;

internal class Program
{
    private static void Main(string[] args)
        => BenchmarkRunner.Run<Benchmarks>(ManualConfig.Create(DefaultConfig.Instance).WithOptions(ConfigOptions.DisableOptimizationsValidator));
}
