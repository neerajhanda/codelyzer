using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class RecordDeclaration : UstNode
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

        [JsonPropertyName("modifiers")]
[JsonPropertyOrder(20)]
        public string Modifiers { get; set; }

        [JsonPropertyName("references")]
[JsonPropertyOrder(99)]
        public Reference Reference { get; set; }
        public string SemanticAssembly { get; set; }

        public RecordDeclaration()
            : base(IdConstants.RecordIdName)
        {
            Reference = new Reference();
        }
        public override bool Equals(object obj)
        {
            if (obj is RecordDeclaration)
            {
                return Equals(obj as RecordDeclaration);
            }
            return false;
        }

        public bool Equals(RecordDeclaration compareNode)
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
