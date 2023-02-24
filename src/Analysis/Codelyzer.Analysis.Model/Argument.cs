using System;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class Argument : UstNode
    {
        [JsonPropertyName("semantic-type")]
[JsonPropertyOrder(10)]
        public string SemanticType { get; set; }

        public Argument()
            : base(IdConstants.ArgumentIdName)
        {
        }
        public override bool Equals(object obj)
        {
            if (obj is Argument)
            {
                return Equals(obj as Argument);
            }
            return false;
        }

        public bool Equals(Argument compareNode)
        {
            return
                compareNode != null &&
                SemanticType?.Equals(compareNode.SemanticType) != false &&
                base.Equals(compareNode);

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(SemanticType, base.GetHashCode());
        }
    }
}
