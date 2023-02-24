using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class StructDeclaration : UstNode
    {
        [JsonPropertyName("base-type")]
[JsonPropertyOrder(10)]
        public string BaseType { get; set; }

        [JsonPropertyName("base-type-original-def")]
[JsonPropertyOrder(11)]
        public string BaseTypeOriginalDefinition { get; set; }

        [JsonPropertyName("base-list")]
[JsonPropertyOrder(12)]
        public List<string> BaseList { get; set; }

        [JsonPropertyName("references")]
[JsonPropertyOrder(99)]
        public Reference Reference { get; set; }
        public StructDeclaration()
            : base(IdConstants.StructIdName)
        {
            Reference = new Reference();
        }
    }
}
