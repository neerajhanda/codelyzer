using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class ClassBlock : UstNode
    {
        [JsonPropertyName("base-type")]
[JsonPropertyOrder(10)]
        public string BaseType { get; set; }

        [JsonPropertyName("base-type-original-def")]
[JsonPropertyOrder(11)]
        public string BaseTypeOriginalDefinition { get; set; }

        [JsonPropertyName("inherits")]
[JsonPropertyOrder(12)]
        public List<string> Inherits { get; set; }

        [JsonPropertyName("modifiers")]
[JsonPropertyOrder(20)]
        public string Modifiers { get; set; }

        [JsonPropertyName("references")]
[JsonPropertyOrder(99)]
        public Reference Reference { get; set; }
        public string SemanticAssembly { get; set; }

        public ClassBlock()
            : base(IdConstants.ClassBlockName)
        {
            Reference = new Reference();
        }

        public override bool Equals(object obj)
        {
            if (obj is ClassBlock)
            {
                return Equals(obj as ClassBlock);
            }
            return false;
        }

        public bool Equals(ClassBlock compareNode)
        {
            return
                compareNode != null &&
                BaseType?.Equals(compareNode.BaseType) != false &&
                BaseTypeOriginalDefinition?.Equals(compareNode.BaseTypeOriginalDefinition) != false &&
                Modifiers?.Equals(compareNode.Modifiers) != false &&
                SemanticAssembly?.Equals(compareNode.SemanticAssembly) != false &&
                base.Equals(compareNode);

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(BaseType, BaseTypeOriginalDefinition, Modifiers, SemanticAssembly, base.GetHashCode());
        }
    }
}
