using System;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class MethodDeclaration : BaseMethodDeclaration
    {
        [JsonPropertyName("return-type")]
[JsonPropertyOrder(12)]
        public string ReturnType { get; set; }
        
        [JsonPropertyName("semantic-return-type")]
[JsonPropertyOrder(13)]
        public string SemanticReturnType { get; set; }

        public MethodDeclaration()
            : base(IdConstants.MethodIdName)
        {
        }
        public MethodDeclaration(string idName)
            : base(idName)
        {
        }
        public override bool Equals(object obj)
        {
            if (obj is MethodDeclaration)
            {
                return Equals(obj as MethodDeclaration);
            }
            return false;
        }

        public bool Equals(MethodDeclaration compareNode)
        {
            return
                compareNode != null &&
                ReturnType?.Equals(compareNode.ReturnType) != false &&
                SemanticReturnType?.Equals(compareNode.SemanticReturnType) != false &&
                base.Equals(compareNode);

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(ReturnType, SemanticReturnType, base.GetHashCode());
        }
    }
}
