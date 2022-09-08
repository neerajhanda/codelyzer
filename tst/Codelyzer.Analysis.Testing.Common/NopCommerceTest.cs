using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codelyzer.Analysis.Analyzer;
using Microsoft.Extensions.Logging;

namespace Codelyzer.Analysis.Testing.Common
{
    public sealed class NopCommerceTest
    {
        private readonly string _solutionSuffix = "nopCommerce-release-3.90\\src\\NopCommerce.sln";
        private readonly ILogger _logger;
        public string? DownloadDirectory { get; private set; } = null;
        public string? OutputDirectoryPathForSync { get; private set; }
        public string? OutputDirectoryPathForAsyncGenerator { get; private set; }

        public NopCommerceTest()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.SetMinimumLevel(LogLevel.Trace).AddConsole());
            _logger = loggerFactory.CreateLogger("Analyzer");
        }
        public async Task Setup()
        {
            DownloadDirectory = Path.Combine(Path.GetTempPath(), "NopCommerce");
            OutputDirectoryPathForSync = Path.Combine(DownloadDirectory, "NopCommerceSyncAnalysisBenchmarks");
            OutputDirectoryPathForAsyncGenerator =
                Path.Combine(DownloadDirectory, "NopCommerceAsyncGeneratorAnalysisBenchmarks");
            Directory.CreateDirectory(DownloadDirectory);
            await Utils.DownloadAndExtractAsync(
                @"https://github.com/nopSolutions/nopCommerce/archive/refs/tags/release-3.90.zip",
                DownloadDirectory);
        }

        public void Cleanup()
        {
            Utils.DeleteDirectory(DownloadDirectory);
            Utils.DeleteDirectory(OutputDirectoryPathForSync);
            Utils.DeleteDirectory(OutputDirectoryPathForAsyncGenerator);
        }

        public async Task AnalyzeSolution()
        {
            var analyzerConfiguration = CreateAnalyzerConfiguration(OutputDirectoryPathForSync ?? throw new ArgumentNullException(nameof(OutputDirectoryPathForSync)));
            CodeAnalyzerByLanguage analyzerByLanguage = new CodeAnalyzerByLanguage(analyzerConfiguration, _logger);

            var results =
                await analyzerByLanguage.AnalyzeSolution(Path.Combine(DownloadDirectory ?? throw new ArgumentNullException(nameof(DownloadDirectory)), _solutionSuffix));
            var allBuildErrors = results.SelectMany(r => r.ProjectBuildResult.BuildErrors);
        }

        public async Task AnalyzeSolutionGenerator()
        {
            var analyzerConfiguration = CreateAnalyzerConfiguration(OutputDirectoryPathForAsyncGenerator ?? throw new ArgumentNullException(nameof(OutputDirectoryPathForAsyncGenerator)));
            CodeAnalyzerByLanguage analyzerByLanguage = new CodeAnalyzerByLanguage(analyzerConfiguration, _logger);

            var results =
                await analyzerByLanguage.AnalyzeSolutionGenerator(Path.Combine(DownloadDirectory ?? throw new ArgumentNullException(nameof(DownloadDirectory)),
                    _solutionSuffix));
            var allBuildErrors = results.SelectMany(r => r.ProjectBuildResult.BuildErrors);
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
    }
}
