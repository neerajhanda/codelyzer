using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class EnumDeclaration : UstNode
    {
        [JsonPropertyName("references")]
[JsonPropertyOrder(99)]
        public Reference Reference { get; set; }
        public EnumDeclaration()
            : base(IdConstants.EnumIdName)
        {
            Reference = new Reference();
        }
    }
}
