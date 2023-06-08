using Codelyzer.Analysis.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace Codelyzer.Analysis
{
    public class AnalyzerConfiguration
    {
        public string Language;
        public int ConcurrentThreads = Constants.DefaultConcurrentThreads;
        public bool AnalyzeFailedProjects = false; 
        public static List<string> DefaultBuildArguments = new()
        {
            Constants.RestorePackagesConfigArgument,
            Constants.RestoreArgument
        };

        public AnalyzerConfiguration(string language, string VisualStudioVersion = null)
        {
            Language = language;
            ExportSettings = new ExportSettings(); 
            MetaDataSettings = new MetaDataSettings();
            if (!string.IsNullOrEmpty(VisualStudioVersion))
            {
                VisualStudioVersion vsVersion;
                Enum.TryParse<VisualStudioVersion>(VisualStudioVersion, out vsVersion);
                BuildSettings = new BuildSettings(vsVersion);
            }
            else 
            {
                BuildSettings = new BuildSettings();
            }
        }

        public ExportSettings ExportSettings;
        public MetaDataSettings MetaDataSettings;
        public BuildSettings BuildSettings;
    }
    
    public static class LanguageOptions
    {
        public const string CSharp = nameof(CSharp);
        public const string Vb = nameof(Vb);
    }

    public class BuildSettings
    {
        public BuildSettings(VisualStudioVersion? visualStudioVersion = null)
        {
            BuildArguments = AnalyzerConfiguration.DefaultBuildArguments;
            VisualStudioVersion = visualStudioVersion;
            if (visualStudioVersion!= null && VisualStudioVersion.HasValue)
            {
                List<string> editions = new List<string> { "Enterprise", "Professional", "Community", "BuildTools" };
                var targets = new string[] { "Microsoft.CSharp.targets", "Microsoft.CSharp.CurrentVersion.targets", "Microsoft.Common.targets" };
                var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                string programFilesX86 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFilesX86);
                DirectoryInfo vsDirectory = null;
                switch (VisualStudioVersion.Value) {
                    case Analysis.VisualStudioVersion.VS2022:
                        vsDirectory = new DirectoryInfo(Path.Combine(programFiles, "Microsoft Visual Studio"));
                        break;
                    case Analysis.VisualStudioVersion.VS2019:
                        vsDirectory = new DirectoryInfo(Path.Combine(programFilesX86, "Microsoft Visual Studio"));
                        
                        break;
                }
                if (vsDirectory != null)
                {
                    MSBuildPath = MSBuildDetector.GetMsBuildPathFromVSDirectory(vsDirectory, editions, targets, null);
                }
            }
        }
        public string MSBuildPath;
        public List<string> BuildArguments;
        public bool BuildOnly = false;
        public bool SyntaxOnly = false;
        public VisualStudioVersion? VisualStudioVersion;
    }

    public class ExportSettings
    {
        public bool GenerateJsonOutput = false;
        public bool GenerateGremlinOutput = false;
        public bool GenerateRDFOutput = false;
        public string OutputPath;
    }
    
    /* By default, it captures Namespaces, directives, classes and methods. */
    public class MetaDataSettings
    {
        public bool MethodInvocations;
        public bool LiteralExpressions;
        public bool LambdaMethods;
        public bool DeclarationNodes;
        public bool Annotations;
        public bool LocationData = true;
        public bool ReferenceData;
        public bool LoadBuildData = false;
        public bool InterfaceDeclarations = false;
        public bool EnumDeclarations = false;
        public bool StructDeclarations = false;
        public bool ReturnStatements = false;
        public bool InvocationArguments = false;
        public bool GenerateBinFiles = false;
        public bool ElementAccess = false;
        public bool MemberAccess = false;
    }

    public enum VisualStudioVersion
    {
        VS2019,
        VS2022
    }
}
