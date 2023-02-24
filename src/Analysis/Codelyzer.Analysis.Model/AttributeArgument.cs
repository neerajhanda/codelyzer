using System;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class AttributeArgument : UstNode
    {
        [JsonPropertyName("argument-name")]
        [JsonPropertyOrder(20)]
        public string ArgumentName { get; set; }

        [JsonPropertyName("argument-expression")]
        [JsonPropertyOrder(25)]
        public string ArgumentExpression { get; set; }

        public AttributeArgument()
            : base(IdConstants.AttributeArgumentIdName)
        {
        }
        public override bool Equals(object obj)
        {
            if (obj is AttributeArgument)
            {
                return Equals(obj as AttributeArgument);
            }
            return false;
        }

        public bool Equals(AttributeArgument compareNode)
        {
            return
                compareNode != null &&
                ArgumentName?.Equals(compareNode.ArgumentName) != false &&
                ArgumentExpression?.Equals(compareNode.ArgumentExpression) != false &&
                base.Equals(compareNode);

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(ArgumentName, ArgumentExpression, base.GetHashCode());
        }
    }
}
