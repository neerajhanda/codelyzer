using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class DeclarationNode : UstNode
    {
        [JsonPropertyName("references")]
[JsonPropertyOrder(99)]
        public Reference Reference { get; set; }
        public DeclarationNode()
            : base(IdConstants.DeclarationNodeIdName)
        {
            Reference = new Reference();
        }
    }
}
