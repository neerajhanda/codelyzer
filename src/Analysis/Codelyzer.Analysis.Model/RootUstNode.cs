using System;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class RootUstNode : UstNode
    {
        [JsonPropertyName("language")]
        [JsonPropertyOrder(10)]
        public string Language { get; set; }

        [JsonPropertyName("file-path")]
        [JsonPropertyOrder(11)]
        public string FilePath { get; set; }

        [JsonPropertyName("file-full-path")]
        [JsonPropertyOrder(12)]
        public string FileFullPath { get; set; }

        [JsonPropertyName("references")]
        [JsonPropertyOrder(99)]
        public UstList<Reference> References { get; set; }
        public RootUstNode() : base(IdConstants.RootIdName)
        {
            References = new UstList<Reference>();
        }

        public void SetPaths(string filePath, string fileFullPath)
        {
            FilePath = filePath;
            FileFullPath = fileFullPath;
        }

        public override bool Equals(object obj)
        {
            if (obj is RootUstNode)
            {
                return Equals((RootUstNode)obj);
            }
            else return false;
        }

        public bool Equals(RootUstNode compareNode)
        {
            return compareNode != null &&
                Language?.Equals(compareNode.Language) != false
                && FilePath?.Equals(compareNode.FilePath) != false
                && FileFullPath?.Equals(compareNode.FileFullPath) != false
                && base.Equals(compareNode);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Language, FilePath, FileFullPath, base.GetHashCode());
        }
    }
}
