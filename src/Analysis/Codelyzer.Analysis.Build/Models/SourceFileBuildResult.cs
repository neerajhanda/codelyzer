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

        private SemanticModel _prePortSemanticModel;
        private SemanticModel _semanticModel;
        public SyntaxTree SyntaxTree { get; set; }
        public SemanticModel SemanticModel
        {
            get
            {
                if (_semanticModel == null)
                {
                    _semanticModel = _compilation?.GetSemanticModel(SyntaxTree);
                }

                return _semanticModel;
            }
        }

        public SemanticModel PrePortSemanticModel
        {
            get
            {
                if (_prePortSemanticModel == null)
                {
                    _prePortSemanticModel = _prePortCompilation?.GetSemanticModel(SyntaxTree);
                }

                return _prePortSemanticModel;
            }
        }

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
