using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Codelyzer.Analysis.Analyzer;
using Codelyzer.Analysis.Testing.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Codelyzer.Analysis.Benchmarks
{
    /*IMPORTANT
    NopCommerce version 3.90 used by these tests requires .NET Framework version 4.5.1 to build.
    Make sure you have .NET Framework 4.5.1 Developer Pack installed before you run these tests
    */
    [SimpleJob(RunStrategy.Monitoring, launchCount: 1)]
    [MemoryDiagnoser()]
    public class NopCommerceAnalysisBenchmarks
    {
        private readonly NopCommerceTest _test = new();
        [GlobalSetup]
        public async Task GlobalSetup()
        {
            await _test.Setup();
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _test.Cleanup();
        }

        [Benchmark(Baseline = true)]
        public async Task AnalyzeSolution()
        {
            await _test.AnalyzeSolution();

        }

        [Benchmark()]
        public async Task AnalyzeSolutionGenerator()
        {
            await _test.AnalyzeSolutionGenerator();
        }
    }
}
