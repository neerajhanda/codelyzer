using System;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class Annotation : UstNode
    {
        [JsonPropertyName("semantic-class-type")]
[JsonPropertyOrder(14)]
        public string SemanticClassType { get; set; }
        [JsonPropertyName("references")]
[JsonPropertyOrder(99)]
        public Reference Reference { get; set; }
        public Annotation()
            : base(IdConstants.AnnotationIdName)
        {
            Reference = new Reference();
        }

        public override bool Equals(object obj)
        {
            if(obj is Annotation)
            {
                return Equals(obj as Annotation);
            }
            return false;
        }

        public bool Equals(Annotation compareNode)
        {
            return
                compareNode != null &&
                Reference?.Equals(compareNode.Reference) != false &&
                SemanticClassType?.Equals(compareNode.SemanticClassType) != false &&
                base.Equals(compareNode);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(SemanticClassType, base.GetHashCode());
        }
    }
}
