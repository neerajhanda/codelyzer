using System;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public abstract class LambdaExpression : UstNode
    {
        [JsonPropertyName("lambda-type")]
[JsonPropertyOrder(1)]
        public abstract string LambdaType { get; }

        [JsonPropertyName("return-type")]
[JsonPropertyOrder(20)]
        public string ReturnType { get; set; }

        [JsonPropertyName("semantic-properties")]
[JsonPropertyOrder(30)]
        public UstList<string> SemanticProperties { get; set; }

        public LambdaExpression(string idName)
            : base(idName)
        {
            SemanticProperties = new UstList<string>();
        }
        public override bool Equals(object obj)
        {
            if (obj is LambdaExpression)
            {
                return Equals(obj as LambdaExpression);
            }
            return false;
        }

        public bool Equals(LambdaExpression compareNode)
        {
            return
                compareNode != null &&
                LambdaType?.Equals(compareNode.LambdaType) != false &&
                ReturnType?.Equals(compareNode.ReturnType) != false &&
                SemanticProperties?.Equals(compareNode.SemanticProperties) != false &&
                base.Equals(compareNode);

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(LambdaType, ReturnType, SemanticProperties, base.GetHashCode());
        }
    }
}
