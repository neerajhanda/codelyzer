using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class MultiLineLambdaExpression : LambdaExpression
    {
        [JsonPropertyName("lambda-type")]
[JsonPropertyOrder(2)]
        public override string LambdaType => IdConstants.MultiLineLambdaExpressionIdName;

        [JsonPropertyName("parameters")]
[JsonPropertyOrder(10)]
        public List<Parameter> Parameters { get; set; }

        public MultiLineLambdaExpression()
            : base(IdConstants.LambdaExpressionIdName)
        {
            Parameters = new List<Parameter>();
        }
        public override bool Equals(object obj)
        {
            if (obj is ParenthesizedLambdaExpression)
            {
                return Equals(obj as ParenthesizedLambdaExpression);
            }
            return false;
        }

        public bool Equals(ParenthesizedLambdaExpression compareNode)
        {
            return
                compareNode != null &&
                Parameters?.SequenceEqual(compareNode.Parameters) != false &&
                base.Equals(compareNode);

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Parameters,base.GetHashCode());
        }
    }
}
