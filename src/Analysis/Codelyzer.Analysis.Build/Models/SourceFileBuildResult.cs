using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codelyzer.Analysis.Build
{
    public class SourceFileBuildResult
    {
        public SyntaxTree SyntaxTree { get; set; }
        public Compilation PrePortCompilation { get; }
        public Compilation Compilation { get; }
        public string SourceFileFullPath { get; set; }
        public string SourceFilePath { get; set; }
        public SyntaxGenerator SyntaxGenerator { get; set; }

        public SourceFileBuildResult(Compilation compilation, Compilation prePortCompilation)
        {
            Compilation = compilation;
            PrePortCompilation = prePortCompilation;
        }
    }
}
