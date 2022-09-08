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
    [SimpleJob(RunStrategy.Monitoring, launchCount: 1, targetCount: 1)]
    [MemoryDiagnoser()]
    public class NopCommerceAnalysisBenchmarks
    {
        private readonly string _solutionSuffix = "nopCommerce-release-3.90\\src\\NopCommerce.sln";
        private readonly ILogger _logger;
        private string? _downloadDirectory = null;
        public NopCommerceAnalysisBenchmarks()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.SetMinimumLevel(LogLevel.Trace).AddConsole());
            _logger = loggerFactory.CreateLogger("Analyzer");
        }

        [GlobalSetup]
        public async Task GlobalSetup()
        {
            _downloadDirectory = Path.Combine(Path.GetTempPath(), "NopCommerce");
            Directory.CreateDirectory(_downloadDirectory);
            await DownloadAndExtractAsync(@"https://github.com/nopSolutions/nopCommerce/archive/refs/tags/release-3.90.zip",
                _downloadDirectory);

        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            if (_downloadDirectory != null)
            {
                if (Directory.Exists(_downloadDirectory))
                {
                    Directory.Delete(_downloadDirectory, true);
                }
            }
        }

        [Benchmark()]
        public async Task AnalyzeSolution()
        {
            AnalyzerConfiguration configuration = new AnalyzerConfiguration(LanguageOptions.CSharp)
            {
                ExportSettings =
                {
                    GenerateJsonOutput = true,
                    GenerateGremlinOutput = false,
                    GenerateRDFOutput = false,
                    OutputPath = @"/tmp/NopCommerceSyncAnalysisBenchmarks"
                },

                MetaDataSettings =
                {
                    LiteralExpressions = true,
                    MethodInvocations = true,
                    Annotations = true,
                    DeclarationNodes = true,
                    LocationData = true,
                    ReferenceData  = true,
                    LoadBuildData = true
                }
            };
            CodeAnalyzerByLanguage analyzerByLanguage = new CodeAnalyzerByLanguage(configuration, _logger);
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
            AnalyzerConfiguration configuration = new AnalyzerConfiguration(LanguageOptions.CSharp)
            {
                ExportSettings =
                {
                    GenerateJsonOutput = true,
                    GenerateGremlinOutput = false,
                    GenerateRDFOutput = false,
                    OutputPath = @"/tmp/NopCommerceAsyncAnalysisBenchmarks"
                },

                MetaDataSettings =
                {
                    LiteralExpressions = true,
                    MethodInvocations = true,
                    Annotations = true,
                    DeclarationNodes = true,
                    LocationData = true,
                    ReferenceData  = true,
                    LoadBuildData = true
                }
            };
            CodeAnalyzerByLanguage analyzerByLanguage = new CodeAnalyzerByLanguage(configuration, _logger);
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

    }
}
