using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class ArgumentList : UstNode
    {
        [Obsolete(Constants.ObsoleteParameterMessage, Constants.DoNotThrowErrorOnUse)]
        [JsonPropertyName("parameters")]
[JsonPropertyOrder(50)]
        public List<Parameter> Parameters { get; set; }
        
        [JsonPropertyName("arguments")]
[JsonPropertyOrder(51)]
        public List<Argument> Arguments { get; set; }
            
        [JsonPropertyName("semantic-properties")]
[JsonPropertyOrder(65)]
        public List<string> SemanticProperties { get; set; }
        public ArgumentList()
            : base(IdConstants.ArgumentListName)
        {
            SemanticProperties = new List<string>();
#pragma warning disable CS0618 // Type or member is obsolete
            Parameters = new List<Parameter>();
#pragma warning restore CS0618 // Type or member is obsolete
            Arguments = new List<Argument>();
        }
        public override bool Equals(object obj)
        {
            if (obj is ArgumentList)
            {
                return Equals(obj as ArgumentList);
            }
            return false;
        }

        public bool Equals(ArgumentList compareNode)
        {
            return
                compareNode != null &&
#pragma warning disable CS0618 // Type or member is obsolete
                Parameters?.SequenceEqual(compareNode.Parameters) != false &&
#pragma warning restore CS0618 // Type or member is obsolete
                Arguments?.SequenceEqual(compareNode.Arguments) != false &&
                base.Equals(compareNode);

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(
#pragma warning disable CS0618 // Type or member is obsolete
                HashCode.Combine(Parameters, Arguments),
#pragma warning restore CS0618 // Type or member is obsolete
                base.GetHashCode());
        }
    }
}
