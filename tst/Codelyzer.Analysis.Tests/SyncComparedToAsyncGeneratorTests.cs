#nullable enable
using Codelyzer.Analysis.Analyzer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Tasks;
using NUnit.Framework;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Codelyzer.Analysis.Tests
{
    [TestFixture]
    [NonParallelizable]
    public class SyncComparedToAsyncGeneratorTests
    {
        private readonly string _solutionSuffix = "nopCommerce-release-3.90\\src\\NopCommerce.sln";
        private ILogger _logger;
        private string? _downloadDirectory = null;
        private string? _outputDirectoryPathForSync;
        private string? _outputDirectoryPathForAsyncGenerator;

        [OneTimeSetUp]
        public async Task GlobalSetup()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.SetMinimumLevel(LogLevel.Trace).AddConsole());
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

        [OneTimeTearDown]
        public void GlobalCleanup()
        {
            DeleteDirectory(_downloadDirectory);
            DeleteDirectory(_outputDirectoryPathForSync);
            DeleteDirectory(_outputDirectoryPathForAsyncGenerator);
        }

        [Test]
        public void CompareResults()
        {
            var syncDirectory = new DirectoryInfo(_outputDirectoryPathForSync);
            var asyncDirectory = new DirectoryInfo(_outputDirectoryPathForAsyncGenerator);
            Assert.True(syncDirectory.Exists && asyncDirectory.Exists);
            {
                var syncFiles = syncDirectory.GetFiles();
                var asyncFiles = asyncDirectory.GetFiles();
                bool equalNumberOfFiles = syncFiles.Length == asyncFiles.Length;
                Assert.IsTrue(equalNumberOfFiles);
                {
                    var syncFileNames = syncFiles.Select(f => f.Name).ToArray();
                    var asyncFileNames = asyncFiles.Select(f => f.Name).ToArray();
                    bool noExtraFilesInSync = !syncFileNames.Except(asyncFileNames).Any();
                    bool noExtraFilesInAsync = !asyncFileNames.Except(syncFileNames).Any();
                    Assert.True(noExtraFilesInSync && noExtraFilesInAsync);
                    {
                        var settings = new JsonSerializerSettings
                            { TypeNameHandling = TypeNameHandling.All, MaxDepth = 128 };
                        foreach (var syncFile in syncFiles)
                        {
                            var syncFilePath = syncFile.FullName;
                            var syncJson =
                                JsonConvert.DeserializeObject<JObject>(File.ReadAllText(syncFilePath), settings);

                            var asyncFilePath = asyncFiles
                                .SingleOrDefault(asyncFile => asyncFile.Name == syncFile.Name)?.FullName;
                            Assert.False(String.IsNullOrWhiteSpace(asyncFilePath));
                            {
                                var asyncJson =
                                    JsonConvert.DeserializeObject<JObject>(File.ReadAllText(asyncFilePath), settings);
                                Assert.True(JToken.DeepEquals(syncJson, asyncJson));
                            }
                        }
                    }
                }
            }
        }

        [Test, Order(1)]
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

            Assert.True(Directory.Exists(_outputDirectoryPathForSync));
        }

        [Test, Order(2)]
        public async Task AnalyzeSolutionGenerator()
        {
            var analyzerConfiguration = CreateAnalyzerConfiguration(_outputDirectoryPathForAsyncGenerator);
            CodeAnalyzerByLanguage analyzerByLanguage = new CodeAnalyzerByLanguage(analyzerConfiguration, _logger);
            if (_downloadDirectory != null)
            {
                var results =
                    await analyzerByLanguage.AnalyzeSolutionGenerator(Path.Combine(_downloadDirectory,
                        _solutionSuffix));
                var allBuildErrors = results.SelectMany(r => r.ProjectBuildResult.BuildErrors);
            }

            Assert.True(Directory.Exists(_outputDirectoryPathForAsyncGenerator));
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

        // private string CalculateMD5(string filename)
        // {
        //     using var md5 = MD5.Create();
        //     using var stream = File.OpenRead(filename);
        //     var hash = md5.ComputeHash(stream);
        //     return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        // }
    }
}