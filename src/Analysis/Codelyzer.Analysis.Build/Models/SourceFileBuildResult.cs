using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codelyzer.Analysis.Build
{
    public class SourceFileBuildResult
    {
        private readonly Compilation _prePortCompilation;
        private readonly Compilation _compilation;
        public SyntaxTree SyntaxTree { get; set; }
        public SemanticModel SemanticModel => _compilation?.GetSemanticModel(SyntaxTree);
        public SemanticModel PrePortSemanticModel => _prePortCompilation?.GetSemanticModel(SyntaxTree);
        public string SourceFileFullPath { get; set; }
        public string SourceFilePath { get; set; }
        public SyntaxGenerator SyntaxGenerator { get; set; }

        public SourceFileBuildResult(Compilation compilation, Compilation prePortCompilation)
        {
            _compilation = compilation;
            _prePortCompilation = prePortCompilation;
        }
    }
}
