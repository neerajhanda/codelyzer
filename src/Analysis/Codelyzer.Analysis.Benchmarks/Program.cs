using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Logging;

namespace Codelyzer.Analysis.Benchmarks
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Debug - 
            //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());

            //Benchmarking
            var summary = BenchmarkRunner.Run<NopCommerceAnalysisBenchmarks>();

        }
    }
}