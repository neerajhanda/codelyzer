using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Codelyzer.Analysis.Analyzer;
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
        private readonly string _solutionSuffix = "nopCommerce-release-3.90\\src\\NopCommerce.sln";
        private ILogger _logger;
        private string? _downloadDirectory = null;
        private string? _outputDirectoryPathForSync;
        private string? _outputDirectoryPathForAsyncGenerator;

        [GlobalSetup]
        public async Task GlobalSetup()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.SetMinimumLevel(LogLevel.Warning).AddConsole());
            _logger = loggerFactory.CreateLogger("Analyzer");
            _downloadDirectory = Path.Combine(Path.GetTempPath(), "NopCommerce");
            _outputDirectoryPathForSync = Path.Combine(_downloadDirectory, "NopCommerceSyncAnalysisBenchmarks");
            _outputDirectoryPathForAsyncGenerator =
                Path.Combine(_downloadDirectory, "NopCommerceAsyncGeneratorAnalysisBenchmarks");
            Directory.CreateDirectory(_downloadDirectory);
            await DownloadAndExtractAsync(
                @"https://github.com/nopSolutions/nopCommerce/archive/refs/tags/release-3.90.zip",
                _downloadDirectory);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            DeleteDirectory(_downloadDirectory);
            DeleteDirectory(_outputDirectoryPathForSync);
            DeleteDirectory(_outputDirectoryPathForAsyncGenerator);
        }

        [Benchmark(Baseline = true)]
        public async Task AnalyzeSolution()
        {
            var analyzerConfiguration = CreateAnalyzerConfiguration(_outputDirectoryPathForSync);
            CodeAnalyzerByLanguage analyzerByLanguage = new CodeAnalyzerByLanguage(analyzerConfiguration, _logger);
            if (_downloadDirectory != null)
            {
                var results =
                    await analyzerByLanguage.AnalyzeSolution(Path.Combine(_downloadDirectory, _solutionSuffix));
                var allBuildErrors = results.SelectMany(r => r.ProjectBuildResult.BuildErrors);
            }
        }

        [Benchmark()]
        public async Task AnalyzeSolutionGenerator()
        {
            var analyzerConfiguration = CreateAnalyzerConfiguration(_outputDirectoryPathForAsyncGenerator);
            CodeAnalyzerByLanguage analyzerByLanguage = new CodeAnalyzerByLanguage(analyzerConfiguration, _logger);
            if (_downloadDirectory != null)
            {
                var results =
                    await analyzerByLanguage.AnalyzeSolutionGenerator(Path.Combine(_downloadDirectory, _solutionSuffix));
                var allBuildErrors = results.SelectMany(r => r.ProjectBuildResult.BuildErrors);
            }

        }

        private async Task DownloadAndExtractAsync(string url, string directory)
        {
            using var client = new HttpClient();
            await using var streamRead = await client.GetStreamAsync(url);
            var fileName = Path.GetTempFileName();
            await using (var streamWrite = File.Create(fileName))
            {
                await streamRead.CopyToAsync(streamWrite);
            }
            ZipFile.ExtractToDirectory(fileName, directory, true);
            File.Delete(fileName);
        }

        private AnalyzerConfiguration CreateAnalyzerConfiguration(string outputDirectoryPath)
        {
            AnalyzerConfiguration configuration = new AnalyzerConfiguration(LanguageOptions.CSharp)
            {
                ExportSettings =
                {
                    GenerateJsonOutput = true,
                    GenerateGremlinOutput = false,
                    GenerateRDFOutput = false,
                    OutputPath = outputDirectoryPath
                },

                MetaDataSettings =
                {
                    LiteralExpressions = true,
                    MethodInvocations = true,
                    Annotations = true,
                    DeclarationNodes = true,
                    LocationData = true,
                    ReferenceData = true,
                    LoadBuildData = true
                }
            };
            return configuration;
        }

        private void DeleteDirectory(string directory)
        {
            if (_downloadDirectory != null)
            {
                if (Directory.Exists(_downloadDirectory))
                {
                    Directory.Delete(_downloadDirectory, true);
                }
            }
        }
    }
}
