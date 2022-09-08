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
using Codelyzer.Analysis.Testing.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Codelyzer.Analysis.Tests
{
    /*IMPORTANT
    NopCommerce version 3.90 used by these tests requires .NET Framework version 4.5.1 to build.
    Make sure you have .NET Framework 4.5.1 Developer Pack installed before you run these tests
    */
    [TestFixture]
    [NonParallelizable]
    public class SyncComparedToAsyncGeneratorTests
    {
        private readonly NopCommerceTest _test = new();

        [OneTimeSetUp]
        public async Task GlobalSetup()
        {
            await _test.Setup();
        }

        [OneTimeTearDown]
        public void GlobalCleanup()
        {
            _test.Cleanup();
        }

        [Test]
        public void CompareResults()
        {
            var syncDirectory = new DirectoryInfo(_test.OutputDirectoryPathForSync);
            var asyncDirectory = new DirectoryInfo(_test.OutputDirectoryPathForAsyncGenerator);
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
            await _test.AnalyzeSolution();
            Assert.True(Directory.Exists(_test.OutputDirectoryPathForSync));
        }

        [Test, Order(2)]
        public async Task AnalyzeSolutionGenerator()
        {
            await _test.AnalyzeSolutionGenerator();
            Assert.True(Directory.Exists(_test.OutputDirectoryPathForAsyncGenerator));
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