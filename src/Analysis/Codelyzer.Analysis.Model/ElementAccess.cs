using System;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class ElementAccess : UstNode
    {
        [JsonPropertyName("expression")]
        [JsonPropertyOrder(10)]
        public string Expression { get; set; }

        [JsonPropertyName("semantic-class-type")]
        [JsonPropertyOrder(14)]
        public string SemanticClassType { get; set; }

        [JsonPropertyName("references")]
        [JsonPropertyOrder(99)]
        public Reference Reference { get; set; }

        public ElementAccess()
            : base(IdConstants.ElementAccessIdName)
        {
            Reference = new Reference();
        }

        public ElementAccess(string idName)
            : base(idName)
        {
            Reference = new Reference();
        }
        public override bool Equals(object obj)
        {
            if (obj is ElementAccess)
            {
                return Equals(obj as ElementAccess);
            }
            return false;
        }

        public bool Equals(ElementAccess compareNode)
        {
            return
                compareNode != null &&
                Expression?.Equals(compareNode.Expression) != false &&
                SemanticClassType?.Equals(compareNode.SemanticClassType) != false &&
                base.Equals(compareNode);

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Expression, SemanticClassType, base.GetHashCode());
        }
    }
}
