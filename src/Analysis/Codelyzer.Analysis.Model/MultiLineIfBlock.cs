using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class MultiLineIfBlock : UstNode
    {

        [JsonPropertyName("modifiers")]
[JsonPropertyOrder(20)]
        public string Modifiers { get; set; }

        public MultiLineIfBlock()
            : base(IdConstants.MultiLineIfBlockName)
        {

        }
    }
}