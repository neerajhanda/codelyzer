
using Codelyzer.Analysis.Build;
using Codelyzer.Analysis.Common;
using Codelyzer.Analysis.VisualBasic;
using Codelyzer.Analysis.Model;
using Microsoft.Extensions.Logging;

namespace Codelyzer.Analysis.Analyzer
{
    class VBAnalyzer : LanguageAnalyzer
    {
        public VBAnalyzer(AnalyzerConfiguration configuration, ILogger logger)
            : base(configuration, logger)
        {
        }
        public override string Language => LanguageOptions.Vb;
        public override string ProjectFilePath { set; get; }

        public override RootUstNode AnalyzeFile(SourceFileBuildResult sourceFileBuildResult, string projectRootPath)
        {
            var prePortSemanticModel =
                sourceFileBuildResult?.PrePortCompilation?.GetSemanticModel(sourceFileBuildResult.SyntaxTree);
            var semanticModel =
                sourceFileBuildResult?.Compilation?.GetSemanticModel(sourceFileBuildResult.SyntaxTree);
            CodeContext codeContext = new CodeContext(prePortSemanticModel,
                semanticModel,
                sourceFileBuildResult.SyntaxTree,
                projectRootPath,
                sourceFileBuildResult.SourceFilePath,
                AnalyzerConfiguration,
                Logger);

            Logger.LogDebug("Analyzing: " + sourceFileBuildResult.SourceFileFullPath);

            using VisualBasicRoslynProcessor processor = new VisualBasicRoslynProcessor(codeContext);

            var result = processor.Visit(codeContext.SyntaxTree.GetRoot());
            return result as RootUstNode;
        }
    }
}
