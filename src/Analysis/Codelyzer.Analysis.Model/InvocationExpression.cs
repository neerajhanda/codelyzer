using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class InvocationExpression : ExpressionStatement
    {
        [JsonPropertyName("method-name")]
        [JsonPropertyOrder(10)]
        public string MethodName { get; set; }

        [JsonPropertyName("modifiers")]
        [JsonPropertyOrder(11)]
        public string Modifiers { get; set; }

        [JsonPropertyName("semantic-namespace")]
        [JsonPropertyOrder(12)]
        public string SemanticNamespace { get; set; }

        [JsonPropertyName("caller-identifier")]
        [JsonPropertyOrder(13)]
        public string CallerIdentifier { get; set; }

        [JsonPropertyName("semantic-class-type")]
        [JsonPropertyOrder(14)]
        public string SemanticClassType { get; set; }

        [JsonPropertyName("semantic-full-class-type")]
        [JsonPropertyOrder(98)]
        public string SemanticFullClassTypeName { get; set; }

        [JsonPropertyName("semantic-method-signature")]
        [JsonPropertyOrder(15)]
        public string SemanticMethodSignature { get; set; }

        [Obsolete(Constants.ObsoleteParameterMessage, Constants.DoNotThrowErrorOnUse)]
        [JsonPropertyName("parameters")]
        [JsonPropertyOrder(30)]
        public List<Parameter> Parameters { get; set; }

        [JsonPropertyName("arguments")]
        [JsonPropertyOrder(31)]
        public List<Argument> Arguments { get; set; }

        [JsonPropertyName("semantic-return-type")]
        [JsonPropertyOrder(35)]
        public string SemanticReturnType { get; set; }

        [JsonPropertyName("semantic-original-def")]
        [JsonPropertyOrder(40)]
        public string SemanticOriginalDefinition { get; set; }

        [JsonPropertyName("semantic-properties")]
        [JsonPropertyOrder(45)]
        public List<string> SemanticProperties { get; set; }

        [JsonPropertyName("semantic-is-extension")]
        [JsonPropertyOrder(50)]
        public bool IsExtension { get; set; }

        [JsonPropertyName("references")]
        [JsonPropertyOrder(99)]
        public Reference Reference { get; set; }

        public InvocationExpression(string typeName)
            : base(typeName)
        {
            SemanticProperties = new List<string>();
#pragma warning disable CS0618 // Type or member is obsolete
            Parameters = new List<Parameter>();
#pragma warning restore CS0618 // Type or member is obsolete
            Arguments = new List<Argument>();
            Reference = new Reference();
        }

        public InvocationExpression()
            : base(IdConstants.InvocationIdName)
        {
            SemanticProperties = new List<string>();
#pragma warning disable CS0618 // Type or member is obsolete
            Parameters = new List<Parameter>();
#pragma warning restore CS0618 // Type or member is obsolete
            Arguments = new List<Argument>();
            Reference = new Reference();
        }
        public override bool Equals(object obj)
        {
            if (obj is InvocationExpression)
            {
                return Equals(obj as InvocationExpression);
            }
            return false;
        }

        public bool Equals(InvocationExpression compareNode)
        {
            return
                compareNode != null &&
                MethodName?.Equals(compareNode.MethodName) != false &&
                Modifiers?.Equals(compareNode.Modifiers) != false &&
                SemanticNamespace?.Equals(compareNode.SemanticNamespace) != false &&
                CallerIdentifier?.Equals(compareNode.CallerIdentifier) != false &&
                SemanticClassType?.Equals(compareNode.SemanticClassType) != false &&
                SemanticMethodSignature?.Equals(compareNode.SemanticMethodSignature) != false &&
#pragma warning disable CS0618 // Type or member is obsolete
                Parameters?.SequenceEqual(compareNode.Parameters) != false &&
#pragma warning restore CS0618 // Type or member is obsolete
                Arguments?.SequenceEqual(compareNode.Arguments) != false &&
                SemanticReturnType?.Equals(compareNode.SemanticReturnType) != false &&
                SemanticOriginalDefinition?.Equals(compareNode.SemanticOriginalDefinition) != false &&
                IsExtension == compareNode.IsExtension &&
                base.Equals(compareNode);

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(
                HashCode.Combine(MethodName, Modifiers, SemanticNamespace, CallerIdentifier, SemanticClassType, SemanticMethodSignature),
#pragma warning disable CS0618 // Type or member is obsolete
                HashCode.Combine(Parameters, Arguments, SemanticReturnType, SemanticOriginalDefinition, IsExtension),
#pragma warning restore CS0618 // Type or member is obsolete
                base.GetHashCode());
        }
    }
}
