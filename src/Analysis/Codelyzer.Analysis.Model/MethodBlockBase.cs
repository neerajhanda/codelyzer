using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class MethodBlockBase : UstNode
    {
        [JsonPropertyName("modifiers")]
[JsonPropertyOrder(10)]
        public string Modifiers { get; set; }

        [JsonPropertyName("parameters")]
[JsonPropertyOrder(11)]
        public List<Parameter> Parameters { get; set; }

        [JsonPropertyName("semantic-properties")]
[JsonPropertyOrder(14)]
        public UstList<string> SemanticProperties { get; set; }

        [JsonPropertyName("semantic-signature")]
[JsonPropertyOrder(30)]
        public string SemanticSignature { get; set; }

        protected MethodBlockBase(string idName)
            : base(idName)
        {
            Parameters = new UstList<Parameter>();
            SemanticProperties = new UstList<string>();
        }
        public override bool Equals(object obj)
        {
            if (obj is MethodBlockBase)
            {
                return Equals(obj as MethodBlockBase);
            }
            return false;
        }

        public bool Equals(MethodBlockBase compareNode)
        {
            return
                compareNode != null &&
                Modifiers?.Equals(compareNode.Modifiers) != false &&
                Parameters?.SequenceEqual(compareNode.Parameters) != false &&
                SemanticProperties?.SequenceEqual(compareNode.SemanticProperties) != false &&
                SemanticSignature?.Equals(compareNode.SemanticSignature) != false &&
                base.Equals(compareNode);

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Modifiers, Parameters, SemanticProperties, SemanticSignature, base.GetHashCode());
        }
    }
}
