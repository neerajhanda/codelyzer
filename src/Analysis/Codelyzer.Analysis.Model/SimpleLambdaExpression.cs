using System;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class SimpleLambdaExpression : LambdaExpression
    {
        [JsonPropertyName("lambda-type")]
        [JsonPropertyOrder(2)]
        public override string LambdaType => IdConstants.SimpleLambdaExpressionIdName;

        [JsonPropertyName("parameter")]
        [JsonPropertyOrder(10)]
        public Parameter Parameter { get; set; }

        public SimpleLambdaExpression()
            : base(IdConstants.LambdaExpressionIdName)
        {
        }
        public override bool Equals(object obj)
        {
            if (obj is SimpleLambdaExpression)
            {
                return Equals(obj as SimpleLambdaExpression);
            }
            return false;
        }

        public bool Equals(SimpleLambdaExpression compareNode)
        {
            return
                compareNode != null &&
                Parameter?.Equals(compareNode.Parameter) != false &&
                base.Equals(compareNode);

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Parameter, base.GetHashCode());
        }
    }
}
