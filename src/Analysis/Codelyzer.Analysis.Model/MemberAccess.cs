using System;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class MemberAccess : UstNode
    {
        [JsonPropertyName("name")]
        [JsonPropertyOrder(10)]
        public string Name { get; set; }

        [JsonPropertyName("expression")]
        [JsonPropertyOrder(11)]
        public string Expression { get; set; }

        [JsonPropertyName("semantic-class-type")]
        [JsonPropertyOrder(14)]
        public string SemanticClassType { get; set; }

        [JsonPropertyName("semantic-full-class-type")]
        [JsonPropertyOrder(98)]
        public string SemanticFullClassTypeName { get; set; }

        [JsonPropertyName("references")]
        [JsonPropertyOrder(99)]
        public Reference Reference { get; set; }

        public MemberAccess()
            : base(IdConstants.MemberAccessIdName)
        {
            Reference = new Reference();
        }

        public MemberAccess(string idName)
            : base(idName)
        {
            Reference = new Reference();
        }
        public override bool Equals(object obj)
        {
            if (obj is MemberAccess)
            {
                return Equals(obj as MemberAccess);
            }
            return false;
        }

        public bool Equals(MemberAccess compareNode)
        {
            return
                compareNode != null &&
                Name?.Equals(compareNode.Name) != false &&
                Expression?.Equals(compareNode.Expression) != false &&
                SemanticClassType?.Equals(compareNode.SemanticClassType) != false &&
                base.Equals(compareNode);

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Expression, SemanticClassType, base.GetHashCode());
        }
    }
}
