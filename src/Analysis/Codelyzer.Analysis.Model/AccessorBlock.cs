using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class AccessorBlock : UstNode
    {
        [JsonPropertyName("modifiers")]
        [JsonPropertyOrder(20)]
        public string Modifiers { get; set; }

        public AccessorBlock()
            : base(IdConstants.AccessorBlockName)
        {

        }
    }
}
